using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApplication
{
    public partial class ChangeNicknameForm : Form
    {
        public String nickname;

        public ChangeNicknameForm()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(textBox1, "You have to input a nickname.");
                return;
            }
            else if (!textBox1.Text.Trim().All(char.IsLetterOrDigit))
            {
                errorProvider1.SetError(textBox1, "You can use letters and numbers in your nickname only.");
                return;
            }
            this.nickname = textBox1.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
