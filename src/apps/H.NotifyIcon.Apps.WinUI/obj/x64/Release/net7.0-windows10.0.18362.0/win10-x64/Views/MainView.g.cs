﻿#pragma checksum "C:\Users\noahj\OneDrive\Dokumente\Visual Studio 2022\repos\PowerModeWinUI\src\apps\H.NotifyIcon.Apps.WinUI\Views\MainView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F1C9954AD9FB361717C7A9031B7CC08D0A8244D00354B64194157D2BB332C960"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace H.NotifyIcon.Apps.Views
{
    partial class MainView : 
        global::Microsoft.UI.Xaml.Window, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2307")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\MainView.xaml line 12
                {
                    this.NavigationView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.NavigationView>(target);
                }
                break;
            case 3: // Views\MainView.xaml line 35
                {
                    this.TrayIconView = global::WinRT.CastExtensions.As<global::H.NotifyIcon.Apps.Views.TrayIconView>(target);
                }
                break;
            case 4: // Views\MainView.xaml line 33
                {
                    this.NavigationViewFrame = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Frame>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2307")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

