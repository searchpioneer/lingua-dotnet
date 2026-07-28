// Vendored from dotnet/runtime, MIT licensed. Do not edit.
// Source: src/libraries/System.Collections/src/System/Collections/Generic/OrderedDictionaryDebugView.cs
// Taken at dotnet/runtime main @ 9f9485d11b6047bf9f9aefa0aa5e6c412a929a7c (2026-07-28).
// See README.md in this directory before changing anything here.

#if !NET9_0_OR_GREATER
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;

namespace System.Collections.Generic
{
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
                KeyValuePair<TKey, TValue>[] items = new KeyValuePair<TKey, TValue>[_dictionary.Count];
                ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(items, 0);
                return items;
            }
        }
    }
}
#endif
