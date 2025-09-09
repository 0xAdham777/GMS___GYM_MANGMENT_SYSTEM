using Guna.UI2.WinForms;
using GYM_BuisnessLayer;
using GYM_MS.Global;
using GYM_MS.Global_Classes;
using GYM_MS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GYM_BuisnessLayer.clsPerson;

namespace GYM_MS.People
{
    public partial class frmAddUpdatePerson : Form
    {

        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        private int _PersonID = -1;
        private clsPerson _Person;


        private enum enMode { AddNew = 0 , Update = 1 }
        private enMode _Mode = enMode.AddNew;

        private void _FillBloodsInComoboBox()
        {
            DataTable dtBloods = clsBloods.GetAllBloods();

            foreach (DataRow dr in dtBloods.Rows)
            {
                cmBlood.Items.Add(dr["BloodName"]);
            }

        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillBloodsInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitel.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitel.Text = "Update Person";
            }

            //set default image for the person.
            if (rbMale.Checked)
                pbImage.Image = Resources.anonymos_man;
            else
                pbImage.Image = Resources.anonymous_woman;

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (pbImage.ImageLocation != null);

            //we set the max date to 5 years from today, and set the default value the same.
            dtpBirthDate.MaxDate = DateTime.Now.AddYears(-5);
            dtpBirthDate.Value = dtpBirthDate.MaxDate;

            //should not allow adding age more than 100 years
            dtpBirthDate.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to A+.
            cmBlood.SelectedIndex = 0;

            txtFirstName.Text = "";
            txtMidName.Text = "";
            txtLastName.Text = "";
            txtPhoneNumber.Text = "";
            rbMale.Checked = true;
            txtEmail.Text = "";
            txtAddress.Text = "";


        }

        private void _LoadData()
        {

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtMidName.Text = _Person.MidName;
            txtLastName.Text = _Person.LastName;
            txtPhoneNumber.Text = _Person.PhoneNumber;
            dtpBirthDate.Value = _Person.BirthDate;

            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            txtAddress.Text = _Person.Address;
            txtEmail.Text = _Person.Email;
            cmBlood.SelectedIndex = cmBlood.FindString(_Person.BloodInfo.BloodType);


            //load person image incase it was set.
            if (_Person.PhotoPath != "")
            {
                pbImage.ImageLocation = _Person.PhotoPath;
                pbImage.Load(pbImage.ImageLocation);

            }

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (_Person.PhotoPath != "");

        }

        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.PhotoPath != pbImage.ImageLocation)
            {
                if (_Person.PhotoPath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.PhotoPath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pbImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }


        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            Guna2TextBox Temp = ((Guna2TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsGlobal.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
            ;

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.anonymos_man;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.anonymous_woman;
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

           if (!this._HandlePersonImage())
           {
                return;
           }

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.MidName = txtMidName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.PhoneNumber = txtPhoneNumber.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.BirthDate = dtpBirthDate.Value;


          
            if (rbMale.Checked)
                _Person.Gender = clsPerson.enGender.Male;
            else
                _Person.Gender = clsPerson.enGender.Female;

            if (pbImage.ImageLocation != null)
                _Person.PhotoPath = pbImage.ImageLocation;
            else
                _Person.PhotoPath = "";


            _Person.BloodID = clsBloods.Find(cmBlood.Text).BloodID;


           

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitel.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbImage.ImageLocation = selectedFilePath;
                pbImage.Load(selectedFilePath);
               
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            pbImage.ImageLocation = null;
            



            if (rbMale.Checked)
                pbImage.Image = Resources.anonymos_man;
            else
                pbImage.Image = Resources.anonymous_woman;

            llRemoveImage.Visible = false;
        }
    }
}
