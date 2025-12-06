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
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

// / //// / /////////////

namespace HLTStudio
{
	// //// / / /////////////

	public static class GlobalProcMtx
	{
		private static Mutex ProcMtx;

		public static bool Create(string procMtxName, string title)
		{
			try
			{
				MutexSecurity security = new MutexSecurity();

				security.AddAccessRule(
					new MutexAccessRule(
						new SecurityIdentifier(
							WellKnownSidType.WorldSid,
							null
							),
						MutexRights.FullControl,
						AccessControlType.Allow
						)
					);

				bool createdNew;
				ProcMtx = new Mutex(false, @"Global\Global_" + procMtxName, out createdNew, security);

				if (ProcMtx.WaitOne(0))
					return true;

				ProcMtx.Close();
				ProcMtx = null;
			}
			catch
			{ }

			CloseProcMtx();

			MessageBox.Show(
				"Already started on the other logon session !",
				title + " / Error",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);

			return false;
		}

		public static void Release()
		{
			CloseProcMtx();
		}

		private static void CloseProcMtx()
		{
			try { ProcMtx.ReleaseMutex(); }
			catch { }

			try { ProcMtx.Close(); }
			catch { }

			ProcMtx = null;
		}
	}

	// / ////
}

//
// <<< Processed by SolutionConv
//