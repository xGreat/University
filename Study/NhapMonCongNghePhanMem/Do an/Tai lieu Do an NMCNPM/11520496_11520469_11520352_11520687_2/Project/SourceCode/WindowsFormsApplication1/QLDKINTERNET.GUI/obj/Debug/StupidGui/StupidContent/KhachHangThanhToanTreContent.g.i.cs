﻿#pragma checksum "..\..\..\..\StupidGui\StupidContent\KhachHangThanhToanTreContent.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1B00DF954B0EC670F71233B5E4FCD820"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace QLDKINTERNET.GUI.StupidGui.StupidContent {
    
    
    /// <summary>
    /// KhachHangThanhToanTreContent
    /// </summary>
    public partial class KhachHangThanhToanTreContent : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\StupidGui\StupidContent\KhachHangThanhToanTreContent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgHoaDonChuaThanhToan;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\StupidGui\StupidContent\KhachHangThanhToanTreContent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgHoaDonQuaHan;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\StupidGui\StupidContent\KhachHangThanhToanTreContent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCapNhat;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\StupidGui\StupidContent\KhachHangThanhToanTreContent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCatMang;
        
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
            System.Uri resourceLocater = new System.Uri("/QLDKINTERNET.GUI;component/stupidgui/stupidcontent/khachhangthanhtoantrecontent." +
                    "xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\StupidGui\StupidContent\KhachHangThanhToanTreContent.xaml"
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
            this.dgHoaDonChuaThanhToan = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.dgHoaDonQuaHan = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.btnCapNhat = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\StupidGui\StupidContent\KhachHangThanhToanTreContent.xaml"
            this.btnCapNhat.Click += new System.Windows.RoutedEventHandler(this.btnCapNhat_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnCatMang = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\..\StupidGui\StupidContent\KhachHangThanhToanTreContent.xaml"
            this.btnCatMang.Click += new System.Windows.RoutedEventHandler(this.btnCatMang_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
