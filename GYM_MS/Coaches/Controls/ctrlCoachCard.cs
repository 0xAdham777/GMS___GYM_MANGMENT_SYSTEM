using GYM_BuisnessLayer;
using GYM_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Coaches.Controls
{
    public partial class ctrlCoachCard : UserControl
    {
        public ctrlCoachCard()
        {
            InitializeComponent();
        }

        private int _CoachID = -1;
        private clsCoach _Coach;
        private clsCoachSpezalations _Spezalation;

        public clsCoach SelectedCoachInfo
        {
            get { return _Coach; }
        }

        private void _FillWithDefaultValue()
        {

            // Coach Info
            lblCoachID.Text = "[???]";
            lblCoachSpelazationName.Text = "[???]";
        }

        private void _LoadData()
        {
            // تحميل بيانات الشخص المرتبط بالكوتش
            ctrlPersonCard1.LoadPersonInfo(_Coach.PersonID);

            // Coach Info
            lblCoachID.Text = _Coach.CoachID.ToString();

            // جلب التخصص
            _Spezalation = clsCoachSpezalations.Find(_Coach.CoachSpezalationsID);
            lblCoachSpelazationName.Text = _Spezalation != null ? _Spezalation.SpelaztionsName : "No Spezalation";
        }

        public void LoadCoachInfo(int CoachID)
        {
            _CoachID = CoachID;
            _Coach = clsCoach.Find(CoachID);

            if (_Coach == null)
            {
                MessageBox.Show($"This Coach With Id {CoachID} Does Not Exist", "Error Message",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FillWithDefaultValue();
                return;
            }

            _LoadData();
        }

        private void llEditCoachInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateCoaches frm = new frmAddUpdateCoaches(_CoachID);
            frm.ShowDialog();

            // لإعادة التحديث بعد التعديل
            LoadCoachInfo(_CoachID);
        }
    }
}
