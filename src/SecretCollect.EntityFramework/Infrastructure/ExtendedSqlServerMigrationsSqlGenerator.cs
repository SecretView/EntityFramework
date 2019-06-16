// Copyright (c) SecretCollect B.V. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for license information.
//
// Based upon work by Michiel van Oudheusden
// https://github.com/mivano/EFIndexInclude
// Changes:
//  - Moved SqlServer:IncludeIndex to constant
//  - Changed namespace to accomodate this library

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace SecretCollect.EntityFramework.Infrastructure
{
    internal class ExtendedSqlServerMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
    {
        protected virtual string StatementTerminator => ";";

        public ExtendedSqlServerMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, IMigrationsAnnotationProvider migrationsAnnotations) :
            base(dependencies, migrationsAnnotations)
        { }

        protected override void Generate(CreateIndexOperation operation, IModel model, MigrationCommandListBuilder builder, bool terminate)
        {
            base.Generate(operation, model, builder, false);

            var includeIndexAnnotation = operation.FindAnnotation(Constants.INCLUDE_INDEX);

            if (includeIndexAnnotation != null)
                builder.Append($" INCLUDE({includeIndexAnnotation.Value})");

            if (terminate)
            {
                builder.AppendLine(StatementTerminator);
                EndStatement(builder);
            }
        }
    }
}
