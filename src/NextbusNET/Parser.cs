using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NextbusNET.Model;

namespace NextbusNET
{
    internal class Parser
    {
        public RouteConfig ParseRouteConfig(string xml)
        {
            XDocument document = XDocument.Parse(xml);

            XElement routeElement = document.Root.Element("route");

            if (routeElement == null)
            {
                return null;
            }

            var route = new RouteConfig
                            {
                                Tag = routeElement.Attr("tag"),
                                Title = routeElement.Attr("title"),
                                Color = routeElement.Attr("color"),
                                OppositeColor = routeElement.Attr("oppositeColor"),
                                LatMin = routeElement.Attr("latMin").ToDecimal(),
                                LatMax = routeElement.Attr("latMax").ToDecimal(),
                                LonMin = routeElement.Attr("lonMin").ToDecimal(),
                                LonMax = routeElement.Attr("lonMax").ToDecimal()
                            };

            route.Stops = routeElement.Elements("stop").Select(x => new Stop
                                                                        {
                                                                            Tag = x.Attr("tag"),
                                                                            Title = x.Attr("title"),
                                                                            Lat = x.Attr("lat").ToDecimal(),
                                                                            Lon = x.Attr("lon").ToDecimal(),
                                                                            StopId = x.Attr("stopId").ToInt()
                                                                        }).ToList();

            IEnumerable<Direction> directions = from x in routeElement.Elements("direction")
                                                select new Direction
                                                           {
                                                               Tag = x.Attr("tag"),
                                                               Title = x.Attr("title"),
                                                               Name = x.Attr("name"),
                                                               UserForUI = x.Attr("useForUI").ToBool(),
                                                               Stops = (from y in route.Stops
                                                                        where
                                                                            x.Elements("stop").Select(u => u.Attr("tag"))
                                                                            .Contains(y.Tag)
                                                                        select y).ToList()
                                                           };

            route.Directions = directions.ToList();

            List<Path> paths = routeElement.Elements("path").Select(x => new Path
                                                                             {
                                                                                 Points =
                                                                                     x.Elements("point").Select(
                                                                                         y => new Point
                                                                                                  {
                                                                                                      Lat = y.Attr("lat").ToDecimal(),
                                                                                                      Lon = y.Attr("lon").ToDecimal()
                                                                                                  }).ToList()
                                                                             }).ToList();

            route.Paths = paths;

            return route;
        }

        public List<Prediction> ParsePrediction(string xml)
        {
            XDocument document = XDocument.Parse(xml);

            if (document.Root == null)
            {
                return new List<Prediction>();
            }
            List<Prediction> predictions = (from x in document.Root.Descendants("prediction")
                                            select new Prediction
                                                       {
                                                           EpochTime = x.Attr("epochTime").ToInt(),
                                                           Seconds = x.Attr("seconds").ToInt(),
                                                           Minutes = x.Attr("minutes").ToInt(),
                                                           IsDeparture = x.Attr("isDeparture").ToBool(),
                                                           AffectedByLayover = x.Attr("affectedByLayover").ToBool(),
                                                           DirTag = x.Attr("dirTag"),
                                                           Vehicle = x.Attr("vehicle"),
                                                           Block = x.Attr("block"),
                                                           TripTag = x.Attr("tripTag")
                                                       }).ToList();

            return predictions;
        }

        public List<RouteSchedule> ParseSchedule(string xml)
        {
            XDocument document = XDocument.Parse(xml);
            if (document.Root == null)
            {
                return new List<RouteSchedule>();
            }

            List<RouteSchedule> routes = (from route in document.Root.Elements("route")
                                          select new RouteSchedule
                                                     {
                                                         Tag = route.Attr("tag"),
                                                         Title = route.Attr("title"),
                                                         ScheduleClass = route.Attr("scheduleClass"),
                                                         ServiceClass = route.Attr("serviceClass"),
                                                         Direction = route.Attr("direction"),
                                                         Stops =
                                                             (from stop in route.FirstNode.ElementsAfterSelf().Descendants("stop")
                                                              select new StopSchedule
                                                                         {
                                                                             Tag = stop.Attr("tag"),
                                                                             EpochTime = stop.Attr("epochTime").ToInt(),
                                                                             BlockId = stop.Parent.Attr("blockID")
                                                                         }).ToList()
                                                     }).ToList();
            return routes;
        }

        public VehicleList ParseVehicle(string xml)
        {
            XDocument document = XDocument.Parse(xml);
            if (document.Root == null)
            {
                return new VehicleList();
            }

            List<Vehicle> vehicles = (from x in document.Root.Elements("vehicle")
                                      select new Vehicle
                                                 {
                                                     Id = x.Attr("id"),
                                                     RouteTag = x.Attr("routeTag"),
                                                     DirTag = x.Attr("dirTag"),
                                                     Lat = x.Attr("lat").ToDecimal(),
                                                     Lon = x.Attr("lon").ToDecimal(),
                                                     SecsSinceReport = x.Attr("secsSinceReport").ToInt(),
                                                     Predictable = x.Attr("predictable").ToBool(),
                                                     Heading = x.Attr("heading")
                                                 }).ToList();
            var vehicleList = new VehicleList
                                  {
                                      Vehicles = vehicles,
                                      LastTime = document.Root.Element("lastTime").Value.ToInt()
                                  };
            return vehicleList;
        }

        public IEnumerable<Agency> ParseAgencies(string xml)
        {
            XDocument document = XDocument.Parse(xml);
            if (document.Root == null)
            {
                return new List<Agency>();
            }

            List<Agency> agencies = (from x in document.Root.Elements("agency")
                                     select new Agency
                                                {
                                                    Tag = x.Attr("tag"),
                                                    Title = x.Attr("title"),
                                                    RegionTitle = x.Attr("regionTitle")
                                                }).ToList();

            return agencies;
        }

        public IEnumerable<Route> ParseRoute(string xml)
        {
            XDocument document = XDocument.Parse(xml);
            if (document.Root == null)
            {
                return new List<Route>();
            }

            List<Route> routes = (from x in document.Root.Elements("route")
                                  select new Route
                                             {
                                                 Tag = x.Attr("tag"),
                                                 Title = x.Attr("title")
                                             }).ToList();

            return routes;
        }
    }

    public static class XLinqExtensions
    {
        public static string Attr(this XElement element, string attrId)
        {
            XAttribute attribute = element.Attribute(attrId);
            return attribute == null ? string.Empty : attribute.Value;
        }

        public static decimal ToDecimal(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? 0M : decimal.Parse(value);
        }

        public static int ToInt(this string value)
        {
            int result;
            return int.TryParse(value, out result) ? result : 0;
        }

        public static bool ToBool(this string value)
        {
            bool result;
            return bool.TryParse(value, out result) && result;
        }
    }
}