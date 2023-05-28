

namespace tgbot_synoptyk.Parsing
{
    class Program
    {
        static void Main()
        {
            TgBot.RunBot().GetAwaiter().GetResult();
            
            // var parser = new Parsing.ParserWorker<string[]>(
            //     new Pohodka.PohodkaParser(),
            //     new Pohodka.PohodkaSettings()
            // );
            // parser.OnNewData += Parser_OnNewData;
            // parser.Start();
        }




        // static void Parser_OnNewData(object arg1, string[] arg2)
        // {
        //     Console.WriteLine("Я живиий");
        //     foreach (var data in arg2)
        //     {
        //         Console.WriteLine(data);
        //     }
        // }
    }
}