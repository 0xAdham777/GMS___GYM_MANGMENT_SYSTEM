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

namespace GYM_MS.Members
{
    public partial class frmShowMemberInfo : Form
    {

        private int _MemberID = -1;
        public frmShowMemberInfo(int MemberID)
        {
            InitializeComponent();
            _MemberID = MemberID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowMemberInfo_Load(object sender, EventArgs e)
        {
            ctrlMemberCard1.LoadMemberInfo(_MemberID);
        }
    }
}
