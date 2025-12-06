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
	public class MutexData : IDisposable
	{
		private Mutex _m;

		public MutexData(string name)
		{
			_m = new Mutex(false, name);
		}

		public void WaitForever()
		{
			_m.WaitOne();
		}

		/// /////////
		/// 
		/// //////////
		/// ////// //////////////// // ////////////////
		/// /////////////////////////
		public bool WaitForMillis(int millis)
		{
			return _m.WaitOne(millis);
		}

		public void Unlock()
		{
			_m.ReleaseMutex();
		}

		public void Dispose()
		{
			if (_m != null)
			{
				_m.Dispose();
				_m = null;
			}
		}

		public class Section : IDisposable
		{
			public Section(string name)
				: this(new MutexData(name), true)
			{ }

			private MutexData _md;
			private bool _autoDispose;

			public Section(MutexData md, bool autoDispose = false)
			{
				_md = md;
				_autoDispose = autoDispose;
				_md.WaitForever();
			}

			public void Dispose()
			{
				if (_md != null)
				{
					_md.Unlock();

					if (_autoDispose)
						_md.Dispose();

					_md = null;
				}
			}
		}
	}
}

//
// <<< Processed by SolutionConv
//