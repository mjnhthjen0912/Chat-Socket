﻿#pragma checksum "C:\Users\mjnht\onedrive\documents\visual studio 2017\Projects\ChatAppUWP\ChatAppUWP\Login.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F2CBDC2121B7027B934884CF583C54CA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatAppUWP
{
    partial class Login : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 10 "..\..\..\Login.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.MainGrid = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                    #line 13 "..\..\..\Login.xaml"
                    ((global::Windows.UI.Xaml.Controls.RelativePanel)this.MainGrid).SizeChanged += this.Page_SizeChanged;
                    #line default
                }
                break;
            case 3:
                {
                    this.contentPlace = (global::Windows.UI.Xaml.Controls.ContentControl)(target);
                }
                break;
            case 4:
                {
                    this.signIn = (global::ChatAppUWP.Sigin)(target);
                }
                break;
            case 5:
                {
                    this.signUp = (global::ChatAppUWP.Signup)(target);
                }
                break;
            case 6:
                {
                    this.signUpFade = (global::Microsoft.Toolkit.Uwp.UI.Animations.Behaviors.Fade)(target);
                }
                break;
            case 7:
                {
                    this.signInFade = (global::Microsoft.Toolkit.Uwp.UI.Animations.Behaviors.Fade)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

