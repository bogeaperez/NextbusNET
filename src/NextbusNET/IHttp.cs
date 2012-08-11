using System.Threading.Tasks;

namespace NextbusNET
{
    internal interface IHttp
    {
        Task<string> Execute(Request request);
    }
}