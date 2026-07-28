# Polyfills

`OrderedDictionary<TKey, TValue>` was introduced in .NET 9.
Lingua also targets .NET 8, so this directory carries the runtime implementation to provide the same type there.

Every file is wrapped in `#if !NET9_0_OR_GREATER`, so on .NET 9 or greater none of it compiles and the built-in type is used instead.
Because the type keeps its original `System.Collections.Generic` namespace, calling code needs no conditional compilation: `OrderedDictionary<TKey, TValue>` resolves to the copy here on .NET 8 and to the framework type on .NET 9 or greater.

## Provenance

Taken from [dotnet/runtime](https://github.com/dotnet/runtime) at commit `9f9485d11b6047bf9f9aefa0aa5e6c412a929a7c` on 2026-07-28, and licensed under the MIT license.

| File | Upstream path |
| --- | --- |
| `OrderedDictionary.cs` | `src/libraries/System.Collections/src/System/Collections/Generic/OrderedDictionary.cs` |
| `OrderedDictionaryDebugView.cs` | `src/libraries/System.Collections/src/System/Collections/Generic/OrderedDictionaryDebugView.cs` |
| `HashHelpers.cs` | `src/libraries/System.Private.CoreLib/src/System/Collections/HashHelpers.cs` |
| `ICollectionDebugView.cs` | `src/libraries/System.Private.CoreLib/src/System/Collections/Generic/ICollectionDebugView.cs` |
| `EnumerableHelpers.cs` | `src/libraries/Common/src/System/Collections/Generic/EnumerableHelpers.cs` |

Everything except `OrderedDictionary.cs` itself is here only because `OrderedDictionary.cs` references it.

## Local modifications

`OrderedDictionary.cs` and `HashHelpers.cs` are modified; the other three are byte for byte identical to upstream apart from a provenance header and the `#if !NET9_0_OR_GREATER` guard.

The modification is that upstream reaches for two indirection layers that exist only to serve the wider runtime, and both are inlined here instead:

- `SR.Xxx` resource lookups become the string literals themselves, taken verbatim from `src/libraries/System.Collections/src/Resources/Strings.resx`.
  Upstream generates `SR` from that `.resx`, so vendoring it would have meant hand writing a file that does not exist upstream.
- `ThrowHelper.ThrowXxx()` calls become the `throw` statements they stand for.

Exception types, messages and parameter names are unchanged, so what callers observe still matches the built-in type on .NET 9 or greater.
One structural consequence: the `break` after `ThrowHelper.ThrowDuplicateKey` in the `InsertionBehavior.ThrowOnExisting` switch case is removed, because a direct `throw` makes it unreachable.

`OrderedDictionary.cs` also guards some dependencies behind `#if SYSTEM_COLLECTIONS`, which this repository does not define.
That excludes the `NonRandomizedStringEqualityComparer` optimisation for string keys, which is not needed here because the only use is keyed by `Language`.

The files otherwise keep upstream's formatting, and are excluded from this repository's code style rules by the `.editorconfig` in this directory.

## Refreshing

Refreshing is not a straight re-copy, because of the modifications above.

1. Re-copy the five files from the upstream paths listed above.
2. Re-apply the provenance header and the `#if !NET9_0_OR_GREATER` guard to each.
3. Re-apply the inlining: replace every `SR.Xxx` with its value from the upstream `Strings.resx`, and every `ThrowHelper.ThrowXxx()` call with the corresponding `throw`, dropping any statement the `throw` makes unreachable.
4. Build for .NET 8, which is what surfaces any newly referenced helper type that also needs copying in.
5. Run the test suite on every target framework.
   `EnumerableExtensionsTests` asserts the behaviour of the dictionary returned by `ToOrderedDictionary` without reference to a target framework, so a run that passes on .NET 8 as well as on the frameworks that use the built-in type is what confirms the two behave identically.

## Removing

When .NET 8 is dropped, delete this directory entirely.
Nothing outside it refers to these types by name.
