using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication
{

    public enum PacketType
    {
        Message,
        Join,
        Joined,
        Part,
        Parted,
        Kick,
        Kicked,
        Ban,
        Banned,
        ChangeNick,
        ChangedNick,
        RequestNickChange,
        Null
    }

    public class Packet
    {
        public PacketType packetType { get; set; }
        public User user { get; set; }
        public Message message { get; set; }
        public Room room { get; set; }
        public List<String> users { get; set; }
        public String newNickname { get; set; }
        

        public Packet()
        {
            packetType = PacketType.Null;
            user = null;
            message = null;
            room = null;
            users = null;
            newNickname = null;
        }

        public Packet(byte[] dataStream)
        {

            String[] data = Encoding.UTF8.GetString(dataStream).Split(';');

            this.user = null;
            this.message = null;
            this.room = null;
            this.newNickname = null;
            this.users = new List<String>();

            Debug.WriteLine("Packet - " + String.Join(" ", data));
            try
            {
                this.packetType = (PacketType)Convert.ToInt32(data[0]);
            }
            catch
            {
                this.packetType = PacketType.Null;
            }
            
            int roomNameLength, nicknameLength, messageLength, usersLength, newNicknameLength, topicLength;
                
            switch (this.packetType)
            {
                case PacketType.Message:
                    roomNameLength = int.Parse(data[1].ToString());
                    nicknameLength = int.Parse(data[2].ToString());
                    messageLength = int.Parse(data[3].ToString());

                    this.room = new Room(data[data.Length-1].Substring(0, roomNameLength));
                    this.user = new User(data[data.Length-1].Substring(roomNameLength, nicknameLength));
                    this.message = new Message(data[data.Length-1].Substring(roomNameLength + nicknameLength, messageLength), this.user);
                    break;
                case PacketType.Join:
                case PacketType.Part:
                case PacketType.Kick:
                case PacketType.Ban:
                case PacketType.Kicked:
                case PacketType.Banned:
                case PacketType.Parted:
                    roomNameLength = int.Parse(data[1].ToString());
                    nicknameLength = int.Parse(data[2].ToString());

                    this.room = new Room(data[data.Length-1].Substring(0, roomNameLength));
                    this.user = new User(data[data.Length-1].Substring(roomNameLength, nicknameLength));
                    break;
                case PacketType.Joined:
                    roomNameLength = int.Parse(data[1].ToString());
                    nicknameLength = int.Parse(data[2].ToString());
                    usersLength = int.Parse(data[3].ToString());
                    topicLength = int.Parse(data[4].ToString());


                    this.room = new Room(data[data.Length-1].Substring(0, roomNameLength));
                    this.user = new User(data[data.Length - 1].Substring(roomNameLength, nicknameLength));
                    this.users = data[data.Length - 1].Substring(roomNameLength + nicknameLength, usersLength).Split(',').ToList();
                    this.room.topic = data[data.Length - 1].Substring(roomNameLength + nicknameLength + usersLength, topicLength);
                    break;
                case PacketType.ChangeNick:
                    nicknameLength = int.Parse(data[1].ToString());
                    newNicknameLength = int.Parse(data[2].ToString());

                    this.user = new User(data[data.Length - 1].Substring(0, nicknameLength));
                    this.newNickname = data[data.Length - 1].Substring(nicknameLength, newNicknameLength);
                    break;
                case PacketType.ChangedNick:
                    roomNameLength = int.Parse(data[1].ToString());
                    nicknameLength = int.Parse(data[2].ToString());
                    newNicknameLength = int.Parse(data[3].ToString());

                    this.room = new Room(data[data.Length - 1].Substring(0, roomNameLength));
                    this.user = new User(data[data.Length - 1].Substring(roomNameLength, nicknameLength));
                    this.newNickname = data[data.Length - 1].Substring(roomNameLength + nicknameLength, newNicknameLength);
                    break;
                case PacketType.RequestNickChange:
                    roomNameLength = int.Parse(data[1].ToString());

                    this.room = new Room(data[data.Length - 1].Substring(0, roomNameLength));
                    break;
                case PacketType.Null:
                    break;
                default:
                    break;
            }

        }

        public byte[] GetDataStream()
        {
            List<byte> dataStream = new List<byte>();

            // Add the dataIdentifier
            int packetType = (int)this.packetType;
            dataStream.AddRange(Encoding.ASCII.GetBytes(packetType.ToString()));
            dataStream.Add(Convert.ToByte(';'));

            switch (this.packetType)
            {
                case PacketType.Message:
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.message.text.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));

                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.message.text));
                    break;
                case PacketType.Join:
                case PacketType.Part:
                case PacketType.Parted:
                case PacketType.Kick:
                case PacketType.Ban:
                case PacketType.Kicked:
                case PacketType.Banned:
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));

                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname));
                    break;
                case PacketType.Joined:
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(String.Join(",", this.users).Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.topic.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));

                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(String.Join(",", this.users)));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.topic));
                    break;
                case PacketType.ChangeNick:
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.newNickname.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));

                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.newNickname));
                    break;
                case PacketType.ChangedNick:
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.newNickname.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));

                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.user.nickname));
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.newNickname));
                    break;
                case PacketType.RequestNickChange:
                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name.Length.ToString()));
                    dataStream.Add(Convert.ToByte(';'));

                    dataStream.AddRange(Encoding.ASCII.GetBytes(this.room.name));
                    break;
                case PacketType.Null:
                    break;
                default:
                    break;
            }
            return dataStream.ToArray();
        }

        public override string ToString()
        {
            switch (packetType)
            {
                case PacketType.Message:
                    return String.Format("Room: {0} | User: {1}: {3}", room.name ?? "NULL", user.nickname ?? "NULL", message.text ?? "NULL");
                case PacketType.Join:
                    return String.Format("Room: {0} | User: {1}", room.name ?? "NULL", user.nickname ?? "NULL");
                case PacketType.Joined:
                    break;
                case PacketType.Part:
                    break;
                case PacketType.Null:
                    break;
                default:
                    return packetType.ToString();
            }
            return "wtf";
            
        }

    }
}
