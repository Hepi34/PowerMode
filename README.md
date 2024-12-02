# PowerMode
Allows you to change the powermode from the taskbar, like you could in Windows 10 with the slider in the battery menu

Features:
- Changing the powermode and switching the battery saver from the taskbar or in the app settings
- Automatically switching to best performance mode when load on the discrete graphics card is detected (only for NVIDIA for now)
- Sending notifications if the powermode automatically changes.
- This app creates an autostart entry in the task manager and will automatically start with Windows by default.
- The icon will show if the battery saver is enabled (or not).
- The available modes will automatically change based on the GUID's available on the device.

If you are using Windows 10, you will have to download the Segoe Fluent Icons Font. You can get it here: 
https://learn.microsoft.com/de-ch/windows/apps/design/downloads/#fonts

Since I have not tested this app on Windows 10, I can not guarantee that it works. 


TBA:
- Support for AMD Laptop GPUs
    -> Difficult as I don't have access to an AMD laptop at the moment.
- Multi-language support

Current bugs:
- Battery saver may not switch on or off correctly. To temponarily fix this, turn it on or off once from the settings app.

Screenshots (old):

<img width="186" alt="image" src="https://github.com/Hepi34/PowerMode/assets/105777839/f74d99f3-edd7-4c34-bff2-57c62cb3dbe3">

<img width="891" alt="image" src="https://github.com/Hepi34/PowerMode/assets/105777839/ce4df8e3-b339-4858-b82a-0b0083f6bb00">


-------------------------------------------------------------------------------------------------------------------

Credits:

https://github.com/AaronKelley/PowerMode -> For figuring out how to change the Windows powermode in C#

https://github.com/HavenDV/H.NotifyIcon -> For the NotifyIcon code, and also for the template on how to use it
