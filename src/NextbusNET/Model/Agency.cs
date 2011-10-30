namespace NextbusNET.Model
{
    public class Agency
    {
        public string Tag { get; internal set; }

        public string Title { get; internal set; }

        public string RegionTitle { get; internal set; }

        public bool Equals(Agency other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Tag, Tag);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Agency)) return false;
            return Equals((Agency) obj);
        }

        public override int GetHashCode()
        {
            return (Tag != null ? Tag.GetHashCode() : 0);
        }

        public static bool operator ==(Agency left, Agency right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Agency left, Agency right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Tag: {0}, Title: {1}, RegionTitle: {2}", Tag, Title, RegionTitle);
        }
    }
}