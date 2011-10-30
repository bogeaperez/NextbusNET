namespace NextbusNET.Model
{
    public class Prediction
    {
        public int EpochTime { get; set; }

        public int Seconds { get; set; }

        public int Minutes { get; set; }

        public bool IsDeparture { get; set; }

        public string TripTag { get; set; }

        public bool AffectedByLayover { get; set; }

        public bool IsScheduleBased { get; set; }

        public bool Delayed { get; set; }

        public string DirTag { get; set; }

        public string Vehicle { get; set; }

        public string Block { get; set; }
    }
}