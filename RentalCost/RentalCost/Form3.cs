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
    public partial class Form3 : Form
    {
        DataBaseConnect dataBaseConnect = new DataBaseConnect();
        public Form3()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet5.Cars". При необходимости она может быть перемещена или удалена.
            

        }

        private void button_savedata_Click(object sender, EventArgs e)
        {
            dataBaseConnect.OpenConnection();

            var price = textBox_surname.Text;
            var rentcost = textBox_name.Text;
            var type = textBox_patromonyc.Text;
            var brand = textBox_address.Text;
            var yearofissue = dateTimePicker_year.Value.ToString("yyyy");


            if (yearofissue.Length != 0 && price.Length != 0 && rentcost.Length != 0 && type.Length !=0 && brand.Length !=0 && Int32.TryParse(price, out int result) && Int32.TryParse(rentcost, out int res))
            {
                var addQuery = $"insert into cars(price, rentalcost, type, brand, yearofissue) values ('{price}','{rentcost}','{type}', '{brand}','{yearofissue}')";

                var command = new SqlCommand(addQuery, dataBaseConnect.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись создана успешно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Запись не удалось создать, проверьте, чтобы не было пустых полей", "Не удалось, проверьте, чтобы не было пустых полей", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dataBaseConnect.ClosedConnection();
        }
    }
}

