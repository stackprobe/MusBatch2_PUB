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
using System.Runtime.InteropServices;

namespace HLTStudio
{
	public static class Win32
	{
		[FlagsAttribute]
		public enum ExecutionState : uint // ///// // //////// //// / /////// // ////////
		{
			ES_SYSTEM_REQUIRED = 1,
			ES_DISPLAY_REQUIRED = 2,
			ES_USER_PRESENT = 4,
			ES_AWAYMODE_REQUIRED = 0x40,
			ES_CONTINUOUS = 0x80000000,
		}

		[DllImport("kernel32.dll")]
		public static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);
	}
}

//
// <<< Processed by SolutionConv
//