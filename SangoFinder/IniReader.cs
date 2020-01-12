using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SangoFinder
{
    public static class IniReader
    {
        public static List<NameValueCollection> GetCollecions(string fileName)
        {
            var collectionList = new List<NameValueCollection>();

            var strs = File.ReadAllLines(fileName, Encoding.GetEncoding("BIG5")).Where(s => !string.IsNullOrEmpty(s)).ToList();
            var numOfItem = strs.Count(s => s == "[ITEM]");
            var itemLen = strs.Count() / numOfItem;
            var strList = new List<string[]>();
            var idx = 0;
            for (int i = 0; i < numOfItem; i++)
            {
                strList.Add(strs.Skip(idx).Take(itemLen).ToArray());
                idx += itemLen;
            }

            foreach (var item in strList)
            {
                collectionList.Add(GetItemCollection(item));
            }
            return collectionList;
        }

        private static NameValueCollection GetItemCollection(string[] item)
        {
            var collection = new NameValueCollection();
            for (int i = 1; i < item.Count(); i++)
            {
                var keyValue = item[i].Split('=');
                collection.Add(keyValue[0].Trim(), keyValue[1].Trim());
            }
            return collection;
        }
    }
}
