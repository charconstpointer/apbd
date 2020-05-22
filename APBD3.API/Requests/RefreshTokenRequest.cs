using System;

namespace APBD3.API.Requests
{
    public class RefreshTokenRequest
    {
        public string Index { get; set; }
        public Guid RefreshToken { get; set; }
    }
}