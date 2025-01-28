using static PowerModeWinUI.Tools.SetPowerMode;
using System.Diagnostics;
using Windows.System.Power;
using System.ComponentModel;
using PowerModeWinUI.Tools;


namespace H.NotifyIcon.Apps.Views;

public sealed partial class PowerView
    {
    
    bool wait = true;
        public TaskbarIcon? TrayIcon { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        Debug.WriteLine($"Property '{propertyName}' changed.");
    }

    public PowerView()
        {
        object s = Windows.Storage.ApplicationData.Current.LocalSettings.Values["BetterPerfState"];
        Debug.WriteLine($"BetterPerformanceGUID: {s}");
        
        //Not all machines have the same GUIDs available. This sets the displayed ones to those that your computer can use

        if (s != null && s is bool onValue)
        {
            if (onValue)
            {
                RecomName = "Recommended";
                BetterPerformanceVisibility = Visibility.Visible;
                BetterBatteryVisibility = Visibility.Collapsed;

            }
            else
            {
                BetterBatteryVisibility = Visibility.Visible;
                BetterPerformanceVisibility = Visibility.Collapsed;
                RecomName = "Balanced";
            }
        }

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
                BettB.IsChecked = false;
            }
            else if (p == SetPowerMode.PowerM.BetterPerformance)
            {
                Debug.WriteLine("Better performance");
                Recom.IsChecked = false;
                Bett.IsChecked = true;
                Best.IsChecked = false;
                BettB.IsChecked= false;
            }
            else if (p == SetPowerMode.PowerM.BestPerformance)
            {
                Debug.WriteLine("Best performance");
                Recom.IsChecked = false;
                Bett.IsChecked = false;
                Best.IsChecked = true;
                BettB.IsChecked = false;
            }
        else if (p == SetPowerMode.PowerM.BetterBattery)
        {
            Debug.WriteLine("Best performance");
            Recom.IsChecked = false;
            Bett.IsChecked = false;
            Best.IsChecked = false;
            BettB.IsChecked = true;
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

    private void BetterBRadioButton_Checked(object sender, RoutedEventArgs e)
    {

        if (sender is RadioButton radioButton && radioButton.IsChecked == true)
        {
            var p = PowerM.BetterBattery;

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

    public void GpuToggled(object sender, RoutedEventArgs e)
        {
  

        if (!wait) //the app loads the current switch position, which sometimes causes gputoggled to activate
            {

            if (dgpu.IsOn)
            {
                Debug.WriteLine("set to true");
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["OnState"] = true;

                var gpu = new PowerModeWinUI.GPU.GPUcheck();

                Task.Run(() => gpu.StartGPUChecking(TrayIcon));

            }

            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["OnState"] = false;
                Debug.WriteLine("set to false");


                App.RenewGlobalToken();

            }
            }
        
        }

    private Visibility _betterBatteryVisibility = Visibility.Visible;
    public Visibility BetterBatteryVisibility
    {
        get => _betterBatteryVisibility;
        set
        {
            if (_betterBatteryVisibility != value)
            {
                _betterBatteryVisibility = value;
                OnPropertyChanged(nameof(BetterBatteryVisibility));
            }
        }
    }

    private Visibility _betterPerformanceVisibility = Visibility.Collapsed;
    public Visibility BetterPerformanceVisibility
    {
        get => _betterPerformanceVisibility;
        set
        {
            if (_betterPerformanceVisibility != value)
            {
                _betterPerformanceVisibility = value;
                OnPropertyChanged(nameof(BetterPerformanceVisibility));
            }
        }
    }

    private string recomName = "Recommended";
    public string RecomName
    {
        get => recomName;
        set
        {
            if (recomName != value)
            {
                recomName = value;
                OnPropertyChanged(nameof(RecomName));
                Debug.WriteLine("name changed");
            }
        }
    }



}
   

