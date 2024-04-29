using System.Collections;

namespace Lingua.Internal;

/// <summary>
/// A Dictionary that also allows access to its keys and values in insertion-order, allowing random access.
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
/// <typeparam name="TValue">Value type</typeparam>
public class IndexedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IEquatable<IndexedDictionary<TKey, TValue>> where TKey : notnull
{
    private ValueCollection? _values;
    private readonly Dictionary<TKey, TValue> _dict;
    private readonly List<TKey> _ordering;

    /// <summary>
    ///     Initializes a new instance of the <see cref="IndexedDictionary{TKey,TValue}"/>
    ///     that is empty, has the default initial capacity, and uses the default equality
    ///     comparer for the key type.
    /// </summary>
    public IndexedDictionary() {
        _dict = new Dictionary<TKey, TValue>();
        _ordering = new List<TKey>();
    }
    /// <summary>
    ///     Initializes a new instance of the <see cref="IndexedDictionary{TKey,TValue}"/>
    ///     that contains elements copied from the specified System.Collections.Generic.IDictionary`2
    ///     and uses the default equality comparer for the key type.
    /// </summary>
    /// <param name="dictionary">The System.Collections.Generic.IDictionary`2 whose elements are copied to the new SquidLib.SquidMath.IndexedDictionary.</param>
    public IndexedDictionary(IDictionary<TKey, TValue> dictionary) {
        _dict = new Dictionary<TKey, TValue>(dictionary);
        _ordering = new List<TKey>(_dict.Keys);
    }
    /// <summary>
    ///     Initializes a new instance of the <see cref="IndexedDictionary{TKey,TValue}"/>
    ///     that is empty, has the default initial capacity, and uses the specified System.Collections.Generic.IEqualityComparer`1.
    /// </summary>
    /// <param name="comparer">The System.Collections.Generic.IEqualityComparer`1 implementation to use when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer`1 for the type of the key.</param>
    public IndexedDictionary(IEqualityComparer<TKey> comparer) {
        _dict = new Dictionary<TKey, TValue>(comparer);
        _ordering = new List<TKey>();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexedDictionary{TKey,TValue}"/>
    /// that is empty, has the specified initial capacity, and uses the default equality
    /// comparer for the key type.
    /// </summary>
    /// <param name="capacity">The initial number of elements that the SquidLib.SquidMath.IndexedDictionary can contain.</param>
    public IndexedDictionary(int capacity) {
        _dict = new Dictionary<TKey, TValue>(capacity);
        _ordering = new List<TKey>(capacity);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexedDictionary{TKey,TValue}"/>
    /// that contains elements copied from the specified System.Collections.Generic.IDictionary`2
    /// and uses the specified System.Collections.Generic.IEqualityComparer`1.
    /// </summary>
    /// <param name="dictionary">The System.Collections.Generic.IDictionary`2 whose elements are copied to the new SquidLib.SquidMath.IndexedDictionary.</param>
    /// <param name="comparer">The System.Collections.Generic.IEqualityComparer`1 implementation to use when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer`1 for the type of the key.</param>
    public IndexedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) {
        _dict = new Dictionary<TKey, TValue>(dictionary, comparer);
        _ordering = new List<TKey>(_dict.Keys);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexedDictionary{TKey,TValue}"/>
    /// that is empty, has the specified initial capacity, and uses the specified System.Collections.Generic.IEqualityComparer`1.
    /// </summary>
    /// <param name="capacity">The initial number of elements that the SquidLib.SquidMath.IndexedDictionary can contain.</param>
    /// <param name="comparer">The System.Collections.Generic.IEqualityComparer`1 implementation to use when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer`1 for the type of the key.</param>
    public IndexedDictionary(int capacity, IEqualityComparer<TKey> comparer) {
        _dict = new Dictionary<TKey, TValue>(capacity, comparer);
        _ordering = new List<TKey>(capacity);
    }

    public TValue this[TKey key] {
        get => _dict[key];
        set {
            if (!_dict.ContainsKey(key))
                _ordering.Add(key);
            _dict[key] = value;
        }
    }

    public ICollection<TKey> Keys => _ordering;

    public ICollection<TValue> Values => _values ??= new ValueCollection(_dict, _ordering);

    public int Count => _ordering.Count;

    public bool IsReadOnly => false;

    /// <summary>
    /// Adds at the end of the ordering, or throws an ArgumentException if key is already present.
    /// </summary>
    /// <param name="key">The key to add; should not be present in this already.</param>
    /// <param name="value">The value to associate with the given key.</param>
    public void Add(TKey key, TValue value) {
        _dict.Add(key, value);
        _ordering.Add(key);
    }

    /// <summary>
    /// Adds at the end of the ordering, or throws an ArgumentException if the key in item is already present.
    /// </summary>
    /// <param name="item">The key and value to add; the key should not be present in this already.</param>
    public void Add(KeyValuePair<TKey, TValue> item) {
        ((IDictionary<TKey, TValue>)_dict).Add(item);
        _ordering.Add(item.Key);
    }

    public void Clear() {
        _dict.Clear();
        _ordering.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item) => ((IDictionary<TKey, TValue>)_dict).Contains(item);

    /// <inheritdoc cref="IDictionary{TKey,TValue}.ContainsKey"/>
    public bool ContainsKey(TKey key) => _dict.ContainsKey(key);

    /// <summary>
    /// Copies the entries as KeyValuePair s into the given array; NOT ORDERED.
    /// </summary>
    /// <param name="array">The array to copy into.</param>
    /// <param name="arrayIndex">The first index in array to insert this into.</param>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((IDictionary<TKey, TValue>)_dict).CopyTo(array, arrayIndex);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => new Enumerator(_dict, _ordering);

    public bool Remove(TKey key) {
        if (_dict.Remove(key)) {
            _ordering.Remove(key);
            return true;
        }
        return false;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item) {
        if (((IDictionary<TKey, TValue>)_dict).Remove(item)) {
            _ordering.Remove(item.Key);
            return true;
        }
        return false;
    }

    public bool TryGetValue(TKey key, out TValue value) => _dict.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object? obj) => Equals(obj as IndexedDictionary<TKey, TValue>);

    public bool Equals(IndexedDictionary<TKey, TValue>? other) => other != null &&
                                                                  EqualityComparer<Dictionary<TKey, TValue>>.Default.Equals(_dict, other._dict) &&
                                                                  EqualityComparer<List<TKey>>.Default.Equals(_ordering, other._ordering);

    public override int GetHashCode() {
        var hashCode = -392379326;
        hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<TKey, TValue>>.Default.GetHashCode(_dict);
        hashCode = hashCode * -1521134295 + EqualityComparer<List<TKey>>.Default.GetHashCode(_ordering);
        return hashCode;
    }

    public sealed class ValueCollection : ICollection<TValue>, ICollection, IReadOnlyCollection<TValue> {
        private readonly List<TKey> _items;
        private readonly Dictionary<TKey, TValue> _dictionary;

        public ValueCollection(Dictionary<TKey, TValue> dictionary, List<TKey> items) {
            _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public Enumerator GetEnumerator() => new(_dictionary, _items);

        public void CopyTo(TValue[] array, int index) {
            if (array == null) {
                throw new ArgumentNullException(nameof(array));
            }

            if ((uint)index > array.Length) {
                throw new IndexOutOfRangeException("The index is not within the range for the array.");
            }

            var count = _items.Count;
            if (array.Length - index < count) {
                throw new ArgumentException("The given array is too small for the copied IndexedDictionary.");
            }

            for (var i = 0; i < count; i++) {
                array[index++] = _dictionary[_items[i]];
            }
        }

        public int Count => _dictionary.Count;

        bool ICollection<TValue>.IsReadOnly => true;

        void ICollection<TValue>.Add(TValue item)
            => throw new NotSupportedException("An IndexedDictionary cannot be modified via its values collection.");

        bool ICollection<TValue>.Remove(TValue item) => throw new NotSupportedException("An IndexedDictionary cannot be modified via its values collection.");

        void ICollection<TValue>.Clear()
            => throw new NotSupportedException("An IndexedDictionary cannot be modified via its values collection.");

        bool ICollection<TValue>.Contains(TValue item)
            => _dictionary.ContainsValue(item);

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            => new Enumerator(_dictionary, _items);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection.CopyTo(Array array, int index) {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Rank != 1) throw new ArgumentException("Multidimensional arrays are not supported for this CopyTo() operation.");
            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException("The array must not have a lower bound other than 0.");
            if ((uint)index > (uint)array.Length)
                throw new IndexOutOfRangeException("The index is not within the range for the array.");
            if (array.Length - index < _dictionary.Count)
                throw new ArgumentException("The given array is too small for the copied IndexedDictionary.");

            if (array is TValue[] values) {
                CopyTo(values, index);
            } else {
                var objects = array as object[];
                if (objects is null) {
                    throw new ArgumentException("The given array has an invalid type.");
                }

                var count = _items.Count;
                try
                {
                    for (var i = 0; i < count; i++)
                    {
                        objects[index++] = _dictionary[_items[i]];
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException("The given array has an invalid type.");
                }
            }
        }

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => ((ICollection)_dictionary).SyncRoot;

        public struct Enumerator : IEnumerator<TValue>
        {
            private readonly Dictionary<TKey, TValue> _dictionary;
            private readonly List<TKey> _items;
            private int _index;

            internal Enumerator(Dictionary<TKey, TValue> dictionary, List<TKey> items) {
                _dictionary = dictionary;
                _items = items;
                _index = 0;
                Current = default;
            }

            public void Dispose() {
            }

            public bool MoveNext() {
                if (_index < _items.Count) {
                    Current = _dictionary[_items[_index++]];
                    return true;
                }
                _index = _items.Count;
                Current = default;
                return false;
            }

            public TValue Current { get; private set; }

            object IEnumerator.Current {
                get {
                    if (_index <= 0 || (_index == _items.Count)) {
                        throw new InvalidOperationException("Cannot get current item if the Enumerator hasn't started or has ended.");
                    }

                    return Current;
                }
            }

            void IEnumerator.Reset() {
                _index = 0;
                Current = default;
            }
        }
    }

    public static bool operator ==(IndexedDictionary<TKey, TValue> left, IndexedDictionary<TKey, TValue> right) =>
	    EqualityComparer<IndexedDictionary<TKey, TValue>>.Default.Equals(left, right);

    public static bool operator !=(IndexedDictionary<TKey, TValue> left, IndexedDictionary<TKey, TValue> right) =>
	    !(left == right);

    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator {
        private readonly Dictionary<TKey, TValue> _dictionary;
        private readonly List<TKey> _items;
        private int _index;

        internal Enumerator(Dictionary<TKey, TValue> dictionary, List<TKey> items) {
            _dictionary = dictionary;
            _items = items;
            _index = 0;
            Current = default;
        }

        public void Dispose() {
        }

        public bool MoveNext() {
            if (_index < _items.Count) {
                Current = new KeyValuePair<TKey, TValue>(_items[_index], _dictionary[_items[_index++]]);
                return true;
            }
            _index = _items.Count;
            Current = default;
            return false;
        }

        public KeyValuePair<TKey, TValue> Current { get; private set; }

        object IEnumerator.Current {
            get {
                if (_index <= 0 || (_index == _items.Count)) {
                    throw new InvalidOperationException("Cannot get current item if the Enumerator hasn't started or has ended.");
                }

                return Current;
            }
        }

        void IEnumerator.Reset() {
            _index = 0;
            Current = default;
        }
    }
}
