// Copyright (c) Daniel Crenna. All rights reserved.
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, you can obtain one at http://mozilla.org/MPL/2.0/.

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