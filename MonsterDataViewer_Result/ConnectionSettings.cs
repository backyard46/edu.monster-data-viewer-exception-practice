using System;
using System.Windows.Forms;

namespace MonsterDataViewer
{
    public partial class ConnectionSettings : Form
    {
        /// <summary>
        /// デフォルトコンストラクター。
        /// </summary>
        public ConnectionSettings()
            : this(string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// 初期設定で各種情報を設定するタイプのコンストラクター。
        /// </summary>
        /// <param name="serverName">サーバー名。</param>
        /// <param name="dbName">データベース名。</param>
        /// <param name="userID">ユーザーID。</param>
        /// <param name="password">パスワード。</param>
        public ConnectionSettings(string serverName, string dbName, string userID, string password)
        {
            InitializeComponent();

            _serverName = serverName;
            _dbName = dbName;
            _userID = userID;
            _password = password;
        }

        private string _serverName = string.Empty;

        /// <summary>
        /// サーバー名情報。
        /// </summary>
        public string ServerName
        {
            get { return _serverName; }
            set { _serverName = value; }
        }

        private string _dbName = string.Empty;

        /// <summary>
        /// データベース名情報。
        /// </summary>
        public string DBName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        private string _userID = string.Empty;

        /// <summary>
        /// ユーザーID情報。
        /// </summary>
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _password = string.Empty;

        /// <summary>
        /// パスワード情報。
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value;}
        }

        /// <summary>
        /// 決定ボタン押下時処理。
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (comboServer.Text.Trim().Length <= 0)
            {
                MessageBox.Show("サーバー名を選択/入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboServer.Focus();
            }
            else if (comboDatabase.Text.Trim().Length <= 0)
            {
                MessageBox.Show("データベース名を選択/入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboDatabase.Focus();
            }
            else if (comboID.Text.Trim().Length <= 0)
            {
                MessageBox.Show("ユーザーIDを選択/入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboID.Focus();
            }
            else if (textPwd.Text.Trim().Length <= 0)
            {
                MessageBox.Show("パスワードを入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textPwd.Focus();
            }

            _serverName = comboServer.Text;
            _dbName = comboDatabase.Text;
            _userID = comboID.Text;
            _password = textPwd.Text;

            this.Close();
        }


        /// <summary>
        /// フォームロード時処理。
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ConnectionSettings_Load(object sender, EventArgs e)
        {
            // 選択肢等を初期設定
            comboServer.Items.Add("SIGMA-WSV009");
            comboServer.Items.Add("SN17002");

            comboDatabase.Items.AddRange(new string[] { "iguchi", "kasai", "kikuchi", "watanuki" });

            comboID.Items.AddRange(new string[] { "iguchi_admin", "kasai_admin", "kikuchi_admin", "watanuki_admin" });

            // コンストラクターで渡された情報を初期設定する
            comboServer.Text = _serverName;
            comboDatabase.Text = _dbName;
            comboID.Text = _userID;
            textPwd.Text = _password;
        }
    }
}
