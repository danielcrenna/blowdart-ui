// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TypeKitchen;

namespace Blowdart.UI.Instructions
{
    public class ObjectTableInstruction : RenderInstruction
    {
        public override string DebuggerDisplay => "Table";
        public IEnumerable<TableRow> Rows { get; set; }
        public IEnumerable<string> Headers { get; protected set; }
    }

    public class ObjectTableInstruction<T> : ObjectTableInstruction
    {
        public ObjectTableInstruction(IEnumerable<T> data, IEnumerable<string> getColumns = null)
        {
            Headers = YieldHeaders();
            Rows = data.Select(x => new TableRow(x, getColumns ?? YieldColumns(x)));
        }

        private static IEnumerable<string> YieldHeaders()
        {
            var members = AccessorMembers.Create(typeof(T), AccessorMemberTypes.Properties, AccessorMemberScope.Public);

            // FIXME: make part of TypeKitchen and cache/unify common attributes
            foreach (var member in members)
            {
                if (member.TryGetAttribute(out DisplayNameAttribute displayName))
                    yield return displayName.DisplayName;
                else
                    yield return member.Name;
            }
        }

        private static IEnumerable<string> YieldColumns(T x)
        {
            var accessor = ReadAccessor.Create(typeof(T), AccessorMemberTypes.Properties, AccessorMemberScope.Public, out var members);
            foreach (var columnName in members.Names)
                if (accessor.TryGetValue(x, columnName, out var value))
                    yield return value.ToString();
        }
    }
}
