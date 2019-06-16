// Copyright (c) SecretCollect B.V. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for license information.
//
// Based upon work by Michiel van Oudheusden
// https://github.com/mivano/EFIndexInclude
// Changes:
//  - Moved SqlServer:IncludeIndex to constant
//  - Changed namespace to accomodate this library

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using System.Collections.Generic;
using System.Linq;

namespace SecretCollect.EntityFramework.Infrastructure
{
    internal class ExtendedSqlServerMigrationsAnnotationProvider : SqlServerMigrationsAnnotationProvider
    {
        public ExtendedSqlServerMigrationsAnnotationProvider(MigrationsAnnotationProviderDependencies dependencies)
            : base(dependencies)
        { }

        public override IEnumerable<IAnnotation> For(IIndex index)
        {
            var baseAnnotations = base.For(index);
            var customAnnotations = index.GetAnnotations().Where(a => a.Name == Constants.INCLUDE_INDEX);

            return baseAnnotations.Concat(customAnnotations);
        }
    }
}
