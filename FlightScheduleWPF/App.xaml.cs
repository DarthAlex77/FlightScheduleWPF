using System;
using System.IO;
using System.Windows;
using FlightScheduleWPF.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlightScheduleWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            JToken settings = JsonConvert.DeserializeObject<JToken>(File.ReadAllText("Settings.json"));
            Settings.ConnectionString = settings["ConnectionString"].ToString();
            Settings.TomorrowString = settings["TomorrowString"].ToString();
            Settings.IsDeparture      = settings["IsDeparture"].ToObject<bool>();
            Settings.TimeFilterStart  = TimeSpan.Parse(settings["TimeFilterStart"].ToString());
            Settings.TimeFilterEnd    = TimeSpan.Parse(settings["TimeFilterEnd"].ToString());
            Settings.UpdateInterval   = TimeSpan.Parse(settings["UpdateInterval"].ToString());
            base.OnStartup(e);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            File.WriteAllText("Error.log", DateTime.Now + " " + e.ExceptionObject);
            MessageBox.Show("Критическая ошибка. Программа будет закрыта.");
            Current.Shutdown(-1);
        }
    }
}