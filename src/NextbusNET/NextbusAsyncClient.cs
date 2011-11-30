using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NextbusNET.Model;
using NextbusNET.Properties;
using RestSharp;
using log4net;

namespace NextbusNET
{
    public class NextbusAsyncClient : INextbusAsyncClient
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NextbusAsyncClient));

        private readonly IRestClient _client;

        private readonly Parser _parser = new Parser();

        private readonly RequestFactory _factory = new RequestFactory();

        public NextbusAsyncClient() : this(new RestClient())
        {
        }

        public NextbusAsyncClient(IRestClient client)
        {
            Log.Debug("Initializing async client");

            _client = client;
            client.BaseUrl = Resources.BaseUrl;

            Log.Debug(string.Format("BaseUrl: {0}", client.BaseUrl));
        }

        public Task<VehicleList> GetVehicles(string agency, string route, int epoch)
        {
            var request = _factory.CreateVehiclesRequest(agency, route, epoch);
            Task<VehicleList> task = ExecuteRequest(request).ContinueWith(x => _parser.ParseVehicle(x.Result.Content));
            return task;
        }

        public Task<IEnumerable<Agency>> GetAgencies()
        {
            var request = _factory.CreateAgenciesRequest();
            Task<IEnumerable<Agency>> agencies = ExecuteRequest(request).ContinueWith(x => _parser.ParseAgencies(x.Result.Content));
            return agencies;
        }

        public Task<IEnumerable<Route>> GetRoutes(string agencyTag)
        {
            var request = _factory.CreateRoutesRequest(agencyTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParseRoute(x.Result.Content));
        }

        public Task<RouteConfig> GetRouteConfig(string agencyTag, string routeTag)
        {
            var request = _factory.CreateRouteConfigRequest(agencyTag, routeTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParseRouteConfig(x.Result.Content));
        }

        public Task<List<Prediction>> GetPredictions(string agencyTag, int stopId, string routeTag = null)
        {
            var request = _factory.CreatePredictionsRequest(agencyTag, stopId, routeTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParsePrediction(x.Result.Content));
        }

        public Task<List<Prediction>> GetPredictions(string agencyTag, string stopTag, string routeTag)
        {
            var request = _factory.CreatePredictionsRequest(agencyTag, stopTag, routeTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParsePrediction(x.Result.Content));
        }

        public Task<List<RouteSchedule>> GetSchedule(string agencyTag, string routeTag)
        {
            var request = _factory.CreateScheduleRequest(agencyTag, routeTag);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParseSchedule(x.Result.Content));
        }

        public Task<List<Prediction>> GetPredictionsForMultiStops(string agencyTag, params string[] routeTags)
        {
            var request = _factory.CreatePredictionsForMultiStopsRequest(agencyTag, routeTags);
            return ExecuteRequest(request).ContinueWith(x => _parser.ParsePrediction(x.Result.Content));
        }

        private Task<IRestResponse> ExecuteRequest(IRestRequest request)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var tcs = new TaskCompletionSource<IRestResponse>();
            _client.ExecuteAsync(request, (response, handle) => tcs.SetResult(response));
            var task = tcs.Task.ContinueWith(x =>
            {
                stopwatch.Stop();
                var response = x.Result;
                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    throw new NextbusException(response.ErrorMessage, response.ErrorException);
                }
                Log.Debug(string.Format("uri: {0} / time: {1} ms / length: {2}", response.ResponseUri, stopwatch.ElapsedMilliseconds, response.RawBytes.Length));
                return response;
            });

            return task;
        }
    }
}