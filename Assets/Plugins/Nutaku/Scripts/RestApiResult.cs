using System;
using System.Collections.Generic;

namespace Nutaku.Unity
{
    /// <summary>
    /// Stores results from REST API when the value of entry is singular.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestApiSimplexResult<T>
    {
        public int statusCode { get; set; }

        public T entry { get; set; }

        public static implicit operator RestApiResult<T>(RestApiSimplexResult<T> other)
        {
            return new RestApiResult<T>()
            {
                statusCode = other.statusCode,
                entry = new List<T>() { other.entry },
            };
        }
    }

    /// <summary>
    /// Stores results from REST API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestApiResult<T> : RestApiSimplexResult<List<T>>
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetEntry()
        {
            return entry;
        }

        public T GetFirstEntry()
        {
            if (entry == null)
                return default(T);

            var e = entry.GetEnumerator();

            if (e.MoveNext())
                return e.Current;
            return default(T);
        }

        internal RestApiResult<U> Convert<U>(Func<T, U> converter)
        {
            List<U> convertedList = new List<U> ();
            foreach (T t in entry)
                convertedList.Add (converter (t));

            return new RestApiResult<U>()
            {
                statusCode = statusCode,
                entry = entry == null ? null : convertedList
            };
        }
    }
}
