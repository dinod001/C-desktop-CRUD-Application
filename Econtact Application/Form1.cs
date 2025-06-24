using Econtact_Application.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact_Application
{
    public partial class Form1 : Form
    {

        contactClass c = new contactClass();

        public Form1()
        {
            InitializeComponent();
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.FirstName = txtFirstName.Text.Trim();
            c.LastName = txtLastName.Text.Trim();
            c.ContactNo = txtContactNo.Text.Trim();
            c.Adress = txtAdress.Text.Trim();
            c.Gender = comGender.Text.Trim();

            //inserting data into database
            bool success = c.Insert(c);
            if (success)
            {
                MessageBox.Show("New Contact Added Successfully");

                //show inserting data in datagridview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                //clear the textboxes after inserting data
                clear();

            }
            else
            {
                MessageBox.Show("Failed to Add New Contact");
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();

        }

        private void clear()
        {
            //clear all the textboxes
            txtContactId.Clear(); // Clear the ContactID textbox as well
            txtFirstName.Clear();
            txtLastName.Clear();
            txtContactNo.Clear();
            txtAdress.Clear();
            comGender.SelectedIndex = -1; // Clear the selected index of the ComboBox
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update the contact information
            c.FirstName = txtFirstName.Text.Trim();
            c.LastName = txtLastName.Text.Trim();
            c.ContactNo = txtContactNo.Text.Trim();
            c.Adress = txtAdress.Text.Trim();
            c.Gender = comGender.Text.Trim();

            c.ContactID = Convert.ToInt32(txtContactId.Text.Trim()); // Get the ContactID from the textbox

            // Updating data in the database
            bool success = c.Update(c);
            if (success)
            {
                MessageBox.Show("Contact Updated Successfully");
                // Refresh the DataGridView to show updated data
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // Clear the textboxes after updating data
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Update Contact");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // When a row header is clicked, populate the textboxes with the selected contact's information
            int rowIndex = e.RowIndex; // Get the index of the clicked row

            // You can also set the ContactID if needed for updates or deletions
            txtContactId.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();


            txtFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtAdress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            comGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete the selected contact
            c.ContactID = Convert.ToInt32(txtContactId.Text.Trim()); // Get the ContactID from the textbox
            // Deleting data from the database
            bool success = c.delete(c);
            if (success)
            {
                MessageBox.Show("Contact Deleted Successfully");
                // Refresh the DataGridView to show updated data
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // Clear the textboxes after deleting data
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete Contact");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Search functionality to filter contacts based on the search text
            string keywords = txtSearch.Text.Trim(); // Get the search keywords from the textbox
            if (keywords.Length > 0) {
                DataTable dt = c.Search(keywords); // Call the Search method from contactClass
                dgvContactList.DataSource = dt; // Bind the search results to the DataGridView
            }
            else
            {
                // If search text is empty, show all contacts
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
            }
        }
    }
}
