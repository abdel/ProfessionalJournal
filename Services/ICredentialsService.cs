using System;

namespace ProfessionalJournal
{
    public interface ICredentialsService
    {
		string Username { get; }

		string Token { get; }

		void SaveCredentials(string userName, string token);

		void DeleteCredentials();

		bool DoCredentialsExist();
    }
}
