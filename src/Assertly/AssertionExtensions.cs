using Assertly.Collections;
using Assertly.Primitives;
using Assertly.Types;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Assertly;
public static class AssertionExtensions
{
    public static BooleanAssertions Assert(this bool? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new BooleanAssertions(null);
        }

        return new BooleanAssertions(actualValue.Value);
    }
    public static BooleanAssertions Assert(this bool actualValue)
    {
        return new BooleanAssertions(actualValue);
    }
    public static ByteAssertions Assert(this byte? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new ByteAssertions(null);
        }

        return new ByteAssertions(actualValue.Value);
    }
    public static ByteAssertions Assert(this byte actualValue)
    {
        return new ByteAssertions(actualValue);
    }
    public static Int16Assertions Assert(this short? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new Int16Assertions(null);
        }

        return new Int16Assertions(actualValue.Value);
    }
    public static Int16Assertions Assert(this short actualValue)
    {
        return new Int16Assertions(actualValue);
    }
    public static Int32Assertions Assert(this int? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new Int32Assertions(null);
        }

        return new Int32Assertions(actualValue.Value);
    }
    public static Int32Assertions Assert(this int actualValue)
    {
        return new Int32Assertions(actualValue);
    }
    public static Int64Assertions Assert(this long? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new Int64Assertions(null);
        }

        return new Int64Assertions(actualValue.Value);
    }
    public static Int64Assertions Assert(this long actualValue)
    {

        return new Int64Assertions(actualValue);
    }
    public static SByteAssertions Assert(this sbyte? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new SByteAssertions(null);
        }

        return new SByteAssertions(actualValue.Value);
    }
    public static SByteAssertions Assert(this sbyte actualValue)
    {

        return new SByteAssertions(actualValue);
    }
    public static SingleAssertions Assert(this float? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new SingleAssertions(null);
        }

        return new SingleAssertions(actualValue.Value);
    }
    public static SingleAssertions Assert(this float actualValue)
    {

        return new SingleAssertions(actualValue);
    }
    public static DecimalAssertions Assert(this decimal? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new DecimalAssertions(null);
        }

        return new DecimalAssertions(actualValue.Value);
    }
    public static DecimalAssertions Assert(this decimal actualValue)
    {

        return new DecimalAssertions(actualValue);
    }
    public static DoubleAssertions Assert(this double? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new DoubleAssertions(null);
        }

        return new DoubleAssertions(actualValue.Value);
    }
    public static DoubleAssertions Assert(this double actualValue)
    {

        return new DoubleAssertions(actualValue);
    }
    public static UInt16Assertions Assert(this ushort? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new UInt16Assertions(null);
        }

        return new UInt16Assertions(actualValue.Value);
    }
    public static UInt16Assertions Assert(this ushort actualValue)
    {

        return new UInt16Assertions(actualValue);
    }
    public static UInt32Assertions Assert(this uint? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new UInt32Assertions(null);
        }

        return new UInt32Assertions(actualValue.Value);
    }
    public static UInt32Assertions Assert(this uint actualValue)
    {
        return new UInt32Assertions(actualValue);
    }
    public static UInt64Assertions Assert(this ulong? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new UInt64Assertions(null);
        }

        return new UInt64Assertions(actualValue.Value);
    }
    public static UInt64Assertions Assert(this ulong actualValue)
    {
        return new UInt64Assertions(actualValue);
    }
    public static DateOnlyAssertions Assert(this DateOnly? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new DateOnlyAssertions(null);
        }

        return new DateOnlyAssertions(actualValue.Value);
    }
    public static DateOnlyAssertions Assert(this DateOnly actualValue)
    {
        return new DateOnlyAssertions(actualValue);
    }
    public static TimeOnlyAssertions Assert(this TimeOnly? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new TimeOnlyAssertions(null);
        }

        return new TimeOnlyAssertions(actualValue.Value);
    }
    public static TimeOnlyAssertions Assert(this TimeOnly actualValue)
    {
        return new TimeOnlyAssertions(actualValue);
    }
    public static DateTimeAssertions Assert(this DateTime? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new DateTimeAssertions(null);
        }

        return new DateTimeAssertions(actualValue.Value);
    }
    public static DateTimeAssertions Assert(this DateTime actualValue)
    {
        return new DateTimeAssertions(actualValue);
    }
    public static DateTimeOffsetAssertions Assert(this DateTimeOffset? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new DateTimeOffsetAssertions(null);
        }

        return new DateTimeOffsetAssertions(actualValue.Value);
    }
    public static DateTimeOffsetAssertions Assert(this DateTimeOffset actualValue)
    {
        return new DateTimeOffsetAssertions(actualValue);
    }
    public static TimeSpanAssertions Assert(this TimeSpan? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new TimeSpanAssertions(null);
        }

        return new TimeSpanAssertions(actualValue.Value);
    }
    public static TimeSpanAssertions Assert(this TimeSpan actualValue)
    {
        return new TimeSpanAssertions(actualValue);
    }
    public static GuidAssertions Assert(this Guid? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new GuidAssertions(null);
        }

        return new GuidAssertions(actualValue.Value);
    }
    public static GuidAssertions Assert(this Guid actualValue)
    {
        return new GuidAssertions(actualValue);
    }
    public static EnumAssertions<TEnum> Assert<TEnum>(this TEnum? actualValue) where TEnum : struct, Enum
    {
        if (!actualValue.HasValue)
        {
            return new EnumAssertions<TEnum>(default);
        }

        return new EnumAssertions<TEnum>(actualValue.Value);
    }
    public static EnumAssertions<TEnum> Assert<TEnum>(this TEnum actualValue) where TEnum : struct, Enum
    {
        return new EnumAssertions<TEnum>(actualValue);
    }
    public static StringAssertions Assert([NotNull] this string actualValue)
    {
        return new StringAssertions(actualValue);
    }
    public static ComparableTypeAssertions<T> Assert<T>([NotNull] this IComparable<T> comparableValue)
    {
        return new ComparableTypeAssertions<T>(comparableValue);
    }
    public static ObjectAssertions Assert([NotNull] this object subject)
    {
        return new ObjectAssertions(subject);
    }
    public static TypeAssertions Assert([NotNull] this Type subject)
    {
        return new TypeAssertions(subject);
    }
    public static AssemblyAssertions Assert([NotNull] this Assembly assembly)
    {
        return new AssemblyAssertions(assembly);
    }
    public static GenericCollectionAssertions<T> Assert<T>([NotNull] this IEnumerable<T> actualValue)
    {
        return new GenericCollectionAssertions<T>(actualValue);
    }
    public static FunctionAssertions<T> Assert<T>([NotNull] this Func<T> func)
    {
        return new FunctionAssertions<T>(func);
    }
}
