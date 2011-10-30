using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NextbusNET.Model;
using RestSharp;
using System.Xml.Linq;

namespace NextbusNET
{
    public class NextbusClient
    {
        private readonly IRestClient _client;

        private readonly Parser _parser = new Parser();

        public NextbusClient() : this(new RestClient())
        {
        }

        public NextbusClient(IRestClient client)
        {
            _client = client;
            client.BaseUrl = "http://webservices.nextbus.com/service/publicXMLFeed";
        }

        public VehicleList GetVehicles(string agency, string route, int epoch)
        {
            var request = new RestRequest();
            request.AddParameter("command", "vehicleLocations");
            request.AddParameter("a", agency);
            request.AddParameter("r", route);
            request.AddParameter("t", epoch);
            RestResponse response = _client.Execute(request);
            VehicleList vehicles = _parser.ParseVehicle(response.Content);
            return vehicles;
        }

        public IEnumerable<Agency> GetAgencies()
        {
            var request = new RestRequest();
            request.AddParameter("command", "agencyList");
            var response = _client.Execute(request);
            IEnumerable<Agency> agencies = _parser.ParseAgencies(response.Content);
            return agencies;
        }

        public IEnumerable<Route> GetRoutes(string agencyTag)
        {
            var request = new RestRequest();
            request.AddParameter("command", "routeList");
            request.AddParameter("a", agencyTag);
            var response = _client.Execute(request);
            return _parser.ParseRoute(response.Content);
        }

        public RouteConfig GetRouteConfig(string agencyTag, string routeTag)
        {
            var request = new RestRequest();
            request.AddParameter("command", "routeConfig");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            RestResponse response = _client.Execute(request);
            var route = _parser.ParseRouteConfig(response.Content);
            return route;
        }

        public IEnumerable<Prediction> GetPredictions(string agencyTag, int stopId, string routeTag = null)
        {
            var request = new RestRequest();
            request.AddParameter("command", "predictions");
            request.AddParameter("a", agencyTag);
            request.AddParameter("stopId", stopId);
            if (routeTag != null)
            {
                request.AddParameter("routeTag", routeTag);
            }
            var response = _client.Execute(request);
            var predictions = _parser.ParsePrediction(response.Content);
            return predictions;
        }

        public IEnumerable<Prediction> GetPredictions(string agencyTag, string stopTag, string routeTag)
        {
            var request = new RestRequest();
            request.AddParameter("command", "predictions");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            request.AddParameter("s", stopTag);
            var response = _client.Execute(request);
            var predictions = _parser.ParsePrediction(response.Content);
            return predictions;
        }

        public IEnumerable<RouteSchedule> GetSchedule(string agencyTag, string routeTag)
        {
            var request = new RestRequest();
            request.AddParameter("command", "schedule");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            var response = _client.Execute(request);
            List<RouteSchedule> routeSchedules = _parser.ParseSchedule(response.Content);
            return routeSchedules;
        }

        public IEnumerable<Prediction> GetPredictionsForMultiStops(string agencyTag, params string[] routeTags)
        {
            var request = new RestRequest();
            request.AddParameter("command", "predictionsForMultiStops");
            request.AddParameter("a", agencyTag);
            foreach (var routeTag in routeTags)
            {
                request.AddParameter("stops", routeTag);                
            }
            var response = _client.Execute(request);
            List<Prediction> predictions = _parser.ParsePrediction(response.Content);
            return predictions;
        }

    }
}
