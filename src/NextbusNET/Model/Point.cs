namespace NextbusNET.Model
{
    public class Point
    {
        public decimal Lat { get; internal set; }

        public decimal Lon { get; internal set; }

        public override string ToString()
        {
            return string.Format("Lat: {0}, Lon: {1}", Lat, Lon);
        }
    }
}