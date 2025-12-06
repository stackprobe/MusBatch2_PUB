// Processed by SolutionConv >>>
//
// 本ソースファイルは、公開時の所定の手続きとして一部のセンシティブな情報をマスキングしています。
// 元データの機微に触れる可能性がある箇所を伏せ字化したものであり、
// リリース版との処理内容に実質的な差異が生じない範囲で調整を加えています。
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Threading;

namespace HLTStudio
{
	public partial class BusyDlg : Form
	{
		#region ////// //

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public delegate void Perform_d();

		public static void Perform(Perform_d dPerform)
		{
			using (BusyDlg f = new BusyDlg(dPerform))
			{
				f.ShowDialog();
			}
		}

		private Thread _th;

		public BusyDlg(Perform_d dPerform)
		{
			_th = new Thread((ThreadStart)delegate { dPerform(); });
			_th.Start();

			InitializeComponent();
		}

		private void BusyWin_Load(object sender, EventArgs e)
		{
			// ////
		}

		private void BusyWin_Shown(object sender, EventArgs e)
		{
			this.MT_Enabled = true;
		}

		private void BusyWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ////
		}

		private void BusyWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				if (5 <= this.MT_Count && _th.IsAlive == false)
				{
					this.MT_Enabled = false;
					this.Close();
					return;
				}
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}
	}
}

//
// <<< Processed by SolutionConv
//