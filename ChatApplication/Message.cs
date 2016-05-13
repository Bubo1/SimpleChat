using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication
{
    public class Message
    {

        public User user { get; set; }
        public String text { get; set; }

        public Message(String text, User user)
        {
            this.user = user;
            this.text = text;
        }

        public override string ToString()
        {
            return String.Format("<{0}>: {1}", user, text);
        }

    }
}
