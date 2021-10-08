using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FileServer.App.Application.Infrastructures
{
    public class DBSaveChangeException : Exception
    {
        public DBSaveChangeException()
        {
        }

        public DBSaveChangeException(string message) : base(message)
        {
        }

        public DBSaveChangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DBSaveChangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
