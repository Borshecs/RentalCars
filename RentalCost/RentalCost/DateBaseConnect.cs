using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RentalCost
{   class DataBaseConnect
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-JD87N8G;Initial Catalog=rentcost;Integrated Security=True"); //подключаем 
        

        public void OpenConnection() //открываем 
        {
            if(sqlConnection.State == System.Data.ConnectionState.Closed) //если подлюкчение закрыто - окрыть
            {
                sqlConnection.Open();
            }
        }

        public void ClosedConnection() //закрываем
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }
    }
       


}
