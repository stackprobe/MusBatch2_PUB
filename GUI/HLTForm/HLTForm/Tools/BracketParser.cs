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
	public class BracketParser
	{
		/// /////////
		/// // // /
		/// /////// /////// /////// ////
		/// //////////
		private const char E = '$';

		public delegate string Filter_d(string text);

		public class Bracket
		{
			/// /////////
			/// ///// ///// ///// ////
			/// //////////
			public string BrChrs;
			public Filter_d D_Filter;

			public Bracket(string b, Filter_d f)
			{
				BrChrs = b;
				D_Filter = f;
			}
		}

		private List<Bracket> Brackets = new List<Bracket>();

		public void Add(Bracket bracket)
		{
			Brackets.Add(bracket);
		}

		private Stack<Info> Infos = new Stack<Info>();

		private class Info
		{
			public Bracket Bracket;
			public int LeftIndex; // //////
		}

		public string Perform(string text)
		{
			Infos.Clear();

			for (int index = 0; index < text.Length; )
			{
				if (1 <= Infos.Count && Infos.Peek().Bracket.BrChrs[1] == text[index])
				{
					Info info = Infos.Pop();
					string innerText = text.Substring(info.LeftIndex + 1, index - info.LeftIndex - 1);

					innerText = info.Bracket.D_Filter(innerText);

					string lead = text.Substring(0, info.LeftIndex) + innerText;
					string trail = text.Substring(index + 1);

					text = lead + trail;
					index = lead.Length;
				}
				else
				{
					if (text[index] == E)
					{
						text = text.Substring(0, index) + text.Substring(index + 1); // ///// /

						foreach (Bracket b in Brackets)
						{
							if (b.BrChrs[0] == text[index])
							{
								Info info = new Info();

								info.Bracket = b;
								info.LeftIndex = index;

								Infos.Push(info);
								break;
							}
						}
					}
					index++;
				}
			}
			return text;
		}
	}
}

//
// <<< Processed by SolutionConv
//