using System;
using ContactBook.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Helpers
{
    public static class DataHelper
    {
       //keeps db updated while being hosted
       public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            //gets an instance of the db application context
            var dbContextsvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            //migration-- equivalent to update database command
            await dbContextsvc.Database.MigrateAsync();
        }
    }
}

