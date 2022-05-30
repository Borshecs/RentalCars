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
    public partial class Veh_add : Form
    {
        DataBaseConnect dataBaseConnect = new DataBaseConnect();
        public Veh_add()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Veh_add_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet20.Clients". При необходимости она может быть перемещена или удалена.
            this.clientsTableAdapter3.Fill(this.rentcostDataSet20.Clients);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet19.Clients". При необходимости она может быть перемещена или удалена.
            this.clientsTableAdapter2.Fill(this.rentcostDataSet19.Clients);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet18.Cars". При необходимости она может быть перемещена или удалена.
            this.carsTableAdapter1.Fill(this.rentcostDataSet18.Cars);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet17.Clients". При необходимости она может быть перемещена или удалена.
            this.clientsTableAdapter1.Fill(this.rentcostDataSet17.Clients);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet7.Clients". При необходимости она может быть перемещена или удалена.
            this.clientsTableAdapter.Fill(this.rentcostDataSet7.Clients);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet6.Cars". При необходимости она может быть перемещена или удалена.
            this.carsTableAdapter.Fill(this.rentcostDataSet6.Cars);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet4.IssuedVehicles". При необходимости она может быть перемещена или удалена.
            this.issuedVehiclesTableAdapter1.Fill(this.rentcostDataSet4.IssuedVehicles);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet3.IssuedVehicles". При необходимости она может быть перемещена или удалена.
            this.issuedVehiclesTableAdapter.Fill(this.rentcostDataSet3.IssuedVehicles);

        }

        private void button_saveIssVeh_Click(object sender, EventArgs e)
        {
            dataBaseConnect.OpenConnection();

         
            var dateofissue = dateTimePicker_issue.Value.ToString("dd/MM/yyyy");
            var returndata = dateTimePicker_return.Value.ToString("dd/MM/yyyy");
            var carcondition = comboBox1_carcondition.Text;
            var id_clients = comboBox_idClients.SelectedValue;
            var id_cars = comboBox_idAuto.SelectedValue;

            
     
            if (dateofissue.Length != 0 && returndata.Length != 0 && carcondition.Length != 0 && id_clients.ToString().Length != 0 && id_cars.ToString().Length != 0)
            {
                var addQuery = $"insert into IssuedVehicles(DateOfIssue, ReturnDate, CarCondition, ID_Clients, ID_Cars) values ('{dateofissue}','{returndata}','{carcondition}', '{id_clients}','{id_cars}')";

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
