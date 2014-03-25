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
                using (credentials = File.OpenText(filename))
                {
                    string email = credentials.ReadLine();
                    string password = credentials.ReadLine();
                    return new Credentials(email, password);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new ArgumentException("File does not exist.", "filename", ex);
            }
        }
    }
}
