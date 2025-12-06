// Processed by SolutionConv >>>
//
// 本ソースファイルは、公開時の所定の手続きとして一部のセンシティブな情報をマスキングしています。
// 元データの機微に触れる可能性がある箇所を伏せ字化したものであり、
// リリース版との処理内容に実質的な差異が生じない範囲で調整を加えています。
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLTStudio.Tools
{
	public static class DictionaryTools
	{
		public static Dictionary<string, V> Create<V>()
		{
			return new Dictionary<string, V>(new StringTools.IEComp());
		}

		public static Dictionary<string, V> CreateIgnoreCase<V>()
		{
			return new Dictionary<string, V>(new StringTools.IECompIgnoreCase());
		}

		public static V Get<K, V>(Dictionary<K, V> dict, K key, V defval)
		{
			if (dict.ContainsKey(key))
			{
				return dict[key];
			}
			return defval;
		}

		public static void Put<K, V>(Dictionary<K, V> dict, K key, V value)
		{
			if (dict.ContainsKey(key))
			{
				dict[key] = value;
			}
			else
			{
				dict.Add(key, value);
			}
		}
	}
}

//
// <<< Processed by SolutionConv
//