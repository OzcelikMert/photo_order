﻿#pragma checksum "..\..\..\..\..\views\pages\product\add.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F2D063D367627B2545B04BB4D118CF5D7453EF203CDE143C9B472F6EBF7CB47C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Photo_Order.views.pages.product;
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


namespace Photo_Order.views.pages.product {
    
    
    /// <summary>
    /// add
    /// </summary>
    public partial class add : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\..\views\pages\product\add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer imagesMain;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\..\views\pages\product\add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel images;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\..\views\pages\product\add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\..\views\pages\product\add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button turnLeft;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\..\views\pages\product\add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button turnRight;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\views\pages\product\add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox isActiveSlider;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\..\views\pages\product\add.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save;
        
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
            System.Uri resourceLocater = new System.Uri("/Photo Order;component/views/pages/product/add.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\views\pages\product\add.xaml"
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
            this.imagesMain = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.images = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.turnLeft = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\..\views\pages\product\add.xaml"
            this.turnLeft.Click += new System.Windows.RoutedEventHandler(this.turnImage);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 16 "..\..\..\..\..\views\pages\product\add.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.chooseImageClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.turnRight = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\..\views\pages\product\add.xaml"
            this.turnRight.Click += new System.Windows.RoutedEventHandler(this.turnImage);
            
            #line default
            #line hidden
            return;
            case 7:
            this.isActiveSlider = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.save = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\..\views\pages\product\add.xaml"
            this.save.Click += new System.Windows.RoutedEventHandler(this.saveClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
