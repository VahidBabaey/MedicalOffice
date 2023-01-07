using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence;

public static class DbContextExtensions
{
    public static async Task UpdateIfNotNull<T>(this DbSet<T> entities, Guid key, T modifiedEntity) where T : class
    {
        var oldEntity = await entities.FindAsync(key);

        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var newValue = property.GetValue(modifiedEntity);
            var oldValue = property.GetValue(oldEntity);

            if (newValue != null)
                property.SetValue(oldEntity, newValue);
            else
                property.SetValue(oldEntity, oldValue);
        }
    }
}
