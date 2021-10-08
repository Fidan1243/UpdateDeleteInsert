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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        string cs = "";
        DataTable table;
        SqlDataReader reader;
        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "Select * From Students";
                command.Connection = conn;
                table = new DataTable();
                bool hasColumnAdded = false;
                using (reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        if (!hasColumnAdded)
                        {

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                table.Columns.Add(reader.GetName(i));
                            }
                            hasColumnAdded = true;
                        }

                        DataRow row = table.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        table.Rows.Add(row);

                    }
                mydatagrid.ItemsSource = table.DefaultView;

            }
        }
        DataSet set;
        SqlDataAdapter dA;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ButtonClick();
        }

        public void ButtonClick()
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();
                set = new DataSet();

                dA = new SqlDataAdapter("Select * From Orders;Select * From Products;Select * From OrderDetails;Select * From Customers", conn);
                mydatagrid.ItemsSource = null;

                dA.Fill(set, "MyBook");
                mydatagrid.ItemsSource = set.Tables[0].DefaultView;
                mydatagrid2.ItemsSource = set.Tables[1].DefaultView;
                mydatagrid3.ItemsSource = set.Tables[2].DefaultView;
                mydatagrid4.ItemsSource = set.Tables[3].DefaultView;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand update = new SqlCommand("InsertProducts", conn);
                update.CommandType = CommandType.StoredProcedure;

                var param1 = new SqlParameter("@ProductName", SqlDbType.NVarChar);
                param1.Value = txt1.Text;
                update.Parameters.Add(param1);

                var param2 = new SqlParameter("@Price", SqlDbType.Int);
                param2.Value = Convert.ToInt32(txt2.Text);
                update.Parameters.Add(param2);

                update.ExecuteNonQuery();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand update = new SqlCommand("InsertCustomers", conn);
                update.CommandType = CommandType.StoredProcedure;
                var param1 = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                param1.Value = txt1.Text;
                update.Parameters.Add(param1);

                var param2 = new SqlParameter("@LastName", SqlDbType.NVarChar);
                param2.Value = txt2.Text;
                update.Parameters.Add(param2);

                update.ExecuteNonQuery();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand update = new SqlCommand("InsertOrders", conn);
                update.CommandType = CommandType.StoredProcedure;
                var param1 = new SqlParameter("@ProductId", SqlDbType.Int);
                param1.Value = Convert.ToInt32(txt1.Text);
                update.Parameters.Add(param1);

                var param2 = new SqlParameter("@CustomerId", SqlDbType.Int);
                param2.Value = Convert.ToInt32(txt2.Text);
                update.Parameters.Add(param2);

                update.ExecuteNonQuery();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand update = new SqlCommand("InsertOrderDetails", conn);
                update.CommandType = CommandType.StoredProcedure;
                var param1 = new SqlParameter("@OrdersQuantity", SqlDbType.NVarChar);
                param1.Value = Convert.ToInt32(txt1.Text);
                update.Parameters.Add(param1);

                var param2 = new SqlParameter("@OrderId", SqlDbType.Int);
                param2.Value = Convert.ToInt32(txt2.Text);
                update.Parameters.Add(param2);

                update.ExecuteNonQuery();
            }
        }

        private void mydatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gd = (DataGrid)sender;
            DataRowView vw = gd.SelectedItem as DataRowView;
            if (vw != null)
            {
                Window1 w = new Window1();
                w.SelectedInfo = "Order";
                w.TextBlo1.Text = "Id ";
                w.TextBlo2.Text = "Product Id ";
                w.TextBlo3.Text = "Customer Id ";
                w.textb1.Text = vw["Id"].ToString();
                w.textb2.Text = vw["ProductId"].ToString();
                w.textb3.Text = vw["CustomerId"].ToString();
                w.vn = this;
                w.UpdateButton.IsEnabled = false;

                w.Show();
            }
        }
        private void mydatagrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gd = (DataGrid)sender;
            DataRowView vw = gd.SelectedItem as DataRowView;
            if (vw != null)
            {
                Window1 w = new Window1();
                w.SelectedInfo = "Product";
                w.TextBlo1.Text = "Id ";
                w.TextBlo2.Text = "Product Name ";
                w.TextBlo3.Text = "Price ";
                w.textb1.Text = vw["Id"].ToString();
                w.textb2.Text = vw["ProductName"].ToString();
                w.textb3.Text = vw["Price"].ToString();
                w.vn = this;
                w.Show();
            }
        }
        private void mydatagrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gd = (DataGrid)sender;
            DataRowView vw = gd.SelectedItem as DataRowView;
            if (vw != null)
            {
                Window1 w = new Window1();
                w.SelectedInfo = "OrderDetails";
                w.TextBlo1.Text = "Id ";
                w.TextBlo2.Text = "Order Quantity ";
                w.TextBlo3.Text = "Date (If you want to update, please write in sql language) ";
                w.textb1.Text = vw["Id"].ToString();
                w.textb2.Text = vw["OrderQuantity"].ToString();
                w.textb3.Text = vw["DateOfOrder"].ToString();
                w.vn = this;
                w.Show();
            }
        }
        private void mydatagrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gd = (DataGrid)sender;
            DataRowView vw = gd.SelectedItem as DataRowView;
            if (vw != null)
            {
                Window1 w = new Window1();
                w.SelectedInfo = "Customer";
                w.TextBlo1.Text = "Id ";
                w.TextBlo2.Text = "FirstName";
                w.TextBlo3.Text = "LastName";
                w.textb1.Text = vw["Id"].ToString();
                w.textb2.Text = vw["FirstName"].ToString();
                w.textb3.Text = vw["LastName"].ToString();
                w.vn = this;
                w.Show();

            }
        }
    }
}
