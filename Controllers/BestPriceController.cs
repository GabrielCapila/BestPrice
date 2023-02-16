using BestPrice.Interface;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace BestPrice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestPriceController : ControllerBase
    {

        private readonly ILogger<BestPriceController> _logger;
        //private readonly IManageHTMLService _manageHTMLService;

        public BestPriceController(ILogger<BestPriceController> logger)
        {
            _logger = logger;
            //_manageHTMLService = manageHTMLService;
        }
        [HttpPost]
        [Route("SendHTML")]

        public void SendHtml()
        {
            string html = @"<body><div>" +
                          "  <h1 class\"price\">This is heading 1</h1>" +
                          "</div>" +
                          "<div>" +
                          "  <h1 class\"price\">This is heading 2</h1>" +
                          "</div>" +
                          "<div>" +
                          "  <h1 class\"price\">This is heading 3</h1>" +
                          "</div>" +
                          "<div>" +
                          "  <h1 class\"price\">This is heading 4</h1>" +
                          "</div>" +
                          "<div>" +
                          "  <h1 class\"price\">This is heading 5</h1>" +
                          "</div>" +
                          "<body>";

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//body/h1");

            foreach (var node in htmlNodes)
            {

                Console.WriteLine(node.InnerHtml);
            }
        }
        [HttpGet]
        [Route("GetHTML")]

        public string GetHtml(string html)
        {
            html = @$"{html}";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//body");
            string retorno = string.Empty;
            foreach (var node in htmlNodes)
            {

                retorno = node.InnerHtml;

            }
            if (retorno != null)
            {
                return retorno;
            }
            else
            {
                return "Did not work!";
            }


        }
        //public string SendHtml(string html, string strStart, string strEnd)
        //{

        //    if (html.Contains(strStart) && html.Contains(strEnd))
        //    {

        //        int Start, End;
        //        string firstMoment = string.Empty;
        //        for (int i = 0; i < html.Length; i++)
        //        {
        //            Start = html.IndexOf(strStart, i) + strStart.Length;
        //            End = html.IndexOf(strEnd, Start);
        //            firstMoment = html.Substring(Start, End - Start);
        //        }

        //        return firstMoment;



        //        //Start = html.IndexOf(strStart, End) + strStart.Length;
        //        //End = html.IndexOf(strEnd, Start);

        //        //string secondMoment = html.Substring(Start, End - Start);

        //        // return html.Substring(Start, End - Start);
        //        //firstMoment += html.Substring(End);
        //    }
        //    else
        //    {
        //        return "there is no word";
        //    }
        //}
        //public string SendHtml(string html, string strStart, string strEnd)
        //{

        //    if (html.Contains(strStart) && html.Contains(strEnd))
        //    {
        //        int Start, End;
        //        Start = html.IndexOf(strStart, 0) + strStart.Length;
        //        End = html.IndexOf(strEnd, Start);
        //        return html.Substring(Start, End - Start);
        //    }
        //    else
        //    {
        //        return "there is no word";
        //    }
        //}
    }
}