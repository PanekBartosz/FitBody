﻿#pragma checksum "..\..\..\ResetPassWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AAFA185974AB3CDC661BB210A490B958AADF269F"
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
    /// ResetPassWindow
    /// </summary>
    public partial class ResetPassWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\ResetPassWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FitBody.ResetPassWindow ResponsiveWindow;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\ResetPassWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\ResetPassWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minimizeButton;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\ResetPassWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button resizeButton;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\ResetPassWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button resetPassButton;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\..\ResetPassWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailBox;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\ResetPassWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button undoButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FitBody;component/resetpasswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ResetPassWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ResponsiveWindow = ((FitBody.ResetPassWindow)(target));
            return;
            case 2:
            this.closeButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\ResetPassWindow.xaml"
            this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 43 "..\..\..\ResetPassWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.closeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.minimizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\ResetPassWindow.xaml"
            this.minimizeButton.Click += new System.Windows.RoutedEventHandler(this.minimizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 71 "..\..\..\ResetPassWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.minimizeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.resizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\ResetPassWindow.xaml"
            this.resizeButton.Click += new System.Windows.RoutedEventHandler(this.resizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 96 "..\..\..\ResetPassWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.resizeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.resetPassButton = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.emailBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 149 "..\..\..\ResetPassWindow.xaml"
            this.emailBox.GotFocus += new System.Windows.RoutedEventHandler(this.emailBox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 10:
            this.undoButton = ((System.Windows.Controls.Button)(target));
            
            #line 160 "..\..\..\ResetPassWindow.xaml"
            this.undoButton.Click += new System.Windows.RoutedEventHandler(this.undoButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

