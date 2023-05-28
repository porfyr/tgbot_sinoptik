using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;

namespace tgbot_synoptyk.Parsing
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        HtmlLoader loader;

        // bool isActive;

        #region Properties

        public IParser<T> Parser
        {
            get { return parser; }
            set { parser = value; }
        }

        public IParserSettings Settings
        {
            get { return parserSettings; }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        // public bool IsActive
        // {
        //     get { return isActive; }
        // }

        #endregion

        public event Action<object, T> OnNewData;
        // public event Action<object> OnCompleted;

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
            loader = new HtmlLoader(Settings);
        }

        public void Start()
        {
            // isActive = true;
            Worker().GetAwaiter().GetResult();
        }

        // public void Abort()
        // {
        //     isActive = false;
        // }

        private async Task Worker()
        {
            // if (!isActive)
            // {
            //     OnCompleted?.Invoke(this);
            //     return;
            // }

            string? source = await loader.GetSourcePageAsync();
            var domParser = new HtmlParser();

            var document = await domParser.ParseDocumentAsync(source);

            var result = parser.Parse(document);

            OnNewData?.Invoke(this, result);

            //OnCompleted?.Invoke(this);
            // isActive = false;
        }
    }
}
