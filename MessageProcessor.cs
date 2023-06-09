﻿using System.Net.Http.Headers;
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
            if (_isConsumed)
            {
                return;
            }

            _lastMessage = e.ChatMessage.Message;
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
            _isConsumed = false;
        }
    }
}
