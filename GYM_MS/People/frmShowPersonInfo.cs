using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.People
{
    public partial class frmShowPersonInfo : Form
    {
        private int PersonID = -1;

        public frmShowPersonInfo(int personID)
        {
            InitializeComponent();
            PersonID = personID;
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void frmShowPersonInfo_Load(object sender, EventArgs e)
        {
            ctrlPersonCard1.LoadPersonInfo(this.PersonID);
            
        }
    }
}
