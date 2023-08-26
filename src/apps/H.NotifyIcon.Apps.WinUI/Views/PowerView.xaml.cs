    using static H.NotifyIcon.Apps.SetPowerMode;
using H.NotifyIcon.Apps;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;
    using Microsoft.Win32;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using Windows.UI.Core;
    using System;
    using System.Management;
    using NvAPIWrapper;
    using NvAPIWrapper.Display;
    using NvAPIWrapper.GPU;
    using NvAPIWrapper.Native.GPU.Structures;
    using NvAPIWrapper.Native.Interfaces.GPU;
    using Windows.Media.AppRecording;
    using NvAPIWrapper.Native.GPU;
using H.NotifyIcon;
using System.Threading;

    namespace H.NotifyIcon.Apps.Views;

    public sealed partial class PowerView
    {

    public TaskbarIcon? TrayIcon { get; set; }
    bool executed = true;
        Guid saved;
        bool wait = true;
        bool ignorefirstp;
    bool sentnoti = false;


    public PowerView()
        {

        InitializeComponent();
 
            dgpu.Toggled += GpuToggled;
        

    }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            wait = true;

            base.OnNavigatedTo(e);

            object s = Windows.Storage.ApplicationData.Current.LocalSettings.Values["OnState"];
            Debug.WriteLine($"OnNavigatedTo - on: {s}");


            if (s != null && s is bool onValue)
            {
                if (onValue)
                {
                    dgpu.IsOn = true;
                }
                else
                {
                    dgpu.IsOn = false;
                }
            }
            else
            {
  
            }

            wait = false;   



            SetPowerMode.PowerGetEffectiveOverlayScheme(out Guid p);

            if (p == SetPowerMode.PowerM.Recommended)
            {
                Debug.WriteLine("Recom");
                Recom.IsChecked = true;
                Bett.IsChecked = false;
                Best.IsChecked = false;
            }
            else if (p == SetPowerMode.PowerM.BetterPerformance)
            {
                Debug.WriteLine("Better performance");
                Recom.IsChecked = false;
                Bett.IsChecked = true;
                Best.IsChecked = false;
            }
            else if (p == SetPowerMode.PowerM.BestPerformance)
            {
                Debug.WriteLine("Best performance");
                Recom.IsChecked = false;
                Bett.IsChecked = false;
                Best.IsChecked = true;
            }


        }

        private void RecommendedRadioButton_Checked(object sender, RoutedEventArgs e)
        {

            if (sender is RadioButton radioButton && radioButton.IsChecked == true)
            {
                var p = PowerM.Recommended;

                var result = SetPowerMode.PowerSetActiveOverlayScheme(p);
            }
        }

        private void BetterRadioButton_Checked(object sender, RoutedEventArgs e)
        {

            if (sender is RadioButton radioButton && radioButton.IsChecked == true)
            {
                var p = PowerM.BetterPerformance;

                var result = SetPowerMode.PowerSetActiveOverlayScheme(p);
            }
        }

        private void BestRadioButton_Checked(object sender, RoutedEventArgs e)
        {

            if (sender is RadioButton radioButton && radioButton.IsChecked == true)
            {
                var p = PowerM.BestPerformance;

                var result = SetPowerMode.PowerSetActiveOverlayScheme(p);
            }
        }

        public async void StartGPUChecking()
        {

        CancellationToken globalToken = App.GlobalCancellationToken;

        while (!globalToken.IsCancellationRequested)
        {
                bool nvidia = GpuType();

                if (!nvidia & executed)
                {
                    PowerGetEffectiveOverlayScheme(out saved);

                }

                if (nvidia)

                {
                    if (ignorefirstp)
                    {
                        Debug.WriteLine("ignoring first positive");
                        ignorefirstp = false;
                    }
                    else
                    {
                        executed = false;
                        Guid p;
                        p = SetPowerMode.PowerM.BestPerformance;
                        Debug.WriteLine("gpu used");

                        PowerSetActiveOverlayScheme(p);


                        object u = Windows.Storage.ApplicationData.Current.LocalSettings.Values["NotiState"];
                        

                        if (u != null && u is bool onValue)
                        {
                            if (onValue)
                            {
                            if (!sentnoti)
                            {
                                await Task.Run(() => Notification());
                                sentnoti = true;
                            }

                        }
                        }
                    }



                }
                else if (executed == false)
                {
                SetPowerMode.PowerSetActiveOverlayScheme(saved);
                executed = true;
                sentnoti = false;
                }
            await Task.Delay(5000); // Wait before the next check
            }


        }

    public void Notification()
    {
        object m = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SoundState"];
        var sound = true;

        if (m != null && m is bool MonValue)
        {
            if (MonValue)
            {
                sound = true;
            }
            else
            {
                sound = false;
            }
        }
        Debug.WriteLine("notify");

        PowerGetEffectiveOverlayScheme(out Guid z);

        var pm = "Recommended";

        if (z == SetPowerMode.PowerM.Recommended)
        {
            pm = "Recommended";

        }
        else if (z == SetPowerMode.PowerM.BetterPerformance)
        {
            pm = "Better Performance";

        }
        else if (z == SetPowerMode.PowerM.BestPerformance)
        {
            pm = "Best Performance";

        }

        TrayIcon?.ShowNotification(

            title: "Hepi34/PowerMode",
            message: "The powermode changed to " + pm,
            icon: NotificationIcon.Info,

            //largeIcon: LargeIconCheckBox.IsChecked ?? false,

            sound: sound,
            respectQuietTime: true,
            realtime: true,
            timeout: null);
    }
    public void GpuToggled(object sender, RoutedEventArgs e)
        {
            bool temp;

            if (!wait)
            {

                if (dgpu.IsOn)
                {
                    temp = true;
                    Debug.WriteLine("set to true");
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["OnState"] = temp;

                    //This is needed because the first gpu check is always true
                    ignorefirstp = true;

                    Task.Run(() => StartGPUChecking());

            }

                else
                {
                    temp = false;
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["OnState"] = temp;
                    Debug.WriteLine("set to false");


                App.RenewGlobalToken();

            }
            }
        
        }


        private bool GpuType()
        {
                NVIDIA.Initialize();

                PhysicalGPU gpu = PhysicalGPU.GetPhysicalGPUs()[0];

            try
            {
                //Dumb way, but when the GPU is off, it throws an exception. I did this so it wouldn't wake the GPU from sleep every time. I  save power this way.
                Debug.WriteLine(gpu.PerformanceStatesInfo);
                return true;
            }

            catch {

                return false;
            }
            
        }



    }
   

