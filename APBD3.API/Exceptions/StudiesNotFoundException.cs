using System;

namespace APBD3.API.Exceptions
{
    public class StudiesNotFoundException : Exception
    {
        public StudiesNotFoundException(string message):base(message)
        {
            
        }
    }
}