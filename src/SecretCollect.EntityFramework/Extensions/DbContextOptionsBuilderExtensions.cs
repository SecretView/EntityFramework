// Copyright (c) SecretCollect B.V. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SecretCollect.EntityFramework.Infrastructure;

namespace SecretCollect.EntityFramework.Extensions
{
    /// <summary>
    /// Extension class for <see cref="DbContextOptionsBuilder"/>
    /// </summary>
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Replace <see cref="IMigrationsAnnotationProvider"/> and <see cref="IMigrationsSqlGenerator"/> with implementation that support include columns
        /// </summary>
        /// <param name="optionsBuilder">The options builder for the <see cref="DbContext"/></param>
        /// <returns>The same optionsbuilder to accomodate chaining</returns>
        public static DbContextOptionsBuilder ReplaceServicesForIncludeColumnSupport(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IMigrationsAnnotationProvider, ExtendedSqlServerMigrationsAnnotationProvider>();
            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, ExtendedSqlServerMigrationsSqlGenerator>();

            return optionsBuilder;
        }
    }
}
