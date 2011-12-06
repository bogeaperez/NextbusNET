using System.Collections.Generic;
using System.Threading.Tasks;
using NextbusNET.Model;

namespace NextbusNET
{
    /// <summary>
    /// Client interaface for calling the Nextbus webservice asynchronously
    /// </summary>
    public interface INextbusAsyncClient
    {
        /// <summary>
        /// Obtains a list of all the agencies.
        /// Represents the "agencyList" command from the webservices.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Agency>> GetAgencies();

        /// <summary>
        /// Obtains a list of all the routes for an agency.
        /// Represents the "routeList" command from the webservices.
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <returns></returns>
        Task<IEnumerable<Route>> GetRoutes(string agencyTag);

        /// <summary>
        /// Obtains the details for a specific route.
        /// Represents the "routeConfig" command from the webservices.
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        Task<RouteConfig> GetRouteConfig(string agencyTag, string routeTag);

        /// <summary>
        /// Obtains the list of predictions associated with a stop.
        /// Represents the "predictions" command from the webservices.
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="stopId"></param>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        Task<List<Prediction>> GetPredictions(string agencyTag, int stopId, string routeTag = null);

        /// <summary>
        /// Obtains the list of predictions associated with a stop.
        /// Represents the "predictions" command from the webservices.
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="stopTag"></param>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        Task<List<Prediction>> GetPredictions(string agencyTag, string stopTag, string routeTag);

        /// <summary>
        /// Obtains the list of predictions for multiple stops.
        /// Represents the "predictionsForMultiStops" command from the webservices.
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="routeTags"></param>
        /// <returns></returns>
        Task<List<Prediction>> GetPredictionsForMultiStops(string agencyTag, params string[] routeTags);

        /// <summary>
        /// Obtains the schedule information for a route.
        /// Represents the "schedule" command from the webservices.
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        Task<List<RouteSchedule>> GetSchedule(string agencyTag, string routeTag);

        /// <summary>
        /// Obtains a list of vehicle locations.
        /// Represents the ""vehicleLocation" command from the webservices.
        /// </summary>
        /// <param name="agency"></param>
        /// <param name="route"></param>
        /// <param name="epoch"></param>
        /// <returns></returns>
        Task<VehicleList> GetVehicles(string agency, string route, int epoch);
    }
}