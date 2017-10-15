using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Cstieg.Geography
{
    /// <summary>
    /// A representatation of a geographical location
    /// Uses freegeoip.net to populate class, which is modeled after the json return data
    /// </summary>
    public class GeoLocation
    {
        [DataMember(Name = "ip")]
        public string Ip { get; set; }

        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        [DataMember(Name = "country_name")]
        public string CountryName { get; set; }

        [DataMember(Name = "region_code")]
        public string RegionCode { get; set; }

        [DataMember(Name = "region_name")]
        public string RegionName { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "zip_code")]
        public string ZipCode { get; set; }

        [DataMember(Name = "time_zone")]
        public string TimeZone { get; set; }

        [DataMember(Name = "latitude")]
        public float Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public float Longitude { get; set; }

        [DataMember(Name = "metro_code")]
        public string MetroCode { get; set; }

        public LatLng LatLng
        { 
            get
            {
                return new LatLng(Latitude, Longitude);
            }
        }

        /// <summary>
        /// Asynchronous factory method which returns a GeoLocation corresponding to the user's IP address
        /// </summary>
        /// <returns>GeoLocation object corresponding to the user's IP address</returns>
        public async static Task<GeoLocation> GetGeoLocation()
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://freegeoip.net/json/");
                var response = await client.SendAsync(request);
                string result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<GeoLocation>(result);
            }
            //"{\"ip\":\"107.77.253.6\",\"country_code\":\"US\",\"country_name\":\"United States\",\"region_code\":\"NY\",\"region_name\":\"New York\",\"city\":\"The Bronx\",\"zip_code\":\"10451\",\"time_zone\":\"America/New_York\",\"latitude\":40.8195,\"longitude\":-73.9209,\"metro_code\":501}\n"
        }

        /// <summary>
        /// Gets a geographical "square" range centered on a given point
        /// </summary>
        /// <param name="center">The center of the square</param>
        /// <param name="range">The smallest radius of the square;
        /// the distance from the center to the edge straight in one of the four compass directions</param>
        /// <returns>A GeoRange object centered on the given point</returns>
        public static GeoRange GetGeoRange(LatLng center, float range)
        {
            GeoRange geoRange = new GeoRange(
                new LatLng(center.Lat - range * 70, center.Lng - range * 70),
                new LatLng(center.Lat + range * 70, center.Lng + range * 70));
            return geoRange;
        }
    }
}