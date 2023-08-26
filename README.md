# PowerMode
Allows you to change the powermode from the taskbar, like you could in Windows 10 with the slider in the battery menu

Features (so far):
- Changing the powermode from the taskbar or in the app settings
- Automatically going to best performance mode when load on the discrete graphics card is detected (only for NVIDIA for now)
- Sending notifications if the powermode automatically changes.

If you are using Windows 10, you will have to download the Segoe Fluent Icons Font. You can get it here: 
https://learn.microsoft.com/de-ch/windows/apps/design/downloads/#fonts

Since I have not tested this app on Windows 10, I can not guarantee that it works. 

TBA:
- Support for AMD Laptop GPUs
- Multi-language support

Screenshots:

<img width="186" alt="image" src="https://github.com/Hepi34/PowerMode/assets/105777839/f74d99f3-edd7-4c34-bff2-57c62cb3dbe3">

<img width="891" alt="image" src="https://github.com/Hepi34/PowerMode/assets/105777839/ce4df8e3-b339-4858-b82a-0b0083f6bb00">


-------------------------------------------------------------------------------------------------------------------

Credits:

https://github.com/AaronKelley/PowerMode -> For figuring out how to change the Windows powermode in C#

https://github.com/HavenDV/H.NotifyIcon -> For the NotifyIcon code, and also for the template on how to use it
