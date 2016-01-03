﻿/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Utilities.DataTypes
{
    /// <summary>
    /// ConcurrentDictionary extensions
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ConcurrentDictionaryExtensions
    {
        /// <summary>
        /// Copies the dictionary to another dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="target">The target dictionary.</param>
        /// <returns>This</returns>
        public static ConcurrentDictionary<TKey, TValue> CopyTo<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, ConcurrentDictionary<TKey, TValue> target)
        {
            dictionary = dictionary ?? new ConcurrentDictionary<TKey, TValue>();
            if (target == null)
                return dictionary;
            Parallel.ForEach(dictionary, x => target.SetValue(x.Key, x.Value));
            return dictionary;
        }

        /// <summary>
        /// Gets the value from a dictionary or the default value if it isn't found
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Dictionary to get the value from</param>
        /// <param name="key">Key to look for</param>
        /// <param name="default">Default value if the key is not found</param>
        /// <returns>
        /// The value associated with the key or the default value if the key is not found
        /// </returns>
        public static TValue GetValue<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            if (dictionary == null)
                return defaultValue;
            TValue ReturnValue = defaultValue;
            return dictionary.TryGetValue(key, out ReturnValue) ? ReturnValue : defaultValue;
        }

        /// <summary>
        /// Sets the value in a dictionary
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Dictionary to set the value in</param>
        /// <param name="key">Key to look for</param>
        /// <param name="value">Value to add</param>
        /// <returns>The dictionary</returns>
        public static ConcurrentDictionary<TKey, TValue> SetValue<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                return new ConcurrentDictionary<TKey, TValue>();
            dictionary.AddOrUpdate(key, value, (x, y) => value);
            return dictionary;
        }
    }
}