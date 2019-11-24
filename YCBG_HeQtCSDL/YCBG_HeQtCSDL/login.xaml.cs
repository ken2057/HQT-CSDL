using System;
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
using System.Windows.Shapes;
using System.Configuration;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["QLMuaHang1"].ConnectionString;
            connectionString += "User ID=" + username.Text + "; Password=" + password.Text;

            //MessageBoxResult t = MessageBox.Show(connectionString, "T");

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // open if login success
                    main_YCBG themSanPhamYCBG = new main_YCBG(connectionString);
                    closeWindow();
                    themSanPhamYCBG.ShowDialog();
                }
                catch (Exception exception)
                {
                    //MessageBox.Show(exception.ToString());
                    MessageBox.Show("Login fail", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void closeWindow()
        {
            this.Close();
        }
    }
}
