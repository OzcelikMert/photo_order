﻿#pragma checksum "..\..\..\Updater\WindowUpdater.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "48E78B84BE54DF3EAE4D5EE91E332BAC37113EA8FCAAA154ED8C2299BEFBFF99"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PhotoOrder_Updater.Updater;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PhotoOrder_Updater.Updater {
    
    
    /// <summary>
    /// WindowUpdater
    /// </summary>
    public partial class WindowUpdater : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridTop;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label companyName;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label developerCompanyName;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridProgressBar;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label totalSizeText;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label statusText;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label downloadedSizeText;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Updater\WindowUpdater.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressDownloadBar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PhotoOrder Updater;component/updater/windowupdater.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Updater\WindowUpdater.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\Updater\WindowUpdater.xaml"
            ((PhotoOrder_Updater.Updater.WindowUpdater)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.gridTop = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.companyName = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.developerCompanyName = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.gridProgressBar = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.totalSizeText = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.statusText = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.downloadedSizeText = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.progressDownloadBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
