using System;

namespace BusinessLogic
{
    internal static class GeoUtils
    {
        private const double RadiusOfTheEarth = 6371;

        /// <summary>
        /// Computes the distance between 2 locations using Haversine formula (0.3% error)
        /// For more accurate computation, Vincenty's formula could be used (less than 0.01% error)
        /// </summary>
        public static double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            var lat = (lat2 - lat1).ToRadians();
            var lon = (lon2 - lon1).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                     Math.Cos(lat1.ToRadians()) * Math.Cos(lat2.ToRadians()) *
                     Math.Sin(lon / 2) * Math.Sin(lon / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return RadiusOfTheEarth * h2;
        }

        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
