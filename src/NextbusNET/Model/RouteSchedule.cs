using System.Collections.Generic;

namespace NextbusNET.Model
{
    public class RouteSchedule
    {
        public string Tag { get; set; }

        public string Title { get; set; }

        public string ScheduleClass { get; set; }

        public string ServiceClass { get; set; }

        public string Direction { get; set; }

        public IList<StopSchedule> Stops { get; set; }

        public bool Equals(RouteSchedule other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Tag, Tag);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (RouteSchedule)) return false;
            return Equals((RouteSchedule) obj);
        }

        public override int GetHashCode()
        {
            return (Tag != null ? Tag.GetHashCode() : 0);
        }

        public static bool operator ==(RouteSchedule left, RouteSchedule right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RouteSchedule left, RouteSchedule right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Tag: {0}, ScheduleClass: {1}, ServiceClass: {2}, Direction: {3}, Stops: {4}", Tag, ScheduleClass, ServiceClass, Direction, Stops.Count);
        }
    }
}