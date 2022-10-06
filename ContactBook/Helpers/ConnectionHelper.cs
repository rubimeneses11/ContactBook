using System;
using MySql;
using MySql.Data.MySqlClient;

namespace ContactBook.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            //looks to see if app is running locally or being hosted so our connection string works
            return String.IsNullOrEmpty(databaseUrl) ? connectionString: BuildConnectionString(databaseUrl);
        }

        //build a connection string from the environment (ie. Heroku)
        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "mysql",
                Database = "contactbook",
                UserID = userInfo[0],
                Password = userInfo[1],
                SslMode = MySqlSslMode.Required,
            };

            return builder.ToString();
        }
    }
}

