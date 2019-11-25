using System;
using System.Collections.Generic;
using System.Windows;
using YCBG_HeQtCSDL.Pages;
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new login();
        }
    }
}
