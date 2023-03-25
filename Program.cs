using StreamFriend;
using TwitchLib.Client;

class Program
{
    static async Task Main(string[] args)
    {
        TwitchChat chat = new TwitchChat();

        chat.Start();

        // Wait for user input to exit
        Console.WriteLine("Press 'M' to print the most recent message. Ctrl + C to exit\n");
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.M)
            {
                string lastMessage = chat.GetLastMessage();
                if (lastMessage != null)
                {
                    Console.WriteLine($": Sending recent message: {lastMessage}\n");

                    var openAIGpt = new OpenAIGpt(Auth.OpenAIGptKey, Auth.OpenAIModelId);
                    var response = await openAIGpt.GenerateResponse(lastMessage);

                    Console.WriteLine($"Response {response}");

                    chat.ClearLastMessage();
                    
                }
                else
                {
                    Console.WriteLine(": No messages received yet.");
                }
            }
            else
            {
                break;
            }
        }

        // exit key Ctrl + C
        var exitSignal = new AutoResetEvent(false);
        Console.CancelKeyPress += (sender, e) =>
        {
            exitSignal.Set();

            e.Cancel = true;
        };

        exitSignal.WaitOne();
    }
}