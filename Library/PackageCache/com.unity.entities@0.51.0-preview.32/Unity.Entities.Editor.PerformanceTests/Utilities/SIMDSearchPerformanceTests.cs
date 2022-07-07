using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity.Collections;
using Unity.PerformanceTesting;

// ReSharper disable StringLiteralTypo
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable AccessToDisposedClosure
namespace Unity.Entities.Editor.PerformanceTests
{
    [TestFixture(Category = Categories.Performance)]
    class SIMDSearchPerformanceTests
    {
        const int k_WarmupCount = 10;
        const int k_MeasurementCount = 250;
        const int k_StringsCount = 1_000_000;

        [Test, Performance]
        public void SIMD_Contains()
        {
            using var sourceStrings = GenerateFixedStrings(k_StringsCount);
            using var patterns = new NativeList<FixedString64Bytes>(1, Allocator.TempJob) { "100" }; // Matches *some* names
            var result = default(NativeBitArray);

            Measure
                .Method(() => result = SIMDSearch.GetFilteredMatches(sourceStrings, patterns))
                .CleanUp(() => result.Dispose())
                .MeasurementCount(k_MeasurementCount)
                .WarmupCount(k_WarmupCount)
                .Run();

            if (result.IsCreated)
                result.Dispose();
        }

        [Test, Performance, Ignore("Only used for comparison; do not run on CI")]
        public void String_Contains(
            [Values(StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase)]
            StringComparison stringComparisonMethod
            )
        {
            var sourceStrings = GenerateStrings(k_StringsCount);
            var patterns = new[] { "100" }; // Matches *some* names
            var result = default(BitArray);

            Measure
                .Method(() => result = ContainsUsingToString(sourceStrings, patterns, stringComparisonMethod))
                .MeasurementCount(k_MeasurementCount)
                .WarmupCount(k_WarmupCount)
                .Run();
        }

        // Approximates the search algorithm on strings, using ToString()
        static BitArray ContainsUsingToString(string[] sourceStrings, string[] patterns, StringComparison comparisonMethod)
        {
            var result = new BitArray(sourceStrings.Length);

            // Threading, for comparison fairness
            Parallel.For(0, sourceStrings.Length,
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                i =>
                {
                    var entry = sourceStrings[i];
                    var patternIndex = 0;
                    var matches = true;
                    while (matches && patternIndex < patterns.Length)
                    {
                        matches = entry.IndexOf(patterns[patternIndex++], comparisonMethod) != -1;
                    }

                    result.Set(i, matches);
                });

            return result;
        }

        static string[] GenerateStrings(int count)
        {
            var result = new string[count];

            for (var i = 0; i < count; ++i)
            {
                result[i] = $"Entity({i}:1)";
            }

            return result;
        }

        static NativeList<FixedString64Bytes> GenerateFixedStrings(int count, Allocator allocator = Allocator.TempJob)
        {
            var result = new NativeList<FixedString64Bytes>(count, allocator);

            for (var i = 0; i < count; ++i)
            {
                result.Add($"Entity({i}:1)");
            }

            return result;
        }
    }
}
