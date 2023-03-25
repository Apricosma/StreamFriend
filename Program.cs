using StreamFriend;
using TwitchLib.Client;

class Program
{
    static void Main(string[] args)
    {
        TwitchChat chat = new TwitchChat();

        chat.Start();

        // Wait for user input to exit
        Console.WriteLine("Press 'M' to print the most recent message. Ctrl + C to exit");
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.M)
            {
                string lastMessage = chat.GetLastMessage();
                if (lastMessage != null)
                {
                    Console.WriteLine($": Last message: {lastMessage}");
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