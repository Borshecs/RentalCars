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
    public partial class Form1 : Form
    {
       
        DataBaseConnect dataBaseConnect = new DataBaseConnect();
        public Form1()
        {
            
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_password.PasswordChar = '*'; //шифр символы

            textBox_password.MaxLength = 50;
            textBox_login.MaxLength = 50;
        }

        private void button_createAccount_Click(object sender, EventArgs e)
        {
           

            var login = textBox_login.Text;
            var password = textBox_password.Text;

            string queryString = $"insert into register(login_user, password_user, is_admin) values('{login}', '{password}', 0)";

            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            dataBaseConnect.OpenConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаун создан успешно ", "Успешно!");
                Form2 frm2 = new Form2();
                this.Hide();
                frm2.ShowDialog();

            }
            else
            {
                MessageBox.Show("Аккаунт не создан");
            }
            dataBaseConnect.ClosedConnection();

            
        }
        private Boolean checkUser()
        {
            var loginUser = textBox_login.Text;
            var passUser = textBox_password.Text;
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string queryString = $"select id_user, login_user, password_user, is_admin from register where login_user ='{loginUser}' and password_user = '{passUser}'";
            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count >0)
            {
                MessageBox.Show("Пользователь уже существует");
                return true;
            }
            else
                return false;

        }

    }
}
