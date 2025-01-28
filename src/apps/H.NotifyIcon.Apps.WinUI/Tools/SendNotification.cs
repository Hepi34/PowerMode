using static PowerModeWinUI.Tools.SetPowerMode;
using System.Diagnostics;

namespace PowerModeWinUI.Tools
{
    internal class SendNotification
    {
        public TaskbarIcon? TrayIcon { get; set; }

        public SendNotification(TaskbarIcon? trayIcon)
        {
            TrayIcon = trayIcon;
        }

        public void Notification()
        {
            object m = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SoundState"];
            bool sound = false;

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
    }
}
