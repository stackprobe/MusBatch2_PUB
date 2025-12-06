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

namespace HLTStudio.Tools
{
	public class WorkBenchDir
	{
		private static WorkBenchDir _i = null;

		public static WorkBenchDir I
		{
			get
			{
				if (_i == null)
					_i = new WorkBenchDir();

				return _i;
			}
		}

		private string _rootDir = FileTools.GetTempPath(Program.APP_IDENT);

		private WorkBenchDir()
		{
			this.Clear();
			Directory.CreateDirectory(_rootDir);
		}

		~WorkBenchDir()
		{
			this.Clear();
		}

		public void Clear()
		{
			try
			{
				Directory.Delete(_rootDir, true);
			}
			catch
			{ }
		}

		public string GetPath(string relPath)
		{
			return StringTools.Combine(_rootDir, relPath);
		}

		public string MakePath()
		{
			return StringTools.Combine(_rootDir, Guid.NewGuid().ToString("B"));
		}
	}
}

//
// <<< Processed by SolutionConv
//