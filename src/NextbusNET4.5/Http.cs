using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NextbusNET
{
    class Http : IHttp
    {
        public async Task<string> Execute(Request request)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string responseBody = await client.GetStringAsync(request.ToString());
                    return responseBody;
                }
            }
            catch (Exception e)
            {
                throw new NextbusException("Error", e);
            }
        }
    }
}