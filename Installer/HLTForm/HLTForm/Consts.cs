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
using System.Text;

namespace HLTStudio
{
	public static class Consts
	{
		/// /////////
		/// /////////
		/// //////////
		public static readonly string VENDOR_FOLDER_NAME = "HLTWorks";

		/// /////////
		/// /////////
		/// ////////////////////
		/// //////////
		public static readonly string APPLICATION_NAME = "MusBatch";

		/// /////////
		/// /////////
		/// /////
		/// //////////
		public static readonly string APPLICATION_LONG_NAME = "マウス・キーボード操作の記録と再生ツール";

		/// /////////
		/// /////////////
		/// //////////
		public static readonly string DEFAULT_INSTALL_DIR = $"C:\\{VENDOR_FOLDER_NAME}\\{APPLICATION_NAME}";

		/// /////////
		/// ///////////
		/// //////////
		public static readonly string[] CLUSTER_FILES = new string[]
		{
			"CUI.cmp-gz",
			"GUI.cmp-gz",
		};

		/// /////////
		/// ///////////////////////
		/// //////////
		public static readonly string HASH_EXTENSION = ".hash";

		/// /////////
		/// ///////
		/// //////////////
		/// //////////
		public static readonly string MAIN_PROGRAM = @"GUI\GUI.exe";

		/// /////////
		/// ///////////////////////
		/// //
		/// /////////////////
		/// ///////////////////////////////////
		/// ////////////////////////
		/// //////////
		public static readonly string INSTALLED_SIGNATURE = "HLT_{dc63e002-fe96-4123-b93e-c5795ed66f42}";
	}
}

//
// <<< Processed by SolutionConv
//