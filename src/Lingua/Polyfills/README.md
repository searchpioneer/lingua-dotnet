# Polyfills

`OrderedDictionary<TKey, TValue>` was introduced in .NET 9.
Lingua also targets .NET 8, so this directory vendors the runtime implementation to provide the same type there.

Every file is wrapped in `#if !NET9_0_OR_GREATER`, so on .NET 9 or greater none of it compiles and the built-in type is used instead.
Because the vendored type keeps its original `System.Collections.Generic` namespace, calling code needs no conditional compilation: `OrderedDictionary<TKey, TValue>` resolves to the vendored copy on .NET 8 and to the framework type on .NET 9 or greater.

## Provenance

Taken from [dotnet/runtime](https://github.com/dotnet/runtime) at commit `9f9485d11b6047bf9f9aefa0aa5e6c412a929a7c` on 2026-07-28, and licensed under the MIT license.

| File | Upstream path |
| --- | --- |
| `OrderedDictionary.cs` | `src/libraries/System.Collections/src/System/Collections/Generic/OrderedDictionary.cs` |
| `OrderedDictionaryDebugView.cs` | `src/libraries/System.Collections/src/System/Collections/Generic/OrderedDictionaryDebugView.cs` |
| `ThrowHelper.cs` | `src/libraries/System.Collections/src/System/Collections/ThrowHelper.cs` |
| `HashHelpers.cs` | `src/libraries/System.Private.CoreLib/src/System/Collections/HashHelpers.cs` |
| `ICollectionDebugView.cs` | `src/libraries/System.Private.CoreLib/src/System/Collections/Generic/ICollectionDebugView.cs` |
| `EnumerableHelpers.cs` | `src/libraries/Common/src/System/Collections/Generic/EnumerableHelpers.cs` |
| `SR.cs` | not vendored, see below |

Everything except `OrderedDictionary.cs` itself is there only because `OrderedDictionary.cs` references it.

The six vendored files are byte for byte identical to upstream apart from a provenance header and the `#if !NET9_0_OR_GREATER` guard.
Leaving them otherwise untouched, including their formatting, keeps refreshing them a straight re-copy.
They are excluded from this repository's code style rules by the `.editorconfig` in this directory.

`SR.cs` is not vendored.
Upstream generates it from `Strings.resx`, so it is hand written here with the string values copied verbatim from `src/libraries/System.Collections/src/Resources/Strings.resx`.
That keeps the exception messages identical to the ones the built-in type throws on .NET 9 or greater.

`OrderedDictionary.cs` guards its remaining dependencies behind `#if SYSTEM_COLLECTIONS`, which this repository does not define.
That excludes the `NonRandomizedStringEqualityComparer` optimisation for string keys, which is not needed here because the only use is keyed by `Language`.

## Refreshing

1. Re-copy the six files from the upstream paths above.
2. Re-apply the provenance header and the `#if !NET9_0_OR_GREATER` guard to each.
3. Build for .NET 8, which is what surfaces any newly referenced helper type that also needs vendoring.
4. Check whether any new `SR` member is referenced, and if so add it to `SR.cs` from the upstream `Strings.resx`.
5. Run the test suite on every target framework.
   `EnumerableExtensionsTests` asserts the behaviour of the dictionary returned by `ToOrderedDictionary` without reference to a target framework, so a passing run on both .NET 8 and .NET 9 is what confirms the vendored copy and the built-in type behave identically.

## Removing

When .NET 8 is dropped, delete this directory entirely.
Nothing outside it refers to these types by name.
