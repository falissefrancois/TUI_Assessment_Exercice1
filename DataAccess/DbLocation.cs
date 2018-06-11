namespace DataAccess
{
    public class DbLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DbLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
