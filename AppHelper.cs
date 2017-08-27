using System;
using System.Text;
using System.Security.Cryptography;

namespace ProfessionalJournal
{
    public static class AppHelper
    {
		public static string GeneratePasswordHash(string password)
		{
			if (string.IsNullOrWhiteSpace(password)) return "";

			return ComputeSHA256(password);
		}

		public static string ComputeSHA256(string text)
		{
			SHA256Managed hashstring = new SHA256Managed();
			byte[] hash = hashstring.ComputeHash(Encoding.UTF8.GetBytes(text));

			string hashString = "";
			foreach (byte x in hash)
			{
				hashString += String.Format("{0:x2}", x);
			}

			return hashString;
		}

    }
}
