using System.Collections.Generic;

namespace FlavorOfTheMonth.Models
{
    /// <summary>
    /// This class will be used to represent a request for data. It will specify
    /// all of the classes, specializations, bracket, and region the user requested.
    /// </summary>
    public class HomeRequestModel
    {
        public string region { get; set; }
        public string bracket { get; set; }
        public string faction { get; set; }
        public List<object> classes { get; set; }
        public List<object> specs { get; set; }
    }
}