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
using HLTStudio.Tools;
using System.IO;

namespace HLTStudio
{
	public partial class SettingWin : Form
	{
		public SettingWin()
		{
			InitializeComponent();
		}

		private void SettingWin_Load(object sender, EventArgs e)
		{
			this.LoadData();
		}

		private void SettingWin_Shown(object sender, EventArgs e)
		{
			// //////////
			{
				this.RecStartMin_CheckedChanged(null, null);
				this.StartMin_CheckedChanged(null, null);
			}

			Utils.PostShown(this);
		}

		private void SettingWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// ////
		}

		private void SettingWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.SaveData();
		}

		// //// ////////// ////

		private void LoadData()
		{
			this.SndInputBatLocal.Checked = Gnd.I.SndInputBatLocal;
			this.RecStrokeMillis.Text = "" + Gnd.I.RecStrokeMillis;
			this.RecClickMillis.Text = "" + Gnd.I.RecClickMillis;
			this.RecDblClickMillis.Text = "" + Gnd.I.RecDblClickMillis;
			this.Recまとめ.Checked = Gnd.I.Recまとめ;
			this.StrokeMillis.Text = "" + Gnd.I.StrokeMillis;
			this.ClickMillis.Text = "" + Gnd.I.ClickMillis;
			this.DblClickMillis.Text = "" + Gnd.I.DblClickMillis;
			this.SamplingMillis.Text = "" + Gnd.I.SamplingMillis;
			this.RecStartMin.Checked = Gnd.I.RecStartMin;
			this.RecEndUnmin.Checked = Gnd.I.RecEndUnmin;
			this.RecRCtrl停止.Checked = Gnd.I.RecRCtrl停止;
			this.StartMin.Checked = Gnd.I.StartMin;
			this.EndUnmin.Checked = Gnd.I.EndUnmin;
			this.RCtrl中断.Checked = Gnd.I.RCtrl中断;
			// ///////
		}

		private void SaveData()
		{
			Gnd.I.SndInputBatLocal = this.SndInputBatLocal.Checked;
			Gnd.I.RecStrokeMillis = IntTools.ToInt(this.RecStrokeMillis.Text, 0, 60000, Gnd.I.RecStrokeMillis);
			Gnd.I.RecClickMillis = IntTools.ToInt(this.RecClickMillis.Text, 0, 60000, Gnd.I.RecClickMillis);
			Gnd.I.RecDblClickMillis = IntTools.ToInt(this.RecDblClickMillis.Text, 0, 60000, Gnd.I.RecDblClickMillis);
			Gnd.I.Recまとめ = this.Recまとめ.Checked;
			Gnd.I.StrokeMillis = IntTools.ToInt(this.StrokeMillis.Text, 0, 60000, Gnd.I.StrokeMillis);
			Gnd.I.ClickMillis = IntTools.ToInt(this.ClickMillis.Text, 0, 60000, Gnd.I.ClickMillis);
			Gnd.I.DblClickMillis = IntTools.ToInt(this.DblClickMillis.Text, 0, 60000, Gnd.I.DblClickMillis);
			Gnd.I.SamplingMillis = IntTools.ToInt(this.SamplingMillis.Text, 1, 60000, Gnd.I.SamplingMillis);
			Gnd.I.RecStartMin = this.RecStartMin.Checked;
			Gnd.I.RecEndUnmin = this.RecEndUnmin.Checked;
			Gnd.I.RecRCtrl停止 = this.RecRCtrl停止.Checked;
			Gnd.I.StartMin = this.StartMin.Checked;
			Gnd.I.EndUnmin = this.EndUnmin.Checked;
			Gnd.I.RCtrl中断 = this.RCtrl中断.Checked;
			// ///////
		}

		private void DoExport(string file)
		{
			XNode xml = new XNode("MusBatchSetting");

			xml.Children.Add(new XNode("SndInputBatLocal", StringTools.ToString(this.SndInputBatLocal.Checked)));
			xml.Children.Add(new XNode("RecStrokeMillis", this.RecStrokeMillis.Text));
			xml.Children.Add(new XNode("RecClickMillis", this.RecClickMillis.Text));
			xml.Children.Add(new XNode("RecDblClickMillis", this.RecDblClickMillis.Text));
			xml.Children.Add(new XNode("RecMatome", StringTools.ToString(this.Recまとめ.Checked)));
			xml.Children.Add(new XNode("StrokeMillis", this.StrokeMillis.Text));
			xml.Children.Add(new XNode("ClickMillis", this.ClickMillis.Text));
			xml.Children.Add(new XNode("DblClickMillis", this.DblClickMillis.Text));
			xml.Children.Add(new XNode("SamplingMillis", this.SamplingMillis.Text));
			xml.Children.Add(new XNode("RecStartMin", StringTools.ToString(this.RecStartMin.Checked)));
			xml.Children.Add(new XNode("RecEndUnmin", StringTools.ToString(this.RecEndUnmin.Checked)));
			xml.Children.Add(new XNode("RecRCtrlTeishi", StringTools.ToString(this.RecRCtrl停止.Checked)));
			xml.Children.Add(new XNode("StartMin", StringTools.ToString(this.StartMin.Checked)));
			xml.Children.Add(new XNode("EndUnmin", StringTools.ToString(this.EndUnmin.Checked)));
			xml.Children.Add(new XNode("RCtrlChuudan", StringTools.ToString(this.RCtrl中断.Checked)));
			// ///////

			xml.Save(file);
		}

		private void DoImport(string file)
		{
			XNode xml = XNode.Load(file);

			this.SndInputBatLocal.Checked = StringTools.ToFlag(xml.GetNode("SndInputBatLocal").Value);
			this.RecStrokeMillis.Text = xml.GetNode("RecStrokeMillis").Value;
			this.RecClickMillis.Text = xml.GetNode("RecClickMillis").Value;
			this.RecDblClickMillis.Text = xml.GetNode("RecDblClickMillis").Value;
			this.Recまとめ.Checked = StringTools.ToFlag(xml.GetNode("RecMatome").Value);
			this.StrokeMillis.Text = xml.GetNode("StrokeMillis").Value;
			this.ClickMillis.Text = xml.GetNode("ClickMillis").Value;
			this.DblClickMillis.Text = xml.GetNode("DblClickMillis").Value;
			this.SamplingMillis.Text = xml.GetNode("SamplingMillis").Value;
			this.RecStartMin.Checked = StringTools.ToFlag(xml.GetNode("RecStartMin").Value);
			this.RecEndUnmin.Checked = StringTools.ToFlag(xml.GetNode("RecEndUnmin").Value);
			this.RecRCtrl停止.Checked = StringTools.ToFlag(xml.GetNode("RecRCtrlTeishi").Value);
			this.StartMin.Checked = StringTools.ToFlag(xml.GetNode("StartMin").Value);
			this.EndUnmin.Checked = StringTools.ToFlag(xml.GetNode("EndUnmin").Value);
			this.RCtrl中断.Checked = StringTools.ToFlag(xml.GetNode("RCtrlChuudan").Value);
			// ///////
		}

		// ////

		private void BtnReset_Click(object sender, EventArgs e)
		{
			this.LoadData();
		}

		private void MillisCommonChanged(TextBox tb, int minval = 0)
		{
			try
			{
				IntTools.Parse(tb.Text, minval, 60000);

				tb.ForeColor = new TextBox().ForeColor;
				tb.BackColor = new TextBox().BackColor;
			}
			catch
			{
				tb.ForeColor = Color.Red;
				tb.BackColor = Color.FromArgb(255, 255, 200);
			}
		}

		private void RecStrokeMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.RecStrokeMillis);
		}

		private void RecClickMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.RecClickMillis);
		}

		private void RecDblClickMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.RecDblClickMillis);
		}

		private void StrokeMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.StrokeMillis);
		}

		private void ClickMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.ClickMillis);
		}

		private void DblClickMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.DblClickMillis);
		}

		private void SamplingMillis_TextChanged(object sender, EventArgs e)
		{
			this.MillisCommonChanged(this.SamplingMillis, 1);
		}

		private void RecStartMin_CheckedChanged(object sender, EventArgs e)
		{
			this.RecEndUnmin.Enabled = this.RecStartMin.Checked;
		}

		private void StartMin_CheckedChanged(object sender, EventArgs e)
		{
			this.EndUnmin.Enabled = this.StartMin.Checked;
		}

		private void CommonKeyPress(KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // /////
			{
				SendKeys.Send("{TAB}");
				e.Handled = true;
			}
		}

		private void SndInputBatLocal_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecStrokeMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecClickMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecDblClickMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void Recまとめ_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void StrokeMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void ClickMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void DblClickMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void SamplingMillis_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecStartMin_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecEndUnmin_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RecRCtrl停止_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void StartMin_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void EndUnmin_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private void RCtrl中断_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.CommonKeyPress(e);
		}

		private static string _settingDir = null;

		private static string GetSettingDir()
		{
			if (_settingDir == null)
			{
				string dir = "Setting";

				if (Directory.Exists(dir) == false)
					dir = @"..\..\..\..\doc\Setting";

				_settingDir = Path.GetFullPath(dir);
			}
			return _settingDir;
		}

		private void BtnExport_Click(object sender, EventArgs e)
		{
			try
			{
				string destFile = TimeData.Now().GetSimpleString() + ".xml";

				for (; ; )
				{
					destFile = SaveLoadDialogs.SaveFile("保存先の設定ファイルを入力してください", "設定:xml", GetSettingDir(), Path.GetFileName(destFile));

					if (destFile != null)
					{
#if false
						// /////////////////////////////
						/
							////////////////
								////////////////////
								////////////////
								/////////////////////
								//////////////////////
								//
							/////////
						/
						// //////////////////////////// ///// ////// ////// ///// ////// // //////
						/
							////////////////
								////////// ////////////////////
								////////////////
								/////////////////////
								//////////////////////
								//
							/////////
						/
#endif
						this.DoExport(destFile);
					}
					break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"" + ex,
					"エクスポートに失敗しました",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					);
			}
		}

		private void BntImport_Click(object sender, EventArgs e)
		{
			try
			{
				/////////////////////////////
				using (OpenFileDialog ofd = new OpenFileDialog())
				{
					////////////////
					///////////////////////////
					ofd.FileName = "*.xml";
					////////////////////
					/////////////////////////////////
					////////////////////// / ////////////////////////////////////////////////////////////
					ofd.InitialDirectory = GetSettingDir();
					/////////////////////////
					//////////////////////
					ofd.Filter =
						"設定ファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
					//////////////////////////////////////////////////////////
					////////////////
					/////////////////////////
					// ///////////////////
					ofd.FilterIndex = 0;
					///////////
					ofd.Title = "インポートする設定ファイルを選択して下さい";
					////////////////////////////////////
					ofd.RestoreDirectory = true;
					/////////////////////////////
					////////////////////////
					ofd.CheckFileExists = true;
					////////////////////////
					////////////////////////
					ofd.CheckPathExists = true;

					////////////
					if (ofd.ShowDialog() == DialogResult.OK) // ///// ///
					{
						Directory.SetCurrentDirectory(BootTools.SelfDir); // ///

						string selFile = ofd.FileName;

#if false
						// ////////////////////////////
						/
							////////////////
								//////////////////
								////////////////
								/////////////////////
								//////////////////////
								//
							///////
						/
						// /////////////////////////// ///// ////// ////// ///// ////// // //////
						/
							////////////////
								////////// ///////////////////////
								////////////////
								/////////////////////
								//////////////////////
								//
							///////
						/
#endif
						this.DoImport(selFile);
					}
					Directory.SetCurrentDirectory(BootTools.SelfDir); // ///
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"" + ex,
					"インポートに失敗しました",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					);
			}
		}
	}
}

//
// <<< Processed by SolutionConv
//