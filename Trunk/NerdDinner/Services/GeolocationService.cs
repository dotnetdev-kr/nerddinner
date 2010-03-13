using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using NerdDinner.Helpers;
using System.Xml.Linq;

namespace NerdDinner.Services
{
    public class GeolocationService
    {

        public static LatLong PlaceOrZipToLatLong(string placeOrZip)
        {
            ObjectCache cache = MemoryCache.Default;

            string url = "http://ws.geonames.org/postalCodeSearch?{0}={1}&maxRows=1&style=SHORT";
            url = String.Format(url, placeOrZip.IsNumeric() ? "postalcode" : "placename", placeOrZip);

            var result = cache[placeOrZip] as XDocument;
            if (result == null)
            {
                result = XDocument.Load(url);
                cache.Add(placeOrZip, result,
                    new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromDays(1) });
            }

            var ll = (from x in result.Descendants("code")
                           select new LatLong
                           {
                               Lat = (float)x.Element("lat"),
                               Long = (float)x.Element("lng")
                       }).First();

            return ll;
        }
    }

    public class LatLong
    {
        public float Lat { get; set; }
        public float Long { get; set; }
    }
}