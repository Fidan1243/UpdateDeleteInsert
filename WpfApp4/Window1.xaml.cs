using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public MainWindow vn = new MainWindow();
        public string SelectedInfo { get; set; }
        SqlConnection conn;
        string cs = "";

        public Window1()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

            conn.ConnectionString = cs;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                if (SelectedInfo == "Product")
                {

                    SqlCommand update = new SqlCommand("UpdateProducts", conn);
                    update.CommandType = CommandType.StoredProcedure;
                    var param1 = new SqlParameter("@ProductName", SqlDbType.NVarChar);
                    param1.Value = textb2.Text;
                    update.Parameters.Add(param1);

                    var param2 = new SqlParameter("@Price", SqlDbType.Int);
                    param2.Value = textb3.Text;
                    update.Parameters.Add(param2);

                    var param3 = new SqlParameter("@Id", SqlDbType.Int);
                    param3.Value = Convert.ToInt32(textb1.Text);
                    update.Parameters.Add(param3);

                    update.ExecuteNonQuery();
                }
                else if (SelectedInfo == "Customer")
                {

                    SqlCommand update = new SqlCommand("UpdateCustomers", conn);
                    update.CommandType = CommandType.StoredProcedure;
                    var param1 = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                    param1.Value = textb2.Text;
                    update.Parameters.Add(param1);

                    var param2 = new SqlParameter("@LastName", SqlDbType.NVarChar);
                    param2.Value = textb3.Text;
                    update.Parameters.Add(param2);

                    var param3 = new SqlParameter("@Id", SqlDbType.Int);
                    param3.Value = Convert.ToInt32(textb1.Text);
                    update.Parameters.Add(param3);

                    update.ExecuteNonQuery();
                }
                else if (SelectedInfo == "OrderDetails")
                {

                    SqlCommand update = new SqlCommand("UpdateOrderDetails", conn);
                    update.CommandType = CommandType.StoredProcedure;
                    var param1 = new SqlParameter("@OrdersQuantity", SqlDbType.NVarChar);
                    param1.Value = Convert.ToInt32(textb2.Text);
                    update.Parameters.Add(param1);

                    var param2 = new SqlParameter("@DateOfOrder", SqlDbType.NVarChar);
                    param2.Value = textb3.Text;
                    update.Parameters.Add(param2);

                    var param3 = new SqlParameter("@Id", SqlDbType.Int);
                    param3.Value = Convert.ToInt32(textb1.Text);
                    update.Parameters.Add(param3);

                    update.ExecuteNonQuery();
                }
            }
            vn.ButtonClick();
            this.Close();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();
                SqlCommand update = new SqlCommand();
                update.Connection = conn;
                if (SelectedInfo == "Product")
                {
                    update.CommandText = "DeleteProducts";
                }
                else if (SelectedInfo == "Customer")
                {
                    update.CommandText = "DeleteCustomers";
                }
                else if (SelectedInfo == "Order")
                {
                    update.CommandText = "DeleteOrders";
                }
                else if (SelectedInfo == "OrderDetail")
                {
                    update.CommandText = "DeleteOrderDetails";
                }
                update.CommandType = CommandType.StoredProcedure;
                var param1 = new SqlParameter("@Id", SqlDbType.Int);
                param1.Value = Convert.ToInt32(textb1.Text);
                update.Parameters.Add(param1);

                update.ExecuteNonQuery();


            }
            vn.ButtonClick();
            this.Close();
        }
    }
}
