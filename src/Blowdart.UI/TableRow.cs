// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Blowdart.UI
{
    public struct TableRow
    {
        public object Data { get; }
        public IEnumerable<string> Columns { get; }

        public TableRow(object data, IEnumerable<string> columns)
        {
            Data = data;
            Columns = columns;
        }
    }
}