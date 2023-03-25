using System.Net.Http.Headers;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace StreamFriend
{
    public class MessageProcessor
    {
        private string _lastMessage;
        private bool _isConsumed;

        public void HandleMessage(object sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine($"Recieved message: {e.ChatMessage.Message}");

            if (_isConsumed)
            {
                return;
            }

            _lastMessage = e.ChatMessage.Message;

            _isConsumed = true;
        }

        public string GetLastMessage()
        {
            string lastMessage = _lastMessage;
            _lastMessage = null;
            _isConsumed = false;

            return lastMessage;
        }

        public void ClearLastMessage()
        {
            _lastMessage = null;
        }
    }
}
