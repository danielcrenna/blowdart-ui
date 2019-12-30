// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using TypeKitchen;

namespace Blowdart.UI.Web.Core.Extensions
{
    internal static class FieldIdentifierExtensions
	{
		public static bool IsPassword(this FieldIdentifier identifier)
        {
            return identifier.IsDataType(DataType.Password);
        }

        public static bool IsDate(this FieldIdentifier identifier)
        {
            return identifier.IsDataType(DataType.Date);
        }

        public static bool IsDateTime(this FieldIdentifier identifier)
        {
            return identifier.IsDataType(DataType.DateTime);
        }

        public static bool IsTime(this FieldIdentifier identifier)
        {
            return identifier.IsDataType(DataType.Time);
        }

        public static bool IsEmailAddress(this FieldIdentifier identifier)
		{
            return identifier.IsDataType(DataType.EmailAddress);
        }

        public static bool IsReadOnly(this FieldIdentifier identifier)
        {
            return PublicProperties(identifier).IsReadOnly(identifier.FieldName);
        }

        public static bool IsVisible(this FieldIdentifier identifier)
        {
            return PublicProperties(identifier).IsVisible(identifier.FieldName);
        }

        private static bool IsDataType(this FieldIdentifier identifier, DataType dataType)
        {
            return PublicProperties(identifier).IsDataType(identifier.FieldName, dataType);
        }
        
        public static string Prompt(this FieldIdentifier identifier)
        {
            return PublicProperties(identifier).Prompt(identifier.FieldName);
        }

        public static string DisplayName(this FieldIdentifier identifier)
        {
            return PublicProperties(identifier).DisplayName(identifier.FieldName);
        }

        public static string GetDateFormat(this FieldIdentifier identifier)
        {
            return PublicProperties(identifier).DateFormat(identifier.FieldName);
        }

        private static AccessorMembers PublicProperties(FieldIdentifier identifier)
        {
            return AccessorMembers.Create(identifier.Model, AccessorMemberTypes.Properties, AccessorMemberScope.Public);
        }
    }
}
