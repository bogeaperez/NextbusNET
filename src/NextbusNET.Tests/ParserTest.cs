using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NextbusNET.Model;

namespace NextbusNET.Tests
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void ParseRouteConfig_should_return_valid_RouteConfig_object()
        {
            var parser = new Parser();
            var text = File.ReadAllText("xml\\routeConfig63.xml");
            RouteConfig route = parser.ParseRouteConfig(text);

            Assert.AreEqual("63", route.Tag);
            Assert.AreEqual("63-Ossington", route.Title);
            Assert.AreEqual("ff0000", route.Color);
            Assert.AreEqual("ffffff", route.OppositeColor);
            Assert.AreEqual(43.6383699, route.LatMin);
            Assert.AreEqual(43.6994299, route.LatMax);
            Assert.AreEqual(-79.41103, route.LonMax);
            Assert.AreEqual(-79.4427, route.LonMin);

            Assert.AreEqual(74, route.Stops.Count);

            var stop = route.Stops.First();

            Assert.AreEqual("14197", stop.Tag);
            Assert.AreEqual("Eglinton West Station", stop.Title);
            Assert.AreEqual(43.6994299, stop.Lat);
            Assert.AreEqual(-79.43649, stop.Lon);
            Assert.AreEqual(14676, stop.StopId);

            Assert.AreEqual(2, route.Directions.Count);

            var direction = route.Directions.First();

            Assert.AreEqual("63_1_63Sun", direction.Tag);
            Assert.AreEqual("North - 63 Ossington towards Eglinton West Station", direction.Title);
            Assert.AreEqual("North", direction.Name);
            Assert.AreEqual(true, direction.UserForUI);

            Assert.AreEqual(36, direction.Stops.Count);

            Assert.AreEqual(24, route.Paths.Count);

            var path = route.Paths.First();
            Assert.AreEqual(7, path.Points.Count());

            var point = path.Points.First();
            Assert.AreEqual(43.66064, point.Lat);
            Assert.AreEqual(-79.42516, point.Lon);
        }

        [Test]
        public void ParseRouteConfig_should_throw_exception_if_error_message_is_returned_from_nextbus()
        {
            var parser = new Parser();
            string text = File.ReadAllText("xml\\error.xml");
            Assert.Throws<NextbusException>(() => parser.ParseRouteConfig(text));
        }

        [Test]
        public void ParsePrediction_should_return_valid_Prediction_list_object()
        {
            var parser = new Parser();
            var text = File.ReadAllText("xml\\predictions63_2115.xml");
            List<Prediction> predictions = parser.ParsePrediction(text);
            Assert.AreEqual(6, predictions.Count);
        }

        [Test]
        public void ParsePrediction_should_throw_exception_if_error_message_is_returned_from_nextbus()
        {
            var parser = new Parser();
            string text = File.ReadAllText("xml\\error.xml");
            Assert.Throws<NextbusException>(() => parser.ParsePrediction(text));
        }

        [Test]
        public void ParseSchedule_should_return_valid_RouteSchedule_list_object()
        {
            var parser = new Parser();
            var text = File.ReadAllText("xml\\schedule63.xml");
            List<RouteSchedule> schedules = parser.ParseSchedule(text);

            Assert.AreEqual(8, schedules.Count);
            
            RouteSchedule schedule = schedules.First();
            Assert.AreEqual("63", schedule.Tag);
            Assert.AreEqual("63-Ossington", schedule.Title);
            Assert.AreEqual("ITP2011Oct2", schedule.ScheduleClass);
            Assert.AreEqual("wkd", schedule.ServiceClass);
            Assert.AreEqual("North", schedule.Direction);

            StopSchedule first = schedule.Stops.First();
            Assert.AreEqual("14051", first.Tag);
            Assert.AreEqual(19020000, first.EpochTime);
            Assert.AreEqual("63_71_10", first.BlockId);

            StopSchedule last = schedule.Stops.Last();
            Assert.AreEqual("14197_ar", last.Tag);
            Assert.AreEqual(-1, last.EpochTime);
            Assert.AreEqual("63_99_70", last.BlockId);
        }

        [Test]
        public void ParseSchedule_should_throw_exception_if_error_message_is_returned_from_nextbus()
        {
            var parser = new Parser();
            string text = File.ReadAllText("xml\\error.xml");
            Assert.Throws<NextbusException>(() => parser.ParseSchedule(text));
        }

        [Test]
        public void ParseAgencies_should_return_valid_Agency_list_object()
        {
            var parser = new Parser();
            string text = File.ReadAllText("xml\\agencies.xml");
            IEnumerable<Agency> agencies = parser.ParseAgencies(text);

            Assert.AreEqual(31, agencies.Count());
            
            Agency agency = agencies.First();
            Assert.AreEqual("actransit", agency.Tag);
            Assert.AreEqual("AC Transit", agency.Title);
            Assert.AreEqual("California-Northern", agency.RegionTitle);
        }

        [Test]
        public void ParseAgencies_should_throw_exception_if_error_message_is_returned_from_nextbus()
        {
            var parser = new Parser();
            string text = File.ReadAllText("xml\\error.xml");
            Assert.Throws<NextbusException>(() => parser.ParseAgencies(text));
        }

        [Test]
        public void ParseRoute_should_return_valid_Route_list_object()
        {
            string text = File.ReadAllText("xml\\routes.xml");
            var parser = new Parser();
            IEnumerable<Route> routes = parser.ParseRoute(text);

            Assert.AreEqual(177, routes.Count());
            Route route = routes.First();
            Assert.AreEqual("1S", route.Tag);
            Assert.AreEqual("1S-Yonge Subway Replacement Shuttle", route.Title);
        }

        [Test]
        public void ParseRoute_should_throw_exception_if_error_message_is_returned_from_nextbus()
        {
            var parser = new Parser();
            string text = File.ReadAllText("xml\\error.xml");
            Assert.Throws<NextbusException>(() => parser.ParseRoute(text));
        }

        [Test]
        public void Test()
        {
            var nextbus = new NextbusClient();
            var route = nextbus.GetRouteConfig("ttc", "111");
            //nextbus.GetPredictions("ttc", 5905);
            //nextbus.GetPredictions("ttc", "14052", "63");
        }
    }
}