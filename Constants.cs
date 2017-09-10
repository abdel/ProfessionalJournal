#define DEBUG

using System;

namespace ProfessionalJournal
{
    public static class Constants
    {
#if DEBUG
        public static string BackendURL = @"htts://localhost:3000";
#else
        public static string BackendURL = @"https://professionaljournal.azurewebsites.net";
#endif
	}
}
