using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NextbusNET
{
    class Http : IHttp
    {
        public Task<string> Execute(Request request)
        {
            try
            {

                var webrequest = WebRequest.Create(request.ToString());
                var task = Task.Factory.FromAsync<WebResponse>(webrequest.BeginGetResponse, webrequest.EndGetResponse,
                                                               null);
                return task.ContinueWith(x =>
                    {
                        using (var reader = new StreamReader(x.Result.GetResponseStream()))
                        {
                            return reader.ReadToEnd().Trim();
                        }
                    });

            }
            catch (Exception e)
            {
                throw new NextbusException("Error", e);
            }
        }
    }
}