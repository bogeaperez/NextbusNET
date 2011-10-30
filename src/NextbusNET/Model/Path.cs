using System.Collections.Generic;
using System.Linq;

namespace NextbusNET.Model
{
    public class Path
    {
        public IEnumerable<Point> Points { get; internal set; }

        public override string ToString()
        {
            return string.Format("Points: {0}", Points.Count());
        }
    }
}