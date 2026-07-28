// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//
// Adapted from dotnet/runtime @ 9f9485d11b6047bf9f9aefa0aa5e6c412a929a7c (2026-07-28)
// src/libraries/System.Collections/src/System/Collections/Generic/OrderedDictionaryDebugView.cs
//
// Provides OrderedDictionary<TKey, TValue> on target frameworks that predate .NET 9,
// where it is not part of the framework. Delete when .NET 8 support is dropped.

#if !NET9_0_OR_GREATER
using System.Diagnostics;

namespace Lingua.Internal;

internal sealed class OrderedDictionaryDebugView<TKey, TValue> where TKey : notnull
{
	private readonly OrderedDictionary<TKey, TValue> _dictionary;

	public OrderedDictionaryDebugView(OrderedDictionary<TKey, TValue> dictionary)
	{
		ArgumentNullException.ThrowIfNull(dictionary);
		_dictionary = dictionary;
	}

	[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
	public KeyValuePair<TKey, TValue>[] Items
	{
		get
		{
			var items = new KeyValuePair<TKey, TValue>[_dictionary.Count];
			((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(items, 0);
			return items;
		}
	}
}
#endif
