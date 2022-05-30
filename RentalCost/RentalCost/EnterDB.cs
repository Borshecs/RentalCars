using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RentalCost
{
    public partial class EnterDB : Form
    {
        DataBaseConnect dataBaseConnect = new DataBaseConnect();

        public EnterDB()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_password.PasswordChar = '*'; //шифр символы
          
            textBox_password.MaxLength = 50;
            textBox_log_in.MaxLength = 50;

        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_log_in.Text;
            var passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter(); 
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user, is_admin from register where (login_user = '{loginUser}' and password_user = '{passUser}')"; //запрос формируем

            SqlCommand command = new SqlCommand(querystring, dataBaseConnect.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1) //так как у нас 1 строка
            {
                var user = new check_user(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));

                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DB db = new DB(user);
                this.Hide();
                db.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button_register_Click(object sender, EventArgs e)
        {
            Form1 registerFORM = new Form1();
            this.Hide();
            registerFORM.ShowDialog();
        }
    }
}
