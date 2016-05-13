using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChatApplication
{
    public partial class Form1 : Form
    {
        private static Socket serverSocket;
        private static int PORT = 30000;

        private byte[] dataStream = new byte[2048];

        private User user;
        private List<Room> rooms = new List<Room>();

        // Delegates for calling UI thread from async
        private delegate void AddMessageToRoomDelegate(Room room, Message message);
        private delegate void AddRoomDelegate(Room room);
        private delegate void RemoveUserFromRoomDelegate(Room room, User user);
        private delegate void ChangeNicknameDelegate(User user, String newNickname, Room room);
        private delegate void UserJoinedDelegate(User user, Room room);


        public Form1()
        {
            InitializeComponent();

            setLocalIP();

            lbRooms.DataSource = this.rooms;

            try
            {
                Debug.WriteLine("Setting up server socket");
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Random random = new Random();
                PORT = random.Next(30000, 30010);

                txtMessage.Enabled = false;

                IPEndPoint server = new IPEndPoint(IPAddress.Any, PORT);
                try
                {
                    serverSocket.Bind(server);
                }
                catch (Exception ex)
                {
                    serverSocket.Bind(new IPEndPoint(IPAddress.Any, ++PORT));
                }

                label1.Text = PORT.ToString();

                serverSocket.Listen(10);
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Load Error: " + ex.Message + " \n" + ex.StackTrace, "UDP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.StackTrace + " " + ex.Message);
            }
           
        }

        public void setLocalIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            lblLocalIP.Text = localIP;
        }

        private void AddMessageToRoom(Room r, Message message)
        {
            Room room = this.rooms.Find(x => x.name == r.name);
            room.messages.Add(message);
            if (this.rooms.ElementAt(lbRooms.SelectedIndex).name == room.name)
            {
                lbMessages.DataSource = null;
                lbMessages.DataSource = room.messages;
                lbUsers.DataSource = null;
                lbUsers.DataSource = room.users;
            }
            
        }

        private void ChangeNickname(User user, String newNickname, Room room)
        {
            this.rooms.Find(x => x.name == room.name).users.Find(x => x.nickname == user.nickname).nickname = newNickname;
        }


        private void AddRoom(Room room)
        {
            this.rooms.Add(room);
            lbRooms.DataSource = null;
            lbRooms.DataSource = this.rooms;
            lbUsers.DataSource = null;
            lbUsers.DataSource = room.users;
            lbMessages.DataSource = null;
            lbMessages.DataSource = room.messages;
            lbRooms.SelectedIndex = this.rooms.IndexOf(room);
            lbTopic.Text = room.topic;
            
        }

        private void RemoveUser(Room r, User user)
        {
            Room room = this.rooms.First(x => x.name == r.name);
            room.users.RemoveAll(x => x.nickname == user.nickname);
            if (lbRooms.SelectedIndex == this.rooms.IndexOf(room))
            {
                lbUsers.DataSource = null;
                lbUsers.DataSource = room.users;
            }
        }

        private void UserJoined(User user, Room room)
        {
            if (this.rooms.Exists(x => x.name == room.name))
            {
                this.rooms.Find(x => x.name == room.name).users.Add(user);
                if (lbRooms.SelectedIndex == this.rooms.IndexOf(room))
                {
                    lbUsers.DataSource = null;
                    lbUsers.DataSource = room.users;
                }
            }
        }

        private void AcceptCallback(IAsyncResult asyncResult)
        {
            Debug.WriteLine("AcceptCallback");
            Socket socket = serverSocket.EndAccept(asyncResult);

            socket.BeginReceive(dataStream, 0, dataStream.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);

            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                Debug.WriteLine("ReceiveCallback");

                Socket socket = (Socket)asyncResult.AsyncState;
                socket.EndReceive(asyncResult);

                byte[] data;

                Packet receivedData = new Packet(this.dataStream);

                Packet sendData = new Packet();
                Room room;

                switch (receivedData.packetType)
                {
                    case PacketType.Join: // User wants to join
                        Debug.WriteLine("ReceiveCallback - PacketType - JOIN");
                        if (!this.rooms.Find(x => x.name == receivedData.room.name).banned.Exists(x => x.nickname == receivedData.user.nickname))  // User banned
                        {
                            if (this.rooms.Find(x=> x.name == receivedData.room.name).users.Exists(x => x.nickname == receivedData.user.nickname))  // Nickname already in use, send RequestNickChange
                            {
                                sendData.packetType = PacketType.RequestNickChange;
                                sendData.room = receivedData.room;
                                data = sendData.GetDataStream();
                                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                            }
                            else // User able to join, send Joined back to user and room users
                            {
                                sendData.packetType = PacketType.Joined;
                                sendData.room = receivedData.room;
                                sendData.users = this.rooms.First(x => x.name == receivedData.room.name).users.Select(i => i.ToString()).ToList();
                                sendData.user = receivedData.user;
                                room = this.rooms.First(x => x.name == receivedData.room.name);
                                sendData.room.topic = room.topic;
                                receivedData.user.socket = socket;

                                room.users.Add(receivedData.user);
                                data = sendData.GetDataStream();

                                foreach (var u in room.users)
                                {
                                    if (u.socket == null) // Don't send to self
                                    {
                                        continue;
                                    }
                                    u.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), u.socket);
                                }
                                this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { room, new Message(String.Format("{0} has joined the chat room.", receivedData.user.nickname), new User("STATUS")) });
                            }
                        }
                        break;

                    case PacketType.Joined: // Successfully joined a room
                        Debug.WriteLine("ReceiveCallback - PacketType - JOINED");
                        room = receivedData.room;
                        room.owner = receivedData.user;
                        room.owner.socket = socket;
                        foreach (var nickname in receivedData.users)
                        {
                            room.users.Add(new User(nickname));
                        }
                        room.users.Add(this.user);
                        if (!this.rooms.Exists(x => x.name == room.name))
                        {
                            this.Invoke(new AddRoomDelegate(this.AddRoom), new object[] { room });

                        }
                        else
                        {
                            this.Invoke(new UserJoinedDelegate(this.UserJoined), new object[] { receivedData.user, room });
                        }   
                        
                        break;

                    case PacketType.Message: // Got new message
                        Debug.WriteLine("ReceiveCallback - PacketType - MESSAGE");
                        room = this.rooms.First(x => x.name == receivedData.room.name);
                        if (room.owner.socket == null)
                        {
                            broadcastMessages(this.rooms.First(x => x.name == receivedData.room.name), receivedData.message, receivedData.user);
                            this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { room, receivedData.message });
                        }
                        else
                        {
                            this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { room, receivedData.message });
                        }
                        break;

                    case PacketType.Part:  // User wants to part room
                        Debug.WriteLine("ReceiveCallback - PacketType - PART");
                        sendData.packetType = PacketType.Parted;
                        sendData.user = receivedData.user;
                        sendData.room = receivedData.room;
                        data = sendData.GetDataStream();
                        room = this.rooms.Find(x => x.name == receivedData.room.name);

                        foreach (var u in room.users)
                        {
                            if (u.socket == null || u.nickname == receivedData.user.nickname) // Don't send to self or back to user
                            {
                                continue;
                            }
                            u.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), u.socket);
                        }

                        this.Invoke(new RemoveUserFromRoomDelegate(this.RemoveUser), new object[] { receivedData.room, receivedData.user });
                        this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { receivedData.room, new Message("has parted the room.", new User("STATUS")) });
                        break;

                    case PacketType.Parted: // User parted room
                        Debug.WriteLine("ReceiveCallback - PacketType - PARTED");
                        this.Invoke(new RemoveUserFromRoomDelegate(this.RemoveUser), new object[] { receivedData.room, receivedData.user });
                        break;

                    case PacketType.Kicked: // User kicked
                    case PacketType.Banned: // User banned
                        Debug.WriteLine("ReceiveCallback - PacketType - KICKED");
                        if (receivedData.user.nickname == this.user.nickname) // Kicked me
                        {
                            if (this.rooms.Exists(x => x.name == receivedData.room.name))
                            {
                                Action<Room> KickedFromRoom = delegate(Room r)
                                {
                                    this.rooms.RemoveAll(x => x.name == r.name);
                                    lbRooms.DataSource = null;
                                    lbRooms.DataSource = this.rooms;
                                };

                                KickedFromRoom(receivedData.room);
                            }
                        }
                        else // Remove kicked user
                        {
                            this.Invoke(new RemoveUserFromRoomDelegate(this.RemoveUser), new object[] { receivedData.room, receivedData.user });
                            if (receivedData.packetType == PacketType.Kicked)
                                this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { receivedData.room, new Message("has been kicked from room.", new User("STATUS")) });
                            else
                                this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { receivedData.room, new Message("has been banned from room.", new User("STATUS")) });
                        }
                        break;

                    case PacketType.ChangeNick: // User changed nick
                        Debug.WriteLine("ReceiveCallback - PacketType - CHANGENICK");
                        foreach (var r in this.rooms)
                        {
                            if (r.owner.socket == null)  // Owner, send to room users
                            {
                                this.Invoke(new ChangeNicknameDelegate(this.ChangeNickname), new object[] { receivedData.user, receivedData.newNickname, r });
                                sendData.packetType = PacketType.ChangedNick;
                                sendData.room = r;
                                sendData.user = receivedData.user;
                                sendData.newNickname = receivedData.newNickname;
                                data = sendData.GetDataStream();
                                foreach (var u in r.users)
                                {
                                    if (u.socket != null)
                                    {
                                        u.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), u.socket);
                                    }
                                }
                            }
                            else // Change nick
                            {
                                this.Invoke(new ChangeNicknameDelegate(this.ChangeNickname), new object[] { receivedData.user, receivedData.newNickname, r });
                                this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { receivedData.room, new Message(String.Format("has changed nickname to {0}.", receivedData.newNickname), receivedData.user) });
                            }
                        }
                        break;

                    case PacketType.RequestNickChange: // User already in use, requested nick change
                        Debug.WriteLine("ReceiveCallback - PacketType - REQUESTNICKCHANGE");
                        MessageBox.Show("Nickname already in use. To join, change your nickname.", "Nickname already in use", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                this.dataStream = new byte[2048];

                Debug.WriteLine(sendData);

                socket.BeginReceive(dataStream, 0, dataStream.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);               
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ReceiveData Error: " + ex.Message + " \n" + ex.StackTrace, "TCP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.StackTrace + " " + ex.Message);
            }
        }

        private void SendCallback(IAsyncResult asyncResult) 
        {
            Debug.WriteLine("SendCallback");
            Socket socket = (Socket)asyncResult.AsyncState;
            socket.EndSend(asyncResult);
        }



        private void Form1_Shown(object sender, EventArgs e)
        {
            ChangeNicknameForm f = new ChangeNicknameForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.user = new User(f.nickname);
                this.Text = user.nickname;
                lblNickname.Text = this.user.nickname;
            }
            else
            {
                this.user = new User("SimpleChat");
                this.Text = user.nickname;
                lblNickname.Text = this.user.nickname;
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {

            JoinRoomForm f = new JoinRoomForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("Joining Room");
                Packet sendData = new Packet();
                sendData.packetType = PacketType.Join;
                sendData.user = user;
                sendData.room = f.room;
                EndPoint endPoint = f.endPoint;

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);

                byte[] data = sendData.GetDataStream();
                Debug.WriteLine("Joining room with data " + Encoding.ASCII.GetString(data));

                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);

                this.dataStream = new byte[1024];

                socket.BeginReceive(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }

            f.Close();            
        }


        private void broadcastMessages(Room room, Message message, User fromUser)
        {
            
            Packet sendData = new Packet();
            sendData.packetType = PacketType.Message;
            sendData.user = fromUser;
            sendData.room = room;
            sendData.message = message;
            byte[] data = sendData.GetDataStream();
            foreach (var u in room.users) // Send message to room users
            {
                if (u.socket == null)  // Don't send to self or back to user
                {
                    continue;
                }

                u.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), u.socket);
                
            }
        }

        private void lbRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbRooms.SelectedIndex != -1)
            {
                txtMessage.Enabled = true;
                Room room = this.rooms.ElementAt(lbRooms.SelectedIndex);
                lbUsers.DataSource = null;
                lbUsers.DataSource = room.users;
                lbMessages.DataSource = null;
                lbMessages.DataSource = room.messages;
                lbTopic.Text = room.topic;
            }
            else
            {
                lbUsers.DataSource = null;
                lbMessages.DataSource = null;
                txtMessage.Enabled = false;
                lbTopic.Text = "";
            }
        }

        private void btnPart_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Parting Room");
            Packet sendData = new Packet();
            sendData.packetType = PacketType.Part;
            sendData.user = user;
            sendData.room = this.rooms.ElementAt(lbRooms.SelectedIndex);

            byte[] data = sendData.GetDataStream();
            Debug.WriteLine("Parting room with data " + Encoding.ASCII.GetString(data));

            sendData.room.owner.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), sendData.room.owner.socket);;

            Action<Room> PartRoom = delegate(Room room)
            {
                this.rooms.Remove(room);
                lbRooms.DataSource = null;
                lbRooms.DataSource = this.rooms;
                //refreshChat();
            };

            PartRoom(sendData.room);
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Trim().Length != 0)
            {

                txtMessage.Text = txtMessage.Text.Replace(';', ':');
                Room room = this.rooms.ElementAt(lbRooms.SelectedIndex);
                if (room.owner.socket == null)
                {
                    Message message = new Message(txtMessage.Text, this.user);
                    broadcastMessages(room, message, this.user);
                    this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { room, message });
                }
                else
                {

                    Debug.WriteLine("Sending message");
                    Packet sendData = new Packet();
                    sendData.packetType = PacketType.Message;
                    sendData.user = this.user;
                    sendData.room = room;
                    sendData.message = new Message(txtMessage.Text, this.user);

                    byte[] data = sendData.GetDataStream();
                    Debug.WriteLine(Encoding.ASCII.GetString(data));

                    sendData.room.owner.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), sendData.room.owner.socket);

                }
                txtMessage.Text = null;
            }
        }

        private void txtMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnMessage_Click(null, null);
                e.Handled = true;
            }
        }

        private void lbUsers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = lbUsers.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    lbUsers.SelectedIndex = index;
                    userContextMenu.Show(Cursor.Position);
                    userContextMenu.Visible = true;
                }
            }
        }

        private void userAction(Packet sendData)
        {
            if (sendData.room.owner.socket == null)
            {
                User user = sendData.room.users.ElementAt(lbUsers.SelectedIndex);
                sendData.user = user;
                foreach (var u in sendData.room.users)
                {
                    if (u.socket == null) 
                    {
                        continue;
                    }
                    byte[] data = sendData.GetDataStream();
                    Debug.WriteLine(Encoding.ASCII.GetString(data));

                    u.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), u.socket);

                }
                this.Invoke(new RemoveUserFromRoomDelegate(this.RemoveUser), new object[] { sendData.room, user });
                this.Invoke(new AddMessageToRoomDelegate(this.AddMessageToRoom), new object[] { sendData.room, new Message(String.Format("{0} has been kicked from the room.", user), new User("STATUS")) });
                sendData.room.users.Remove(user);
            } 
            else
            {
                sendData.room.messages.Add(new Message("You are not the owner of the room.", new User("STATUS")));
                lbMessages.DataSource = null;
                lbMessages.DataSource = sendData.room.messages;
            }
        }

        private void kickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Room room = this.rooms.ElementAt(lbRooms.SelectedIndex);
            Packet sendData = new Packet();
            sendData.room = room;
            sendData.packetType = PacketType.Kicked;
            userAction(sendData);
        }

        private void banToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Room room = this.rooms.ElementAt(lbRooms.SelectedIndex);
            Packet sendData = new Packet();
            sendData.room = room;
            sendData.packetType = PacketType.Banned;
            userAction(sendData);           
        }

        private void partToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Parting Room");
            Packet sendData = new Packet();
            sendData.packetType = PacketType.Part;
            sendData.user = user;
            sendData.room = this.rooms.ElementAt(lbRooms.SelectedIndex);

            byte[] data = sendData.GetDataStream();
            Debug.WriteLine("Parting room with data " + Encoding.ASCII.GetString(data));

            sendData.room.owner.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), sendData.room.owner.socket);

            this.rooms.Remove(sendData.room);
            lbRooms.DataSource = null;
            lbRooms.DataSource = this.rooms;
            //refreshChat();
        }

        private void lbRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = lbRooms.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    lbRooms.SelectedIndex = index;
                    roomContextMenu.Show(Cursor.Position);
                    roomContextMenu.Visible = true;
                }
            }
        }

        private void changeNickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeNicknameForm f = new ChangeNicknameForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (this.rooms.Count != 0)
                {
                    Packet sendData = new Packet();
                    sendData.user = this.user;
                    sendData.newNickname = f.nickname;
                    sendData.packetType = PacketType.ChangeNick;
                    byte[] data = sendData.GetDataStream();
                    foreach (var room in this.rooms)
                    {
                        room.users.Find(x => x.nickname == this.user.nickname).nickname = f.nickname;
                        if (room.owner.socket == null)
                        {
                            foreach (var u in room.users)
                            {
                                if (u.socket == null)
                                    continue;
                                u.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), u.socket);
                            }
                            continue;
                        }
                        room.owner.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), room.owner.socket);
                    }
                }
                refreshChat();
                this.Text = f.nickname;
                this.user = new User(f.nickname);
                lblNickname.Text = this.user.nickname;
            }
        }

        private void refreshChat()
        {
            if (lbRooms.SelectedIndex != -1)
            {
                Room room = this.rooms.ElementAt(lbRooms.SelectedIndex);
                lbUsers.DataSource = null;
                lbUsers.DataSource = room.users;
                lbMessages.DataSource = null;
                lbMessages.DataSource = room.messages;
            }
        }

        private void joinRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JoinRoomForm f = new JoinRoomForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("Joining Room");
                Packet sendData = new Packet();
                sendData.packetType = PacketType.Join;
                sendData.user = user;
                sendData.room = f.room;
                EndPoint endPoint = f.endPoint;

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);

                byte[] data = sendData.GetDataStream();
                Debug.WriteLine("Joining room with data " + Encoding.ASCII.GetString(data));

                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);

                this.dataStream = new byte[1024];

                socket.BeginReceive(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }

            f.Close();
        }

        private void partRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Parting Room");
            Packet sendData = new Packet();
            sendData.packetType = PacketType.Part;
            sendData.user = user;
            sendData.room = this.rooms.ElementAt(lbRooms.SelectedIndex);

            byte[] data = sendData.GetDataStream();
            Debug.WriteLine("Parting room with data " + Encoding.ASCII.GetString(data));

            if (sendData.room.owner.socket != null)
                sendData.room.owner.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), sendData.room.owner.socket);


            this.rooms.Remove(sendData.room);
            lbRooms.DataSource = null;
            lbRooms.DataSource = this.rooms;
            //refreshChat();

        }

        private void createRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRoomForm f = new CreateRoomForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                txtMessage.Enabled = true;
                Room room = new Room(f.roomName);
                room.topic = f.topic;
                room.owner = this.user;
                room.users.Add(this.user);
                this.rooms.Add(room);
                lbRooms.DataSource = null;
                lbRooms.DataSource = this.rooms;
                lbRooms.SelectedIndex = this.rooms.IndexOf(room);
                lbTopic.Text = room.topic;
                lbUsers.DataSource = null;
                lbUsers.DataSource = room.users;
                lbMessages.DataSource = null;
                lbMessages.DataSource = room.messages;
            }
        }

    }
}
