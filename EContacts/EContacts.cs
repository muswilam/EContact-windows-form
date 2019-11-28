using EContacts.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContacts
{
    public partial class EContacts : Form
    {
        public EContacts()
        {
            InitializeComponent();
        }

        Contact contact = new Contact();

        private void EContacts_Load(object sender, EventArgs e)
        {
            ListData();
        }

        //load data from dataGridView
        private void ListData()
        {
            DataTable dt = contact.Select();
            dgvContactsList.DataSource = dt;
        }

        //empty all fields 
        private void Clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the value from the input fields
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.ContactNo = txtContactNo.Text;
            contact.Address = txtAddress.Text;
            contact.Gender = cmbGender.SelectedItem.ToString();

            //inserting data into db 
            bool sucess = contact.Insert(contact);

            if (sucess)
            {
                MessageBox.Show(contact.LastName + " successfully inserted.");
                Clear();
            }
            else
                MessageBox.Show("Failed!, try again please.");

            ListData();
        }
    }
}
