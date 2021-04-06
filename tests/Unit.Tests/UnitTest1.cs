using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace tests
{
    public class UnitTest
    {
        [Fact]
        public void Should_be_equal()
        {
            int expectedValue = 2;
            int actualValue = 2;

            Assert.Equal(expectedValue, actualValue);
        }
    }
    public class InlineDataTest
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 5)]
        public void Should_be_equal(int expectedValue, int actualValue)
        {
            Assert.Equal(expectedValue, actualValue);
        }
    }
    public class MemberDataTest
    {
        public static IEnumerable<object[]> Data => new[]{
            new object[] {1,2,false},
            new object[] {2,2, true},
            new object[] {3,3, true},
        };
        public static TheoryData<int, int, bool> TypedData => new TheoryData<int, int, bool>{
            {3,2, false},
            {2,3, false},
            {5,5, true},
        };
        [Theory]
        [MemberData(nameof(Data))]
        [MemberData(nameof(TypedData))]
        [MemberData(nameof(ExternalData.GetData), 10, MemberType = typeof(ExternalData))]
        [MemberData(nameof(ExternalData.TypedData), MemberType = typeof(ExternalData))]
        public void Should_be_equal(int expectedValue, int actualValue, bool shouldBeEqual)
        {
            if (shouldBeEqual)
            {
                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.NotEqual(expectedValue, actualValue);
            }
        }
    }
    public class ExternalData
    {
        public static IEnumerable<object[]> GetData(int start) => new[] {
            new object[] {start, start, true},
            new object[] {start, start + 1, false},
            new object[] {start + 1, start + 1, true}
        };
        public static TheoryData<int, int, bool> TypedData => new TheoryData<int, int, bool>{
            {20, 30, false},
            {50, 50, true},
            {30, 40, false}
        };
    }
    public class ClassDataTest
    {
        [Theory]
        [ClassData(typeof(TheryDataClass))]
        [ClassData(typeof(TheoryTypedDataClass))]
        public void Should_be_equal(int expectedValue, int actualValue, bool shouldBeEqual)
        {
            if (shouldBeEqual)
            {
                Assert.Equal(expectedValue, actualValue);
            }
            else
            {
                Assert.NotEqual(expectedValue, actualValue);
            }
        }
    }
    public class TheryDataClass : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, 2, true };
            yield return new object[] { 3, 3, true };
            yield return new object[] { 3, 5, false };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public class TheoryTypedDataClass : TheoryData<int, int, bool>
    {
        public TheoryTypedDataClass()
        {
            Add(102, 104, false);
        }
    }

}
