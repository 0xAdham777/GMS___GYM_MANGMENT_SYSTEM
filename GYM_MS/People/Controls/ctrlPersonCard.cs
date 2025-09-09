using GYM_BuisnessLayer;
using GYM_MS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GYM_MS.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {

     

        private clsPerson _Person;

        private int _PersonID = -1;

        public int SelectedPersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        
        private void _LoadPersonImage()
        {
            //if (_Person.Gender == 0)
            //    pbImage.Image = Resources.anonymos_man;
            //else
            //    pbImage.Image = Resources.anonymous_woman;

            string ImagePath = _Person.PhotoPath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;

            llEditPersonInfo.Enabled = true;
            lblAddress.Text = _Person.Address;
            lblBirthDate.Text = _Person.BirthDate.ToShortDateString();
            lblEmail.Text = _Person.Email;
            lblName.Text = _Person.FullName;
            lblPeronsID.Text = _Person.PersonID.ToString();
            lblPhoneNumber.Text = _Person.PhoneNumber;
            lblGender.Text = (_Person.Gender == clsPerson.enGender.Male ) ? "Male" : "Female";
            lblBlood.Text = clsBloods.Find(_Person.BloodID).BloodType;

            _LoadPersonImage();

        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;

            lblAddress.Text = "[???]";
            lblBirthDate.Text = "[???]";
            lblEmail.Text = "[???]";
            lblName.Text = "[???]";
            lblPeronsID.Text = "[???]";
            lblPhoneNumber.Text = "[???]";
            lblGender.Text = "[???]";
            lblBlood.Text = "[???]";

            pbImage.Image = Resources.anonymos_man;

        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();

            //refresh
            LoadPersonInfo(_PersonID);
        }
    }
}
