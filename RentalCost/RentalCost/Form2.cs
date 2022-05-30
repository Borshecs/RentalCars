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
   
    public partial class Form2 : Form
    {
        

        DataBaseConnect dataBaseConnect = new DataBaseConnect();

        int SelectedRow;

        public Form2()
        {
            
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            
           
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id_cars", "п/п авто");
            dataGridView1.Columns.Add("Price", "Цена авто");
            dataGridView1.Columns.Add("RentalCost", "Цена в час");
            dataGridView1.Columns.Add("Type", "Тип авто");
            dataGridView1.Columns.Add("Brand", "Марка");
            dataGridView1.Columns.Add("YearOfIssue", "Год выпуска");
            dataGridView1.Columns.Add("IsNew", string.Empty);

        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record) //предоставляет доступ к значениями столбцов
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetDecimal(1), record.GetDecimal(2), record.GetString(3), record.GetString(4), record.GetString(5), RowState.ModifiedNew);

        }
        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from cars";
            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            dataBaseConnect.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }
      

        private void label4_Click(object sender, EventArgs e)
        {

        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[SelectedRow];
                textBox_id.Text = row.Cells[0].Value.ToString();
                textBox_surname.Text = row.Cells[1].Value.ToString();
                textBox_name.Text = row.Cells[2].Value.ToString();
                textBox_patronymic.Text = row.Cells[3].Value.ToString();
                textBox_address.Text = row.Cells[4].Value.ToString();
                textBox_phone.Text = row.Cells[5].Value.ToString();
                
              

            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }

}
