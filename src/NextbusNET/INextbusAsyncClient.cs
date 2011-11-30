using System.Collections.Generic;
using System.Threading.Tasks;
using NextbusNET.Model;

namespace NextbusNET
{
    public interface INextbusAsyncClient
    {
        Task<VehicleList> GetVehicles(string agency, string route, int epoch);
        Task<IEnumerable<Agency>> GetAgencies();
        Task<IEnumerable<Route>> GetRoutes(string agencyTag);
        Task<RouteConfig> GetRouteConfig(string agencyTag, string routeTag);
        Task<List<Prediction>> GetPredictions(string agencyTag, int stopId, string routeTag = null);
        Task<List<Prediction>> GetPredictions(string agencyTag, string stopTag, string routeTag);
        Task<List<RouteSchedule>> GetSchedule(string agencyTag, string routeTag);
        Task<List<Prediction>> GetPredictionsForMultiStops(string agencyTag, params string[] routeTags);
    }
}