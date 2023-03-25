class Program
{
    static void Main(string[] args)
    {
        TwitchChat.GetChat();

        // exit key Ctrl + C
        var exitSignal = new AutoResetEvent(false);
        Console.CancelKeyPress += (sender, e) =>
        {
            exitSignal.Set();

            e.Cancel = true;
        };

        exitSignal.WaitOne();
    }

    //private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    //{
    //    throw new NotImplementedException();
    //}
}