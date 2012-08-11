using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NextbusNET.Model;
using NextbusNET.Properties;
using System.Net.Http;

namespace NextbusNET
{
    public class NextbusAsyncClient : INextbusAsyncClient
    {
        private readonly Parser _parser = new Parser();

        private readonly RequestFactory _factory = new RequestFactory();

        public Task<VehicleList> GetVehicles(string agency, string route, int epoch)
        {
            var request = _factory.CreateVehiclesRequest(agency, route, epoch);
            Task<VehicleList> task = ExecuteRequest(request).ContinueWith(x => _parser.ParseVehicle(x.Result));
            return task;
        }

        public Task<IEnumerable<Agency>> GetAgencies()
        {
            var request = _factory.CreateAgenciesRequest();
            Task<IEnumerable<Agency>> agencies = ExecuteRequest(request).ContinueWith(x => _parser.ParseAgencies(x.Result));
            return agencies;
        }

        public Task<IEnumerable<Route>> GetRoutes(string agencyTag)
        {
            var request = _factory.CreateRoutesRequest(agencyTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParseRoute(x.Result));
        }

        public Task<RouteConfig> GetRouteConfig(string agencyTag, string routeTag)
        {
            var request = _factory.CreateRouteConfigRequest(agencyTag, routeTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParseRouteConfig(x.Result));
        }

        public Task<List<Prediction>> GetPredictions(string agencyTag, int stopId, string routeTag = null)
        {
            var request = _factory.CreatePredictionsRequest(agencyTag, stopId, routeTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParsePrediction(x.Result));
        }

        public Task<List<Prediction>> GetPredictions(string agencyTag, string stopTag, string routeTag)
        {
            var request = _factory.CreatePredictionsRequest(agencyTag, stopTag, routeTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParsePrediction(x.Result));
        }

        public Task<List<RouteSchedule>> GetSchedule(string agencyTag, string routeTag)
        {
            var request = _factory.CreateScheduleRequest(agencyTag, routeTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParseSchedule(x.Result));
        }

        public Task<List<Prediction>> GetPredictionsForMultiStops(string agencyTag, params string[] routeTags)
        {
            var request = _factory.CreatePredictionsForMultiStopsRequest(agencyTag, routeTags);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParsePrediction(x.Result));
        }

        private async Task<string> ExecuteRequest(Request request)
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