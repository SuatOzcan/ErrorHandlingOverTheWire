using System;
using ErrorHandling;
using System.Net;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

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

                // Console.WriteLine(ex.Status);
                // Console.WriteLine(ex.Message);
                ProcessException(ex);
            }

            void ProcessException(WebException exception)
            {
                switch (exception.Status)
                {
                    case WebExceptionStatus.ConnectFailure:
                    case WebExceptionStatus.ConnectionClosed:
                    case WebExceptionStatus.RequestCanceled:
                    case WebExceptionStatus.PipelineFailure:
                    case WebExceptionStatus.SendFailure:
                    case WebExceptionStatus.KeepAliveFailure:
                    case WebExceptionStatus.Timeout:
                         Console.WriteLine("We should retry connection attempts");
                         break;
                    case WebExceptionStatus.NameResolutionFailure:
                    case WebExceptionStatus.ProxyNameResolutionFailure:
                    case WebExceptionStatus.ServerProtocolViolation:
                    case WebExceptionStatus.ProtocolError:
                         Console.WriteLine("Prevent further attempts and notify consumers to check URL configurations");
                         break;
                    case WebExceptionStatus.SecureChannelFailure:
                    case WebExceptionStatus.TrustFailure: 
                         Console.WriteLine("Authentication or security issue. Prompt for credentials and perhaps try again");
                         break;
                    default: 
                         Console.WriteLine("We don't know how to handle this. We should postthe error message and terminate our current workflow.");
                         break;
                }
            }
        }
    }    
}
