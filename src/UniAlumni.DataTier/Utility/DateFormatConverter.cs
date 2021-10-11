using Newtonsoft.Json.Converters;

namespace UniAlumni.DataTier.Utility
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}