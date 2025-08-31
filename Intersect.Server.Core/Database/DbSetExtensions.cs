using System;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Database;

/// <summary>
///     Extension helpers for working with <see cref="DbSet{TEntity}"/> instances.
/// </summary>
public static class DbSetExtensions
{
    /// <summary>
    ///     Ensures that an entity with the provided key exists in the set. If not found, a new entity
    ///     is created using <paramref name="factory"/>. If found, <paramref name="update"/> is invoked
    ///     to mutate the existing entity.
    /// </summary>
    public static TEntity EnsureEntity<TEntity, TKey>(
        this DbSet<TEntity> set,
        TKey key,
        Func<TEntity> factory,
        Action<TEntity>? update = null
    ) where TEntity : class
    {
        var entity = set.Find(key);
        if (entity == null)
        {
            entity = factory();
            set.Add(entity);
        }
        else
        {
            update?.Invoke(entity);
        }

        return entity;
    }
}

