namespace MeetEric.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Collections;
    using Diagnostics;

    public static class MeetEricExtensions
    {
        public static bool IsTrue(this bool? value)
        {
            return value.HasValue && value.Value;
        }

        public static bool IsTrueOrNoValue(this bool? value)
        {
            return !value.HasValue || value.Value;
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string SafeSubstring(this string s, int start, int length)
        {
            var result = string.Empty;

            if (s != null && s.Length >= start + length)
            {
                result = s.Substring(start, length);
            }

            return result;
        }

        public static string MaxLength(this string s, int length)
        {
            if (s != null && s.Length > length)
            {
                s = s.Substring(0, length);
            }

            return s;
        }

        public static IEnumerable<string> AsLines(this string value)
        {
            StringReader reader = new StringReader(value);
            string line = string.Empty;

            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    yield return line;
                }
            }
        }

        public static T? AsEnum<T>(this string value)
            where T : struct
        {
            T result = default(T);
            T? nullable = null;

            if (Enum.TryParse<T>(value, true, out result))
            {
                nullable = result;
            }

            return nullable;
        }

        public static IEnumerable<ICollection<T>> Batch<T>(this IEnumerable<T> batch, int batchSize)
        {
            int batchCount = 0;
            var forwardOnly = new ForwardOnlyEnumerable<T>(batch);

            while (true)
            {
                var result = forwardOnly.Take(batchSize)
                    .ToList();

                if (!result.Any())
                {
                    break;
                }

                yield return result;
                batchCount++;
            }

            forwardOnly.Close();
        }

        public static Stream AsStream(this string content)
        {
            var returnStream = new MemoryStream();

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(content);
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(returnStream);
                returnStream.Seek(0, SeekOrigin.Begin);
            }

            return returnStream;
        }

        public static Stream AsStream(this byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static async Task<string> AsString(this Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static void FlattenAndThrow(this AggregateException e)
        {
            var flattenExceptions = e.Flatten();
            if (flattenExceptions.InnerExceptions.Count == 1)
            {
                throw flattenExceptions.InnerExceptions.First();
            }

            throw new AggregateException(e);
        }

        public static Uri AsBaseUri(this Uri uri)
        {
            if (!uri.AbsoluteUri.EndsWith("/"))
            {
                uri = new Uri($"{uri.AbsoluteUri}/");
            }

            return uri;
        }

        public static void LogCancel(this ILoggingContext log, string reason)
        {
            var context = new Dictionary<string, string>()
            {
                { "Reason", reason }
            };

            log.LogEvent("IssueCancellation", context);
        }
    }
}
