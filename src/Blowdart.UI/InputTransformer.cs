// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using TypeKitchen;

namespace Blowdart.UI
{
    public class InputTransformer : IInputTransformer
    {
        public string ToString(object value, object model, string memberName)
        {
            if (value is DateTime dateTime)
            {
                var dateFormat = PublicProperties(model).DateFormat(memberName);
                var result = dateTime.ToString(dateFormat);
                return result;
            }

            if (value is DateTimeOffset dateTimeOffset)
            {
                var dateFormat = PublicProperties(model).DateFormat(memberName);
                var result = dateTimeOffset.ToString(dateFormat);
                return result;
            }
            
            return value?.ToString();
        }

        public bool FromString(Type elementType, string value, out object result, out string errorMessage)
        {
            if (elementType == typeof(string))
            {
                result = value;
                errorMessage = null;
                return true;
            }

            if (elementType == typeof(DateTime))
            {
                if (DateTime.TryParse(value, out var parsed))
                    result = parsed;
                else
                    result = value;

                errorMessage = null;
                return true;
            }

            if (elementType == typeof(DateTimeOffset))
            {
                if (DateTimeOffset.TryParse(value, out var parsed))
                    result = parsed;
                else
                    result = value;

                errorMessage = null;
                return true;
            }

            if (elementType == typeof(int) || elementType == typeof(int?))
            {
                if (int.TryParse(value, out var parsed))
                    result = parsed;
                else
                    result = value;

                errorMessage = null;
                return true;
            }
            

            throw new NotSupportedException();
        }

        private static AccessorMembers PublicProperties(object model)
        {
            return AccessorMembers.Create(model, AccessorMemberTypes.Properties, AccessorMemberScope.Public);
        }
    }
}
