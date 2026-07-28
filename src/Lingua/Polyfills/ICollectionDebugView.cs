// Vendored from dotnet/runtime, MIT licensed. Do not edit.
// Source: src/libraries/System.Private.CoreLib/src/System/Collections/Generic/ICollectionDebugView.cs
// Taken at dotnet/runtime main @ 9f9485d11b6047bf9f9aefa0aa5e6c412a929a7c (2026-07-28).
// See README.md in this directory before changing anything here.

#if !NET9_0_OR_GREATER
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;

namespace System.Collections.Generic
{
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
                T[] items = new T[_collection.Count];
                _collection.CopyTo(items, 0);
                return items;
            }
        }
    }
}
#endif
