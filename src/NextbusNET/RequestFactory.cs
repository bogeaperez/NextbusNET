using System.Collections.Generic;
using NextbusNET.Model;
using RestSharp;

namespace NextbusNET
{
    internal class RequestFactory
    {
        internal RestRequest CreateVehiclesRequest(string agency, string route, int epoch)
        {
            var request = new RestRequest();
            request.AddParameter("command", "vehicleLocations");
            request.AddParameter("a", agency);
            request.AddParameter("r", route);
            request.AddParameter("t", epoch);
            return request;
        }

        internal RestRequest CreateAgenciesRequest()
        {
            var request = new RestRequest();
            request.AddParameter("command", "agencyList");
            return request;
        }

        internal RestRequest CreateRoutesRequest(string agencyTag)
        {
            var request = new RestRequest();
            request.AddParameter("command", "routeList");
            request.AddParameter("a", agencyTag);
            return request;
        }

        internal RestRequest CreateRouteConfigRequest(string agencyTag, string routeTag)
        {
            var request = new RestRequest();
            request.AddParameter("command", "routeConfig");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            return request;
        }

        internal RestRequest CreatePredictionsRequest(string agencyTag, int stopId, string routeTag = null)
        {
            var request = new RestRequest();
            request.AddParameter("command", "predictions");
            request.AddParameter("a", agencyTag);
            request.AddParameter("stopId", stopId);
            if (routeTag != null)
            {
                request.AddParameter("routeTag", routeTag);
            }
            return request;
        }

        internal RestRequest CreatePredictionsRequest(string agencyTag, string stopTag, string routeTag)
        {
            var request = new RestRequest();
            request.AddParameter("command", "predictions");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            request.AddParameter("s", stopTag);
            return request;
        }

        internal RestRequest CreateScheduleRequest(string agencyTag, string routeTag)
        {
            var request = new RestRequest();
            request.AddParameter("command", "schedule");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            return request;
        }

        internal RestRequest CreatePredictionsForMultiStopsRequest(string agencyTag, params string[] routeTags)
        {
            var request = new RestRequest();
            request.AddParameter("command", "predictionsForMultiStops");
            request.AddParameter("a", agencyTag);
            foreach (var routeTag in routeTags)
            {
                request.AddParameter("stops", routeTag);
            }
            return request;
        } 
    }
}