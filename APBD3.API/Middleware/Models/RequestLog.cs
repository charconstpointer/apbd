using System;
using System.Text;

namespace APBD3.API.Middleware.Models
{
    public class RequestLog
    {
        public string Method { get; set; }
        public string Resource { get; set; }
        public string Body { get; set; }
        public string Query { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var propertyInfo in typeof(RequestLog).GetProperties())
            {
                var value = propertyInfo.GetValue(this, null);
                builder.AppendLine($"{propertyInfo.Name} => {value ?? "Empty"}");
            }

            return builder.ToString();
        }
    }
}