// Copyright (c) SecretCollect B.V. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for license information.
//
// Based upon work by Michiel van Oudheusden
// https://github.com/mivano/EFIndexInclude
// Changes:
//  - Moved SqlServer:IncludeIndex to constant
//  - Changed namespace to accomodate easy usage

using Microsoft.EntityFrameworkCore.Internal;
using SecretCollect.EntityFramework;
using System;
using System.Linq.Expressions;
using System.Text;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders
{
    /// <summary>
    /// Extentions class for <see cref="IndexBuilder"/>
    /// </summary>
    public static class IndexExtensions
    {
        /// <summary>
        /// Include columns in the index
        /// </summary>
        /// <typeparam name="TEntity">The entity for which an index is being created</typeparam>
        /// <param name="indexBuilder">The builder</param>
        /// <param name="indexExpression">The expression that selects the columns</param>
        /// <returns>The indexbuilder for chaining purposes</returns>
        public static IndexBuilder Include<TEntity>(this IndexBuilder indexBuilder, Expression<Func<TEntity, object>> indexExpression)
        {
            var includeStatement = new StringBuilder();
            foreach (var column in indexExpression.GetPropertyAccessList())
            {
                if (includeStatement.Length > 0)
                    includeStatement.Append(", ");

                includeStatement.AppendFormat("[{0}]", column.Name);
            }

            indexBuilder.HasAnnotation(Constants.INCLUDE_INDEX, includeStatement.ToString());

            return indexBuilder;
        }
    }
}
