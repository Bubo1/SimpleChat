using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication
{
    public class Room
    {

        public String name { get; set; }
        public List<User> users { get; set; }
        public List<Message> messages { get; set; }
        public User owner { get; set; }
        public List<User> banned { get; set; }
        public String topic { get; set; }

        public Room(String name)
        {
            this.name = name;
            this.users = new List<User>();
            this.messages = new List<Message>();
            this.owner = null;
            this.banned = new List<User>();
            this.topic = "";
        }

        public Room(String name, User user)
        {
            this.name = name;
            this.users = new List<User>();
            this.users.Add(user);
            this.messages = new List<Message>();
            this.owner = null;
            this.banned = new List<User>();
            this.topic = "";
        }

        public override string ToString()
        {
            return String.Format("{0}", name);
        }

    }
}
