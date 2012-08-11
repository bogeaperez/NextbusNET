using NextbusNET.Model;

namespace NextbusNET
{
    internal class RequestFactory
    {
        internal Request CreateVehiclesRequest(string agency, string route, int epoch)
        {
            var request = new Request();
            request.AddParameter("command", "vehicleLocations");
            request.AddParameter("a", agency);
            request.AddParameter("r", route);
            request.AddParameter("t", epoch);
            return request;
        }

        internal Request CreateAgenciesRequest()
        {
            var request = new Request();
            request.AddParameter("command", "agencyList");
            return request;
        }

        internal Request CreateRoutesRequest(string agencyTag)
        {
            var request = new Request();
            request.AddParameter("command", "routeList");
            request.AddParameter("a", agencyTag);
            return request;
        }

        internal Request CreateRouteConfigRequest(string agencyTag, string routeTag)
        {
            var request = new Request();
            request.AddParameter("command", "routeConfig");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            return request;
        }

        internal Request CreatePredictionsRequest(string agencyTag, int stopId, string routeTag = null)
        {
            var request = new Request();
            request.AddParameter("command", "predictions");
            request.AddParameter("a", agencyTag);
            request.AddParameter("stopId", stopId);
            if (routeTag != null)
            {
                request.AddParameter("routeTag", routeTag);
            }
            return request;
        }

        internal Request CreatePredictionsRequest(string agencyTag, string stopTag, string routeTag)
        {
            var request = new Request();
            request.AddParameter("command", "predictions");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            request.AddParameter("s", stopTag);
            return request;
        }

        internal Request CreateScheduleRequest(string agencyTag, string routeTag)
        {
            var request = new Request();
            request.AddParameter("command", "schedule");
            request.AddParameter("a", agencyTag);
            request.AddParameter("r", routeTag);
            return request;
        }

        internal Request CreatePredictionsForMultiStopsRequest(string agencyTag, params string[] routeTags)
        {
            var request = new Request();
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