using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCData_Group5.Utilities
{
    public enum MessageType
    {
        Success,
        Info,
        Warning,
        Danger
    }

    public class Message
    {
        public Message(MessageType type, string text)
        {
            Type = type;
            Text = text;
        }

        public MessageType Type { get; set; }
        public string Text { get; set; }
    }

    public class MessageContainer : List<Message>
    {
        public void NewSuccess(string text)
        {
            Add(new Message(MessageType.Success, text));
        }
        public void NewInfo(string text)
        {
            Add(new Message(MessageType.Info, text));
        }
        public void NewWarning(string text)
        {
            Add(new Message(MessageType.Warning, text));
        }
        public void NewDanger(string text)
        {
            Add(new Message(MessageType.Danger, text));
        }
        /// <summary>
        /// Returns all messages in the container and then clears the container
        /// </summary>
        /// <returns>All messages in the container</returns>
        public ICollection<Message> GetAll()
        {
            List<Message> list = this.ToList();
            this.Clear();
            return list;
        }
    }
}