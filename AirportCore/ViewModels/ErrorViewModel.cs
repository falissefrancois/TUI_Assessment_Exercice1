namespace AirportCore.ViewModels
{
    public class ErrorViewModel
    {
        public string ControllerName { get; set; } = "Home";
        public string ActionName { get; set; } = "Index";
        public int StatusCode { get; set; }

        public string GetErrorMessage()
        {
            switch(StatusCode)
            {
                case 400:
                    return "Bad request: The request cannot be fulfilled due to bad syntax";
                case 403:
                    return "Forbidden";
                case 404:
                    return "Page not found";
                case 408:
                    return "The server timed out waiting for the request";
                case 500:
                    return "Internal Server Error - Server was unable to finish processing the request";
                default:
                    return "An unexpected error occured";
            }
        }
    }
}
