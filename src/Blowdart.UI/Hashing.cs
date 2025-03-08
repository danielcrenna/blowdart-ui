// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Text;

namespace Blowdart.UI;

internal class Hashing
{
	/// <summary>
	///     <see href="https://en.wikipedia.org/wiki/MurmurHash" />
	/// </summary>
	public static UInt128 MurmurHash3(byte[] key, UInt128 seed = default)
	{
		int len = key.Length;
		int nblocks = len / 16;

		ulong h1 = (ulong)(seed >> 64);
		ulong h2 = (ulong)seed;

		ulong c1 = 0x87c37b91114253d5u;
		ulong c2 = 0x4cf5ad432745937fu;

		for (int i = 0; i < nblocks; i++)
		{
			int offset = i * 16;
			ulong k1 = BitConverter.ToUInt64(key, offset);
			ulong k2 = BitConverter.ToUInt64(key, offset + 8);

			k1 *= c1;
			k1 = Rotl64(k1, 31);
			k1 *= c2;
			h1 ^= k1;

			h1 = Rotl64(h1, 27);
			h1 += h2;
			h1 = h1 * 5 + 0x52dce729;

			k2 *= c2;
			k2 = Rotl64(k2, 33);
			k2 *= c1;
			h2 ^= k2;

			h2 = Rotl64(h2, 31);
			h2 += h1;
			h2 = h2 * 5 + 0x38495ab5;
		}

		// Process tail.
		int tailIndex = nblocks * 16;
		ulong k1_tail = 0, k2_tail = 0;
		int tailLength = len - tailIndex;
		for (int i = 0; i < tailLength; i++)
		{
			byte b = key[tailIndex + i];
			if (i < 8)
				k1_tail |= ((ulong) b) << (8 * i);
			else
				k2_tail |= ((ulong) b) << (8 * (i - 8));
		}

		if (tailLength > 0)
		{
			if (tailLength > 8)
			{
				k2_tail *= c2;
				k2_tail = Rotl64(k2_tail, 33);
				k2_tail *= c1;
				h2 ^= k2_tail;
			}

			k1_tail *= c1;
			k1_tail = Rotl64(k1_tail, 31);
			k1_tail *= c2;
			h1 ^= k1_tail;
		}

		// Finalization.
		h1 ^= (ulong) len;
		h2 ^= (ulong) len;

		h1 += h2;
		h2 += h1;

		h1 = Fmix64(h1);
		h2 = Fmix64(h2);

		h1 += h2;
		h2 += h1;

		// Combine the two 64-bit parts into a single UInt128 value.
		return ((UInt128) h1 << 64) | h2;
	}

	public static UInt128 MurmurHash3(string key, UInt128 seed = default)
	{
		int len = key.Length * 2;
		int nblocks = key.Length / 8;

		ulong h1 = (ulong)(seed >> 64);
		ulong h2 = (ulong)seed;

		const ulong c1 = 0x87c37b91114253d5u;
		const ulong c2 = 0x4cf5ad432745937fu;

		for (int i = 0; i < nblocks; i++)
		{
			int index = i * 8;
			ulong k1 = key[index];
			k1 |= (ulong) key[index + 1] << 16;
			k1 |= (ulong) key[index + 2] << 32;
			k1 |= (ulong) key[index + 3] << 48;

			ulong k2 = key[index + 4];
			k2 |= (ulong) key[index + 5] << 16;
			k2 |= (ulong) key[index + 6] << 32;
			k2 |= (ulong) key[index + 7] << 48;

			k1 *= c1;
			k1 = Rotl64(k1, 31);
			k1 *= c2;
			h1 ^= k1;

			h1 = Rotl64(h1, 27);
			h1 += h2;
			h1 = h1 * 5 + 0x52dce729;

			k2 *= c2;
			k2 = Rotl64(k2, 33);
			k2 *= c1;
			h2 ^= k2;

			h2 = Rotl64(h2, 31);
			h2 += h1;
			h2 = h2 * 5 + 0x38495ab5;
		}

		int tailIndex = nblocks * 8;
		ulong k1_tail = 0, k2_tail = 0;
		switch (key.Length & 7)
		{
			case 7:
				k2_tail |= (ulong) key[tailIndex + 6] << 32;
				goto case 6;
			case 6:
				k2_tail |= (ulong) key[tailIndex + 5] << 16;
				goto case 5;
			case 5:
				k2_tail |= (ulong) key[tailIndex + 4] << 0;
				k2_tail *= c2;
				k2_tail = Rotl64(k2_tail, 33);
				k2_tail *= c1;
				h2 ^= k2_tail;
				goto case 4;
			case 4:
				k1_tail |= (ulong) key[tailIndex + 3] << 48;
				goto case 3;
			case 3:
				k1_tail |= (ulong) key[tailIndex + 2] << 32;
				goto case 2;
			case 2:
				k1_tail |= (ulong) key[tailIndex + 1] << 16;
				goto case 1;
			case 1:
				k1_tail |= (ulong) key[tailIndex + 0] << 0;
				k1_tail *= c1;
				k1_tail = Rotl64(k1_tail, 31);
				k1_tail *= c2;
				h1 ^= k1_tail;
				break;
		}

		h1 ^= (ulong) len;
		h2 ^= (ulong) len;

		h1 += h2;
		h2 += h1;

		h1 = Fmix64(h1);
		h2 = Fmix64(h2);

		h1 += h2;
		h2 += h1;

		return ((UInt128) h1 << 64) | h2;
	}

