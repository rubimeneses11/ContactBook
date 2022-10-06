using System;
using ContactBook.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Helpers
{
    public static class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            //needed for publishing - maintains db & keeps it updated

            //get an instance of the db application context
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            //migration: this is equivalent to update database cmd
            await dbContextSvc.Database.MigrateAsync();
        }
    }
}

