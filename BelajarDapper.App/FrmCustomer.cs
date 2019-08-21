using BelajarDapper.App.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BelajarDapper.App
{
    public partial class FrmCustomer : Form
    {
        
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void LoadCustomers()
        {
            using (var conn = DBConnection.GetOpenConnection())
            {
                var sql = "Select * From Customers";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var dtr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dtr);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private static List<Customers> GetCustomers()
        {
            var listCustomers = new List<Customers>();

            using (var conn = DBConnection.GetOpenConnection())
            {
                var sql = "Select * From Customers";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            var customers = new Customers();
                            customers.CustomerId = dtr["CustomerId"].ToString();
                            customers.CompanyName = dtr["CompanyName"].ToString();
                            customers.City = dtr["City"].ToString();

                            listCustomers.Add(customers);
                        }
                    }
                }
            }

            return listCustomers;
        }

        private void LoadCustomers2()
        {
            List<Customers> customers = GetCustomers();
            dataGridView2.DataSource = customers.ToList();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            LoadCustomers2();
        }

        private void LoadCustomersDapper()
        {
            using (var conn = DBConnection.GetOpenConnection())
            {
                var sql = "Select * From Customers";
                List<Customers> listCustomers = conn.Query<Customers>(sql, null).ToList();

                dataGridView3.DataSource = listCustomers;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            LoadCustomersDapper();
        }

    }
}
