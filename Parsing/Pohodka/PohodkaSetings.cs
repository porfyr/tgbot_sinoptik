
namespace tgbot_synoptyk.Parsing.Pohodka
{
    class PohodkaSettings : IParserSettings
    {
        public PohodkaSettings()
        {
        }

        public string BaseUrl { get; set; } = "https://ua.sinoptik.ua/"; //"https://ua.sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%BB%D1%8C%D0%B2%D1%96%D0%B2";

        public string Sufix { get; set; } = "%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%BB%D1%8C%D0%B2%D1%96%D0%B2/"; // тут може впишу дату
    }
}