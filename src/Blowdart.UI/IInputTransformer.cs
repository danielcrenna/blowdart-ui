// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI
{
    public interface IInputTransformer
    {
        string ToString(object value, object model, string memberName);
        bool FromString(Type elementType, string value, out object result, out string errorMessage);
    }
}
