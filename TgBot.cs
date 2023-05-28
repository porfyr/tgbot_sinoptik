using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace tgbot_synoptyk
{
    class TgBot
    {
        public async static Task RunBot()
        {
            string TOKEN = System.IO.File.ReadAllText("TOKEN.txt");
            var botClient = new TelegramBotClient(TOKEN);
            using CancellationTokenSource cts = new ();

            ReceiverOptions receiverOptions = new ()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            
            Console.WriteLine("Зачав прослуховування");

            Console.ReadLine();

            cts.Cancel();
        }

        async static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatID = message.Chat.Id;

            if (messageText == "/start")
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatID,
                    text: $"Йду на зліт",
                    cancellationToken: cancellationToken
                );
            }

            if (messageText == "/weather")
            {
                var pohodka = getPohodka();
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatID,
                    text: $"Погода на сьогодні:\n\n{pohodka[0]}\n\n{pohodka[1]}",
                    cancellationToken: cancellationToken
                );
            }
        }

        static string[] getPohodka()
        {
            string[] pohodka = {"", ""};
            var parser = new Parsing.ParserWorker<string[]>(
                new Parsing.Pohodka.PohodkaParser(),
                new Parsing.Pohodka.PohodkaSettings()
            );
            parser.OnNewData += ((object arg0, string[] arg1) => 
            {
                pohodka = arg1;
            });
            parser.Start();
            return pohodka;
        }
        

        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"помилка Телеграм API:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}