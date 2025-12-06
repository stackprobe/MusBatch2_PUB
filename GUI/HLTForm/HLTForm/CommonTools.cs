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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace HLTStudio
{
	public static class CommonTools
	{
		public static Process StartProc(string file, string args = "")
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = file;
			psi.Arguments = args;
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.WorkingDirectory = Path.Combine(BootTools.SelfDir, @"..\CUI");

			return Process.Start(psi);
		}

		[DllImport("user32.dll")]
		private static extern int GetAsyncKeyState(int vKey);
		private static readonly int VK_RCONTROL = 0x000000a3;

		public static bool IsRCtrlPressed()
		{
			return GetAsyncKeyState(VK_RCONTROL) != 0;
		}
	}
}

//
// <<< Processed by SolutionConv
//