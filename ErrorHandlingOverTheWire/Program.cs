using System;
using ErrorHandling;
using System.Net;
using System.IO;

namespace ErrorHandlingOverTheWire
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            ResultObject result = new ResultObject();
            WebRequest request = WebRequest.Create("http://test-domain.com");
            request.Method = "POST";
            Stream reqStream = request.GetRequestStream();
            using (StreamWriter sw = new StreamWriter(reqStream))
            {
                sw.Write("Our test data query");
            }
            var responseTask = request.GetResponseAsync();
           
            try
            {

                var webResponse = await responseTask;
                using (StreamReader sr = new
               StreamReader(webResponse.GetResponseStream()))
                {
                    result.RequestResult = await sr.ReadToEndAsync();
                }
            }
            catch (WebException ex)
            {

                Console.WriteLine(ex.Status);
                Console.WriteLine(ex.Message);
            }

           
        }
    }    
}
