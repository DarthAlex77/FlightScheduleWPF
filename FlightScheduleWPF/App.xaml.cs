using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows;
using FlightScheduleWPF.Models;
using FlightScheduleWPF.ViewModels;
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
                {"Arrival", $"https://rasp.yandex.ru/station/{Settings.StationCode}/?event=arrival&time=all"},
                {"ArrivalTomorrow", $"https://rasp.yandex.ru/station/{Settings.StationCode}/?event=arrival&date=tomorrow&time=all"},
                {"Departure", $"https://rasp.yandex.ru/station/{Settings.StationCode}/?time=all"},
                {"DepartureTomorrow", $"https://rasp.yandex.ru/station/{Settings.StationCode}/?date=tomorrow&time=all"}
            };
            Parser.GetFlightData(strings);
            Timer timer = new Timer();
            timer.Interval  =  Settings.UpdateInterval.TotalMilliseconds;
            timer.AutoReset =  true;
            timer.Enabled   =  true;
            timer.Elapsed   += (_, _) => Parser.GetFlightData(strings);
            timer.Start();

            int arrivalWindowCount         = 1;
            int departureWindowCount       = 1;
            int firstPageOfDepartureWindow = 0;
            int firstPageOfArrivalWindow   = 0;
            foreach (WindowSettings setting in Settings.WindowSettings)
            {
                MainWindow mainWindow = new MainWindow();
                int        index      = setting.IsArrivalWindow ? firstPageOfArrivalWindow : firstPageOfDepartureWindow;

                mainWindow.Loaded += (sender, args) =>
                {
                    MainWindow           window = (MainWindow) args.OriginalSource;
                    MainWindowsViewModel vm     = (MainWindowsViewModel) window.DataContext;
                    vm.IsArrivalWindow         = setting.IsArrivalWindow;
                    vm.SetStrings();
                    vm.TwoColumnPerWindow      = setting.TwoColumnPerWindow;
                    vm.FirstPageOfWindow       = index;
                    vm.NumberOfStringPerWindow = setting.NumberOfStringPerWindow;
                    if (setting.IsArrivalWindow)
                    {
                        vm.WindowName = $"Arrival Window # {arrivalWindowCount}";
                        arrivalWindowCount++;
                    }
                    else
                    {
                        vm.WindowName = $"Departure Window # {departureWindowCount}";
                        departureWindowCount++;
                    }
                };

                if (setting.IsArrivalWindow)
                {
                    if (setting.TwoColumnPerWindow)
                    {
                        firstPageOfArrivalWindow = +1 * 2;
                    }
                    else
                    {
                        firstPageOfArrivalWindow = +1;
                    }
                }
                else
                {
                    if (setting.TwoColumnPerWindow)
                    {
                        firstPageOfDepartureWindow = +1 * 2;
                    }
                    else
                    {
                        firstPageOfDepartureWindow = +1;
                    }
                }

                mainWindow.Show();
            }
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