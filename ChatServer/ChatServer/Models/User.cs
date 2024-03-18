
using System;
using System.Net.Sockets;
using System.Text;
namespace ChatServer.Models
{
    
    public class User
    {


        public string Name { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public string LocationL { get; set; }
        public string LocationA { get; set; }
        public TcpClient Connection { get; set; }

        public User(string name, int age, string photo, string locationL, string locationA, TcpClient connection)
        {
            Name = name;
            Age = age;
            Photo = photo;
            LocationL = locationL;
            LocationA = locationA;
            Connection = connection;
        }
        public override string ToString()
        {
            return $"{Name}#{Age}#{LocationL}#{LocationA}#{Photo}";
        }
    }

}
