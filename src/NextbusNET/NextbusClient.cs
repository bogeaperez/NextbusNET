using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NextbusNET.Model;
using NextbusNET.Properties;
using System.Xml.Linq;

namespace NextbusNET
{
    public class NextbusClient : INextbusClient
    {
        private readonly Parser _parser = new Parser();

        private readonly RequestFactory _factory = new RequestFactory();

        public VehicleList GetVehicles(string agency, string route, int epoch)
        {
            var request = _factory.CreateVehiclesRequest(agency, route, epoch);
            string response = ExecuteRequest(request);
            VehicleList vehicles = _parser.ParseVehicle(response);
            return vehicles;
        }

        public IEnumerable<Agency> GetAgencies()
        {
            var request = _factory.CreateAgenciesRequest();
            string response = ExecuteRequest(request);
            IEnumerable<Agency> agencies = _parser.ParseAgencies(response);
            return agencies;
        }

        public IEnumerable<Route> GetRoutes(string agencyTag)
        {
            var request = _factory.CreateRoutesRequest(agencyTag);
            string response = ExecuteRequest(request);
            return _parser.ParseRoute(response);
        }

        public RouteConfig GetRouteConfig(string agencyTag, string routeTag)
        {
            var request = _factory.CreateRouteConfigRequest(agencyTag, routeTag);
            string response = ExecuteRequest(request);
            var route = _parser.ParseRouteConfig(response);
            return route;
        }

        public IEnumerable<Prediction> GetPredictions(string agencyTag, int stopId, string routeTag = null)
        {
            var request = _factory.CreatePredictionsRequest(agencyTag, stopId, routeTag);
            string response = ExecuteRequest(request);
            var predictions = _parser.ParsePrediction(response);
            return predictions;
        }

        public IEnumerable<Prediction> GetPredictions(string agencyTag, string stopTag, string routeTag)
        {
            var request = _factory.CreatePredictionsRequest(agencyTag, stopTag, routeTag);
            string response = ExecuteRequest(request);
            var predictions = _parser.ParsePrediction(response);
            return predictions;
        }

        public IEnumerable<RouteSchedule> GetSchedule(string agencyTag, string routeTag)
        {
            var request = _factory.CreateScheduleRequest(agencyTag, routeTag);
            string response = ExecuteRequest(request);
            List<RouteSchedule> routeSchedules = _parser.ParseSchedule(response);
            return routeSchedules;
        }

        public IEnumerable<Prediction> GetPredictionsForMultiStops(string agencyTag, params string[] routeTags)
        {
            var request = _factory.CreatePredictionsForMultiStopsRequest(agencyTag, routeTags);
            string response = ExecuteRequest(request);
            List<Prediction> predictions = _parser.ParsePrediction(response);
            return predictions;
        }

        private string ExecuteRequest(Request request)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Task<string> responseBody = client.GetStringAsync(request.ToString());
                    return responseBody.Result;
                }
            }
            catch (Exception e)
            {
                throw new NextbusException("Error", e);
            }
        }
    }
}
