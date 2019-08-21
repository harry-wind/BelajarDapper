using BelajarDapper.App.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BelajarDapper.App
{
    public partial class FrmTrnCustomer : Form
    {
        private string action = "";
        private Customers customer = null;

        public FrmTrnCustomer()
        {
            InitializeComponent();
            ClearAllText();
            EnabledControl(false);
        }

        private void FrmTrnCustomer_Load(object sender, EventArgs e)
        {
            ClearAllText();
            LoadCustomers();
        }

        // Method untuk  memanggil data customers
        private void LoadCustomers()
        {
            using (var conn = DBConnection.GetOpenConnection())
            {
                var sql = "Select * From Customers";
                List<Customers> listCustomers = conn.Query<Customers>(sql, null).ToList();

                dataGridView1.DataSource = listCustomers;
            }
        }

        // Method untuk membersihkan inputan TextBox
        private void ClearAllText()
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
        }

        private void EnabledControl(bool enabled)
        {
            txtCustomerID.Enabled = enabled;
            txtCompanyName.Enabled = enabled;
            txtCity.Enabled = enabled;
            btnAdd.Enabled =  !enabled;
            btnEdit.Enabled = !enabled;
            btnSave.Enabled = enabled;
            btnCancel.Enabled = enabled;
            btnDelete.Enabled = !enabled;
            
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearAllText();
            action = "add";
            btnSave.Text = "Save";
            EnabledControl(true);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ClearAllText();
            EnabledControl(false);
            action = "";
        }

        private int SaveData()
        {
            int result = 0;

            customer = new Customers
            {
                CustomerId = txtCustomerID.Text,
                CompanyName = txtCompanyName.Text,
                City = txtCity.Text
            };

            using (var conn = DBConnection.GetOpenConnection())
            {
                var sql = "Insert into Customers (CustomerId, CompanyName, City) Values (@CustomerId, @CompanyName, @City)";
                result = conn.Execute(sql, customer);
            }

            return result;
        }

        private int SaveData2()
        {
            int result = 0;

            customer = new Customers
            {
                CustomerId = txtCustomerID.Text,
                CompanyName = txtCompanyName.Text,
                City = txtCity.Text
            };

            using (var conn = DBConnection.GetOpenConnection())
            {
                var sql = "Insert into Customers (CustomerId, CompanyName, City) Values (@CustId, @CompName, @City)";
                result = conn.Execute(sql, new {
                    CustId = customer.CustomerId, CompName = customer.CompanyName
                });
            }

            return result;
        }

        private int UpdateData()
        {
            int result = 0;

            customer = new Customers
            {
                CustomerId = txtCustomerID.Text,
                CompanyName = txtCompanyName.Text,
                City = txtCity.Text
            };

            using (var conn = DBConnection.GetOpenConnection())
            {
                var sql = "Update Customers set CompanyName = @CompanyName, City = @City Where CustomerId = @CustomerId";
                result = conn.Execute(sql, customer);
            }

            return result;
        }

        private int DeleteData()
        {
            int result = 0;

            customer = new Customers
            {
                CustomerId = txtCustomerID.Text
            };

            using (var conn = DBConnection.GetOpenConnection())
            {
                var sql = "Delete Customers Where CustomerId = @CustomerId";
                result = conn.Execute(sql, customer);
            }

            return result;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtCustomerID.Text = row.Cells[0].Value.ToString();
                txtCompanyName.Text = row.Cells[1].Value.ToString();
                txtCity.Text = row.Cells[2].Value.ToString();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (action == "add")
            {
                if (SaveData() > 0)
                {
                    MessageBox.Show("Simpan Data Berhasil");

                    ClearAllText();
                    EnabledControl(false);
                    LoadCustomers();
                } else
                {
                    MessageBox.Show("Simpan Data Gagal");
                }
            } else if (action == "edit")
            {
                if (UpdateData() > 0)
                {
                    MessageBox.Show("Update Data Berhasil");
                    ClearAllText();
                    EnabledControl(false);
                    LoadCustomers();
                } else
                {
                    MessageBox.Show("Update Data Gagal");
                }
            } 
            
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            action = "edit";
            btnSave.Text = "Update";
            EnabledControl(true);
            txtCustomerID.Enabled = false;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Anda yakin untuk menghapus data?","Konfirmasi",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (DeleteData() > 0)
                {
                    MessageBox.Show("Hapus Data Berhasil");
                    ClearAllText();
                    LoadCustomers();
                }
                else
                {
                    MessageBox.Show("Hapus Data Gagal");
                }
            }
        }
    }
}
