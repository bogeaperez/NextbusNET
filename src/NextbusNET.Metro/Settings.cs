namespace NextbusNET.Properties
{


    internal sealed partial class Settings
    {

        private static readonly Settings defaultInstance = new Settings();

        public static Settings Default
        {
            get { return defaultInstance; }
        }

        public string BaseUrl
        {
            get { return "http://webservices.nextbus.com/service/publicXMLFeed"; }
        }
    }
}