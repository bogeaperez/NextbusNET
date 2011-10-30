using System.Collections.Generic;

namespace NextbusNET.Model
{
    public class RouteConfig
    {
        public string Tag { get; set; }

        public string Title { get; set; }

        public string ShortTitle { get; set; }

        public string Color { get; set; }

        public string OppositeColor { get; set; }

        public decimal LatMin { get; set; }

        public decimal LatMax { get; set; }

        public decimal LonMin { get; set; }

        public decimal LonMax { get; set; }

        public List<Stop> Stops { get; set; }

        public List<Direction> Directions { get; set; }

        public List<Path> Paths { get; set; }

        public bool Equals(RouteConfig other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Tag, Tag);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (RouteConfig)) return false;
            return Equals((RouteConfig) obj);
        }

        public override int GetHashCode()
        {
            return (Tag != null ? Tag.GetHashCode() : 0);
        }

        public static bool operator ==(RouteConfig left, RouteConfig right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RouteConfig left, RouteConfig right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Tag: {0}, Title: {1}", Tag, Title);
        }
    }
}