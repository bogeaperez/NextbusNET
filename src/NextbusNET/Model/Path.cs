using System.Collections.Generic;
using System.Linq;

namespace NextbusNET.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Path
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Point> Points { get; internal set; }

        public override string ToString()
        {
            return string.Format("Points: {0}", Points.Count());
        }
    }
}