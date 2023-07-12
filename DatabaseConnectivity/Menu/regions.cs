using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectivity.Menu
{
    public class regions
    {
        private static string _connectionString = "Data Source=DESKTOP-L3T0EDT\\MSSQLSERVER01;" +
    "database=db_kantor;" +
    "Integrated Security=True;" +
    "Connect Timeout=30;";

        private static SqlConnection _connection;
        public static void GetRegions()
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_regions";


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
                Console.WriteLine("Tidak ada Regions");
            }


        }

        public static void InsertRegions(string name)
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO tbl_regions VALUES (@name)";

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

        public static void InsertIntoRegion()
        {
            Console.WriteLine("Nama: ");
            string inputName = Console.ReadLine();

            InsertRegions(inputName);
        }

        public static void UpdateRegions(string name, int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE tbl_regions SET name = @name WHERE id = @region_id";

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

                SqlParameter pRegionId = new SqlParameter();
                pRegionId.ParameterName = "@region_id";
                pRegionId.SqlDbType = System.Data.SqlDbType.Int;
                pRegionId.Value = id;
                sqlCommand.Parameters.Add(pRegionId);

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

        public static void UpdateIntoRegion()
        {
            Console.WriteLine("update ID: ");
            int inputId = int.Parse(Console.ReadLine());
            Console.WriteLine("update Nama: ");
            string inputName = Console.ReadLine();


            UpdateRegions(inputName, inputId);
        }

        public static void DeleteRegions(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM tbl_regions WHERE id = @region_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pRegionId = new SqlParameter();
                pRegionId.ParameterName = "@region_id";
                pRegionId.SqlDbType = System.Data.SqlDbType.Int;
                pRegionId.Value = id;
                sqlCommand.Parameters.Add(pRegionId);

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

        public static void DeleteByRegion()
        {
            Console.WriteLine("Hapus Id: ");
            int inputId = int.Parse(Console.ReadLine());

            DeleteRegions(inputId);
        }

        public static void GetRegionById(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_regions WHERE id = @region_id";
            sqlCommand.Parameters.AddWithValue("@region_id", id);

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
                    }
                }
                else
                {
                    Console.WriteLine("tidak ada region");
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        public static void SearchRegionById()
        {
            int inputId = int.Parse(Console.ReadLine());
            Console.WriteLine("Cari region Id: ");
            GetRegionById(inputId);
        }

        public static void MenuRegion()
        {
            Console.WriteLine("Basic Authentication");
            Console.WriteLine("Pilih menu");
            Console.WriteLine("1. Tambah Region");
            Console.WriteLine("2. Update Region");
            Console.WriteLine("3. Hapus Region");
            Console.WriteLine("4. Search By Id Region");
            Console.WriteLine("5. Get All Regions");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");

            try
            {
                int pilihMenu = int.Parse(Console.ReadLine());

                switch (pilihMenu)
                {

                    case 1:
                        Console.Clear();
                        InsertIntoRegion();
                        MenuRegion();
                        break;
                    case 2:
                        Console.Clear();
                        UpdateIntoRegion();
                        MenuRegion();
                        break;
                    case 3:
                        Console.Clear();
                        DeleteByRegion();
                        MenuRegion();
                        break;
                    case 4:
                        Console.Clear();
                        SearchRegionById();
                        MenuRegion();
                        break;
                    case 5:
                        Console.Clear();
                        GetRegions();
                        MenuRegion();
                        break;
                    case 6:
                        Console.Clear();
                        Program.MainMenu();
                        break;
                    default:
                        Console.WriteLine("hanya diatas yang tersedia");
                        MenuRegion();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("tidak ada pilihan");
                MenuRegion();
            }
        }
    }
}
