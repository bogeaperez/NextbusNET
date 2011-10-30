using System.Collections.Generic;

namespace NextbusNET.Model
{
    public class Direction
    {
        public string Tag { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public bool UserForUI { get; set; }

        public IList<Stop> Stops { get; set; }

        public bool Equals(Direction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Tag, Tag);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Direction)) return false;
            return Equals((Direction) obj);
        }

        public override int GetHashCode()
        {
            return (Tag != null ? Tag.GetHashCode() : 0);
        }

        public static bool operator ==(Direction left, Direction right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Direction left, Direction right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Tag: {0}, Title: {1}, Name: {2}, Stops: {3}", Tag, Title, Name, Stops.Count);
        }
    }
}