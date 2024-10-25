using System;
using System.Collections.Generic;

namespace ChatMediatorPattern
{
    public interface IMediator
    {
        void SendMessage(string message, User user);
        void AddUser(User user);
    }

    public class ChatRoom : IMediator
    {
        private readonly List<User> users = new List<User>();

        public void SendMessage(string message, User user)
        {
            foreach (var u in users)
            {
                if (u != user)
                {
                    u.ReceiveMessage(message);
                }
            }
        }

        public void AddUser(User user) => users.Add(user);
    }

    public class User
    {
        private readonly IMediator mediator;
        private readonly string name;

        public User(IMediator mediator, string name)
        {
            this.mediator = mediator;
            this.name = name;
            mediator.AddUser(this);
        }


        public void SendMessage(string message) => mediator.SendMessage(message, this);

        public void ReceiveMessage(string message) => Console.WriteLine($"{name} received: {message}");
    }

    public class Program
    {
        public static void Main()
        {
            var chatRoom = new ChatRoom();

            var user1 = new User(chatRoom, "Alinur");
            var user2 = new User(chatRoom, "Bauka");
            var user3 = new User(chatRoom, "Nurik");

            user1.SendMessage("Hello, everyone!");
            user2.SendMessage("Hi, Bauka!");
            user3.SendMessage("Good to see you all!");
        }
    }
}
