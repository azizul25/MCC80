using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectivity.Menu
{
    public class deparmens
    {
        private static string _connectionString = "Data Source=DESKTOP-L3T0EDT\\MSSQLSERVER01;" +
"database=db_kantor;" +
"Integrated Security=True;" +
"Connect Timeout=30;";

        private static SqlConnection _connection;
        public static void GetDepartments()
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_departments";


            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Name: " + reader.GetString(1));
                        Console.WriteLine("Location Id: " + reader.GetString(2));
                        Console.WriteLine("Manager Id: " + reader.GetString(3));
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
                Console.WriteLine("Tidak ada Departments");
            }


        }

        public static void InsertDepartments(string name)
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO tbl_departments VALUES (@name)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = name;
                sqlCommand.Parameters.Add(pName);

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

        public static void InsertIntoDepartments()
        {
            Console.WriteLine("Tambah Department: ");
            string inputName = Console.ReadLine();

            InsertDepartments(inputName);
        }

        public static void UpdateDepartment(string name, int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE tbl_departments SET name = @name WHERE id = @department_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = name;
                sqlCommand.Parameters.Add(pName);

                SqlParameter pDepartmentId = new SqlParameter();
                pDepartmentId.ParameterName = "@department_id";
                pDepartmentId.SqlDbType = System.Data.SqlDbType.Int;
                pDepartmentId.Value = id;
                sqlCommand.Parameters.Add(pDepartmentId);

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

        public static void UpdateIntoDepartment()
        {
            Console.WriteLine("Ganti Nama: ");
            string inputName = Console.ReadLine();
            Console.WriteLine("Ganti ID_regions: ");
            int inputId = int.Parse(Console.ReadLine());

            UpdateDepartment(inputName, inputId);
        }

        public static void DeleteDepartment(int department_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM departments WHERE department_id = @department_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pDepartmentId = new SqlParameter();
                pDepartmentId.ParameterName = "@department_id";
                pDepartmentId.SqlDbType = System.Data.SqlDbType.Int;
                pDepartmentId.Value = department_id;
                sqlCommand.Parameters.Add(pDepartmentId);

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

        public static void DeleteByDepartment()
        {
            int inputId = int.Parse(Console.ReadLine());
            Console.WriteLine("Hapus Department Id: ");
            DeleteDepartment(inputId);
        }

        public static void GetDepartmentById(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_departments WHERE id = @department_id";
            sqlCommand.Parameters.AddWithValue("@department_id", id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Name: " + reader.GetString(1));
                        Console.WriteLine("Location Id: " + reader.GetString(2));
                        Console.WriteLine("Manager Id: " + reader.GetString(3));
                    }
                }
                else
                {
                    Console.WriteLine("tidak ada department");
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        public static void SearchDepartmentById()
        {
            Console.WriteLine("Cari Department Id: ");
            int inputId = int.Parse(Console.ReadLine());

            GetDepartmentById(inputId);
        }

        public static void MenuDepartment()
        {
            Console.WriteLine("Basic Authenticatio");
            Console.WriteLine("Pilih menu");
            Console.WriteLine("1. Tambah Department");
            Console.WriteLine("2. Update Department");
            Console.WriteLine("3. Hapus Department");
            Console.WriteLine("4. Search By Id Department");
            Console.WriteLine("5. Get All Department");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");

            try
            {
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {

                    case 1:
                        Console.Clear();
                        InsertIntoDepartments();
                        MenuDepartment();
                        break;
                    case 2:
                        Console.Clear();
                        UpdateIntoDepartment();
                        MenuDepartment();
                        break;
                    case 3:
                        Console.Clear();
                        DeleteByDepartment();
                        MenuDepartment();
                        break;
                    case 4:
                        Console.Clear();
                        SearchDepartmentById();
                        MenuDepartment();
                        break;
                    case 5:
                        Console.Clear();
                        GetDepartments();
                        MenuDepartment();
                        break;
                    case 6:
                        Console.Clear();
                        Program.MainMenu();
                        break;
                    default:
                        Console.WriteLine("gagal tak ada pilihan");
                        MenuDepartment();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Input Hanya diantara 1-6!");
                MenuDepartment();
            }
        }
    }
}
