namespace Cstieg.Geography
{
    /// <summary>
    /// A geographical range consisting of a geographical "rectangle" between a top left geopoint and a bottom right geopoint
    /// </summary>
    public class GeoRange
    {
        public LatLng TopLeft { get; set; }

        public LatLng BottomRight { get; set; }

        /// <summary>
        /// Constructor taking the two geopoints as four lat and lng parameters
        /// </summary>
        /// <param name="topLat">The northernmost latitude of the range</param>
        /// <param name="leftLng">The westernmost longitude of the range</param>
        /// <param name="bottomLat">The southernmost latitude of the range </param>
        /// <param name="rightLng">The easternmost longitude of the range</param>
        public GeoRange(float topLat, float leftLng, float bottomLat, float rightLng)
        {
            TopLeft = new LatLng(topLat, leftLng);
            BottomRight = new LatLng(bottomLat, rightLng);
        }

        /// <summary>
        /// Constructor taking two geopoints
        /// </summary>
        /// <param name="topLeft">The northwesternmost corner of the range</param>
        /// <param name="bottomRight">The southeasternmost corner of the range</param>
        public GeoRange(LatLng topLeft, LatLng bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
    }
}