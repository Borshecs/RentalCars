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
    public partial class Add_info : Form
    {   DataBaseConnect dataBaseConnect = new DataBaseConnect();    
        public Add_info()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button_savedata_Click(object sender, EventArgs e)
        {
            dataBaseConnect.OpenConnection();

            var surname = textBox_surname.Text;
            var name = textBox_name.Text;
            var patronomyc = textBox_patromonyc.Text;
            var address = textBox_address.Text;
            var phone = textBox_phone.Text;

            if(surname.Length != 0 && name.Length != 0 && patronomyc.Length != 0 && address.Length != 0 && phone.Length != 0 && Int64.TryParse(phone, out long result))
            {
                var addQuery = $"insert into clients(surname, name, patronymic, address, phone) values ('{surname}','{name}','{patronomyc}', '{address}','{phone}')";

                var command = new SqlCommand(addQuery, dataBaseConnect.GetConnection());
                command.ExecuteNonQuery();

                /*  using (SqlCommand cmd = new SqlCommand("DateTestClients", dataBaseConnect.GetConnection()))
                 {
                     SqlParameter surnameParam = new SqlParameter
                     {
                         ParameterName = "@Surname",
                         Value = textBox_surname

                 };
                     cmd.Parameters.Add(surnameParam);

                     SqlParameter nameParam = new SqlParameter
                     {
                         ParameterName = "@Name",
                         Value = textBox_name.Text

                     };
                     cmd.Parameters.Add(nameParam);

                     SqlParameter patronomycParam = new SqlParameter
                     {
                         ParameterName = "@Patronomyc",
                         Value = textBox_surname.Text

                     };
                     cmd.Parameters.Add(patronomycParam);

                     SqlParameter addressParam = new SqlParameter
                     {
                         ParameterName = "@Address",
                         Value = textBox_address.Text

                     };
                     cmd.Parameters.Add(addressParam);
                     SqlParameter phoneParam = new SqlParameter
                     {
                         ParameterName = "@Phone",
                         Value = textBox_phone.Text

                     };
                     cmd.Parameters.Add(phoneParam);

                     cmd.ExecuteNonQuery();

                 }*/



                MessageBox.Show("Запись создана успешно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
               
                

                

            }
            else
                MessageBox.Show("Запись не удалось создать, проверьте, чтобы не было пустых полей.", "Не удалось, проверьте, чтобы не было пустых полей!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dataBaseConnect.ClosedConnection();
        }

        private void Add_info_Load(object sender, EventArgs e)
        {

        }
    }
}
