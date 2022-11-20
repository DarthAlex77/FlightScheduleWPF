using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace FlightScheduleWPF.Models
{
    internal static class Parser
    {
        public static List<Flight> GetFlightData(string html)
        {
            EdgeOptions options = new EdgeOptions();
            options.AddArgument($"user-data-dir={Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}");
            options.AddArgument("headless");
            EdgeDriverService edgeDriverService = EdgeDriverService.CreateDefaultService();
            edgeDriverService.HideCommandPromptWindow = true;
            EdgeDriver driver = new EdgeDriver(edgeDriverService, options);
            driver.Navigate().GoToUrl(html);
            string script = driver.FindElement(By.XPath("/html/body/script[2]")).GetAttribute("innerHTML");

            if (script.Contains("SSR_DATA"))
            {
                IWebElement? checkbox = driver.FindElement(By.ClassName("CheckboxCaptcha-Button"));
                Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(2, 5)));
                checkbox.Click();
                Thread.Sleep(TimeSpan.FromSeconds(5));
                script = driver.FindElement(By.XPath("/html/body/script[2]")).GetAttribute("innerHTML");
            }
            driver.Quit();
            driver.CloseDevToolsSession();
            KillEdgeProcess();
            const string toBeSearched = "window.INITIAL_STATE = ";
            string       code         = script[(script.IndexOf(toBeSearched, StringComparison.Ordinal) + toBeSearched.Length)..];
            string       jsonText     = code.Remove(code.IndexOf("}};", StringComparison.Ordinal) + 2);
            JObject      obj          = JObject.Parse(jsonText);
            List<JToken> tokens       = obj.SelectTokens("$.station.threads").Children().ToList();

            return ParseFlightData(tokens);
        }

        private static void KillEdgeProcess()
        {
            foreach (Process process in Process.GetProcessesByName("msedgedriver"))
            {
                process.Kill();
            }
            foreach (Process process in Process.GetProcessesByName("msedge"))
            {
                process.Kill();
            }
        }

        private static List<Flight> ParseFlightData(List<JToken> tokens)
        {
            List<Flight> flights = new List<Flight>(tokens.Count);
            foreach (JToken token in tokens)
            {
                Flight flight = new Flight
                {
                    Number          = token.SelectToken("$.number")!.ToString(),
                    ActualDt        = token.SelectToken("$.status.actualDt")?.ToObject<DateTimeOffset>(),
                    CheckInDesks    = token.SelectToken("$.status.checkInDesks")?.ToString(),
                    PlannedDateTime = token.SelectToken("$.eventDt.datetime")!.ToObject<DateTimeOffset>(),
                    Station         = token.SelectToken("$.routeStations[0].settlement")!.ToString(),
                    TimeToEvent     = TimeSpan.FromHours(token.SelectToken("$.hoursBeforeEvent")!.ToObject<double>()),
                    Gate            = token.SelectToken("$.status.gate")?.ToString()
                };
                if (token["minutesBetweenEventDtAndActualDt"] != null)
                {
                    flight.TimeDifferent = TimeSpan.FromMinutes(token["minutesBetweenEventDtAndActualDt"]!.ToObject<int>());
                }
                if (token.SelectToken("$.codeshares") != null)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine(flight.Number);
                    foreach (JToken codeshare in token.SelectToken("$.codeshares")!)
                    {
                        builder.AppendLine(codeshare["number"].ToString());
                    }
                    flight.Number = builder.ToString().TrimEnd('\r', '\n');
                }
                flight.Status = token.SelectToken("$.status.status")!.ToString() switch
                {
                    "on_time"   => "По расписанию",
                    "arrived"   => "Прибыл",
                    "early"     => "По расписанию",
                    "delayed"   => "Задержан",
                    "cancelled" => "Отменен",
                    "unknown"   => "Нет данных",
                    "departed"  => "Вылетел",
                    _           => flight.Status
                };

                flights.Add(flight);
            }
            return flights;
        }
    }
}