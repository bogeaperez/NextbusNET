using System.Collections.Generic;
using NextbusNET.Model;

namespace NextbusNET
{
    /// <summary>
    /// 
    /// </summary>
    public interface INextbusClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agency"></param>
        /// <param name="route"></param>
        /// <param name="epoch"></param>
        /// <returns></returns>
        VehicleList GetVehicles(string agency, string route, int epoch);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Agency> GetAgencies();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <returns></returns>
        IEnumerable<Route> GetRoutes(string agencyTag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        RouteConfig GetRouteConfig(string agencyTag, string routeTag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="stopId"></param>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        IEnumerable<Prediction> GetPredictions(string agencyTag, int stopId, string routeTag = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="stopTag"></param>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        IEnumerable<Prediction> GetPredictions(string agencyTag, string stopTag, string routeTag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        IEnumerable<RouteSchedule> GetSchedule(string agencyTag, string routeTag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyTag"></param>
        /// <param name="routeTags"></param>
        /// <returns></returns>
        IEnumerable<Prediction> GetPredictionsForMultiStops(string agencyTag, params string[] routeTags);
    }
}