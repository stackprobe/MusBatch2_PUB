// Processed by SolutionConv >>>
//
// 本ソースファイルは、公開時の所定の手続きとして一部のセンシティブな情報をマスキングしています。
// 元データの機微に触れる可能性がある箇所を伏せ字化したものであり、
// リリース版との処理内容に実質的な差異が生じない範囲で調整を加えています。
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HLTStudio.Commons;
using IWshRuntimeLibrary;

namespace HLTStudio.Modules
{
	// ///// ///// // /// // /////// ////// //// ////// /////

	public static class ShortcutCreator
	{
		public static string GetShortcutPath()
		{
			// ///////////
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

			// //////////////
			string shortcutPath = Path.Combine(desktopPath, Consts.APPLICATION_NAME + ".lnk");
			////////////// / //////////////////////////////////////

			return shortcutPath;
		}

		public static void Run(string mainProgram)
		{
			// //////////////
			string shortcutPath = GetShortcutPath();

			// ////
			SCommon.DeletePath(shortcutPath);

			// ///////////// ///
			var shell = new WshShell();

			// //////////
			IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

			// /////////////////////////
			shortcut.TargetPath = mainProgram;

			// ///////////
			shortcut.WorkingDirectory = SCommon.ToParentPath(mainProgram);

			// /////////
			shortcut.IconLocation = mainProgram + ",0";

			// /////////
			shortcut.Description = Consts.APPLICATION_NAME;

			// //
			shortcut.Save();

			// ///
			Marshal.FinalReleaseComObject(shortcut);
			Marshal.FinalReleaseComObject(shell);
		}
	}
}

//
// <<< Processed by SolutionConv
//