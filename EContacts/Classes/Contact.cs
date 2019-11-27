using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EContacts.Classes
{
    class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string connectionString = ConfigurationManager.ConnectionStrings["ContactsCS"].ConnectionString;

        //listing data from db 
        public DataTable Select()
        {
            //step 1: db connection 
            SqlConnection con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            try
            {
                //step 2: writing sql query
                string sql = "select * from Contacts";
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
    }
}
