namespace NextbusNET.Model
{
    public class StopSchedule
    {
        public string Tag { get; set; }

        public int EpochTime { get; set; }

        public string BlockId { get; set; }

        public override string ToString()
        {
            return string.Format("Tag: {0}, EpochTime: {1}, BlockId: {2}", Tag, EpochTime, BlockId);
        }
    }
}