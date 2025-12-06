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
using HLTStudio.Commons;

namespace HLTStudio.Tools
{
	/// /////////
	/// /////////////// ////////// ///////////////
	/// //////////
	public static class ExecutableFileTools
	{
		/// /////////
		/// /////////////////////
		/// //////////
		/// ////// //////////////////////////
		/// ////////////////////////
		public static DateTime GetBuiltDateTime(string file)
		{
			uint peTimeDateStamp = GetPETimeDateStamp(file);

			DateTime builtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
				.AddSeconds(peTimeDateStamp)
				.ToLocalTime();

			return builtDateTime;
		}

		/// /////////
		/// ////////////////////////////////
		/// //////////
		/// ////// //////////////////////////
		/// ///////////////////////////////////
		public static uint GetPETimeDateStamp(string file)
		{
			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				if (ReadByte(reader) != 'M') throw null;
				if (ReadByte(reader) != 'Z') throw null;

				reader.Seek(0x3c, SeekOrigin.Begin);

				uint peHedPos = (uint)ReadByte(reader);
				peHedPos |= (uint)ReadByte(reader) << 8;
				peHedPos |= (uint)ReadByte(reader) << 16;
				peHedPos |= (uint)ReadByte(reader) << 24;

				reader.Seek(peHedPos, SeekOrigin.Begin);

				if (ReadByte(reader) != 'P') throw null;
				if (ReadByte(reader) != 'E') throw null;
				if (ReadByte(reader) != 0x00) throw null;
				if (ReadByte(reader) != 0x00) throw null;

				reader.Seek(0x04, SeekOrigin.Current);

				uint timeDateStamp = (uint)ReadByte(reader);
				timeDateStamp |= (uint)ReadByte(reader) << 8;
				timeDateStamp |= (uint)ReadByte(reader) << 16;
				timeDateStamp |= (uint)ReadByte(reader) << 24;

				return timeDateStamp;
			}
		}

		private static int ReadByte(FileStream reader)
		{
			int chr = reader.ReadByte();

			if (chr == -1) // / ///
				throw new Exception("Read EOF");

			return chr;
		}
	}
}

//
// <<< Processed by SolutionConv
//