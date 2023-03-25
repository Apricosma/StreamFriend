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
    public static void GetChat()
    {
        var api = new TwitchAPI();
        var client = new TwitchClient();

        api.Settings.ClientId = Auth.TwitchClientId;
        api.Settings.AccessToken = Auth.TwitchAccessToken;

        client.Initialize(new ConnectionCredentials(Auth.TwitchAppName, Auth.OAuthToken));

        client.OnConnected += (sender, e) =>
        {
            client.JoinChannel("Damalia_VT");
        };

        client.Connect();

        client.OnMessageReceived += (sender, e) =>
        {
            Console.WriteLine($"[{e.ChatMessage.Username}] {e.ChatMessage.Message}");
        };

        
    }

}