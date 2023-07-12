
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace DatabaseConnectivity.Menu
{
    public class location
    {
        private static string _connectionString = "Data Source=DESKTOP-L3T0EDT\\MSSQLSERVER01;" +
                                            "database=db_kantor;" +
                                            "Integrated Security=True;" +
                                            "Connect Timeout=30;";

        private static SqlConnection _connection;
        public static void GetLocations()
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_locations";


            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Street Address: " + reader.GetString(1));
                        Console.WriteLine("Postal Code: " + reader.GetString(2));
                        Console.WriteLine("City: " + reader.GetString(3));
                        Console.WriteLine("State Province: " + reader.GetString(4));
                        Console.WriteLine("Country Id: " + reader.GetString(5));
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
                Console.WriteLine("Tidak ada Locations");
            }


        }

        public static void InsertLocations(string street_address, string postal_code, string city, string state_province)
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO tbl_locations VALUES (@street_address), (@postal_code), (@city), (@state_province)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pStreetAddress = new SqlParameter();
                pStreetAddress.ParameterName = "@street_address";
                pStreetAddress.SqlDbType = System.Data.SqlDbType.VarChar;
                pStreetAddress.Value = street_address;
                sqlCommand.Parameters.Add(pStreetAddress);

                SqlParameter pPostalCode = new SqlParameter();
                pPostalCode.ParameterName = "@postal_code";
                pPostalCode.SqlDbType = System.Data.SqlDbType.VarChar;
                pPostalCode.Value = postal_code;
                sqlCommand.Parameters.Add(pPostalCode);

                SqlParameter pCity = new SqlParameter();
                pCity.ParameterName = "@city";
                pCity.SqlDbType = System.Data.SqlDbType.VarChar;
                pCity.Value = city;
                sqlCommand.Parameters.Add(pCity);

                SqlParameter pStateProvince = new SqlParameter();
                pStateProvince.ParameterName = "@state_province";
                pStateProvince.SqlDbType = System.Data.SqlDbType.VarChar;
                pStateProvince.Value = state_province;
                sqlCommand.Parameters.Add(pStateProvince);

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

        public static void InsertIntoLocations()
        {
            Console.WriteLine("Tambah StreetAddress: ");
            string inputStreetAddress = Console.ReadLine();
            Console.WriteLine("Tambah PostalCode: ");
            string inputPostalCode = Console.ReadLine();
            Console.WriteLine("Tambah City: ");
            string inputCity = Console.ReadLine();
            Console.WriteLine("Tambah inputStateProvince: ");
            string inputStateProvince = Console.ReadLine();

            InsertLocations(inputStreetAddress, inputPostalCode, inputCity, inputStateProvince);
        }

        public static void UpdateLocations(int id, string street_address, string postal_code, string city, string state_province)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE tbl_locations SET street_address = @street_Address, postal_code = @postal_code, city = @city, state_province = @state_province WHERE location_id = @location_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pLocationId = new SqlParameter();
                pLocationId.ParameterName = "@location_id";
                pLocationId.SqlDbType = System.Data.SqlDbType.Int;
                pLocationId.Value = id;
                sqlCommand.Parameters.Add(pLocationId);

                SqlParameter pStreetAddress = new SqlParameter();
                pStreetAddress.ParameterName = "@street_address";
                pStreetAddress.SqlDbType = System.Data.SqlDbType.VarChar;
                pStreetAddress.Value = street_address;
                sqlCommand.Parameters.Add(pStreetAddress);

                SqlParameter pPostalCode = new SqlParameter();
                pPostalCode.ParameterName = "@postal_code";
                pPostalCode.SqlDbType = System.Data.SqlDbType.VarChar;
                pPostalCode.Value = postal_code;
                sqlCommand.Parameters.Add(pPostalCode);

                SqlParameter pCity = new SqlParameter();
                pCity.ParameterName = "@city";
                pCity.SqlDbType = System.Data.SqlDbType.VarChar;
                pCity.Value = city;
                sqlCommand.Parameters.Add(pCity);

                SqlParameter pStateProvince = new SqlParameter();
                pStateProvince.ParameterName = "@state_province";
                pStateProvince.SqlDbType = System.Data.SqlDbType.VarChar;
                pStateProvince.Value = state_province;
                sqlCommand.Parameters.Add(pStateProvince);

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

        public static void UpdateIntoLocation()
        {
            int inputLocationId = int.Parse(Console.ReadLine());
            Console.WriteLine("Update StreetAddress: ");
            string inputStreetAddress = Console.ReadLine();
            Console.WriteLine("Update PostalCode: ");
            string inputPostalCode = Console.ReadLine();
            Console.WriteLine("Update City: ");
            string inputCity = Console.ReadLine();
            Console.WriteLine("Update StateProvince: ");
            string inputStateProvince = Console.ReadLine();

            UpdateLocations(inputLocationId, inputStreetAddress, inputPostalCode, inputCity, inputStateProvince);

        }

        public static void DeleteLocation(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM tbl_locations WHERE id = @location_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pLocationId = new SqlParameter();
                pLocationId.ParameterName = "@location_id";
                pLocationId.SqlDbType = System.Data.SqlDbType.Int;
                pLocationId.Value = id;
                sqlCommand.Parameters.Add(pLocationId);

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

        public static void DeleteByLocation()
        {
            Console.WriteLine("Hapus Locaation Id: ");
            int inputId = int.Parse(Console.ReadLine());

            DeleteLocation(inputId);
        }

        public static void GetLocationById(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_locations WHERE id = @location_id";
            sqlCommand.Parameters.AddWithValue("@location_id", id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetString(0));
                        Console.WriteLine("Street Address: " + reader.GetString(1));
                        Console.WriteLine("Postal Code: " + reader.GetString(2));
                        Console.WriteLine("City: " + reader.GetString(3));
                        Console.WriteLine("State Province: " + reader.GetString(4));
                        Console.WriteLine("Country Id: " + reader.GetString(5));
                    }
                }
                else
                {
                    Console.WriteLine("tidak ada location");
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        public static void SearchLocationById()
        {
            int inputId = int.Parse(Console.ReadLine());
            Console.WriteLine("Cari Location Id: ");
            GetLocationById(inputId);
        }

        public static void MenuLocation()
        {
            Console.WriteLine("Basic Authentication ");
            Console.WriteLine("Pilih menu ");
            Console.WriteLine("1. Tambah Location");
            Console.WriteLine("2. Update Location");
            Console.WriteLine("3. Hapus Location");
            Console.WriteLine("4. Search By Id Location");
            Console.WriteLine("5. Get All Location");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");

            try
            {
                int Pilihan = int.Parse(Console.ReadLine());

                switch (Pilihan)
                {

                    case 1:
                        Console.WriteLine("1. Tambah Location");
                        Console.Clear();
                        InsertIntoLocations();
                        MenuLocation();
                        break;
                    case 2:
                        Console.Clear();
                        UpdateIntoLocation();
                        MenuLocation();
                        break;
                    case 3:
                        Console.Clear();
                        DeleteByLocation();
                        MenuLocation();
                        break;
                    case 4:
                        Console.Clear();
                        SearchLocationById();
                        MenuLocation();
                        break;
                    case 5:
                        Console.Clear();
                        GetLocations();
                        MenuLocation();
                        break;
                    case 6:
                        Console.Clear();
                        Program.MainMenu();
                        break;
                    default:
                        Console.WriteLine("Silahkan Pilih Nomor 1-6");
                        MenuLocation();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("tidak ada pilihan");
                MenuLocation();
            }
        }
    }
}