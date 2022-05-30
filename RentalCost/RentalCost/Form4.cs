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
    
    public partial class Form4 : Form
    {
        DataBaseConnect dataBaseConnect = new DataBaseConnect();
        public Form4()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet13.IssuedVehicles". При необходимости она может быть перемещена или удалена.
            this.issuedVehiclesTableAdapter.Fill(this.rentcostDataSet13.IssuedVehicles);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet12.Discounts". При необходимости она может быть перемещена или удалена.
            this.discountsTableAdapter.Fill(this.rentcostDataSet12.Discounts);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBaseConnect.OpenConnection();


            var id_dis = comboBox_idDis.Text;
            var id_issveh = comboBox_idcars.Text;
           
            if (id_dis.Length != 0 && id_issveh.Length != 0)
            {
                var addQuery = $"insert into IssuedVehicles_Discounts(ID_Discounts, ID_IssuedVehicles) values ('{id_dis}','{id_issveh}')";

                var command = new SqlCommand(addQuery, dataBaseConnect.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись создана успешно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);






            }
            else
                MessageBox.Show("Запись не удалось создать, проверьте, чтобы не было пустых полей", "Не удалось, проверьте, чтобы не было пустых полей!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dataBaseConnect.ClosedConnection();
        }
    }
}
