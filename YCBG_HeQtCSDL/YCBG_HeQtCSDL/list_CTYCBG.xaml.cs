using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using YCBG_HeQtCSDL.Pages;
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for list_CTYCBG.xaml
    /// </summary>
    ///
    
    public partial class list_CTYCBG : Window
    {
        public static ChiTietYeuCauBaoGiaVM chiTietYeuCauBaoGiaVM { get; set; }
        public bool isClosed;
        string connectionString;

        YeuCauBaoGiaVM clickedYCBG;
        List<YeuCauBaoGiaVM> yeuCauBaoGiaVMs;
        ListChiTIetYeuCauBaoGia listChiTIetYeuCauBaoGia;
        List<ListChiTIetYeuCauBaoGia> listChiTIetYeuCauBaoGias;
        List<int> updateIndex;

        public list_CTYCBG(string connectionString, YeuCauBaoGiaVM ycbgVM)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            this.clickedYCBG = ycbgVM;
            updateIndex = new List<int>();
            get_listCTYCBG();
            if (Func.utils.convertUTF8(ycbgVM.TinhTrang) != "Da tao")
                btnGui.IsEnabled = false;
            else 
                btnLuu.IsEnabled = false;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var t = (ListChiTIetYeuCauBaoGia)dtgList_YCBG.SelectedItem;
            chiTietYeuCauBaoGiaVM = new ChiTietYeuCauBaoGiaVM()
            {
                TenNPP = t.NhaPhanPhoi,
                MaSP = t.MaSP + "",
                TenSP = t.TenSanPham,
                SL = t.SoLuong + ""
            };


            CTBaoGia cTBaoGia = new CTBaoGia(connectionString, clickedYCBG, chiTietYeuCauBaoGiaVM);
            //chiTietBaoGiaVM = new ChiTietBaoGiaVM(((ChiTietBaoGiaVM)chiTietBaoGiaVMTemp));
            //MessageBox.Show(yeuCauBaoGiaVM.MaYCBG);
            cTBaoGia.ShowDialog();
            // Some operations with this row

            //refresh when windows closed
            if (cTBaoGia.isClosed && cTBaoGia.isSaved)
            {
                int index = dtgList_YCBG.SelectedIndex;
                listChiTIetYeuCauBaoGias[index].GiaDaBao = decimal.Parse(cTBaoGia.clickedCTYCBG.Gia);

                if (!updateIndex.Contains(index))
                    updateIndex.Add(index);

                //get_listCTYCBG();
            }
            dtgList_YCBG.Items.Refresh();
        }

        private void get_listCTYCBG()
        {
            listChiTIetYeuCauBaoGias = Func.getData.get_listCTYCBG(connectionString, clickedYCBG);
            dtgList_YCBG.ItemsSource = listChiTIetYeuCauBaoGias;
        }

        private void btnGui_Click(object sender, RoutedEventArgs e)
        {
            gui_email(clickedYCBG.MaYCBG);
            this.isClosed = true;
            Close();
        }

        private void gui_email(string maYCBG)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_gui_YCBG", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@maYCBG", maYCBG);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_update_gia_ctycbg", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    
                    DataTable table = new DataTable();
                    var colString = System.Type.GetType("System.String");
                    var colDecimal = System.Type.GetType("System.Decimal");
                    table.Columns.Add("MaNPP", colString);
                    table.Columns.Add("MaSP", colString);
                    table.Columns.Add("Gia", colDecimal);

                    if (updateIndex.Count == 0) return;

                    updateIndex.ForEach(t => {
                        table.Rows.Add(
                            listChiTIetYeuCauBaoGias[t].NhaPhanPhoi,
                            listChiTIetYeuCauBaoGias[t].MaSP,
                            listChiTIetYeuCauBaoGias[t].GiaDaBao
                        );
                    });

                    command.Parameters.AddWithValue("@dsCTYCBG", table);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Done");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
            }
            isClosed = true;
            Close();
        }
    }
}
