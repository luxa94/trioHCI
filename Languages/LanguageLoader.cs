using System;
using System.Collections.Generic;
using System.Resources;
using System.Threading;
using HCI.Properties;

namespace HCI.Languages
{
    public static class LanguageLoader
    {

        //use this dictionary to cache resouce
        private static readonly Dictionary<string, string> CachedText = new Dictionary<string, string>();
        public static void FlushCache()
        {
            CachedText.Clear();
        }
        // used for thread-safety
        private static readonly Object ThreadLock = new Object();
        /// <summary>
        /// Gets the value from the dictionary with the provided key.
        /// </summary>
        /// <param name="key">The key to search from the Dictionary.</param>
        /// <returns>The value of the provided key.</returns>
        public static string GetText(string key)
        {
            try
            {
                lock (ThreadLock)
                {
                    if (CachedText.ContainsKey(key))
                        return CachedText[key];
                    string strLookup = Resources.ResourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture);
                    CachedText.Add(key, strLookup);
                    return strLookup;
                }
            }
            catch (System.ArgumentNullException)
            {
                return String.Format(System.Globalization.CultureInfo.CurrentCulture, "Error 0001 - Unknown Error.");
            }
            catch (System.InvalidOperationException)
            {
                return String.Format(System.Globalization.CultureInfo.CurrentCulture, "Error 0002 - Unknown Error.");
            }
            catch (System.Resources.MissingManifestResourceException)
            {
                return String.Format(System.Globalization.CultureInfo.CurrentCulture, "Error 0003 - Unknown Error.");
            }
        }
        /// <summary>
        /// Gets the value ofr the provided key, and if not found, the default string will
        /// be shown.
        /// </summary>
        /// <param name="key">The key to search from the Dictionary.</param>
        /// <param name="defaultstr">the string that'll be shown if the key is not found.</param>
        /// <returns>The value/default string.</returns>
        public static string GetText(string key, string defaultstr)
        {
            try
            {
                lock (ThreadLock)
                {
                    if (CachedText.ContainsKey(key))
                        return CachedText[key];
                    string result = Resources.ResourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture);

                    if (String.IsNullOrEmpty(result))
                    {
                        return defaultstr;
                    }
                    else
                    {
                        CachedText.Add(key, result);
                        return result;
                    }
                }
            }
            catch (System.ArgumentNullException)
            {
                return defaultstr;
            }
            catch (System.InvalidOperationException)
            {
                return defaultstr;
            }
            catch (System.Resources.MissingManifestResourceException)
            {
                return defaultstr;
            }
        }
        public static ResourceManager ResourceManager
        {
            get { return Resources.ResourceManager; }
        }
    }
}
