﻿#pragma checksum "C:\Users\noahj\OneDrive\Dokumente\Visual Studio 2022\repos\PowerModeWinUI\src\apps\H.NotifyIcon.Apps.WinUI\Views\PowerView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2DD602738B6F6C00E66F59957CCD61DE8D3462F665AD3EA43795528FA90E8F2E"
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
    partial class PowerView : 
        global::Microsoft.UI.Xaml.Controls.Page, 
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
            case 2: // Views\PowerView.xaml line 15
                {
                    this.pbut = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButtons>(target);
                }
                break;
            case 3: // Views\PowerView.xaml line 35
                {
                    this.dgpu = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ToggleSwitch>(target);
                }
                break;
            case 4: // Views\PowerView.xaml line 16
                {
                    this.Recom = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButton)this.Recom).Checked += this.RecommendedRadioButton_Checked;
                }
                break;
            case 5: // Views\PowerView.xaml line 21
                {
                    this.Bett = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButton)this.Bett).Checked += this.BetterRadioButton_Checked;
                }
                break;
            case 6: // Views\PowerView.xaml line 26
                {
                    this.Best = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButton)this.Best).Checked += this.BestRadioButton_Checked;
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

