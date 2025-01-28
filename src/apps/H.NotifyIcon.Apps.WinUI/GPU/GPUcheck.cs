using H.NotifyIcon.Apps;
using PowerModeWinUI.Tools;
using System.Diagnostics;
using static PowerModeWinUI.Tools.SetPowerMode;
using Windows.System.Power;


namespace PowerModeWinUI.GPU
{
    internal class GPUcheck
    {
        bool ignorefirstp;
        bool sentnoti = false;
        bool executed = true;
        Guid saved;

        EnergySaverStatus GetCurrentEnergySaverStatus()
        {
            return PowerManager.EnergySaverStatus;
        }

        public async void StartGPUChecking(TaskbarIcon? trayIcon)
        {

            var Noti = new SendNotification(trayIcon);

            var nvidiaInstance = new PowerModeWinUI.GPU.Nvidia();
            var amdInstance = new PowerModeWinUI.GPU.Amd();
            ignorefirstp = true; //This is needed because the first nvidia gpu check is always true

            EnergySaverStatus currentStatus = GetCurrentEnergySaverStatus();

            CancellationToken globalToken = App.GlobalCancellationToken;

            while (!globalToken.IsCancellationRequested)
            {

                if (currentStatus == EnergySaverStatus.On)
                {
                    //nothing as power saver is on
                    await Task.Delay(5000);
                }

                else if (currentStatus == EnergySaverStatus.Off || currentStatus == EnergySaverStatus.Disabled)
                {
                    var GPUtype = "na";

                    if (nvidiaInstance.NVIDIApresent())
                    {
                        GPUtype = "nv";
                    }
                    else if (amdInstance.GetAMD())
                    {
                        GPUtype = "amd";
                    }

                    if (GPUtype == "nv")
                    {
                        bool nvidia = nvidiaInstance.GpuType();

                        //executed starts off true, so this runs and saves the mode before the gpu was used
                        if (!nvidia & executed)
                        {
                            PowerGetEffectiveOverlayScheme(out saved);

                        }

                        //nvidia = true -> Gpu is used
                        if (nvidia)

                        {
                            //1st positive is ignored
                            if (ignorefirstp)
                            {
                                Debug.WriteLine("ignoring first positive");
                                ignorefirstp = false;
                            }
                            //when gpu is used
                            else
                            {
                                executed = false; //so it no longer saves the current mode (as it will be set to best performance when using the dGPU)
                                Guid p;
                                p = SetPowerMode.PowerM.BestPerformance;
                                Debug.WriteLine("nvidia gpu used");

                                PowerSetActiveOverlayScheme(p);


                                object u = Windows.Storage.ApplicationData.Current.LocalSettings.Values["NotiState"];

                                //if notifications are on
                                if (u != null && u is bool onValue)
                                {
                                    if (onValue)
                                    {
                                        if (!sentnoti)
                                        {
                                            await Task.Run(() => Noti.Notification());
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
                            await Task.Run(() => Noti.Notification());
                        }

                        await Task.Delay(5000); // Wait before the next check
                    }

                    else if (GPUtype == "amd")
                    {
                        bool amd = amdInstance.AmdDGPUIsActive();

                        //executed starts off true, so this runs and saves the mode before the gpu was used
                        if (!amd & executed)
                        {
                            PowerGetEffectiveOverlayScheme(out saved);

                        }

                        //amd = true -> Gpu is used
                        if (amd)

                        {
                            //when gpu is used
                            executed = false; //so it no longer saves the current mode (as it will be set to best performance when using the dGPU)
                            Guid p;
                            p = SetPowerMode.PowerM.BestPerformance;
                            Debug.WriteLine("amd gpu used");

                            PowerSetActiveOverlayScheme(p);

                            object u = Windows.Storage.ApplicationData.Current.LocalSettings.Values["NotiState"];

                            //if notifications are on
                            if (u != null && u is bool onValue)
                            {
                                if (onValue)
                                {
                                    if (!sentnoti)
                                    {
                                        await Task.Run(() => Noti.Notification());
                                        sentnoti = true;
                                    }

                                }
                            }

                        }
                        else if (executed == false)
                        {
                            SetPowerMode.PowerSetActiveOverlayScheme(saved);
                            executed = true;
                            sentnoti = false;
                            await Task.Run(() => Noti.Notification());
                        }

                        await Task.Delay(5000); // Wait before the next check

                    }

                }
            }

        }
    }
}
