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
    /// <summary>
    /// 例外実習、回答例。
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MonsterViewer : Form
    {
        // データベース接続用情報
        private string serverName = "SIGMA-WSV009";
        private string databaseName = "koushi";
        private string userId = "koushi_admin";
        private string userPwd = "koushi_admin";

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
    }
}
