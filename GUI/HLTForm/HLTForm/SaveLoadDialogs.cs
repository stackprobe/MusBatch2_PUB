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
using System.IO;
using System.Drawing;
using System.Windows.Forms;

// / //// / ///////////////

namespace HLTStudio
{
	// //// / / ///////////////

	public static class SaveLoadDialogs
	{
		public static string SaveFile(string title, string filterString, string initialFile)
		{
			// //////////// // ////////////// ///

			string[] exts = filterString.Split('.');

			int filterIndex = exts.Select(ext => "." + ext.ToLower()).ToList().IndexOf(Path.GetExtension(initialFile).ToLower());
			filterIndex += exts.Length + 1;
			filterIndex %= exts.Length + 1;
			filterIndex++;

			return SaveFile(title, filterString, Path.GetDirectoryName(initialFile), Path.GetFileName(initialFile), dlg => dlg.FilterIndex = filterIndex);
		}

		public static string LoadFile(string title, string filterString, string initialFile)
		{
			// //////////// // /////////////////// ///

			string genericName = null;

			{
				int p = filterString.IndexOf(':');

				if (p != -1)
				{
					genericName = filterString.Substring(0, p);
					filterString = filterString.Substring(p + 1);
				}
			}

			string[] exts = filterString.Split('.');

			if (genericName == null)
				genericName = string.Join("_", exts);

			filterString = string.Join(";", filterString.Split('.').Select(ext => "*." + ext));
			filterString = genericName + "ファイル(" + filterString + ")|" + filterString + "|すべてのファイル(*.*)|*.*";

			int filterIndex = exts.Select(ext => "." + ext.ToLower()).Contains(Path.GetExtension(initialFile).ToLower()) ? 1 : 2;

			return LoadFile(title, "", Path.GetDirectoryName(initialFile), Path.GetFileName(initialFile), dlg =>
			{
				dlg.Filter = filterString;
				dlg.FilterIndex = filterIndex;
			});
		}

		public static string SaveFile(string title, string filterString, string initialDir, string initialFile, Action<FileDialog> initializer = null)
		{
			return SaveLoadFile(title, filterString, initialDir, initialFile, initializer, true);
		}

		public static string LoadFile(string title, string filterString, string initialDir, string initialFile, Action<FileDialog> initializer = null)
		{
			return SaveLoadFile(title, filterString, initialDir, initialFile, initializer, false);
		}

		public static bool SaveFileOverwritePrompt = true;

		private static string SaveLoadFile(string title, string filterString, string initialDir, string initialFile, Action<FileDialog> initializer, bool saveFlag)
		{
			string homeDir = Directory.GetCurrentDirectory();
			try
			{
				using (FileDialog dlg = saveFlag ? (FileDialog)new SaveFileDialog() : (FileDialog)new OpenFileDialog())
				{
					dlg.Title = title;
					dlg.Filter = GetFilter(filterString);
					///////////////// / /////////////// // ///
					dlg.FilterIndex = 1;
					dlg.InitialDirectory = initialDir;
					dlg.FileName = initialFile;

					if (initializer != null)
						initializer(dlg);

					if (saveFlag)
						((SaveFileDialog)dlg).OverwritePrompt = SaveFileOverwritePrompt;

					if (dlg.ShowDialog() == DialogResult.OK)
					{
						return dlg.FileName;
					}
				}
			}
			finally
			{
				Directory.SetCurrentDirectory(homeDir);
			}
			return null;
		}

		///////// ////// /// ///////////////

		private static string GetFilter(string filterString)
		{
			// //////////// // //////////////////////////////// ///

			StringBuilder buff = new StringBuilder();

			//////////////// / //

			foreach (string fExtension in filterString.Split('.').Select(extension => extension.Trim()).Where(extension => extension != ""))
			{
				string name = fExtension.ToUpper();
				string extension = fExtension.ToLower();

				{
					int p = extension.IndexOf(':');

					if (p != -1)
					{
						name = extension.Substring(0, p);
						extension = extension.Substring(p + 1);
					}
				}

				///////////////////

				buff.Append(name + "ファイル(*." + extension + ")|*." + extension + "|");
			}
			buff.Append("すべてのファイル(*.*)|*.*");

			return buff.ToString();
		}

		public static bool SelectFolder(ref string dir, string description, Action<FolderBrowserDialog> initializer = null)
		{
			string homeDir = Directory.GetCurrentDirectory();
			try
			{
				using (FolderBrowserDialog dlg = new FolderBrowserDialog())
				{
					dlg.SelectedPath = dir;
					dlg.Description = description;

					if (initializer != null)
						initializer(dlg);

					if (dlg.ShowDialog() == DialogResult.OK)
					{
						dir = dlg.SelectedPath;
						return true;
					}
				}
			}
			finally
			{
				Directory.SetCurrentDirectory(homeDir);
			}
			return false;
		}

		public static bool SelectColor(ref Color color, int[] customColors = null, Action<ColorDialog> initializer = null)
		{
			if (customColors == null)
				customColors = GetDefCustomColors();

			using (ColorDialog dlg = new ColorDialog())
			{
				dlg.Color = color;
				dlg.CustomColors = customColors;

				if (initializer != null)
					initializer(dlg);

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					color = dlg.Color;
					return true;
				}
			}
			return false;
		}

		private static int[] GetDefCustomColors()
		{
			int[] colors = new int[16];
			Random random = new Random();

			for (int index = 0; index < colors.Length; index++)
			{
				colors[index] = random.Next() & 0xffffff;
			}
			return colors;
		}
	}

	// / ////
}

//
// <<< Processed by SolutionConv
//