	public static UInt128 MurmurHash3(StringBuilder key, UInt128 seed = default)
	{
		int len = key.Length * 2;
		int nblocks = key.Length / 8;

		ulong h1 = (ulong)(seed >> 64);
		ulong h2 = (ulong)seed;

		const ulong c1 = 0x87c37b91114253d5u;
		const ulong c2 = 0x4cf5ad432745937fu;

		for (int i = 0; i < nblocks; i++)
		{
			int index = i * 8;
			ulong k1 = key[index];
			k1 |= (ulong) key[index + 1] << 16;
			k1 |= (ulong) key[index + 2] << 32;
			k1 |= (ulong) key[index + 3] << 48;

			ulong k2 = key[index + 4];
			k2 |= (ulong) key[index + 5] << 16;
			k2 |= (ulong) key[index + 6] << 32;
			k2 |= (ulong) key[index + 7] << 48;

			k1 *= c1;
			k1 = Rotl64(k1, 31);
			k1 *= c2;
			h1 ^= k1;

			h1 = Rotl64(h1, 27);
			h1 += h2;
			h1 = h1 * 5 + 0x52dce729;

			k2 *= c2;
			k2 = Rotl64(k2, 33);
			k2 *= c1;
			h2 ^= k2;

			h2 = Rotl64(h2, 31);
			h2 += h1;
			h2 = h2 * 5 + 0x38495ab5;
		}

		int tailIndex = nblocks * 8;
		ulong k1_tail = 0, k2_tail = 0;
		switch (key.Length & 7)
		{
			case 7:
				k2_tail |= (ulong) key[tailIndex + 6] << 32;
				goto case 6;
			case 6:
				k2_tail |= (ulong) key[tailIndex + 5] << 16;
				goto case 5;
			case 5:
				k2_tail |= (ulong) key[tailIndex + 4] << 0;
				k2_tail *= c2;
				k2_tail = Rotl64(k2_tail, 33);
				k2_tail *= c1;
				h2 ^= k2_tail;
				goto case 4;
			case 4:
				k1_tail |= (ulong) key[tailIndex + 3] << 48;
				goto case 3;
			case 3:
				k1_tail |= (ulong) key[tailIndex + 2] << 32;
				goto case 2;
			case 2:
				k1_tail |= (ulong) key[tailIndex + 1] << 16;
				goto case 1;
			case 1:
				k1_tail |= (ulong) key[tailIndex + 0] << 0;
				k1_tail *= c1;
				k1_tail = Rotl64(k1_tail, 31);
				k1_tail *= c2;
				h1 ^= k1_tail;
				break;
		}

		h1 ^= (ulong) len;
		h2 ^= (ulong) len;

		h1 += h2;
		h2 += h1;

		h1 = Fmix64(h1);
		h2 = Fmix64(h2);

		h1 += h2;
		h2 += h1;

		return ((UInt128) h1 << 64) | h2;
	}

	public static UInt128 MurmurHash3(ulong key, UInt128 seed = default)
	{
		const int len = 4;

		var h1 = (ulong)(seed >> 64);
		var h2 = (ulong)seed;

		const ulong c1 = 0x87c37b91114253d5u;
		const ulong c2 = 0x4cf5ad432745937fu;

		{
			ulong k1 = key;
			k1 *= c1;
			k1 = Rotl64(k1, 31);
			k1 *= c2;
			h1 ^= k1;
		}

		h1 ^= len;
		h2 ^= len;

		h1 += h2;
		h2 += h1;

		h1 = Fmix64(h1);
		h2 = Fmix64(h2);

		h1 += h2;
		h2 += h1;

		return ((UInt128)h1 << 64) | h2;
	}

		
	private static ulong Rotl64(ulong x, int r)
	{
		return (x << r) | (x >> (64 - r));
	}

	private static ulong Fmix64(ulong k)
	{
		k ^= k >> 33;
		k *= 0xff51afd7ed558ccd;
		k ^= k >> 33;
		k *= 0xc4ceb9fe1a85ec53;
		k ^= k >> 33;
		return k;
	}
}