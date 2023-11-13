using System.Diagnostics;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using WinUIEx;
using static H.NotifyIcon.Apps.SetPowerMode;
using Windows.System.Power;
using System;
using Microsoft.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using Windows.ApplicationModel.Core;


namespace H.NotifyIcon.Apps.Views;

public partial class TrayIconView : UserControl, INotifyPropertyChanged


{
    EnergySaverStatus GetCurrentEnergySaverStatus()
    {

        return PowerManager.EnergySaverStatus;
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        Debug.WriteLine($"Property '{propertyName}' changed.");
    }

    private bool isBatterySaverOn;

    public bool IsBatterySaverOn
    {
        get { return isBatterySaverOn; }
        set
        {
            if (isBatterySaverOn != value)
            {
                isBatterySaverOn = value;
                OnPropertyChanged(nameof(IsBatterySaverOn));
            }
        }
    }

    public TrayIconView()
    {
        DataContext = this;

        object o = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SeperateState"];
        if (o != null && o is bool OonValue)
        {
            if (OonValue)
            {
                object m = Windows.Storage.ApplicationData.Current.LocalSettings.Values["FlyoutDark"];

                if (m != null && m is bool MonValue)
                    if (MonValue)
                    {
                        this.RequestedTheme = ElementTheme.Dark;
                    }
                    else
                    {
                        this.RequestedTheme = ElementTheme.Light;
                    }

            }
        }

        EnergySaverStatus currentStatus = GetCurrentEnergySaverStatus();

        if (currentStatus == EnergySaverStatus.On)
        {
            IsBatterySaverOn = true;
        }
        else if (currentStatus == EnergySaverStatus.Off)
        {
            IsBatterySaverOn = false;
        }
        else if (currentStatus == EnergySaverStatus.Disabled)
        {
            IsBatterySaverOn = false;
        }

        InitializeComponent();


        DispatcherQueue.TryEnqueue(() => UpdateIcon());

    }

    [RelayCommand]
    public void ShowHideWindow()
    {
        var window = App.MainWindow;
        if (window == null)
        {
            return;
        }

        if (window.Visible)
        {
            window.Hide();
        }
        else
        {
            window.Show();
        }
    }

    [RelayCommand]

    public void ExitApplication()
    {

        App.HandleClosedEvents = false;
        TrayIcon.Dispose();
        App.MainWindow?.Close();
        Environment.Exit(0);

    }

    [RelayCommand]

    public void Load()
    {
        SetPowerMode.PowerGetEffectiveOverlayScheme(out Guid p);

        if (p == SetPowerMode.PowerM.Recommended)
        {
            Debug.WriteLine("Recom");
            RecommendedItem.IsChecked = true;
            BetterPerformanceItem.IsChecked = false;
            BestPerformanceItem.IsChecked = false;

        }
        else if (p == SetPowerMode.PowerM.BetterPerformance)
        {
            Debug.WriteLine("Better performance");
            RecommendedItem.IsChecked = false;
            BetterPerformanceItem.IsChecked = true;
            BestPerformanceItem.IsChecked = false;

        }
        else if (p == SetPowerMode.PowerM.BestPerformance)
        {
            Debug.WriteLine("Best performance");
            RecommendedItem.IsChecked = false;
            BetterPerformanceItem.IsChecked = false;
            BestPerformanceItem.IsChecked = true;

        }

        EnergySaverStatus currentStatus = GetCurrentEnergySaverStatus();

        if (currentStatus == EnergySaverStatus.On)
        {
            Debug.WriteLine("on");
            BatterySaverItem.IsChecked = true;
        }
        else if (currentStatus == EnergySaverStatus.Off)
        {
            Debug.WriteLine("off");
            BatterySaverItem.IsChecked = false;
        }
        else if (currentStatus == EnergySaverStatus.Disabled)
        {
            Debug.WriteLine("disabled");
            BatterySaverItem.IsChecked = false;
        }


        UpdateUIBasedOnPowerStatus();

    }

