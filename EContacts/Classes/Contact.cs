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

        //inserting data into db 
        public bool Insert(Contact contact)
        {
            //setting a default return type value to false
            bool isSuccess = false;

            //step 1: connect db 
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                //step 2:  create sql query to insert data
                string sql = "insert into Contact (FirstName, LastName, ContactNo. Address, Gender) Values (@FirstName, @LastName, @ContactNo. @Address, @Gender)";

                SqlCommand cmd = new SqlCommand();

                //create parameters to add data 
                cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", contact.ContactNo);
                cmd.Parameters.AddWithValue("@Address", contact.Address);
                cmd.Parameters.AddWithValue("@Gender", contact.Gender);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                //if the query runs successfully then value be greater than 0 else its value be 0
                if (rows > 0)
                    isSuccess = true;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                con.Close();
            }

            return isSuccess;
        }
    }
}
