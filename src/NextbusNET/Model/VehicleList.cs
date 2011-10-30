using System.Collections.Generic;
using System.Linq;

namespace NextbusNET.Model
{
    public class VehicleList
    {
        public IEnumerable<Vehicle> Vehicles { get; internal set; }

        public int LastTime { get; internal set; }

        public override string ToString()
        {
            return string.Format("Vehicles: {0}, LastTime: {1}", Vehicles.Count(), LastTime);
        }
    }
}