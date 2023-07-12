using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectivity.Menu
{
    public class jobs
    {
        private static string _connectionString = "Data Source=DESKTOP-L3T0EDT\\MSSQLSERVER01;" +
       "database=db_kantor;" +
       "Integrated Security=True;" +
       "Connect Timeout=30;";

        private static SqlConnection _connection;
        public static void GetJobs()
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_jobs";


            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Title: " + reader.GetString(1));
                        Console.WriteLine("Min Salary: " + reader.GetInt32(2));
                        Console.WriteLine("Max Salary: " + reader.GetInt32(3));
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
                Console.WriteLine("Tidak ada Jobs");
            }


        }

        public static void InsertJobs(string title, int min_salary, int max_salary)
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO  tbl_jobs VALUES (@title), (@min_salary), (@max_salary)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pTitle = new SqlParameter();
                pTitle.ParameterName = "@title";
                pTitle.SqlDbType = System.Data.SqlDbType.VarChar;
                pTitle.Value = title;
                sqlCommand.Parameters.Add(pTitle);

                SqlParameter pMinSalary = new SqlParameter();
                pMinSalary.ParameterName = "@min_salary";
                pMinSalary.SqlDbType = System.Data.SqlDbType.Int;
                pMinSalary.Value = min_salary;
                sqlCommand.Parameters.Add(pMinSalary);

                SqlParameter pMaxSalary = new SqlParameter();
                pMaxSalary.ParameterName = "@max_salary";
                pMaxSalary.SqlDbType = System.Data.SqlDbType.VarChar;
                pMaxSalary.Value = max_salary;
                sqlCommand.Parameters.Add(pMaxSalary);

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
                Console.WriteLine("Tidak ada Regions");
            }


        }

        public static void InsertIntoJobs()
        {
            Console.WriteLine("Title: ");
            string inputTitle = Console.ReadLine();
            Console.WriteLine("Min Salary: ");
            int inputMinSalary = int.Parse(Console.ReadLine());
            Console.WriteLine("MaxSalary: ");
            int inputMaxSalary = int.Parse(Console.ReadLine());

            InsertJobs(inputTitle, inputMinSalary, inputMaxSalary);
        }

        public static void UpdateJobs(int id, string title, int min_salary, int max_salary)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE  tbl_jobs SET title = @title, min_salary = @min_salary, max_salary = @max_salary WHERE id = @job_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@jobs_id";
                pJobId.SqlDbType = System.Data.SqlDbType.Int;
                pJobId.Value = id;
                sqlCommand.Parameters.Add(pJobId);

                SqlParameter pTitle = new SqlParameter();
                pTitle.ParameterName = "@title";
                pTitle.SqlDbType = System.Data.SqlDbType.VarChar;
                pTitle.Value = title;
                sqlCommand.Parameters.Add(pTitle);

                SqlParameter pMinSalary = new SqlParameter();
                pMinSalary.ParameterName = "@min_salary";
                pMinSalary.SqlDbType = System.Data.SqlDbType.Int;
                pMinSalary.Value = min_salary;
                sqlCommand.Parameters.Add(pMinSalary);

                SqlParameter pMaxSalary = new SqlParameter();
                pMaxSalary.ParameterName = "@max_salary";
                pMaxSalary.SqlDbType = System.Data.SqlDbType.VarChar;
                pMaxSalary.Value = max_salary;
                sqlCommand.Parameters.Add(pMaxSalary);

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

        public static void UpdateIntoJob()
        {
            Console.WriteLine("Update Id");
            int inputJobId = int.Parse(Console.ReadLine());
            Console.WriteLine("Update Title: ");
            string inputTitle = Console.ReadLine();
            Console.WriteLine("Update Min Salary: ");
            int inputMinSalary = int.Parse(Console.ReadLine());
            Console.WriteLine("Update MaxSalary: ");
            int inputMaxSalary = int.Parse(Console.ReadLine());

            UpdateJobs(inputJobId, inputTitle, inputMinSalary, inputMaxSalary);

        }

        public static void DeleteJob(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM tbl_jobs WHERE job_id = @job_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@job_id";
                pJobId.SqlDbType = System.Data.SqlDbType.Int;
                pJobId.Value = id;
                sqlCommand.Parameters.Add(pJobId);

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

        public static void DeleteByJob()
        {
            Console.WriteLine("Hapus Job Id: ");
            int inputId = int.Parse(Console.ReadLine());

            deparmens.DeleteDepartment(inputId);
        }

        public static void GetJobById(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM jobs WHERE job_id = @job_id";
            sqlCommand.Parameters.AddWithValue("@job_id", id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Title: " + reader.GetString(1));
                        Console.WriteLine("Min Salary: " + reader.GetInt32(2));
                        Console.WriteLine("Max Salary: " + reader.GetInt32(3));
                    }
                }
                else
                {
                    Console.WriteLine("tidak ada job");
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        public static void SearchJobById()
        {
            int inputId = int.Parse(Console.ReadLine());
            Console.WriteLine("Cari job Id: ");
            GetJobById(inputId);
        }

        public static void MenuJobs()
        {
            Console.WriteLine("Basic Authentication");
            Console.WriteLine("Pilih menu ");
            Console.WriteLine("1. Tambah Job");
            Console.WriteLine("2. Update Job");
            Console.WriteLine("3. Hapus Job");
            Console.WriteLine("4. Search By Id Job");
            Console.WriteLine("5. Get All Job");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");

            try
            {
                int pilih = int.Parse(Console.ReadLine());

                switch (pilih)
                {

                    case 1:
                        Console.Clear();
                        InsertIntoJobs();
                        MenuJobs();
                        break;
                    case 2:
                        Console.Clear();
                        UpdateIntoJob();
                        MenuJobs();
                        break;
                    case 3:
                        Console.Clear();
                        DeleteByJob();
                        MenuJobs();
                        break;
                    case 4:
                        Console.Clear();
                        SearchJobById();
                        MenuJobs();
                        break;
                    case 5:
                        Console.Clear();
                        GetJobs();
                        MenuJobs();
                        break;
                    case 6:
                        Console.WriteLine("MainMenu");
                        Program.MainMenu();
                        break;
                    default:
                        Console.WriteLine("Silahkan Pilih Nomor 1-6");
                        MenuJobs();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("tidak ada!");
                MenuJobs();
            }
        }
    }
}
