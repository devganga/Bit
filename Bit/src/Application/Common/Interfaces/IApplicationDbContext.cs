﻿using Bit.Domain.Entities;

namespace Bit.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Article> Articles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
