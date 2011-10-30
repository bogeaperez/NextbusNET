namespace NextbusNET.Model
{
    public class Vehicle
    {
        public string Id { get; set; }

        public string RouteTag { get; set; }

        public string DirTag { get; set; }

        public decimal Lat { get; set; }

        public decimal Lon { get; set; }

        public int SecsSinceReport { get; set; }

        public bool Predictable { get; set; }

        public string Heading { get; set; }

        public bool Equals(Vehicle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Vehicle)) return false;
            return Equals((Vehicle) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public static bool operator ==(Vehicle left, Vehicle right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vehicle left, Vehicle right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, RouteTag: {1}, SecsSinceReport: {2}, DirTag: {3}", Id, RouteTag, SecsSinceReport, DirTag);
        }
    }
}