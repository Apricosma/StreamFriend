using StreamFriend;
using System;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.Services;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage;

class TwitchChat
{
    private TwitchClient client;
    private MessageProcessor processor;

    public TwitchChat()
    {
        client = new TwitchClient();
        processor = new MessageProcessor();

        client.Initialize(new ConnectionCredentials(Auth.TwitchAppName, Auth.OAuthToken));

        client.OnConnected += (sender, e) =>
        {
            try
            {
                client.JoinChannel("Damalia_VT");
                Console.WriteLine("Successfully joined channel");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            client.OnMessageReceived += (sender, e) =>
            {
                Console.WriteLine($"[{e.ChatMessage.Username}] {e.ChatMessage.Message}");
                processor.HandleMessage(sender, e);
            };
        };
    }

    public void Start()
    {
        client.Connect();
    }
    
    public string GetLastMessage()
    {
        return processor.GetLastMessage();
    }

    public void ClearLastMessage()
    {
        processor.ClearLastMessage();
    }
}