    private void UpdateUIBasedOnPowerStatus()
    {
        // Get the current power supply status
        PowerSupplyStatus powerSupplyStatus = PowerManager.PowerSupplyStatus;

        // Update your UI elements based on the power status
        BatterySaverItem.IsEnabled = powerSupplyStatus != PowerSupplyStatus.Adequate;

    }
    private void Recom_Click(object sender, RoutedEventArgs e)
    {

        if (sender is ToggleMenuFlyoutItem menuItem)
        {
            RecommendedItem.IsChecked = true;
            BetterPerformanceItem.IsChecked = false;
            BestPerformanceItem.IsChecked = false;
            var p = PowerM.Recommended;

            var result = SetPowerMode.PowerSetActiveOverlayScheme(p);


        }
    }

    private void Bett_Click(object sender, RoutedEventArgs e)
    {

        if (sender is ToggleMenuFlyoutItem menuItem)
        {
            RecommendedItem.IsChecked = false;
            BetterPerformanceItem.IsChecked = true;
            BestPerformanceItem.IsChecked = false;
            var p = PowerM.BetterPerformance;

            var result = SetPowerMode.PowerSetActiveOverlayScheme(p);


        }
    }

    private void Best_Click(object sender, RoutedEventArgs e)
    {

        if (sender is ToggleMenuFlyoutItem menuItem)
        {
            RecommendedItem.IsChecked = false;
            BetterPerformanceItem.IsChecked = false;
            BestPerformanceItem.IsChecked = true;
            var p = PowerM.BestPerformance;

            var result = SetPowerMode.PowerSetActiveOverlayScheme(p);


        }
    }

    private void Battery_Click(object sender, RoutedEventArgs e)
    {

        if (sender is ToggleMenuFlyoutItem menuItem)
        {

            EnergySaverStatus currentStatus = GetCurrentEnergySaverStatus();
            Debug.WriteLine(currentStatus.ToString());

            if (currentStatus == EnergySaverStatus.On)
            {
                //off

                ExecuteCommandSync("powercfg /setdcvalueindex SCHEME_CURRENT SUB_ENERGYSAVER ESBATTTHRESHOLD 1");
                ExecuteCommandSync("powercfg /setactive scheme_current");
                ExecuteCommandSync("powercfg /setdcvalueindex SCHEME_CURRENT SUB_ENERGYSAVER ESBATTTHRESHOLD 20");
                IsBatterySaverOn = false;

            }
            else if (currentStatus == EnergySaverStatus.Off)
            {
                //on
                ExecuteCommandSync("powercfg /setdcvalueindex SCHEME_CURRENT SUB_ENERGYSAVER ESBATTTHRESHOLD 100");
                ExecuteCommandSync("powercfg /setactive scheme_current");
                ExecuteCommandSync("powercfg /setdcvalueindex SCHEME_CURRENT SUB_ENERGYSAVER ESBATTTHRESHOLD 20");
                IsBatterySaverOn = true;
            }
            else if (currentStatus == EnergySaverStatus.Disabled)
            {
                //on
                ExecuteCommandSync("powercfg /setdcvalueindex SCHEME_CURRENT SUB_ENERGYSAVER ESBATTTHRESHOLD 100");
                ExecuteCommandSync("powercfg /setactive scheme_current");
                ExecuteCommandSync("powercfg /setdcvalueindex SCHEME_CURRENT SUB_ENERGYSAVER ESBATTTHRESHOLD 20");
                IsBatterySaverOn = true;
            }


        }
    }

    public void ExecuteCommandSync(object command)
    {
        try
        {
            // create the ProcessStartInfo using "cmd" as the program to be run,
            // and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows,
            // and then exit.
            System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = true;
            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            // Get the output into a string
            string result = proc.StandardOutput.ReadToEnd();
            // Display the command output.
            Console.WriteLine(result);
        }
        catch (Exception objException)
        {
            // Log the exception
        }
    }

    private async void UpdateIcon()
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            // Your code here

            Debug.WriteLine("reached");

            // Delay before the next iteration
            await Task.Delay(5000);

            EnergySaverStatus currentStatus = GetCurrentEnergySaverStatus();

            if (currentStatus == EnergySaverStatus.On)
            {
                IsBatterySaverOn = true;
            }
            else if (currentStatus == EnergySaverStatus.Off)
            {
                IsBatterySaverOn = false;
            }
            else if (currentStatus == EnergySaverStatus.Disabled)
            {
                IsBatterySaverOn = false;
            }

        }
    }




}


