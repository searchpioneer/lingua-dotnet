// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//
// Adapted from dotnet/runtime @ 9f9485d11b6047bf9f9aefa0aa5e6c412a929a7c (2026-07-28)
// src/libraries/System.Private.CoreLib/src/System/Collections/Generic/ICollectionDebugView.cs
//
// Provides OrderedDictionary<TKey, TValue> on target frameworks that predate .NET 9,
// where it is not part of the framework. Delete when .NET 8 support is dropped.

#if !NET9_0_OR_GREATER
using System.Diagnostics;

namespace Lingua.Internal;

internal sealed class ICollectionDebugView<T>
{
	private readonly ICollection<T> _collection;

	public ICollectionDebugView(ICollection<T> collection)
	{
		ArgumentNullException.ThrowIfNull(collection);

		_collection = collection;
	}

	[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
	public T[] Items
	{
		get
		{
			var items = new T[_collection.Count];
			_collection.CopyTo(items, 0);
			return items;
		}
	}
}
#endif
