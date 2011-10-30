namespace NextbusNET.Model
{
    public class Stop
    {
        public string Tag { get; set; }

        public string Title { get; set; }

        public decimal Lat { get; set; }

        public decimal Lon { get; set; }

        public int StopId { get; set; }

        public bool Equals(Stop other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Tag, Tag);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Stop)) return false;
            return Equals((Stop) obj);
        }

        public override int GetHashCode()
        {
            return (Tag != null ? Tag.GetHashCode() : 0);
        }

        public static bool operator ==(Stop left, Stop right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Stop left, Stop right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Tag: {0}, Title: {1}, Lat: {2}, Lon: {3}, StopId: {4}", Tag, Title, Lat, Lon, StopId);
        }
    }
}