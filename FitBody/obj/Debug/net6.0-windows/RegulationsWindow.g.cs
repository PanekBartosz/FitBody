﻿#pragma checksum "..\..\..\RegulationsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C42A25236016A1DAA15182DD4F9B54E0ABAE7000"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FitBody;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace FitBody {
    
    
    /// <summary>
    /// RegulationsWindow
    /// </summary>
    public partial class RegulationsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\RegulationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FitBody.RegulationsWindow ResponsiveWindow;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\RegulationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\RegulationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minimizeButton;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\RegulationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button resizeButton;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\..\RegulationsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button undoButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FitBody;component/regulationswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\RegulationsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ResponsiveWindow = ((FitBody.RegulationsWindow)(target));
            return;
            case 2:
            this.closeButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\RegulationsWindow.xaml"
            this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 43 "..\..\..\RegulationsWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.closeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.minimizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\RegulationsWindow.xaml"
            this.minimizeButton.Click += new System.Windows.RoutedEventHandler(this.minimizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 71 "..\..\..\RegulationsWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.minimizeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.resizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\RegulationsWindow.xaml"
            this.resizeButton.Click += new System.Windows.RoutedEventHandler(this.resizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 96 "..\..\..\RegulationsWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.resizeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.undoButton = ((System.Windows.Controls.Button)(target));
            
            #line 191 "..\..\..\RegulationsWindow.xaml"
            this.undoButton.Click += new System.Windows.RoutedEventHandler(this.undoButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

