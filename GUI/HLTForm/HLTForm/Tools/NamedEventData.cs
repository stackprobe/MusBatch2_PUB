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

namespace HLTStudio.Tools
{
	public class NamedEventData : IDisposable
	{
		private EventWaitHandle _ewh;

		public NamedEventData(string name)
		{
			_ewh = new EventWaitHandle(false, EventResetMode.AutoReset, name);
		}

		public void Set()
		{
			_ewh.Set();
		}

		public void WaitForever()
		{
			_ewh.WaitOne();
		}

		/// /////////
		/// 
		/// //////////
		/// ////// //////////////// // ////////////////
		/// //////////////////////////////
		public bool WaitForMillis(int millis)
		{
			return _ewh.WaitOne(millis);
		}

		public void Dispose()
		{
			if (_ewh != null)
			{
				_ewh.Dispose();
				_ewh = null;
			}
		}
	}
}

//
// <<< Processed by SolutionConv
//