// Processed by SolutionConv >>>
//
// 本ソースファイルは、公開時の所定の手続きとして一部のセンシティブな情報をマスキングしています。
// 元データの機微に触れる可能性がある箇所を伏せ字化したものであり、
// リリース版との処理内容に実質的な差異が生じない範囲で調整を加えています。
//

using System;
using System.Collections.Generic;
using System.Linq;
using HLTStudio.Commons;

namespace HLTStudio
{
	static class Program
	{
		/// /////////
		/// //////////// //// ///////
		/// //////////
		[STAThread]
		static void Main()
		{
			ProcMain.CUIMain(ar =>
			{
				GUIProcMain.GUIMain(() => new MainWin());
			});
		}
	}
}

//
// <<< Processed by SolutionConv
//