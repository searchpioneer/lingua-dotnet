// Supplies the resource strings that the vendored dotnet/runtime sources in this
// directory expect. The values are copied verbatim from
// src/libraries/System.Collections/src/Resources/Strings.resx so that the exception
// messages match those thrown by the built-in OrderedDictionary<TKey, TValue> on
// .NET 9 or greater.
//
// See README.md in this directory before changing anything here.

#if !NET9_0_OR_GREATER
namespace System
{
    internal static class SR
    {
        public const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";
        public const string Arg_HTCapacityOverflow = "Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table.";
        public const string Arg_KeyNotFoundWithKey = "The given key '{0}' was not present in the dictionary.";
        public const string Arg_NonZeroLowerBound = "The lower bound of target array must be zero.";
        public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";
        public const string Arg_WrongType = "The value '{0}' is not of type '{1}' and cannot be used in this generic collection.";
        public const string Argument_AddingDuplicate = "An item with the same key has already been added. Key: {0}";
        public const string Argument_IncompatibleArrayType = "Target array type is not compatible with the type of items in the collection.";
        public const string InvalidOperation_ConcurrentOperationsNotSupported = "Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.";
        public const string InvalidOperation_EnumFailedVersion = "Collection was modified after the enumerator was instantiated.";

        public static string Format(string resourceFormat, object? p1) =>
            string.Format(resourceFormat, p1);

        public static string Format(string resourceFormat, object? p1, object? p2) =>
            string.Format(resourceFormat, p1, p2);
    }
}
#endif
