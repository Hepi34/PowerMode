﻿#pragma checksum "C:\Users\noahj\Desktop\PowerModeWinUI\src\apps\H.NotifyIcon.Apps.WinUI\Views\TrayIconView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0EF02E4BE1AF3BA3DD57B7827CB57F4EF08C9FA85D4A4E24D35B22145990D28E"
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
    partial class TrayIconView : 
        global::Microsoft.UI.Xaml.Controls.UserControl, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2307")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_H_NotifyIcon_TaskbarIcon_LeftClickCommand(global::H.NotifyIcon.TaskbarIcon obj, global::System.Windows.Input.ICommand value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Windows.Input.ICommand) global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Windows.Input.ICommand), targetNullValue);
                }
                obj.LeftClickCommand = value;
            }
            public static void Set_H_NotifyIcon_TaskbarIcon_RightClickCommand(global::H.NotifyIcon.TaskbarIcon obj, global::System.Windows.Input.ICommand value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Windows.Input.ICommand) global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Windows.Input.ICommand), targetNullValue);
                }
                obj.RightClickCommand = value;
            }
            public static void Set_Microsoft_UI_Xaml_Controls_MenuFlyoutItem_Command(global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem obj, global::System.Windows.Input.ICommand value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Windows.Input.ICommand) global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Windows.Input.ICommand), targetNullValue);
                }
                obj.Command = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2307")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class TrayIconView_obj1_Bindings :
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            ITrayIconView_Bindings
        {
            private global::H.NotifyIcon.Apps.Views.TrayIconView dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::H.NotifyIcon.TaskbarIcon obj2;
            private global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem obj7;
            private global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem obj8;

            public TrayIconView_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2: // Views\TrayIconView.xaml line 10
                        this.obj2 = global::WinRT.CastExtensions.As<global::H.NotifyIcon.TaskbarIcon>(target);
                        break;
                    case 7: // Views\TrayIconView.xaml line 27
                        this.obj7 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                        break;
                    case 8: // Views\TrayIconView.xaml line 29
                        this.obj8 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                        break;
                    default:
                        break;
                }
            }
                        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2307")]
                        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target) 
                        {
                            return null;
                        }

            // ITrayIconView_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = global::WinRT.CastExtensions.As<global::H.NotifyIcon.Apps.Views.TrayIconView>(newDataRoot);
                    return true;
                }
                return false;
            }

            public void Activated(object obj, global::Microsoft.UI.Xaml.WindowActivatedEventArgs data)
            {
                this.Initialize();
            }

            public void Loading(global::Microsoft.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::H.NotifyIcon.Apps.Views.TrayIconView obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_LoadCommand(obj.LoadCommand, phase);
                        this.Update_ShowHideWindowCommand(obj.ShowHideWindowCommand, phase);
                        this.Update_ExitApplicationCommand(obj.ExitApplicationCommand, phase);
                    }
                }
            }
            private void Update_LoadCommand(global::CommunityToolkit.Mvvm.Input.IRelayCommand obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\TrayIconView.xaml line 10
                    XamlBindingSetters.Set_H_NotifyIcon_TaskbarIcon_LeftClickCommand(this.obj2, obj, null);
                    // Views\TrayIconView.xaml line 10
                    XamlBindingSetters.Set_H_NotifyIcon_TaskbarIcon_RightClickCommand(this.obj2, obj, null);
                }
            }
            private void Update_ShowHideWindowCommand(global::CommunityToolkit.Mvvm.Input.IRelayCommand obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\TrayIconView.xaml line 27
                    XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_MenuFlyoutItem_Command(this.obj7, obj, null);
                }
            }
            private void Update_ExitApplicationCommand(global::CommunityToolkit.Mvvm.Input.IRelayCommand obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\TrayIconView.xaml line 29
                    XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_MenuFlyoutItem_Command(this.obj8, obj, null);
                }
            }
        }

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2307")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\TrayIconView.xaml line 10
                {
                    this.TrayIcon = global::WinRT.CastExtensions.As<global::H.NotifyIcon.TaskbarIcon>(target);
                }
                break;
            case 3: // Views\TrayIconView.xaml line 22
                {
                    this.MenuFlyout1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyout>(target);
                }
                break;
            case 4: // Views\TrayIconView.xaml line 23
                {
                    this.RecommendedItem = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem)this.RecommendedItem).Click += this.Recom_Click;
                }
                break;
            case 5: // Views\TrayIconView.xaml line 24
                {
                    this.BetterPerformanceItem = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem)this.BetterPerformanceItem).Click += this.Bett_Click;
                }
                break;
            case 6: // Views\TrayIconView.xaml line 25
                {
                    this.BestPerformanceItem = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ToggleMenuFlyoutItem)this.BestPerformanceItem).Click += this.Best_Click;
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
            switch(connectionId)
            {
            case 1: // Views\TrayIconView.xaml line 1
                {                    
                    global::Microsoft.UI.Xaml.Controls.UserControl element1 = (global::Microsoft.UI.Xaml.Controls.UserControl)target;
                    TrayIconView_obj1_Bindings bindings = new TrayIconView_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

