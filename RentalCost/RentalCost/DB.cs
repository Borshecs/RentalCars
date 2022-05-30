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
    enum RowState //запрос для msSQL
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class DB : Form
    {
        private readonly check_user _user;



        DataBaseConnect dataBaseConnect = new DataBaseConnect();

        int SelectedRow;

        public DB(check_user user)
        {
            _user = user;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;



        }



        private void IsAdmin() //разрешить админу, запрет юзеру
        {
            toolStripMenuItem1.Enabled = _user.IsAdmin;
            button_newEntry.Enabled = _user.IsAdmin;
        }

        private void CreateColumns() //создаем колонки для работы с dgv
        {
            dataGridView1.Columns.Add("id_clients", "id");
            dataGridView1.Columns.Add("Surname", "Фамилия");
            dataGridView1.Columns.Add("Name", "Имя");
            dataGridView1.Columns.Add("Patronymic", "Отчество");
            dataGridView1.Columns.Add("Address", "Адрес");
            dataGridView1.Columns.Add("Phone", "Телефон");
            dataGridView1.Columns.Add("IsNew", string.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;

        }



        private void ClearFields() //фукнция очистить поля
        {
            textBox_id.Text = "";
            textBox_surname.Text = "";
            textBox_name.Text = "";
            textBox_patronymic.Text = "";
            textBox_address.Text = "";
            textBox_phone.Text = "";
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record) //предоставляет доступ к значениями столбцов
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), RowState.ModifiedNew);



        }

        private void RefreshDataGrid(DataGridView dgv) //обновить dgv
        {
            dgv.Rows.Clear();
            string queryString = $"select * from clients";
            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            dataBaseConnect.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }
        private void DB_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet16.Cars". При необходимости она может быть перемещена или удалена.
            this.carsTableAdapter1.Fill(this.rentcostDataSet16.Cars);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet15.Clients". При необходимости она может быть перемещена или удалена.
            this.clientsTableAdapter.Fill(this.rentcostDataSet15.Clients);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet14.Cars". При необходимости она может быть перемещена или удалена.
            this.carsTableAdapter.Fill(this.rentcostDataSet14.Cars);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet11.IssuedVehicles". При необходимости она может быть перемещена или удалена.
            this.issuedVehiclesTableAdapter2.Fill(this.rentcostDataSet11.IssuedVehicles);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet10.Discounts". При необходимости она может быть перемещена или удалена.
            this.discountsTableAdapter.Fill(this.rentcostDataSet10.Discounts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet9.IssuedVehicles_Discounts". При необходимости она может быть перемещена или удалена.
            this.issuedVehicles_DiscountsTableAdapter.Fill(this.rentcostDataSet9.IssuedVehicles_Discounts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet2.IssuedVehicles". При необходимости она может быть перемещена или удалена.
            this.issuedVehiclesTableAdapter1.Fill(this.rentcostDataSet2.IssuedVehicles);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rentcostDataSet.IssuedVehicles". При необходимости она может быть перемещена или удалена.
            this.issuedVehiclesTableAdapter.Fill(this.rentcostDataSet.IssuedVehicles);
            toolStripTextBox1.Text = $"{_user.Login}: {_user.Status}";
            IsAdmin();
            CreateColumns();
            CreateColumnsCars();
            CreateColumnsIssuedVehicles();
            CreateColumnsDis();
            CreateColumnsFines();
           

            RefreshDataGrid(dataGridView1);
            RefreshDataGridCars(dataGridView2);
            RefreshDataGridIssued(dataGridView3);
            RefreshDataGridDis(dataGridView4);
            RefreshDataGridFines(dataGridView5);
            

            textBox_amountDis.Maximum = 50000000;

            textBox_amountFines.Maximum = 50000000;

            dateTimePicker_years.Format = DateTimePickerFormat.Custom;
            dateTimePicker_years.ValueChanged += dateTimePicker_years_ValueChanged;

            dateTimePicker_issue.Format = DateTimePickerFormat.Short;
            dateTimePicker_issue.ValueChanged += dateTimePicker_issue_ValueChanged;

            dateTimePicker_return.Format = DateTimePickerFormat.Short;
            dateTimePicker_return.ValueChanged += dateTimePicker_return_ValueChanged;

            comboBox1_carcondition.SelectedIndexChanged += comboBox1_carcondition_SelectedIndexChanged;
            comboBox_idAuto.SelectedIndexChanged += comboBox_idAuto_SelectedIndexChanged;
            comboBox_idClients.SelectedIndexChanged += comboBox_idClients_SelectedIndexChanged;


         


        }

   

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //при выборе поля - параметры перенсти в textbox'ы
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

        private void pictureBox_refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();

        }

        private void button_newEntry_Click(object sender, EventArgs e)
        {
            Add_info add_Info = new Add_info();
            add_Info.Show();
        }

        private void SearchInfo(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select * from clients where concat (id_clients, surname, name, patronymic, address, phone) like '%" + textBox_search.Text + "%'";

            SqlCommand com = new SqlCommand(queryString, dataBaseConnect.GetConnection());
            dataBaseConnect.OpenConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgv, read);
            }

            read.Close();
        }

        private void DeleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;

                return;

            }
            dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
        }

        private void Update()
        {
            dataBaseConnect.OpenConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {

                var rowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from clients where id_clients = {id}";

                    StringBuilder errorMessages = new StringBuilder();
                    try
                    {
                        var command = new SqlCommand(deleteQuery, dataBaseConnect.GetConnection());
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append(
                             "Ошибка: " + ex.Errors[i].Message + "\n"
                             );
                        }
                        MessageBox.Show(errorMessages.ToString());
                    }

                }
                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var surname = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var patronomyc = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var address = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var phone = dataGridView1.Rows[index].Cells[5].Value.ToString();

                    var changeQuery = $"update clients set surname = '{surname}', name = '{name}', patronymic = '{patronomyc}', address = '{address}', phone = '{phone}' where id_clients = '{id}'";

                    var command = new SqlCommand(changeQuery, dataBaseConnect.GetConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBaseConnect.ClosedConnection();
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            SearchInfo(dataGridView1);
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            DeleteRow();
            ClearFields();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void ChangeData()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBox_id.Text;
            var surname = textBox_surname.Text;
            var name = textBox_name.Text;
            var patronomyc = textBox_patronymic.Text;
            var address = textBox_address.Text;
            var phone = textBox_phone.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (surname.Length != 0 && name.Length != 0 && patronomyc.Length != 0 && address.Length != 0 && phone.Length != 0)
                {
                    dataGridView1.Rows[selectedRowIndex].SetValues(id, surname, name, patronomyc, address, phone);

                    dataGridView1.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;


                }
                else
                {
                    MessageBox.Show("Запись не удалось изменить, Вы заполнили не все поля.", "Не удалось, Вы заполнили не все поля! ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void button_Change_Click(object sender, EventArgs e)
        {
            ChangeData();

        }

        private void button_cleartextbox_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        ///////////////////
        ///тачки ниже пошли
        ///////////////////

        private void CreateColumnsCars()
        {
            dataGridView2.Columns.Add("id_cars", "id");
            dataGridView2.Columns.Add("Price", "Цена");
            dataGridView2.Columns.Add("RentalCost", "Цена аренды в час");
            dataGridView2.Columns.Add("Type", "Тип авто");
            dataGridView2.Columns.Add("Brand", "Марка");
            dataGridView2.Columns.Add("YearOfIssue", "Год выпуска");
            dataGridView2.Columns.Add("IsNew", string.Empty);
            dataGridView2.Columns["IsNew"].Visible = false;

        }
        private void ReadSingleRowCars(DataGridView dgv, IDataRecord record) //предоставляет доступ к значениями столбцов
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetDecimal(1), record.GetDecimal(2), record.GetString(3), record.GetString(4), record.GetDateTime(5), RowState.ModifiedNew);

        }
        private void RefreshDataGridCars(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from cars";
            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            dataBaseConnect.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowCars(dgv, reader);
            }
            reader.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[SelectedRow];
                textBox_idcars.Text = row.Cells[0].Value.ToString();
                textBox_price.Text = row.Cells[1].Value.ToString();
                textBox_rentcost.Text = row.Cells[2].Value.ToString();
                textBox_type.Text = row.Cells[3].Value.ToString();
                textBox_brand.Text = row.Cells[4].Value.ToString();
                dateTimePicker_years.Text = row.Cells[5].Value.ToString();




            }

        }

        private void pictureBox_refresh1_Click(object sender, EventArgs e)
        {
            RefreshDataGridCars(dataGridView2);
            ClearFieldsCars();
        }

        private void button_newEntry1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void SearchInfoCars(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select * from cars where concat (id_cars, price, rentalcost, type, brand, yearofissue, ID_IssuedVehicles) like '%" + textBox_search1.Text + "%'";

            SqlCommand com = new SqlCommand(queryString, dataBaseConnect.GetConnection());
            dataBaseConnect.OpenConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowCars(dgv, read);
            }

            read.Close();
        }
        private void DeleteRowCars()
        {
            int index = dataGridView2.CurrentCell.RowIndex;

            dataGridView2.Rows[index].Visible = false;

            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[6].Value = RowState.Deleted;

                return;

            }
            dataGridView2.Rows[index].Cells[6].Value = RowState.Deleted;
        }
        private void UpdateCars()
        {
            dataBaseConnect.OpenConnection();

            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {

                var rowState = (RowState)dataGridView2.Rows[index].Cells[6].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from cars where id_cars = {id}";

                    StringBuilder errorMessages = new StringBuilder();
                    try
                    {
                        var command = new SqlCommand(deleteQuery, dataBaseConnect.GetConnection());
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append(
                             "Ошибка: " + ex.Errors[i].Message + "\n"
                             );
                        }
                        MessageBox.Show(errorMessages.ToString());
                    }

                }
                if (rowState == RowState.Modified)
                {
                    var id = dataGridView2.Rows[index].Cells[0].Value.ToString();
                    var price = dataGridView2.Rows[index].Cells[1].Value.ToString();
                    var rentcost = dataGridView2.Rows[index].Cells[2].Value.ToString();
                    var type = dataGridView2.Rows[index].Cells[3].Value.ToString();
                    var brand = dataGridView2.Rows[index].Cells[4].Value.ToString();
                    var yearofissue = dataGridView2.Rows[index].Cells[5].Value.ToString();


                    var changeQuery = $"update cars set price = '{price}', rentalcost = '{rentcost}', type = '{type}', brand = '{brand}', yearofissue = '{yearofissue}' where id_cars = '{id}'";

                    var command = new SqlCommand(changeQuery, dataBaseConnect.GetConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBaseConnect.ClosedConnection();
        }

        private void textBox_search1_TextChanged(object sender, EventArgs e)
        {
            SearchInfoCars(dataGridView2);
        }
        private void ClearFieldsCars()
        {
            textBox_idcars.Text = "";
            textBox_price.Text = "";
            textBox_rentcost.Text = "";
            textBox_type.Text = "";
            textBox_brand.Text = "";
            dateTimePicker_years.Value = new DateTime(2022, 6, 20);

        }
        private void button_delete1_Click(object sender, EventArgs e)
        {

            DeleteRowCars();
            ClearFieldsCars();
        }

        private void button_save1_Click(object sender, EventArgs e)
        {
            UpdateCars();
        }

        private void ChangeDataCars()
        {
            var selectedRowIndex = dataGridView2.CurrentCell.RowIndex;
            var id = textBox_idcars.Text;
            var surname = textBox_price.Text;
            var name = textBox_rentcost.Text;
            var patronomyc = textBox_type.Text;
            var address = textBox_brand.Text;
            var phone = dateTimePicker_years.Value.ToString("yyyy");



            if (dataGridView2.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (surname.Length != 0 && name.Length != 0 && patronomyc.Length != 0 && address.Length != 0 && phone.Length != 0)
                {
                    dataGridView2.Rows[selectedRowIndex].SetValues(id, surname, name, patronomyc, address, phone);

                    dataGridView2.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;


                }
                else
                {
                    MessageBox.Show("Запись не удалось изменить, Вы заполнили не все поля.", "Не удалось, Вы заполнили не все поля!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void button_change1_Click(object sender, EventArgs e)
        {
            ChangeDataCars();

        }

        private void pictureBox_clearbox_Click(object sender, EventArgs e)
        {
            ClearFieldsCars();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        ////
        //выданные авто
        ////
        private void CreateColumnsIssuedVehicles()
        {
            dataGridView3.Columns.Add("DateOfIssue", "Дата выдачи");
            dataGridView3.Columns.Add("ReturnDate", "Дата возврата");
            dataGridView3.Columns.Add("CarCondition", "Состояние авто");
            dataGridView3.Columns.Add("ID_clients", "ID клиента");
            dataGridView3.Columns.Add("ID_cars", "ID авто");
            dataGridView3.Columns.Add("ID_issuedVehicles", "ID выданного авто");
            dataGridView3.Columns.Add("IsNew", string.Empty);
            dataGridView3.Columns["IsNew"].Visible = false;

        }


        private void comboBox1_carcondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1_carcondition.SelectedItem.ToString();
        }




        private void dateTimePicker_issue_ValueChanged(object sender, EventArgs e)
        {
            DateTime issue = dateTimePicker_issue.Value.Date;
        }

        private void dateTimePicker_return_ValueChanged(object sender, EventArgs e)
        {
            DateTime returnday = dateTimePicker_return.Value.Date;

        }
        private void ClearFieldsIssued()
        {
            dateTimePicker_issue.Value = new DateTime(2022, 6, 20);
            dateTimePicker_return.Value = new DateTime(2022, 6, 20);
            comboBox1_carcondition.Text = "";
            comboBox_idAuto.Text = "";
            comboBox_idClients.Text = "";

            textBox_id_iss_veh.Text = "";
        }
        private void ReadSingleRowIssued(DataGridView dgv, IDataRecord record) //предоставляет доступ к значениями столбцов
        {
            dgv.Rows.Add(record.GetDateTime(0), record.GetDateTime(1), record.GetString(2), record.GetInt32(3), record.GetInt32(4), record.GetInt32(5), RowState.ModifiedNew);

        }
        private void RefreshDataGridIssued(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from IssuedVehicles";
            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            dataBaseConnect.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowIssued(dgv, reader);
            }
            reader.Close();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[SelectedRow];
                dateTimePicker_issue.Text = row.Cells[0].Value.ToString();
                dateTimePicker_return.Text = row.Cells[1].Value.ToString();
                comboBox1_carcondition.Text = row.Cells[2].Value.ToString();
                comboBox_idClients.Text = row.Cells[3].Value.ToString();
                comboBox_idAuto.Text = row.Cells[4].Value.ToString();
                textBox_id_iss_veh.Text = row.Cells[5].Value.ToString();

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            RefreshDataGridIssued(dataGridView3);
            ClearFieldsIssued();
        }

        private void button_newEntry2_Click(object sender, EventArgs e)
        {
            Veh_add veh_Add = new Veh_add();
            veh_Add.Show();

        }
        private void SearchInfoVeh(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select * from IssuedVehicles where concat (DateOfIssue, ReturnDate, CarCondition, ID_Clients, ID_Cars, ID_IssuedVehicles) like '%" + textBox_search_veh.Text + "%'";

            SqlCommand com = new SqlCommand(queryString, dataBaseConnect.GetConnection());
            dataBaseConnect.OpenConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowIssued(dgv, read);
            }

            read.Close();
        }
        private void DeleteRowIssued()
        {
            int index = dataGridView3.CurrentCell.RowIndex;

            dataGridView3.Rows[index].Visible = false;

            if (dataGridView3.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView3.Rows[index].Cells[6].Value = RowState.Deleted;

                return;

            }
            dataGridView3.Rows[index].Cells[6].Value = RowState.Deleted;
        }
        private void UpdateIssued()
        {
            dataBaseConnect.OpenConnection();

            for (int index = 0; index < dataGridView3.Rows.Count; index++)
            {

                var rowState = (RowState)dataGridView3.Rows[index].Cells[6].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    
                    var id = Convert.ToInt32(dataGridView3.Rows[index].Cells[5].Value);
                    var deleteQuery = $"delete from IssuedVehicles where id_IssuedVehicles = {id}";
                    StringBuilder errorMessages = new StringBuilder();
                    try
                    {
                        var command = new SqlCommand(deleteQuery, dataBaseConnect.GetConnection());
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append(
                             "Ошибка: " + ex.Errors[i].Message + "\n"
                             );
                        }
                        MessageBox.Show(errorMessages.ToString());
                    }

                }
                if (rowState == RowState.Modified)
                {
                    var id = dataGridView3.Rows[index].Cells[0].Value.ToString();
                    var price = dataGridView3.Rows[index].Cells[1].Value.ToString();
                    var rentcost = dataGridView3.Rows[index].Cells[2].Value.ToString();
                    var type = dataGridView3.Rows[index].Cells[3].Value.ToString();
                    var brand = dataGridView3.Rows[index].Cells[4].Value.ToString();
                    var id_issuedvehicles = dataGridView3.Rows[index].Cells[5].Value.ToString();
                    StringBuilder errorMessages = new StringBuilder();
                    var changeQuery = $"SET IDENTITY_INSERT cars ON update IssuedVehicles set DateOfIssue = '{id}', ReturnDate = '{price}', CarCondition = '{rentcost}', ID_Clients = '{type}',  ID_Cars = '{brand}' where id_issuedvehicles = '{id_issuedvehicles}' SET IDENTITY_INSERT cars OFF";
                    try {
                        var command = new SqlCommand(changeQuery, dataBaseConnect.GetConnection());
                        command.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {
                        for (int i =0; i < ex.Errors.Count; i++)
                        {
                            errorMessages.Append(
                            "Ошибка!! Нельзя удалить данную запись, не удалив данные из родительской таблицы"
                            );
                        }
                        Console.WriteLine(errorMessages.ToString());
                    }
                }


                   

            }

            dataBaseConnect.ClosedConnection();
        }

        private void textBox_search_veh_TextChanged(object sender, EventArgs e)
        {
            SearchInfoVeh(dataGridView3);

        }

        private void button_del2_Click(object sender, EventArgs e)
        {
            DeleteRowIssued();
            ClearFieldsIssued();
        }

        private void button_save2_Click(object sender, EventArgs e)
        {
            UpdateIssued();
        }
        private void ChangeDataIssued()
        {

            var selectedRowIndex = dataGridView3.CurrentCell.RowIndex;
            var dateofissue = dateTimePicker_issue.Value.ToString("dd/MM/yyyy");
            var returndata = dateTimePicker_return.Value.ToString("dd/MM/yyyy");
            var carcondition = comboBox1_carcondition.Text;
            var id_clients = comboBox_idClients.Text;
            var id_cars = comboBox_idAuto.Text;
            var id_issveh = textBox_id_iss_veh.Text;


            if (dataGridView3.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (id_issveh.Length != 0 && dateofissue.Length != 0 && returndata.Length != 0 && carcondition.Length != 0 && id_clients.Length != 0 && id_cars.Length != 0)
                {
                    dataGridView3.Rows[selectedRowIndex].SetValues(dateofissue, returndata, carcondition, id_clients, id_cars, id_issveh);

                    dataGridView3.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;


                }
                else
                {
                    MessageBox.Show("Запись не удалось изменить, Вы заполнили не все поля.", "Не удалось, Вы заполнили не все поля!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void button_change2_Click(object sender, EventArgs e)
        {
            ChangeDataIssued();

        }

        private void pictureBox_clear_Click(object sender, EventArgs e)
        {
            ClearFieldsIssued();
        }

        private void comboBox_idClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox_idClients.SelectedItem.ToString();

        }

        private void comboBox_idAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox_idAuto.SelectedItem.ToString();
        }


        /*скидки*/

        private void CreateColumnsDis()
        {

            dataGridView4.Columns.Add("AmountDiscount", "Размер скидки");
            dataGridView4.Columns.Add("ID_Discounts", "id");
            dataGridView4.Columns.Add("Name", "Название скидки");

            dataGridView4.Columns.Add("IsNew", string.Empty);
            dataGridView4.Columns["IsNew"].Visible = false;

        }
        private void ClearFieldsDis()
        {
            textBox_idDis.Text = "";
            textBox_amountDis.Text = "";
            textBox_nameDis.Text = "";
        }
        private void ReadSingleRowDis(DataGridView dgv, IDataRecord record) //предоставляет доступ к значениями столбцов
        {
            dgv.Rows.Add(record.GetDecimal(0), record.GetInt32(1), record.GetString(2), RowState.ModifiedNew);

        }
        private void RefreshDataGridDis(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Discounts";
            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            dataBaseConnect.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowDis(dgv, reader);
            }
            reader.Close();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[SelectedRow];
                textBox_amountDis.Text = row.Cells[0].Value.ToString();
                textBox_idDis.Text = row.Cells[1].Value.ToString();
                textBox_nameDis.Text = row.Cells[2].Value.ToString();
            }

        }

        private void pictureBox_UpdateDis_Click(object sender, EventArgs e)
        {
            RefreshDataGridDis(dataGridView4);
            ClearFieldsDis();
        }

        private void button_newEntryDis_Click(object sender, EventArgs e)
        {
            AddDataDis addDataDis = new AddDataDis();
            addDataDis.Show();
        }

        private void SearchInfoDis(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select * from Discounts where concat (AmountDiscount, id_Discounts, name) like '%" + textBox_searchDis.Text + "%'";

            SqlCommand com = new SqlCommand(queryString, dataBaseConnect.GetConnection());
            dataBaseConnect.OpenConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowDis(dgv, read);
            }

            read.Close();
        }
        private void DeleteRowDis()
        {
            int index = dataGridView4.CurrentCell.RowIndex;

            dataGridView4.Rows[index].Visible = false;

            if (dataGridView4.Rows[index].Cells[1].Value.ToString() == string.Empty)
            {
                dataGridView4.Rows[index].Cells[3].Value = RowState.Deleted;

                return;

            }
            dataGridView4.Rows[index].Cells[3].Value = RowState.Deleted;
        }
        private void UpdateDis()
        {
            dataBaseConnect.OpenConnection();

            for (int index = 0; index < dataGridView4.Rows.Count; index++)
            {

                var rowState = (RowState)dataGridView4.Rows[index].Cells[3].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView4.Rows[index].Cells[1].Value);
                    var deleteQuery = $"delete from Discounts where id_Discounts = '{id}'";

                    var command = new SqlCommand(deleteQuery, dataBaseConnect.GetConnection());
                    command.ExecuteNonQuery();

                }
                if (rowState == RowState.Modified)
                {


                    /*var changeQuery = $"update Discounts set AmountDiscount = {amountDis}, Name = '{name}' where id_Discounts = '{id}'";

                    var command = new SqlCommand(changeQuery, dataBaseConnect.GetConnection());
                    command.ExecuteNonQuery();*/
                    var command = new SqlCommand("update Discounts set AmountDiscount = @amountDis, Name = @name where id_Discounts = @id", dataBaseConnect.GetConnection());
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox_idDis.Text));
                    command.Parameters.AddWithValue("amountDis", textBox_amountDis.Value);
                    command.Parameters.AddWithValue("name", textBox_nameDis.Text);
                    command.ExecuteNonQuery();
                }

            }

            dataBaseConnect.ClosedConnection();
        }

        private void textBox_searchDis_TextChanged(object sender, EventArgs e)
        {
            SearchInfoDis(dataGridView4);
        }

        private void button_delDis_Click(object sender, EventArgs e)
        {
            DeleteRowDis();
            ClearFieldsDis();
        }
        private void ChangeDataDis()
        {
            var selectedRowIndex = dataGridView4.CurrentCell.RowIndex;
            var id = textBox_idDis.Text;
            var surname = textBox_amountDis.Text;
            var name = textBox_nameDis.Text;


            if (dataGridView4.Rows[selectedRowIndex].Cells[1].Value.ToString() != string.Empty)
            {
                if (surname.Length != 0 && name.Length != 0 && (Decimal.TryParse(surname, out decimal result)))
                {
                    dataGridView4.Rows[selectedRowIndex].SetValues(surname, id, name);

                    dataGridView4.Rows[selectedRowIndex].Cells[3].Value = RowState.Modified;


                }
                else
                {
                    MessageBox.Show("Запись не удалось изменить, Вы заполнили не все поля.", "Не удалось, Вы заполнили не все поля! ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void button_changeDis_Click(object sender, EventArgs e)
        {
            ChangeDataDis();
        }

        private void pictureBox_refreshDis_Click(object sender, EventArgs e)
        {
            ClearFieldsDis();
        }


        /*штрафы*/

        private void CreateColumnsFines()
        {
            dataGridView5.Columns.Add("AmountFines", "Размер штрафа");
            dataGridView5.Columns.Add("ID_Fines", "ID");
            dataGridView5.Columns.Add("Name", "Название штрафа");
            dataGridView5.Columns.Add("IsNew", string.Empty);
            dataGridView5.Columns["IsNew"].Visible = false;

        }
        private void ReadSingleRowFines(DataGridView dgv, IDataRecord record) //предоставляет доступ к значениями столбцов
        {
            dgv.Rows.Add(record.GetDecimal(0), record.GetInt32(1), record.GetString(2), RowState.ModifiedNew);

        }

        private void RefreshDataGridFines(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Fines";
            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            dataBaseConnect.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowFines(dgv, reader);
            }
            reader.Close();
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5.Rows[SelectedRow];
                textBox_amountFines.Text = row.Cells[0].Value.ToString();
                textBox_idFines.Text = row.Cells[1].Value.ToString();
                textBox_nameFines.Text = row.Cells[2].Value.ToString();

            }

        }
        private void ClearFieldsFines()
        {
            textBox_idFines.Text = "";
            textBox_amountFines.Text = "";
            textBox_nameFines.Text = "";
        }
        private void pictureBox_clearFines_Click(object sender, EventArgs e)
        {

            ClearFieldsFines();
        }

        private void button_newEntryFines_Click(object sender, EventArgs e)
        {
            AddDataFines addDataFines = new AddDataFines();
            addDataFines.ShowDialog();

        }
        private void SearchInfoFines(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select * from Fines where concat (AmountFines, id_fines, name) like '%" + textBox_searchFines.Text + "%'";

            SqlCommand com = new SqlCommand(queryString, dataBaseConnect.GetConnection());
            dataBaseConnect.OpenConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowFines(dgv, read);
            }

            read.Close();
        }

        private void DeleteRowFines()
        {
            int index = dataGridView5.CurrentCell.RowIndex;

            dataGridView5.Rows[index].Visible = false;

            if (dataGridView5.Rows[index].Cells[1].Value.ToString() == string.Empty)
            {
                dataGridView5.Rows[index].Cells[3].Value = RowState.Deleted;

                return;

            }
            dataGridView5.Rows[index].Cells[3].Value = RowState.Deleted;
        }

        private void UpdateFines()
        {
            dataBaseConnect.OpenConnection();

            for (int index = 0; index < dataGridView5.Rows.Count; index++)
            {

                var rowState = (RowState)dataGridView5.Rows[index].Cells[3].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView5.Rows[index].Cells[1].Value);
                    var deleteQuery = $"delete from Fines where id_Fines = {id}";

                    var command = new SqlCommand(deleteQuery, dataBaseConnect.GetConnection());
                    command.ExecuteNonQuery();

                }
                if (rowState == RowState.Modified)
                {
                    /* var id = dataGridView5.Rows[index].Cells[0].Value;
                     var surname = dataGridView5.Rows[index].Cells[1].Value.ToString();
                     var name = dataGridView5.Rows[index].Cells[2].Value.ToString();*/

                    var command = new SqlCommand("update fines set AmountFines = @amountFines, Name = @name where id_Fines = @id", dataBaseConnect.GetConnection());
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox_idFines));
                    command.Parameters.AddWithValue("@amountFines", textBox_amountFines.Value);
                    command.Parameters.AddWithValue("name", textBox_nameFines.Text);

                    command.ExecuteNonQuery();
                }

            }

            dataBaseConnect.ClosedConnection();
        }
        private void textBox_searchFines_TextChanged(object sender, EventArgs e)
        {
            SearchInfoFines(dataGridView5);
        }

        private void button_delFines_Click(object sender, EventArgs e)
        {
            DeleteRowFines();
            ClearFieldsFines();

        }
        private void ChangeDataFines()
        {
            var selectedRowIndex = dataGridView5.CurrentCell.RowIndex;
            var id = textBox_idFines.Text;
            var surname = textBox_amountFines.Text;
            var name = textBox_nameFines.Text;


            if (dataGridView5.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (surname.Length != 0 && name.Length != 0)
                {
                    dataGridView5.Rows[selectedRowIndex].SetValues(id, surname, name);

                    dataGridView5.Rows[selectedRowIndex].Cells[3].Value = RowState.Modified;


                }
                else
                {
                    MessageBox.Show("Запись не удалось изменить, Вы заполнили не все поля.", "Не удалось, Вы заполнили не все поля! ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void button_changeFines_Click(object sender, EventArgs e)
        {
            ChangeDataFines();
        }

        private void pictureBox_refreshFines_Click(object sender, EventArgs e)
        {
            RefreshDataGridFines(dataGridView5);
            ClearFieldsFines();
        }

        private void button_saveFines_Click(object sender, EventArgs e)
        {
            UpdateFines();
        }

        private void button_saveDis_Click(object sender, EventArgs e)
        {
            UpdateDis();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dateTimePicker_years_ValueChanged(object sender, EventArgs e)
        {
            DateTime years = dateTimePicker_years.Value.Date;
        }

        private void comboBox1_carcondition_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        /*выд_скидк*//*
        private void CreateColumnsDisCars()
        {
            dataGridView6.Columns.Add("ID_Discounts", "ID скидки");
            dataGridView6.Columns.Add("ID_IssuedVehicles", "ID выданного авто");

            dataGridView6.Columns.Add("IsNew", string.Empty);
            dataGridView6.Columns["IsNew"].Visible = false;

        }
        private void ReadSingleRowDisCars(DataGridView dgv, IDataRecord record) //предоставляет доступ к значениями столбцов
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), RowState.ModifiedNew);

        }
        private void RefreshDataGriDisCars(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from IssuedVehicles_Discounts";
            SqlCommand command = new SqlCommand(queryString, dataBaseConnect.GetConnection());

            dataBaseConnect.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRowDisCars(dgv, reader);
            }
            reader.Close();
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView6.Rows[SelectedRow];
                comboBox_idDis.Text = row.Cells[0].Value.ToString();
                comboBox_idCars.Text = row.Cells[1].Value.ToString();

            }

        }

        private void pictureBox_refreshDisCar_Click(object sender, EventArgs e)
        {
            RefreshDataGriDisCars(dataGridView6);
            ClearFieldDisCars();
        }

        private void button_newEntryDisFines_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.ShowDialog();

        }

        private void SearchInfoDisCars(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select * from IssuedVehicles_Discounts where concat (id_discounts, id_IssuedVehicles) like '%" + textBox_searchDisCars.Text + "%'";

            SqlCommand com = new SqlCommand(queryString, dataBaseConnect.GetConnection());
            dataBaseConnect.OpenConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRowDisCars(dgv, read);
            }

            read.Close();
        }
        private void DeleteRowDisCars()
        {
            int index = dataGridView6.CurrentCell.RowIndex;

            dataGridView6.Rows[index].Visible = false;

            if (dataGridView6.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView6.Rows[index].Cells[2].Value = RowState.Deleted;

                return;

            }
            dataGridView6.Rows[index].Cells[2].Value = RowState.Deleted;
        }
        private void UpdateDisCars()
        {
            dataBaseConnect.OpenConnection();

            for (int index = 0; index < dataGridView6.Rows.Count; index++)
            {

                var rowState = (RowState)dataGridView6.Rows[index].Cells[2].Value;

                if (rowState == RowState.Existed)
                {
                    continue;
                }
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView6.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from IssuedVehicles_Discounts where ID_IssuedVehicles = {id}";

                    var command = new SqlCommand(deleteQuery, dataBaseConnect.GetConnection());
                    command.ExecuteNonQuery();

                }
                if (rowState == RowState.Modified)
                {
                    var id = dataGridView6.Rows[index].Cells[0].Value.ToString();
                    var price = dataGridView6.Rows[index].Cells[1].Value.ToString();
                    


                    var changeQuery = $"update IssuedVehicles_Discounts set ID_Discounts = '{id}', ID_IssuedVehicles = '{id}' where ID_IssuedVehicles = '{id}'";

                    var command = new SqlCommand(changeQuery, dataBaseConnect.GetConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBaseConnect.ClosedConnection();
        }

        private void textBox_searchDisCars_TextChanged(object sender, EventArgs e)
        {
            SearchInfoDisCars(dataGridView6);
        }

        private void ClearFieldDisCars()
        {
            comboBox_idCars.Text = "";
            comboBox_idDis.Text = "";

        }

        private void button_delDisFines_Click(object sender, EventArgs e)
        {
            DeleteRowDisCars();
            ClearFieldDisCars();

        }

        private void button_saveDisFines_Click(object sender, EventArgs e)
        {
            ChangeDataDisCars();
        }
        private void ChangeDataDisCars()
        {
            var selectedRowIndex = dataGridView6.CurrentCell.RowIndex;
            var id = comboBox_idDis.Text;
            var surname = comboBox_idCars.Text;
       



            if (dataGridView6.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (surname.Length != 0 && id.Length !=0)
                {
                    dataGridView6.Rows[selectedRowIndex].SetValues(id, surname);

                    dataGridView6.Rows[selectedRowIndex].Cells[2].Value = RowState.Modified;


                }
                else
                {
                    MessageBox.Show("Запись не удалось изменить, Вы заполнили не все поля.", "Не удалось, Вы заполнили не все поля!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void pictureBox_ClearDisCar_Click(object sender, EventArgs e)
        {
            ClearFieldDisCars();
        }
    }*/




    }
}
