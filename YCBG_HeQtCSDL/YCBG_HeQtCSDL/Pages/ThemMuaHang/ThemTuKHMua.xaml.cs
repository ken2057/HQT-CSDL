﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YCBG_HeQtCSDL.Pages.ThemMuaHang
{
    /// <summary>
    /// Interaction logic for ThemTuKHMua.xaml
    /// </summary>
    public partial class ThemTuKHMua : Page
    {
        string connectionString;
        List<EF.KeHoachMuaHang> listKHMua;
        EF.KeHoachMuaHang khMua;
        Window parent;

        public ThemTuKHMua(string connectionString, Window parent)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.parent = parent;

            get_KHMua();
        }

        private void get_KHMua(string maNV = "")
        {
            listKHMua = Func.getData.get_KHMua(this.connectionString, maNV);
            dtgKHMua.ItemsSource = listKHMua;
            dtgKHMua.Items.Refresh();
        }

        private void txtMaNV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                get_KHMua(txtMaNV.Text);
        }

        private void dtgKHMua_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            infoKHMua sO = new infoKHMua(connectionString, parent, listKHMua[dtgKHMua.SelectedIndex]);
            navService.Navigate(sO);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            ThemMoi sO = new ThemMoi(connectionString, parent, listKHMua[dtgKHMua.SelectedIndex]);
            navService.Navigate(sO);
        }
    }
}
