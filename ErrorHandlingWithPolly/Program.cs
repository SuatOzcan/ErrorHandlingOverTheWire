using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace ErrorHandlingWithPolly
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteRemoteLookup();
        }

        private List<WebExceptionStatus> connectionFailurew = new List<WebExceptionStatus>
            {
                WebExceptionStatus.ConnectFailure,
                WebExceptionStatus.ConnectionClosed,
                WebExceptionStatus.RequestCanceled,
                WebExceptionStatus.PipelineFailure,
                WebExceptionStatus.SendFailure,
                WebExceptionStatus.KeepAliveFailure,
                WebExceptionStatus.Timeout
            };
        private List<WebExceptionStatus> resourceAccessFailure = new List<WebExceptionStatus>() {
                WebExceptionStatus.NameResolutionFailure,
                WebExceptionStatus.ProxyNameResolutionFailure,
                WebExceptionStatus.ServerProtocolViolation
            };
        private List<WebExceptionStatus> securityFailure = new List<WebExceptionStatus>
        {
            WebExceptionStatus.SecureChannelFailure,
            WebExceptionStatus.TrustFailure
        };
        
        static void ExecuteRemoteLookUpWitPolly()
        {
             
              /*  Policy connFailurePolicy = Policy.Handle<WebException>(x => connectionFailure.Contains(x.Status))
                .RetryForever();
                HttpResponseMessage resp = connFailurePolicy.Execute(() =>
               ExecuteRemoteLookup());
            if (resp.IsSuccessStatusCode)
            {
                Console.WriteLine("Success!");
            }           */
        }

        private static HttpResponseMessage ExecuteRemoteLookup()
        {
            if (new Random().Next() % 2 == 0)
            {
                Console.WriteLine("Retrying connections...");
                throw new WebException("Connection Failure",
               WebExceptionStatus.ConnectFailure);
            }
            return new HttpResponseMessage();
        }
    }
}

