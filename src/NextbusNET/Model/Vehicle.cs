namespace NextbusNET.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RouteTag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DirTag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Lat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Lon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SecsSinceReport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Predictable { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
            return string.Format("Id: {0}, RouteTag: {1}, SecsSinceReport: {2}, DirTag: {3}", Id, RouteTag,
                                 SecsSinceReport, DirTag);
        }
    }
}