﻿#pragma checksum "..\..\..\LoginWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A6D06961265B5F22F5B720E968ED815DEDF5754E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
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
    /// LoginWindow
    /// </summary>
    public partial class LoginWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FitBody.LoginWindow ResponsiveWindow;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minimizeButton;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button resizeButton;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox loginTextBox;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordBox;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock overlayTextBlock;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logginButton;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button registerButton;
        
        #line default
        #line hidden
        
        
        #line 209 "..\..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button resetPassButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FitBody;V1.0.0.0;component/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\LoginWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ResponsiveWindow = ((FitBody.LoginWindow)(target));
            return;
            case 2:
            this.closeButton = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\LoginWindow.xaml"
            this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 45 "..\..\..\LoginWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.closeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.minimizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\..\LoginWindow.xaml"
            this.minimizeButton.Click += new System.Windows.RoutedEventHandler(this.minimizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 73 "..\..\..\LoginWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.minimizeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.resizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\LoginWindow.xaml"
            this.resizeButton.Click += new System.Windows.RoutedEventHandler(this.resizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 98 "..\..\..\LoginWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.resizeImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.loginTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 133 "..\..\..\LoginWindow.xaml"
            this.loginTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.login_GotFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.passwordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 144 "..\..\..\LoginWindow.xaml"
            this.passwordBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.passwordBox_PasswordChanged);
            
            #line default
            #line hidden
            
            #line 145 "..\..\..\LoginWindow.xaml"
            this.passwordBox.GotFocus += new System.Windows.RoutedEventHandler(this.passwordBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 145 "..\..\..\LoginWindow.xaml"
            this.passwordBox.LostFocus += new System.Windows.RoutedEventHandler(this.passwordBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 10:
            this.overlayTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.logginButton = ((System.Windows.Controls.Button)(target));
            
            #line 164 "..\..\..\LoginWindow.xaml"
            this.logginButton.Click += new System.Windows.RoutedEventHandler(this.logginButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.registerButton = ((System.Windows.Controls.Button)(target));
            
            #line 189 "..\..\..\LoginWindow.xaml"
            this.registerButton.Click += new System.Windows.RoutedEventHandler(this.registerButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.resetPassButton = ((System.Windows.Controls.Button)(target));
            
            #line 210 "..\..\..\LoginWindow.xaml"
            this.resetPassButton.Click += new System.Windows.RoutedEventHandler(this.resetPassButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

