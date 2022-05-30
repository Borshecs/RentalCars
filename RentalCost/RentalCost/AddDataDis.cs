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
    public partial class AddDataDis : Form
    {
        DataBaseConnect dataBaseConnect = new DataBaseConnect();
        public AddDataDis()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void AddDataDis_Load(object sender, EventArgs e)
        {

        }

        private void button_save_Click(object sender, EventArgs e)
        {
            dataBaseConnect.OpenConnection();


            
            var amountdis = textBox_AmountDis.Text;
            var name = textBox_nameDis.Text;
    

            if (amountdis.Length != 0 && name.Length != 0 && (decimal.TryParse(amountdis, out decimal result)))
            {
                var addQuery = $"insert into Discounts(AmountDiscount, Name) values ('{amountdis}','{name}')";

                var command = new SqlCommand(addQuery, dataBaseConnect.GetConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись создана успешно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);






            }
            else
                MessageBox.Show("Запись не удалось создать, проверьте, чтобы не было пустых полей и в поле <Размер скидки> было введено число", "Не удалось, проверьте!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dataBaseConnect.ClosedConnection();
        }
    }
}
