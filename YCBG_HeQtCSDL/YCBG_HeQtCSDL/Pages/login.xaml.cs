using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace YCBG_HeQtCSDL.Pages
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _login();
        }

        private void _login()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QLMuaHang2"].ConnectionString;
            connectionString += "User ID=" + username.Text + "; Password=" + password.Password;

            //MessageBox.Show(connectionString, "T");

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // open if login success
                    NavigationService navService = NavigationService.GetNavigationService(this);
                    showOption sO = new showOption(connectionString);
                    navService.Navigate(sO);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    MessageBox.Show("Login fail", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _login();
            }
        }
    }
}
