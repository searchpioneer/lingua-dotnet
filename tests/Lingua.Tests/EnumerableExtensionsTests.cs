using FluentAssertions;
using Lingua.Internal;
using Xunit;

namespace Lingua.Tests;

/// <summary>
/// Pins the observable behaviour of the dictionary returned by
/// <see cref="EnumerableExtensions.ToOrderedDictionary{TKey,TValue}"/>.
/// <para />
/// The implementation differs by target framework: .NET 9 or greater uses the built-in
/// <c>OrderedDictionary&lt;TKey, TValue&gt;</c>, earlier frameworks use the copy of the runtime
/// implementation vendored under Polyfills. These tests are deliberately framework agnostic, so
/// running the suite on every target framework asserts that the vendored copy is complete and
/// behaves identically to the built-in type.
/// </summary>
public class EnumerableExtensionsTests
{
	private static IDictionary<string, TValue> Build<TValue>(params (string Key, TValue Value)[] entries) =>
		entries.Select(e => new KeyValuePair<string, TValue>(e.Key, e.Value)).ToOrderedDictionary();

	private static IDictionary<string, int> Sample() => Build(("b", 2), ("a", 1), ("c", 3));

	[Fact]
	public void PreservesInsertionOrderRatherThanKeyOrder()
	{
		var dictionary = Sample();

		dictionary.Select(kv => kv.Key).Should().Equal("b", "a", "c");
		dictionary.Select(kv => kv.Value).Should().Equal(2, 1, 3);
		dictionary.Keys.Should().Equal("b", "a", "c");
		dictionary.Values.Should().Equal(2, 1, 3);
	}

	[Fact]
	public void SupportsPositionalAccessUsedByTheDetector()
	{
		// DetectLanguageFromLanguages relies on First() being the highest confidence
		// value and ElementAt(1) being the second highest
		var dictionary = Build(("de", 0.81), ("en", 0.19));

		dictionary.Count.Should().Be(2);
		dictionary.First().Key.Should().Be("de");
		dictionary.ElementAt(1).Value.Should().Be(0.19);
		dictionary["de"].Should().Be(0.81);
	}

	[Fact]
	public void LookupBehavesLikeADictionary()
	{
		var dictionary = Sample();

		dictionary.ContainsKey("a").Should().BeTrue();
		dictionary.ContainsKey("zz").Should().BeFalse();
		dictionary.TryGetValue("a", out var value).Should().BeTrue();
		value.Should().Be(1);
		dictionary.TryGetValue("zz", out var missing).Should().BeFalse();
		missing.Should().Be(0);
		dictionary.Contains(new KeyValuePair<string, int>("b", 2)).Should().BeTrue();
		dictionary.Contains(new KeyValuePair<string, int>("b", 9)).Should().BeFalse();
		dictionary.IsReadOnly.Should().BeFalse();

		var lookup = () => dictionary["zz"];
		lookup.Should().Throw<KeyNotFoundException>();
	}

	[Fact]
	public void AssigningAnExistingKeyOverwritesInPlace()
	{
		var dictionary = Sample();

		dictionary["a"] = 99;

		dictionary["a"].Should().Be(99);
		dictionary.Count.Should().Be(3);
		dictionary.Select(kv => kv.Key).Should().Equal("b", "a", "c");
	}

	[Fact]
	public void AssigningANewKeyAppendsIt()
	{
		var dictionary = Sample();

		dictionary["z"] = 26;

		dictionary.Select(kv => kv.Key).Should().Equal("b", "a", "c", "z");
	}

	[Fact]
	public void AddingADuplicateKeyThrows()
	{
		var dictionary = Sample();

		var add = () => dictionary.Add("a", 5);

		add.Should().Throw<ArgumentException>();
	}

	[Fact]
	public void RemovedKeyIsAppendedWhenAddedAgain()
	{
		var dictionary = Sample();

		dictionary.Remove("a").Should().BeTrue();
		dictionary.Remove("zz").Should().BeFalse();
		dictionary.Add("a", 111);

		dictionary.Select(kv => kv.Key).Should().Equal("b", "c", "a");
	}

	[Fact]
	public void CopyToWritesEntriesInInsertionOrder()
	{
		// a removal frees a slot that the next insert reuses, so a CopyTo that delegates
		// to the backing Dictionary would emit entries out of insertion order here
		var dictionary = Sample();
		dictionary.Remove("a");
		dictionary.Add("z", 26);

		var array = new KeyValuePair<string, int>[4];
		dictionary.CopyTo(array, 1);

		array[0].Should().Be(default(KeyValuePair<string, int>));
		array.Skip(1).Select(kv => kv.Key).Should().Equal("b", "c", "z");
		array.Skip(1).Select(kv => kv.Key).Should().Equal(dictionary.Select(kv => kv.Key));
	}

	[Fact]
	public void CopyToValidatesItsArguments()
	{
		var dictionary = Sample();

		var nullArray = () => dictionary.CopyTo(null!, 0);
		nullArray.Should().Throw<ArgumentNullException>();

		var negativeIndex = () => dictionary.CopyTo(new KeyValuePair<string, int>[3], -1);
		negativeIndex.Should().Throw<ArgumentOutOfRangeException>();

		var indexPastEnd = () => dictionary.CopyTo(new KeyValuePair<string, int>[3], 4);
		indexPastEnd.Should().Throw<ArgumentException>();

		var arrayTooSmall = () => dictionary.CopyTo(new KeyValuePair<string, int>[2], 0);
		arrayTooSmall.Should().Throw<ArgumentException>();

		var noRoomFromIndex = () => dictionary.CopyTo(new KeyValuePair<string, int>[4], 2);
		noRoomFromIndex.Should().Throw<ArgumentException>();
	}

	[Fact]
	public void KeysAndValuesCollectionsAreReadOnly()
	{
		var dictionary = Sample();

		dictionary.Keys.IsReadOnly.Should().BeTrue();
		dictionary.Values.IsReadOnly.Should().BeTrue();

		var addKey = () => dictionary.Keys.Add("injected");
		addKey.Should().Throw<NotSupportedException>();

		var removeKey = () => dictionary.Keys.Remove("b");
		removeKey.Should().Throw<NotSupportedException>();

		var clearKeys = () => dictionary.Keys.Clear();
		clearKeys.Should().Throw<NotSupportedException>();

		var addValue = () => dictionary.Values.Add(9);
		addValue.Should().Throw<NotSupportedException>();
	}

	[Fact]
	public void KeysAndValuesTrackSubsequentMutations()
	{
		var dictionary = Sample();
		var keys = dictionary.Keys;
		var values = dictionary.Values;

		dictionary.Remove("a");
		dictionary["z"] = 26;

		keys.Should().Equal("b", "c", "z");
		values.Should().Equal(2, 3, 26);
		keys.Count.Should().Be(3);
		values.Count.Should().Be(3);
	}

	[Fact]
	public void ClearEmptiesTheDictionary()
	{
		var dictionary = Sample();

		dictionary.Clear();

		dictionary.Should().BeEmpty();
		dictionary.Count.Should().Be(0);
		dictionary.Keys.Should().BeEmpty();
		dictionary.Values.Should().BeEmpty();
	}

	[Fact]
	public void EmptySourceProducesEmptyDictionary()
	{
		var dictionary = Build<int>();

		dictionary.Should().BeEmpty();
		dictionary.Count.Should().Be(0);
		dictionary.CopyTo([], 0);
	}
}
