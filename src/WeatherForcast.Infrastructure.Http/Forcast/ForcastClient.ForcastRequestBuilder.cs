namespace WeatherForcast.Infrastructure.Http.Forcast;
internal sealed partial class ForcastClient
{
    internal ref struct ForcastRequestBuilder
    {
        private string _host;
        private string _relativeAddress;
        private string _key;
        private decimal _longitude;
        private decimal _latitude;
        
        public ForcastRequestBuilder WithHost(string host) 
        { 
            _host = host; 
            return this;
        }

        public ForcastRequestBuilder WithAddress(string relativeAddress)
        {
            _relativeAddress = relativeAddress;
            return this;
        }

        public ForcastRequestBuilder WithKey(string key)
        {
            _key = key;
            return this;
        }

        public ForcastRequestBuilder WithLongitude(decimal longitude)
        {
            _longitude = longitude;
            return this;
        }

        public ForcastRequestBuilder WithLatitude(decimal latitude)
        {
            _latitude = latitude;
            return this;
        }

        public string Build() 
            => String.Format(
                "{0}{1}?key={2}&q={3},{4}",
                string.Empty,
                _relativeAddress,
                _key,
                _latitude,
                _longitude);
    }
}
