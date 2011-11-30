using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NextbusNET.Model;
using NextbusNET.Properties;
using RestSharp;
using System.Xml.Linq;
using log4net;

namespace NextbusNET
{
    public class NextbusClient : INextbusClient
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (NextbusClient));

        private readonly IRestClient _client;

        private readonly Parser _parser = new Parser();

        private readonly RequestFactory _factory = new RequestFactory();

        public NextbusClient() : this(new RestClient())
        {
        }

        public NextbusClient(IRestClient client)
        {
            Log.Debug("Initializing client");

            _client = client;
            client.BaseUrl = Resources.BaseUrl;

            Log.Debug(string.Format("BaseUrl: {0}", client.BaseUrl));
        }

        public VehicleList GetVehicles(string agency, string route, int epoch)
        {
            var request = _factory.CreateVehiclesRequest(agency, route, epoch);
            IRestResponse response = ExecuteRequest(request);
            VehicleList vehicles = _parser.ParseVehicle(response.Content);
            return vehicles;
        }

        public IEnumerable<Agency> GetAgencies()
        {
            var request = _factory.CreateAgenciesRequest();
            IRestResponse response = ExecuteRequest(request);
            IEnumerable<Agency> agencies = _parser.ParseAgencies(response.Content);
            return agencies;
        }

        public IEnumerable<Route> GetRoutes(string agencyTag)
        {
            var request = _factory.CreateRoutesRequest(agencyTag);
            IRestResponse response = ExecuteRequest(request);
            return _parser.ParseRoute(response.Content);
        }

        public RouteConfig GetRouteConfig(string agencyTag, string routeTag)
        {
            var request = _factory.CreateRouteConfigRequest(agencyTag, routeTag);
            IRestResponse response = ExecuteRequest(request);
            var route = _parser.ParseRouteConfig(response.Content);
            return route;
        }

        public IEnumerable<Prediction> GetPredictions(string agencyTag, int stopId, string routeTag = null)
        {
            var request = _factory.CreatePredictionsRequest(agencyTag, stopId, routeTag);
            IRestResponse response = ExecuteRequest(request);
            var predictions = _parser.ParsePrediction(response.Content);
            return predictions;
        }

        public IEnumerable<Prediction> GetPredictions(string agencyTag, string stopTag, string routeTag)
        {
            var request = _factory.CreatePredictionsRequest(agencyTag, stopTag, routeTag);
            IRestResponse response = ExecuteRequest(request);
            var predictions = _parser.ParsePrediction(response.Content);
            return predictions;
        }

        public IEnumerable<RouteSchedule> GetSchedule(string agencyTag, string routeTag)
        {
            var request = _factory.CreateScheduleRequest(agencyTag, routeTag);
            IRestResponse response = ExecuteRequest(request);
            List<RouteSchedule> routeSchedules = _parser.ParseSchedule(response.Content);
            return routeSchedules;
        }

        public IEnumerable<Prediction> GetPredictionsForMultiStops(string agencyTag, params string[] routeTags)
        {
            var request = _factory.CreatePredictionsForMultiStopsRequest(agencyTag, routeTags);
            IRestResponse response = ExecuteRequest(request);
            List<Prediction> predictions = _parser.ParsePrediction(response.Content);
            return predictions;
        }

        private IRestResponse ExecuteRequest(IRestRequest request)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            IRestResponse response = _client.Execute(request);
            stopwatch.Stop();
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new NextbusException(response.ErrorMessage, response.ErrorException);
            }
            Log.Debug(string.Format("uri: {0} / time: {1} ms", response.ResponseUri, stopwatch.ElapsedMilliseconds));
            return response;
        }
    }
}
