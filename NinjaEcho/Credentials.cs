using System;
using System.IO;

namespace NinjaEcho
{
    class Credentials
    {
        public string Email
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        private Credentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public static Credentials LoadFrom(string filename)
        {
            StreamReader credentials;
            try
            {
                credentials = File.OpenText(filename);
            }
            catch (FileNotFoundException ex)
            {
                throw new ArgumentException("File does not exist.", "filename", ex);
            }
            string email = credentials.ReadLine();
            string password = credentials.ReadLine();
            return new Credentials(email, password);
        }
    }
}
