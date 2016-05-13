using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication
{
    public class User
    {

        public String nickname { get; set; }
        public Socket socket { get; set; }

        public User(String nickname)
        {
            this.nickname = nickname;
            this.socket = null;
        }

        public User(String nickname, Socket socket)
        {
            this.nickname = nickname;
            this.socket = socket;
        }

        public override string ToString()
        {
            return String.Format("{0}", nickname);
        }

    }
}
