namespace NextbusNET.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class StopSchedule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int EpochTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BlockId { get; set; }

        public override string ToString()
        {
            return string.Format("Tag: {0}, EpochTime: {1}, BlockId: {2}", Tag, EpochTime, BlockId);
        }
    }
}