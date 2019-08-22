using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BelajarDapper.App
{
    public class DBConnection
    {
        public static SqlConnection GetOpenConnection()
        {
            SqlConnection conn;
            try
            {
                string server = ".\\LOCAL";
                string database = "Northwind";
                string userDb = "sa";
                string passwordDb = "p@ssw0rd";

                string conString = "server=" + server + 
                    ";database=" + database + 
                    ";User ID=" + userDb + 
                    ";Password=" + passwordDb + 
                    ";Integrated Security=False;";

                conn = new SqlConnection(conString);
                conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                throw;
            }
            return conn;
        }

    }
}
