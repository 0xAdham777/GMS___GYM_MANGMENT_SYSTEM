using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Coach_Spelazations
{
    public partial class frmShowSpelazationInfo : Form
    {
        private int _SpelazationID = -1;

        public frmShowSpelazationInfo(int SpelazationID)
        {
            InitializeComponent();
            _SpelazationID = SpelazationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowSpelazationInfo_Load(object sender, EventArgs e)
        {
            ctrlSpelazationCard1.LoadCoachSpecializationInfo(_SpelazationID);
        }
    }
}
