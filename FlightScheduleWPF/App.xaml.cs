using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows;
using FlightScheduleWPF.Models;
using Microsoft.Extensions.Configuration;

namespace FlightScheduleWPF
{
    public partial class App
    {
        public static FlightScheduleConfig Settings;

        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            IConfigurationRoot    config  = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            IConfigurationSection section = config.GetSection(nameof(FlightScheduleConfig));
            Settings = section.Get<FlightScheduleConfig>() ?? throw new InvalidOperationException();
            Dictionary<string, string> strings = new Dictionary<string, string>
            {
                {"Arrival", Settings.ArrivalString},
                {"ArrivalTomorrow", Settings.ArrivalStringTomorrow},
                {"Departure", Settings.DepartureString},
                {"DepartureTomorrow", Settings.DepartureStringTomorrow}
            };
            Parser.GetFlightData(strings);
            Timer timer = new Timer();
            timer.Interval  =  Settings.UpdateInterval.TotalMilliseconds;
            timer.AutoReset =  true;
            timer.Enabled   =  true;
            timer.Elapsed   += (_, _) => Parser.GetFlightData(strings);
            timer.Start();
            base.OnStartup(e);
            for (int i = 0; i < Settings.NumberOfArrivalWindows; i++)
            {
                new ArrivalWindow(i).Show();
            }
            for (int i = 0; i < Settings.NumberOfDepartureWindows; i++)
            {
                new DepartureWindow(i).Show();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            File.WriteAllText("Error.log", DateTime.Now + " " + e.ExceptionObject);
            MessageBox.Show("Критическая ошибка. Программа будет закрыта.");
            Current.Shutdown(-1);
        }
    }
}