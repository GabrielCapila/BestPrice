using BestPrice.Interface;
using FireSharp.Config;
using FireSharp.Interfaces;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

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


        //public void SendHtml()
        //{
        //    string html = @"<body><div>" +
        //                  "  <h1 class\"price\">This is heading 1</h1>" +
        //                  "</div>" +
        //                  "<div>" +
        //                  "  <h1 class\"price\">This is heading 2</h1>" +
        //                  "</div>" +
        //                  "<div>" +
        //                  "  <h1 class\"price\">This is heading 3</h1>" +
        //                  "</div>" +
        //                  "<div>" +
        //                  "  <h1 class\"price\">This is heading 4</h1>" +
        //                  "</div>" +
        //                  "<div>" +
        //                  "  <h1 class\"price\">This is heading 5</h1>" +
        //                  "</div>" +
        //                  "<body>";

        //    HtmlDocument htmlDoc = new HtmlDocument();
        //    htmlDoc.LoadHtml(html);

        //    var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//body/h1");

        //    foreach (var node in htmlNodes)
        //    {

        //        Console.WriteLine(node.InnerHtml);
        //    }
        //}
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
        [HttpPost]
        [Route("SaveHTML")]
        public void SaveHTML(Product product)
        {
            string authSecret = "15EXhVdHKfsRkijFl8rOX8JQyECBU21820hfgZwO";
            string basePath = "https://bestprice-12a8d-default-rtdb.firebaseio.com";
            string senderAppName = "BestPriceApp";


            IFirebaseClient client;
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath
            };

            client = new FireSharp.FirebaseClient(config);
            if (client != null && !string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(authSecret))
            {
                var data = new
                {
                    //Name=a,
                    //Price=b
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                };
                 client.Push("doc/", data);
            }
            
        }



    }
    //[HttpPost]
    //[Route("SendHTML")]
    //public string SendHtml(string html, string path)
    //{
    //    HtmlWeb web = new HtmlWeb();
    //    var retorno = string.Empty;
    //    var doc = web.Load(html);

    //    var nodes = doc.DocumentNode.SelectNodes($"{path}[position()>1]");

    //    foreach (var node in nodes)
    //    {
    //        retorno = node.SelectSingleNode("a[2]").InnerText;
    //    }


    //    if (retorno != null)

    //        return retorno;
    //    else
    //        return "did not work";
    //}

}