using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModemCtlApp
{
    public partial class DlgFrm : Form
    {
        public DlgFrm()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(11 != txt.Text.Length)
            {
                MessageBox.Show("手机号码输入长度未达到11位！","错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Regex reg = new Regex(@"1\d{10}");
            if(!reg.IsMatch(txt.Text))
            {
                MessageBox.Show("手机号码输入有误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Tag = txt.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
