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
using System.Windows.Forms;
using HLTStudio.Commons;
using HLTStudio.Dialogs;
using HLTStudio.Modules;
using HLTStudio.Tools;

namespace HLTStudio
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;

			this.Text = $"{Consts.APPLICATION_NAME} インストーラ　( ビルド番号：{GetRevisionCode()} )";
			this.LMainMessage.Text = $"「{Consts.APPLICATION_LONG_NAME}」をインストールします。";
			this.TxtInstallDir.Text = Consts.DEFAULT_INSTALL_DIR;
		}

		private static string GetRevisionCode()
		{
			DateTime dt = ExecutableFileTools.GetBuiltDateTime(ProcMain.SelfFile);

			int shouwaEraYear = dt.Year - 1925;

			return $"v{shouwaEraYear}-{dt.Month * 31 + dt.Day:D3}-{dt.Hour * 3600 + dt.Minute * 60 + dt.Second:D5}";
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.RefreshView();
			this.SetInitialFocus();
		}

		private void RefreshView()
		{
			bool alreadyInstalled = File.Exists(Path.Combine(this.TxtInstallDir.Text, Consts.INSTALLED_SIGNATURE));

			if (alreadyInstalled)
			{
				this.BtnInstall.Text = "再インストール";
				this.BtnUninstall.Visible = true;
			}
			else
			{
				this.BtnInstall.Text = "インストール";
				this.BtnUninstall.Visible = false;
			}
		}

		private void SetInitialFocus()
		{
			/////////////////////////
			this.BtnResetInstallDir.Focus();
		}

		private void BtnBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				string homeDir = Directory.GetCurrentDirectory();
				try
				{
					string initialSelectedPath = this.TxtInstallDir.Text;

					// ///////////////
					initialSelectedPath = SCommon.ToParentPath(initialSelectedPath);

					// ////////////
					while (
						!SCommon.IsAbsRootDir(initialSelectedPath) &&
						!Directory.Exists(initialSelectedPath)
						)
						initialSelectedPath = SCommon.ToParentPath(initialSelectedPath);

					using (FolderBrowserDialog fbd = new FolderBrowserDialog())
					{
						const bool 新しいフォルダの作成を許可するか_Flag = true;

						fbd.Description = "インストール先フォルダを変更します。\r\n"
							+ $"選択されたパス + \"{Consts.VENDOR_FOLDER_NAME}\\{Consts.APPLICATION_NAME}\"\r\n"
							+ "という構成でフォルダを作成し、アプリケーションをインストールします。";
						fbd.RootFolder = Environment.SpecialFolder.MyComputer;
						fbd.SelectedPath = initialSelectedPath;
						fbd.ShowNewFolderButton = 新しいフォルダの作成を許可するか_Flag;

						if (fbd.ShowDialog() == DialogResult.OK)
						{
							string installDir = SCommon.MakeFullPath(fbd.SelectedPath);

							// // /////// //

							if (!SCommon.IsFairFullPath(installDir))
							{
								throw new Exception(
									"選択されたフォルダは以下の理由から使用できません。\r\n"
									+ "・空白や半角ピリオドの入れ方 (連続している、パスデリミタに接している など)\r\n"
									+ "・特殊文字を含む\r\n"
									+ "・その他、本アプリケーションの基準に合わない"
									);
							}
							if (SCommon.ENCODING_SJIS.GetByteCount(installDir) > 100) // ///// /////
							{
								throw new Exception("選択されたフォルダパスは長すぎます。");
							}

							// ////

							// ///////////////
							if (
								!installDir.EndsWithIgnoreCase($"\\{Consts.VENDOR_FOLDER_NAME}") &&
								!installDir.EndsWithIgnoreCase($"\\{Consts.VENDOR_FOLDER_NAME}\\{Consts.APPLICATION_NAME}")
								)
								installDir = Path.Combine(installDir, Consts.VENDOR_FOLDER_NAME);

							// ///////////////
							if (!installDir.EndsWithIgnoreCase($"\\{Consts.APPLICATION_NAME}"))
								installDir = Path.Combine(installDir, Consts.APPLICATION_NAME);

							this.TxtInstallDir.Text = installDir;

							this.RefreshView();
						}
					}
				}
				finally
				{
					Directory.SetCurrentDirectory(homeDir);
				}
			}
			catch (Exception ex)
			{
				MessageDlg.Run(
					MessageDlg.Kind_e.Error,
					"エラー",
					"インストール先フォルダの変更に失敗しました。\r\n"
					+ "原因：" + GetInnermostException(ex).Message,
					ex,
					new string[] { "OK" }
					);
			}
		}

		private static Exception GetInnermostException(Exception ex)
		{
			while (ex.InnerException != null)
				ex = ex.InnerException;

			return ex;
		}

		private void BtnResetInstallDir_Click(object sender, EventArgs e)
		{
			this.TxtInstallDir.Text = Consts.DEFAULT_INSTALL_DIR;

			this.RefreshView();
		}

		private void TxtInstallDirMenu_コピー_Click(object sender, EventArgs e)
		{
			try
			{
				Clipboard.SetText(this.TxtInstallDir.Text);
			}
			catch
			{ }
		}

		private void FormMenu_ビルド番号をクリップボードにコピーする_Click(object sender, EventArgs e)
		{
			try
			{
				Clipboard.SetText(GetRevisionCode());
			}
			catch
			{ }
		}

		#region //////

		private void BtnInstall_Click(object sender, EventArgs e)
		{
			this.Visible = false;

			try
			{
				DoInstall();

				MessageDlg.Run(
					MessageDlg.Kind_e.Information,
					"インストール完了",
					"インストールが完了しました。",
					null,
					new string[] { "OK" }
					);

				this.Close();
				return;
			}
			catch (SCommon.Cancelled)
			{
				MessageDlg.Run(
					MessageDlg.Kind_e.Warning,
					"インストール中止",
					"インストールを中止しました。",
					null,
					new string[] { "OK" }
					);
			}
			catch (Exception ex)
			{
				MessageDlg.Run(
					MessageDlg.Kind_e.Error,
					"インストール失敗",
					"インストールに失敗しました。\r\n"
					+ "原因：" + GetInnermostException(ex).Message,
					ex,
					new string[] { "OK" }
					);
			}

			this.Visible = true;

			this.RefreshView();
			this.SetInitialFocus();
		}

		/// /////////
		/// /////////////////
		/// //////////
		private void DoInstall()
		{
			string installDir = this.TxtInstallDir.Text;
			string sigFile = Path.Combine(installDir, Consts.INSTALLED_SIGNATURE);
			bool createShortcutFlag = this.CBCreateShortcut.Checked;

			if (
				Directory.Exists(installDir) &&
				!File.Exists(sigFile)
				)
			{
				int ret = MessageDlg.Run(
					MessageDlg.Kind_e.Warning,
					"上書き確認",
					"インストール先フォルダは既に存在します。\r\n"
					+ "上書きしてよろしいですか？",
					null,
					new string[] { "はい", "いいえ", "キャンセル" }
					);

				if (ret != 1)
					throw new SCommon.Cancelled();
			}

			if (createShortcutFlag)
			{
				string shortcutPath = ShortcutCreator.GetShortcutPath();

				if (File.Exists(shortcutPath))
				{
					int ret = MessageDlg.Run(
						MessageDlg.Kind_e.Warning,
						"ショートカットの上書き確認",
						$"ショートカット「{Path.GetFileNameWithoutExtension(shortcutPath)}」はデスクトップに既に作成されています。\r\n"
						+ "上書きしてよろしいですか？",
						null,
						new string[] { "はい", "いいえ", "キャンセル" }
						);

					if (ret != 1)
						throw new SCommon.Cancelled();
				}
			}

			// //
			{
				int ret = MessageDlg.Run(
					MessageDlg.Kind_e.Question,
					"インストール実行の確認",
					$"「{Consts.APPLICATION_LONG_NAME}」のインストールを実行します。",
					null,
					new string[] { "OK", "キャンセル" }
					);

				if (ret != 1)
					throw new SCommon.Cancelled();
			}

			ProcessingDlg.Run("インストール", () =>
			{
				DoInstallWkTh(
					installDir,
					sigFile,
					createShortcutFlag
					);
			});
		}

		/// /////////
		/// /////////////////
		/// /////////////////////
		/// //////////
		/// //////////
		/// ////// /////////////////////////////////
		/// ////// ////////////////////////////////
		/// ////// ///////////////////////////////////////////////
		private void DoInstallWkTh(
			string installDir,
			string sigFile,
			bool createShortcutFlag
			)
		{
			foreach (string fileName in Consts.CLUSTER_FILES)
			{
				string clusterFile = Path.Combine(ProcMain.SelfDir, fileName);
				string clusterHashFile = clusterFile + Consts.HASH_EXTENSION;

				if (!File.Exists(clusterFile))
					throw new Exception($"クラスタファイル「{fileName}」が見つかりません。");

				if (!File.Exists(clusterHashFile))
					throw new Exception($"クラスタファイル「{fileName}」のハッシュ値が見つかりません。");

				byte[] hash1 = SCommon.GetSHA512File(clusterFile);
				byte[] hash2 = SCommon.Hex.I.GetBytes(File.ReadAllText(clusterHashFile, Encoding.ASCII).Trim());

				if (SCommon.Comp(hash1, hash2, SCommon.Comp) != 0)
					throw new Exception($"クラスタファイル「{fileName}」が破損しています。");
			}

			if (Directory.Exists(installDir)) // //////////////////////
			{
				string escapeDir = SCommon.ToCreatablePath(installDir);

				try
				{
					SCommon.EnsureMoveDir(installDir, escapeDir);
				}
				catch (Exception ex)
				{
					throw new Exception("インストール先のフォルダは現在使用されています。", ex);
				}

				// /////
				// //////////////////////////////////
				// /////////////////////////////

				SCommon.EnsureMoveDir(escapeDir, installDir); // /////
			}
			SCommon.CreateDir(installDir);

			// /////
			// /////////////////////////////
			// ////////////////////////

			SCommon.DeletePath(sigFile);
			File.WriteAllBytes(sigFile, SCommon.EMPTY_BYTES);

			foreach (string fileName in Consts.CLUSTER_FILES)
			{
				string clusterFile = Path.Combine(ProcMain.SelfDir, fileName);
				string extractedDir = Path.Combine(installDir, P_EraseExtension(fileName));

				DirToClusterFileTools.ClusterFileToDir(clusterFile, extractedDir);
			}

			// // ///// //

			SCommon.CreateDir(Path.Combine(installDir, "bat"));

			// ////

			if (createShortcutFlag)
				ShortcutCreator.Run(Path.Combine(installDir, Consts.MAIN_PROGRAM));
		}

		private static string P_EraseExtension(string fileName)
		{
			int p = fileName.LastIndexOf('.');

			if (p == -1) // / ///////
				throw null; // /////

			if (p == 0) // / ///////////
				throw null; // /////

			return fileName.Substring(0, p);
		}

		#endregion

		#region ////////

		private void BtnUninstall_Click(object sender, EventArgs e)
		{
			this.Visible = false;

			try
			{
				DoUninstall();

				MessageDlg.Run(
					MessageDlg.Kind_e.Information,
					"アンインストール完了",
					"アンインストールが完了しました。",
					null,
					new string[] { "OK" }
					);

				this.Close();
				return;
			}
			catch (SCommon.Cancelled)
			{
				MessageDlg.Run(
					MessageDlg.Kind_e.Warning,
					"アンインストール中止",
					"アンインストールを中止しました。",
					null,
					new string[] { "OK" }
					);
			}
			catch (Exception ex)
			{
				MessageDlg.Run(
					MessageDlg.Kind_e.Error,
					"アンインストール失敗",
					"アンインストールに失敗しました。\r\n"
					+ "原因：" + GetInnermostException(ex).Message,
					ex,
					new string[] { "OK" }
					);
			}

			this.Visible = true;

			this.RefreshView();
			this.SetInitialFocus();
		}

		/// /////////
		/// ///////////////////
		/// //////////
		private void DoUninstall()
		{
			string installDir = this.TxtInstallDir.Text;
			bool removeShortcutFlag = false;

			// ////////////
			{
				string shortcutPath = ShortcutCreator.GetShortcutPath();

				if (File.Exists(shortcutPath))
				{
					int ret = MessageDlg.Run(
						MessageDlg.Kind_e.Question,
						"ショートカット削除の確認",
						$"デスクトップ上のショートカット「{Path.GetFileNameWithoutExtension(shortcutPath)}」を削除しますか？",
						null,
						new string[] { "はい", "いいえ", "キャンセル" }
						);

					if (ret == 1)
						removeShortcutFlag = true;
					else if (ret == 2)
						removeShortcutFlag = false;
					else
						throw new SCommon.Cancelled();
				}
			}

			// //
			{
				int ret = MessageDlg.Run(
					MessageDlg.Kind_e.Warning,
					"アンインストール実行の確認",
					$"「{Consts.APPLICATION_LONG_NAME}」をアンインストールします。\r\n"
					+ "インストール先フォルダにある全てのファイルが削除されます。\r\n"
					+ "実行してよろしいですか？\r\n"
					+ $"( ショートカットの削除：{(removeShortcutFlag ? "する" : "しない")} )",
					null,
					new string[] { "OK", "キャンセル" }
					);

				if (ret != 1)
					throw new SCommon.Cancelled();
			}

			ProcessingDlg.Run("アンインストール", () =>
			{
				DoUninstallWkTh(
					installDir,
					removeShortcutFlag
					);
			});
		}

		/// /////////
		/// ///////////////////
		/// /////////////////////
		/// //////////
		/// //////////
		/// ////// /////////////////////////////////
		/// ////// ///////////////////////////////////////////////
		private void DoUninstallWkTh(
			string installDir,
			bool removeShortcutFlag
			)
		{
			if (!Directory.Exists(installDir))
				throw new Exception("インストール先のフォルダが存在しません！");

			string vendorFolder = SCommon.ToParentPath(installDir);

			// ////////////////////// // //
			{
				string escapeDir = SCommon.ToCreatablePath(installDir);

				try
				{
					SCommon.EnsureMoveDir(installDir, escapeDir);
				}
				catch (Exception ex)
				{
					throw new Exception("インストール先のフォルダは現在使用されています。", ex);
				}

				SCommon.DeletePath(escapeDir);
			}

			// ////////////////////
			{
				string[] strPaths = Directory.GetDirectories(vendorFolder)
					.Concat(Directory.GetFiles(vendorFolder))
					.ToArray();

				if (strPaths.Length == 0)
				{
					try
					{
						Directory.Delete(vendorFolder, false);
					}
					catch
					{ }
				}
			}

			if (removeShortcutFlag)
				SCommon.DeletePath(ShortcutCreator.GetShortcutPath());
		}

		#endregion
	}
}

//
// <<< Processed by SolutionConv
//