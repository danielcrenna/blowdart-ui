// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Text;

namespace Blowdart.UI;

public static class Ids
{
	public static UInt128 NextId(ref UInt128 nextIdHash, string id)
	{
		nextIdHash = Hashing.MurmurHash3(id, nextIdHash) ^ nextIdHash;
		return nextIdHash;
	}

	public static UInt128 NextId(ref UInt128 nextIdHash, StringBuilder id)
	{
		nextIdHash = Hashing.MurmurHash3(id, nextIdHash) ^ nextIdHash;
		return nextIdHash;
	}

	public static UInt128 NextId(ref UInt128 nextIdHash, int i)
	{
		nextIdHash = Hashing.MurmurHash3((ulong)i, nextIdHash) ^ nextIdHash;
		return nextIdHash;
	}
}