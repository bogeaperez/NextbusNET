using System.Collections.Generic;

namespace NextbusNET.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OppositeColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal LatMin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal LatMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal LonMin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal LonMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Stop> Stops { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Direction> Directions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Path> Paths { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(RouteConfig left, RouteConfig right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
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