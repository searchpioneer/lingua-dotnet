// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//
// Adapted from dotnet/runtime @ 9f9485d11b6047bf9f9aefa0aa5e6c412a929a7c (2026-07-28)
// src/libraries/Common/src/System/Collections/Generic/EnumerableHelpers.cs
//
// Provides OrderedDictionary<TKey, TValue> on target frameworks that predate .NET 9,
// where it is not part of the framework. Delete when .NET 8 support is dropped.

#if !NET9_0_OR_GREATER
using System.Collections;

namespace Lingua.Internal;

/// <summary>
/// Internal helper functions for working with enumerables.
/// </summary>
internal static class EnumerableHelpers
{
	/// <summary>Calls Reset on an enumerator instance.</summary>
	/// <remarks>Enables Reset to be called without boxing on a struct enumerator that lacks a public Reset.</remarks>
	internal static void Reset<T>(ref T enumerator) where T : IEnumerator => enumerator.Reset();

	/// <summary>Gets an enumerator singleton for an empty collection.</summary>
	internal static IEnumerator<T> GetEmptyEnumerator<T>() =>
		((IEnumerable<T>)[]).GetEnumerator();
}
#endif
