using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MonsterDataViewer
{
    public partial class MonsterViewer : Form
    {
        // データベース接続用情報
        private string serverName = "SIGMA-WSV009";
        private string databaseName = "kasai";
        private string userId = "kasai_admin";
        private string userPwd = "kasai_admin";

        /// <summary>
        /// Initializes a new instance of the <see cref="MonsterViewer"/> class.
        /// </summary>
        public MonsterViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 閉じるボタン押下時処理。
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            // DB接続情報を設定ファイルに保存して閉じる（次回も同じ情報で接続できるようにする）。
            Properties.Settings.Default["ServerName"] = serverName;
            Properties.Settings.Default["DatabaseName"] = databaseName;
            Properties.Settings.Default["UserID"] = userId;
            Properties.Settings.Default["Password"] = userPwd;

            Properties.Settings.Default.Save();

            this.Close();
        }

        /// <summary>
        /// 表示押下時処理。
        /// </summary>
        /// <remarks>
        /// TableAdapterを使ってパラメーターによる条件指定を用いたSQLでデータを取得。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShow_Click(object sender, EventArgs e)
        {
            // サーバー名： SIGMA-WSV009
            // データベース名： 各自の苗字ローマ字
            // ユーザーIDとパスワード： ローマ字苗字_admin

            monsterTblAdapter.Connection.ConnectionString = "Data Source="+serverName+";Initial Catalog="+databaseName+";User ID=" + userId +";Password=" + userPwd;

            // データ取得と表示
            try
            {
                if (textCondition.Text.Trim().Length == 0)
                {
                    // 検索条件なしの場合
                    monsterTblAdapter.Fill(monsterDataSet.Monsters);
                }
                else
                {
                    // 検索条件がある場合
                    monsterTblAdapter.FillByTypes(monsterDataSet.Monsters, textCondition.Text, textCondition.Text);
                }
            }
            catch (SqlException ex)
            {
                // 下記のやりかたは「日本語環境でのみ可能なやりかた」なのであまり模範回答とは言えません。
                // 厳密に対応する場合、例外オブジェクトexの「Number」プロパティと、
                // https://docs.microsoft.com/ja-jp/sql/relational-databases/errors-events/database-engine-events-and-errors?view=sql-server-ver15
                // にあるエラーコードを見てエラーの仕分けを行ってください。
                if (ex.InnerException == null)
                {
                    // サーバー名に誤りがある場合（この場合、どうやらInnerExceptionがnullになる）
                    MessageBox.Show("サーバー「" + serverName + "」に接続できません。\n\r接続先を再確認してください。", "接続失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.StartsWith("この"))
                {
                    // データベース名に誤りがある場合メッセージが「このログインで」で始まる
                    MessageBox.Show("データベース「" + serverName + "」に接続できません。\n\rデータベース名を再確認してください。", "接続失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // その他 …… ログインIDかパスワードに誤りがある
                    MessageBox.Show("ユーザーIDまたはパスワードに誤りがあります。", "接続失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            gridMain.DataSource = monsterDataSet.Monsters;
        }

        /// <summary>
        /// クリアボタン押下時処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            gridMain.DataSource = null;
        }

        /// <summary>
        /// 接続先設定ボタン押下時処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonServerInfo_Click(object sender, EventArgs e)
        {
            ConnectionSettings child = new ConnectionSettings(serverName, databaseName, userId, userPwd);
            child.ShowDialog();

            serverName = child.ServerName;
            databaseName = child.DBName;
            userId = child.UserID;
            userPwd = child.Password;

            child.Dispose();
        }

        /// <summary>
        /// フォームロード時処理。
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void MonsterViewer_Load(object sender, EventArgs e)
        {
            // 設定ファイルに保存されている接続情報を読み取る。
            serverName=Properties.Settings.Default["ServerName"].ToString();
            databaseName=Properties.Settings.Default["DatabaseName"].ToString();
            userId=Properties.Settings.Default["UserID"].ToString();
            userPwd=Properties.Settings.Default["Password"].ToString();
        }
    }
}
