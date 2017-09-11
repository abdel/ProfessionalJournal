using System;
using System.Collections.Generic;

namespace ProfessionalJournal
{
    public class Response
    {
        public int StatusCode { get; set; }
		public string Message { get; set; }
        public string Token { get; set; }

        public IEnumerable<Journal> journals { get; set; }
    }
}
