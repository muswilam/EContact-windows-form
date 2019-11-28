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
            txtContactId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
        }

        #region Add Data
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the value from the input fields
            contact.FirstName = txtFirstName.Text;
            contact.LastName = " " + txtLastName.Text;
            contact.ContactNo = txtContactNo.Text;
            contact.Address = txtAddress.Text;
            contact.Gender = cmbGender.SelectedItem.ToString();

            //inserting data into db 
            bool sucess = contact.Insert(contact);

            if (sucess)
            {
                MessageBox.Show("Added successfully.","Success");
                Clear();
            }
            else
                MessageBox.Show("Something went wrong.", "Failed!");

            ListData();
        }
        #endregion

        #region Update data
        //get data from dataGridView and load it to input fields 
        private void dgvContactsList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            var name = dgvContactsList.Rows[rowIndex].Cells[1].Value.ToString();
            var space = name.IndexOf(" ");
            string firstName = "";
            string lastName = "";
            if (space > 0)
            {
                firstName = name.Substring(0, space);
                lastName = name.Substring(space + 1);
            }
            else
            {
                firstName = name;
            }

            txtContactId.Text = dgvContactsList.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName != null ? lastName : "";
            txtContactNo.Text = dgvContactsList.Rows[rowIndex].Cells[2].Value.ToString();
            txtAddress.Text = dgvContactsList.Rows[rowIndex].Cells[3].Value.ToString();
            cmbGender.Text = dgvContactsList.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContactId.Text))
            {
                //get data from input fields
                contact.ContactId = int.Parse(txtContactId.Text);
                contact.FirstName = txtFirstName.Text;
                contact.LastName = " " + txtLastName.Text;
                contact.ContactNo = txtContactNo.Text;
                contact.Address = txtAddress.Text;
                contact.Gender = cmbGender.Text;

                //update data in db 
                bool success = contact.Update(contact);

                if (success)
                {
                    MessageBox.Show("Updated Successfully","Success");
                    ListData();
                    Clear();
                }
                else
                    MessageBox.Show("Something went wrong.", "Failed!");
            }
            else
                MessageBox.Show("You have to select a record for updating.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Delete data & Clear inputs
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtContactId.Text))
            {
                var result = MessageBox.Show("Are you sure, delete this record?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    //get row id from dataGridView 
                    int id = int.Parse(txtContactId.Text);

                    bool success = contact.Delete(id);

                    if (success)
                    {
                        MessageBox.Show("Deleted successfully.", "Success");
                        ListData();
                        Clear();
                    }
                    else
                        MessageBox.Show("Something went wrong.", "Failed!");
                }
            }
            else
                MessageBox.Show("You have to select a record for deleting.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #endregion

    }
}
