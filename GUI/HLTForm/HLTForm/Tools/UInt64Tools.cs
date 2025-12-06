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
	public static class UInt64Tools
	{
		public const UInt64 IMAX = 1000000000ul; // ////
		public const UInt64 IMAX_64 = 1000000000000000000ul; // /////

		public static bool IsRange(UInt64 value, UInt64 minval = 0, UInt64 maxval = IMAX_64)
		{
			return minval <= value && value <= maxval;
		}

		public static UInt64 ToUInt64(string str, UInt64 minval = 0, UInt64 maxval = IMAX_64, UInt64 defval = 0)
		{
			try
			{
				UInt64 value = UInt64.Parse(str);

				if (IsRange(value, minval, maxval))
					return value;
			}
			catch
			{ }

			return defval;
		}
	}
}

//
// <<< Processed by SolutionConv
//