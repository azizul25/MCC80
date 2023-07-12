using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectivity.Menu
{
    public class countries
    {
        private static string _connectionString = "Data Source=DESKTOP-L3T0EDT\\MSSQLSERVER01;" +
"database=db_kantor;" +
"Integrated Security=True;" +
"Connect Timeout=30;";

        private static SqlConnection _connection;
        public static void GetCountry()
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_countries";


            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetString(0));
                        Console.WriteLine("Name: " + reader.GetString(1));
                        Console.WriteLine("Region_Id: " + reader.GetInt32(2));
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
                Console.WriteLine("Tidak ada Country");
            }


        }

        public static void InsertCountry(string id, string name)
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO tbl_countries VALUES (@id),(@name)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {
                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@id";
                pid.SqlDbType = System.Data.SqlDbType.VarChar;
                pid.Value = name;
                sqlCommand.Parameters.Add(pid);

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

        public static void InsertIntoCountry()
        {
            Console.WriteLine("Tambah ID: ");
            string inputid = Console.ReadLine();
            Console.WriteLine("Tambah Nama: ");
            string inputName = Console.ReadLine();

            InsertCountry(inputid, inputName);
        }

        public static void UpdateCountry(string name, string id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE tbl_countries SET name = @name WHERE id = @country_id";

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

                SqlParameter pCountryId = new SqlParameter();
                pCountryId.ParameterName = "@country_id";
                pCountryId.SqlDbType = System.Data.SqlDbType.Int;
                pCountryId.Value = id;
                sqlCommand.Parameters.Add(pCountryId);

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

        public static void UpdateIntoCountry()
        {
            Console.WriteLine("Ganti Nama: ");
            string inputName = Console.ReadLine();
            Console.WriteLine("Ganti Regions_ID: ");
            string inputId = Console.ReadLine();

            UpdateCountry(inputName, inputId);
        }

        public static void DeleteCountry(string id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM tbl_countries WHERE id = @country_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pCountryId = new SqlParameter();
                pCountryId.ParameterName = "@country_id";
                pCountryId.SqlDbType = System.Data.SqlDbType.Int;
                pCountryId.Value = id;
                sqlCommand.Parameters.Add(pCountryId);

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

        public static void DeleteByCountry()
        {
            Console.WriteLine("Hapus Country_Id: ");
            string inputId = Console.ReadLine();

            DeleteCountry(inputId);
        }

        public static void GetCountryById(string country_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_countries WHERE region_id = @country_id";
            sqlCommand.Parameters.AddWithValue("@country_id", country_id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetString(0));
                        Console.WriteLine("Name: " + reader.GetString(1));
                        Console.WriteLine("Region Id: " + reader.GetString(2));
                    }
                }
                else
                {
                    Console.WriteLine("tidak ada country");
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        public static void SearchCountryById()
        {
            string inputId = Console.ReadLine();
            Console.WriteLine("Cari Country Id: ");
            GetCountryById(inputId);
        }

        public static void MenuCountry()
        {
            Console.WriteLine("Basic Authentication ");
            Console.WriteLine("Pilih menu ");
            Console.WriteLine("1. Tambah Country");
            Console.WriteLine("2. Update Country");
            Console.WriteLine("3. Hapus Country");
            Console.WriteLine("4. Search By Id Country");
            Console.WriteLine("5. Get All Country");
            Console.WriteLine("6. MainMenu");
            Console.WriteLine("Pilih: ");

            try
            {
                int pilihMenu = int.Parse(Console.ReadLine());

                switch (pilihMenu)
                {

                    case 1:
                        Console.Clear();
                        InsertIntoCountry();
                        MenuCountry();
                        break;
                    case 2:
                        Console.Clear();
                        UpdateIntoCountry();
                        MenuCountry();
                        break;
                    case 3:
                        Console.Clear();
                        DeleteByCountry();
                        MenuCountry();
                        break;
                    case 4:
                        Console.Clear();
                        SearchCountryById();
                        MenuCountry();
                        break;
                    case 5:
                        Console.Clear();
                        GetCountry();
                        MenuCountry();
                        break;
                    case 6:
                        Console.Clear();
                        Program.MainMenu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("TIDAK ADA PILIHAN");
                        MenuCountry();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Input Hanya diantara 1-6!");
                MenuCountry();
            }
        }
    }
}
