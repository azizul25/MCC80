using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectivity.Menu
{
    public class histories
    {
        private static string _connectionString = "Data Source=DESKTOP-L3T0EDT\\MSSQLSERVER01;" +
"database=db_kantor;" +
"Integrated Security=True;" +
"Connect Timeout=30;";

        private static SqlConnection _connection;
        public static void GetHistories()
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_histories";


            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Start Date: " + reader.GetDateTime(0));
                        Console.WriteLine("Employee Id: " + reader.GetInt32(1));
                        Console.WriteLine("End Date: " + reader.GetDateTime(2));
                        Console.WriteLine("Department Id: " + reader.GetInt32(3));
                        Console.WriteLine("Job Id: " + reader.GetInt32(4));
                    }
                }
                else
                {
                    reader.Close();
                    _connection.Close();

                }
            }
            catch
            {
                Console.WriteLine("Tidak ada Histories");
            }


        }

        public static void InsertHistories(DateTime start_date, DateTime end_date)
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO tbl_histories VALUES (@start_Date), (@end_Date)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pStartDate = new SqlParameter();
                pStartDate.ParameterName = "@start_date";
                pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pStartDate.Value = start_date;
                sqlCommand.Parameters.Add(pStartDate);

                SqlParameter pEndDate = new SqlParameter();
                pEndDate.ParameterName = "@end_date";
                pEndDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pEndDate.Value = end_date;
                sqlCommand.Parameters.Add(pEndDate);


                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Berhasil");
                }
                else
                {
                    Console.WriteLine("Gagal");
                }

                transaction.Commit();
                _connection.Close();

            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("Tidak ada Id");
            }


        }

        public static DateTime DateValidation()
        {
            DateTime inputTanggal;
            Console.Write("Tanggal (mm/dd/yyyy) : ");
            bool cekValid = DateTime.TryParse(Console.ReadLine(), out inputTanggal);
            if (!cekValid)
            {
                Console.WriteLine("Tanggal salah, masukkan ulang!");
                DateValidation();
            }
            return inputTanggal;
        }

        public static void InsertIntoHistories()
        {
            Console.Write("StartDate : ");
            DateTime inputStartDate = DateValidation();
            Console.Write("EndDate : ");
            DateTime inputEndDate = DateValidation();

            InsertHistories(inputStartDate, inputEndDate);
        }

        public static void UpdateHistories(DateTime start_date, DateTime end_date)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE tbl_histories SET end_date = @end_date WHERE start_date = @start_date";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pStartDate = new SqlParameter();
                pStartDate.ParameterName = "@start_date";
                pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pStartDate.Value = start_date;
                sqlCommand.Parameters.Add(pStartDate);

                SqlParameter pEndDate = new SqlParameter();
                pEndDate.ParameterName = "@end_date";
                pEndDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pEndDate.Value = end_date;
                sqlCommand.Parameters.Add(pEndDate);



                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Update Success");
                }
                else
                {
                    Console.WriteLine("Update Fail");
                }
                transaction.Commit();
                _connection.Close();
            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("Error connecting to the database");
            }
        }

        public static void UpdateIntoHistories()
        {
            Console.Write("StartDate : ");
            DateTime inputStartDate = DateValidation();
            Console.Write("EndDate : ");
            DateTime inputEndDate = DateValidation();

            UpdateHistories(inputStartDate, inputEndDate);

        }

        public static void DeleteHistories(DateTime start_date)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM tbl_histories WHERE start_date = @start_date";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pStartDate = new SqlParameter();
                pStartDate.ParameterName = "@start_date";
                pStartDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pStartDate.Value = start_date;
                sqlCommand.Parameters.Add(pStartDate);

                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Delete Success");
                }
                else
                {
                    Console.WriteLine("Delete Fail");
                }
                transaction.Commit();
                _connection.Close();
            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("Error connecting to the database");
            }
        }

        public static void DeleteByHistories()
        {
            Console.Write("Hapus History Start Date: ");
            DateTime inputStartDate = DateValidation();

            DeleteHistories(inputStartDate);
        }

        public static void GetHistoriesById(DateTime start_date)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_histories WHERE start_date = @start_date";
            sqlCommand.Parameters.AddWithValue("@start_date", start_date);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Start Date: " + reader.GetDateTime(0));
                        Console.WriteLine("Employee Id: " + reader.GetInt32(1));
                        Console.WriteLine("End Date: " + reader.GetDateTime(2));
                        Console.WriteLine("Department Id: " + reader.GetInt32(3));
                        Console.WriteLine("Job Id: " + reader.GetInt32(4));
                    }
                }
                else
                {
                    Console.WriteLine("tidak ada id");
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        public static void SearchHistoriesById()
        {
            DateTime inputStartDate = DateValidation();
            Console.Write("Cari Histories berdasarkan StartDate : ");
            GetHistoriesById(inputStartDate);
        }

        public static void MenuHistories()
        {
            Console.WriteLine("Basic Authentication");
            Console.WriteLine("Pilih menu ");
            Console.WriteLine("1. Tambah Histories");
            Console.WriteLine("2. Update Histories");
            Console.WriteLine("3. Hapus Histories");
            Console.WriteLine("4. Search By Id Histories");
            Console.WriteLine("5. Get All Histories");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");

            try
            {
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {

                    case 1:
                        Console.Clear();
                        InsertIntoHistories();
                        MenuHistories();
                        break;
                    case 2:
                        Console.Clear();
                        UpdateIntoHistories();
                        MenuHistories();
                        break;
                    case 3:
                        Console.Clear();
                        DeleteByHistories();
                        MenuHistories();
                        break;
                    case 4:
                        Console.Clear();
                        SearchHistoriesById();
                        MenuHistories();
                        break;
                    case 5:
                        GetHistories();
                        MenuHistories();
                        break;
                    case 6:
                        Console.Clear();
                        Program.MainMenu();
                        break;
                    default:
                        Console.WriteLine("Silahkan Pilih Nomor 1-6");
                        MenuHistories();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("tidak ada!");
                MenuHistories();
            }
        }
    }
}
