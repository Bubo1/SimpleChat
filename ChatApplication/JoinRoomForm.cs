using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApplication
{
    public partial class JoinRoomForm : Form
    {
        public EndPoint endPoint;
        public Room room;

        public JoinRoomForm()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex("^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$");
            if (textBox1.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(textBox1, "Can't be blank");
                return;
            } else if (!regex.IsMatch(textBox2.Text.Trim())) {
                errorProvider1.SetError(textBox2, "Enter a valid IP address");
                return;
            } else if (!textBox3.Text.All(char.IsDigit)) {
                errorProvider1.SetError(textBox3, "Enetry a valid port.");
                return;
            }

            this.room = new Room(textBox1.Text);

            IPAddress serverIP = IPAddress.Parse(textBox2.Text);

            IPEndPoint server;
            server = new IPEndPoint(serverIP, Convert.ToInt32(textBox3.Text.Trim()));

            this.endPoint = (EndPoint)server;

            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }
    }
}
