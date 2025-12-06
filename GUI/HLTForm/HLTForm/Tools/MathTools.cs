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
	public static class MathTools
	{
		private static Random _random = new Random();

		public static int Random(int modulo)
		{
			return _random.Next(modulo);
		}

		public static int Random(int minval, int maxval)
		{
			return _random.Next(minval, maxval);
		}
	}
}

//
// <<< Processed by SolutionConv
//