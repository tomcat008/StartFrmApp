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

namespace ModemCtlApp
{
    public partial class ParamSetting : Form
    {
        public ParamSetting()
        {
            InitializeComponent();
            callNumbers = new List<CallNumber>();
        }
        List<CallNumber> callNumbers;

        public List<CallNumber> CallNumbers
        {
            get { return callNumbers; }
            set { callNumbers = value; }
        }
        private void ParamSetting_Load(object sender, EventArgs e)
        {
            lb_call_number.Items.AddRange(callNumbers.Select(i=>i.PhoneNumber).ToArray());
        }

        private void btn_add_call_number_Click(object sender, EventArgs e)
        {
            foreach(var i in lb_call_number.Items)
            {
                if(txt_call_number.Text.Equals(i))
                {
                    MessageBox.Show(string.Format("{0}，已存在！", txt_call_number.Text), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            callNumbers.Add(new CallNumber() { PhoneNumber = txt_call_number.Text, State = 0 });
            lb_call_number.Items.Add(txt_call_number.Text);
        }

        public string ServiceCenter { get; set; }
        public string MEM1 { get; set; }
        public string MEM2 { get; set; }
        public string MEM3 { get; set; }

        public Mode Mode { get; set; }
        public MT MT { get; set; }
        public bool ReadDelete { get; set; }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            ServiceCenter = txtServiceCenter.Text;
            MEM1 = txtMEM1.Text;
            MEM2 = txtMEM2.Text;
            MEM3 = txtMEM3.Text;
            this.Mode = (Mode)Enum.Parse(typeof(Mode), txtMode.Text);
            this.MT = (MT)Enum.Parse(typeof(MT), txtMT.Text);
            ReadDelete = radReadDelete.Checked;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }


        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            var txt = lb_call_number.SelectedItem.ToString();
            for (var i = 0; i < callNumbers.Count; i++)
            {
                if(callNumbers[i].PhoneNumber == txt)
                {
                    callNumbers.RemoveAt(i);
                    lb_call_number.Items.Remove(txt);
                    break;
                }
            }
        }
    }
}
