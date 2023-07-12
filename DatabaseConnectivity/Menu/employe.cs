using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectivity.Menu
{
    public class employe
    {
        private static string _connectionString = "Data Source=DESKTOP-L3T0EDT\\MSSQLSERVER01;" +
"database=db_kantor;" +
"Integrated Security=True;" +
"Connect Timeout=30;";

        private static SqlConnection _connection;
        public static void GetEmployees()
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_employees";


            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader.GetInt32(0));
                        Console.WriteLine("First Name: " + reader.GetString(1));
                        Console.WriteLine("Last Name: " + reader.GetString(2));
                        Console.WriteLine("Email: " + reader.GetString(3));
                        Console.WriteLine("Phone Number: " + reader.GetString(4));
                        Console.WriteLine("Hire Date: " + reader.GetDateTime(5));
                        Console.WriteLine("Salary: " + reader.GetInt32(6));
                        Console.WriteLine("Comission PCT: " + reader.GetInt32(7));

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
                Console.WriteLine("Tidak ada Employees");
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


        public static void InsertEmployees(string first_name, string last_name, string email, string phone_number, DateTime hire_date, int salary, int comission_pct)
        {
            _connection = new SqlConnection(_connectionString);


            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO tbl_employees VALUES (@first_name), (@last_name), (@phone_number), (@hire_date), (@salary), (@comission_pct)";

            _connection.Open();
            using SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;


            try
            {

                SqlParameter pFName = new SqlParameter();
                pFName.ParameterName = "@first_name";
                pFName.SqlDbType = System.Data.SqlDbType.VarChar;
                pFName.Value = first_name;
                sqlCommand.Parameters.Add(pFName);

                SqlParameter pLName = new SqlParameter();
                pLName.ParameterName = "@last_name";
                pLName.SqlDbType = System.Data.SqlDbType.Int;
                pLName.Value = last_name;
                sqlCommand.Parameters.Add(pLName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@email";
                pEmail.SqlDbType = System.Data.SqlDbType.VarChar;
                pEmail.Value = email;
                sqlCommand.Parameters.Add(pEmail);

                SqlParameter pPhoneNumber = new SqlParameter();
                pPhoneNumber.ParameterName = "@phone_number";
                pPhoneNumber.SqlDbType = System.Data.SqlDbType.VarChar;
                pPhoneNumber.Value = phone_number;
                sqlCommand.Parameters.Add(pPhoneNumber);

                SqlParameter hd = new SqlParameter();
                hd.ParameterName = "@hire_date";
                hd.SqlDbType = System.Data.SqlDbType.DateTime;
                hd.Value = hire_date;
                sqlCommand.Parameters.Add(hd);

                SqlParameter pSalary = new SqlParameter();
                pSalary.ParameterName = "@salary";
                pSalary.SqlDbType = System.Data.SqlDbType.Int;
                pSalary.Value = salary;
                sqlCommand.Parameters.Add(pSalary);

                SqlParameter pComissionPCT = new SqlParameter();
                pComissionPCT.ParameterName = "@comission_pct";
                pComissionPCT.SqlDbType = System.Data.SqlDbType.Int;
                pComissionPCT.Value = comission_pct;
                sqlCommand.Parameters.Add(pComissionPCT);

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

        public static void InsertIntoEmployees()
        {
            Console.WriteLine("Tambah First Name: ");
            string inputFirstName = Console.ReadLine();
            Console.WriteLine("Tambah Last Name: ");
            string inputLastName = Console.ReadLine();
            Console.WriteLine("Tambah Email: ");
            string inputEmail = Console.ReadLine();
            Console.WriteLine("Tambah Nomor HP: ");
            string inputPhone = Console.ReadLine();
            Console.Write("Tambah Hire Date: ");
            DateTime inputHireDate = DateValidation();
            Console.WriteLine("Tambah Salary: ");
            int inputSalary = int.Parse(Console.ReadLine());
            Console.WriteLine("Tambah Commision PCT: ");
            int inputComission = int.Parse(Console.ReadLine());

            InsertEmployees(inputFirstName, inputLastName, inputEmail, inputPhone, inputHireDate, inputSalary, inputComission);
        }

        public static void UpdateEmployees(int id, string first_name, string last_name, string email, string phone_number, DateTime hire_date, int salary, int comission_pct)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE tbl_employees SET first_name = @first_name, last_name = @last_name, email = @email, phone_number = @phone_number, hire_date = @hire_date, salary = @salary, comission_pct = @comission_pct WHERE id = @employee_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pEmployeeId = new SqlParameter();
                pEmployeeId.ParameterName = "@employee_id";
                pEmployeeId.SqlDbType = System.Data.SqlDbType.Int;
                pEmployeeId.Value = id;
                sqlCommand.Parameters.Add(id);

                SqlParameter pFName = new SqlParameter();
                pFName.ParameterName = "@first_name";
                pFName.SqlDbType = System.Data.SqlDbType.VarChar;
                pFName.Value = first_name;
                sqlCommand.Parameters.Add(pFName);

                SqlParameter pLName = new SqlParameter();
                pLName.ParameterName = "@last_name";
                pLName.SqlDbType = System.Data.SqlDbType.Int;
                pLName.Value = last_name;
                sqlCommand.Parameters.Add(pLName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@email";
                pEmail.SqlDbType = System.Data.SqlDbType.VarChar;
                pEmail.Value = email;
                sqlCommand.Parameters.Add(pEmail);

                SqlParameter pPhoneNumber = new SqlParameter();
                pPhoneNumber.ParameterName = "@phone_number";
                pPhoneNumber.SqlDbType = System.Data.SqlDbType.VarChar;
                pPhoneNumber.Value = phone_number;
                sqlCommand.Parameters.Add(pPhoneNumber);

                SqlParameter pHireDate = new SqlParameter();
                pHireDate.ParameterName = "@hire_date";
                pHireDate.SqlDbType = System.Data.SqlDbType.DateTime;
                pHireDate.Value = hire_date;
                sqlCommand.Parameters.Add(pHireDate);

                SqlParameter pSalary = new SqlParameter();
                pSalary.ParameterName = "@salary";
                pSalary.SqlDbType = System.Data.SqlDbType.Int;
                pSalary.Value = salary;
                sqlCommand.Parameters.Add(pSalary);

                SqlParameter pComissionPCT = new SqlParameter();
                pComissionPCT.ParameterName = "@comission_pct";
                pComissionPCT.SqlDbType = System.Data.SqlDbType.Int;
                pComissionPCT.Value = comission_pct;
                sqlCommand.Parameters.Add(pComissionPCT);


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

        public static void UpdateIntoEmployees()
        {
            Console.WriteLine("Update Id");
            int inputEmployeeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Tambah First Name: ");
            string inputFirstName = Console.ReadLine();
            Console.WriteLine("Tambah Last Name: ");
            string inputLastName = Console.ReadLine();
            Console.WriteLine("Tambah Email: ");
            string inputEmail = Console.ReadLine();
            Console.WriteLine("Tambah Nomor HP: ");
            string inputPhone = Console.ReadLine();
            Console.Write("Tambah Hire Date: ");
            DateTime inputHireDate = DateValidation();
            Console.WriteLine("Tambah Salary: ");
            int inputSalary = int.Parse(Console.ReadLine());
            Console.WriteLine("Tambah Commision PCT: ");
            int inputComission = int.Parse(Console.ReadLine());

            UpdateEmployees(inputEmployeeId, inputFirstName, inputLastName, inputEmail, inputPhone, inputHireDate, inputSalary, inputComission);

        }

        public static void DeleteEmployee(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM tbl_employees WHERE id = @employee_id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;
            try
            {
                SqlParameter pEmployeeId = new SqlParameter();
                pEmployeeId.ParameterName = "@employee_id";
                pEmployeeId.SqlDbType = System.Data.SqlDbType.Int;
                pEmployeeId.Value = id;
                sqlCommand.Parameters.Add(pEmployeeId);

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

        public static void DeleteByEmployee()
        {
            int inputId = int.Parse(Console.ReadLine());
            Console.WriteLine("Hapus Employee Id: ");
            DeleteEmployee(inputId);
        }

        public static void GetEmployeeById(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM tbl_employees WHERE id = @employee_id";
            sqlCommand.Parameters.AddWithValue("@employee_id", id);

            try
            {
                _connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader.GetInt32(0));
                        Console.WriteLine("First Name: " + reader.GetString(1));
                        Console.WriteLine("Last Name: " + reader.GetString(2));
                        Console.WriteLine("Email: " + reader.GetString(3));
                        Console.WriteLine("Phone Number: " + reader.GetString(4));
                        Console.WriteLine("Hire Date: " + reader.GetDateTime(5));
                        Console.WriteLine("Salary: " + reader.GetInt32(6));
                        Console.WriteLine("Comission PCT: " + reader.GetInt32(7));
                    }
                }
                else
                {
                    Console.WriteLine("tidak ada Employee");
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        public static void SearchEmployeeById()
        {
            int inputId = int.Parse(Console.ReadLine());
            Console.WriteLine("Cari Employee Id: ");
            GetEmployeeById(inputId);
        }

        public static void MenuEmployees()
        {
            Console.WriteLine("Basic Authentication");
            Console.WriteLine("Pilih menu ");
            Console.WriteLine("1. Tambah Employee");
            Console.WriteLine("2. Update Employee");
            Console.WriteLine("3. Hapus Employee");
            Console.WriteLine("4. Search By Id Employee");
            Console.WriteLine("5. Get All Employee");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("Pilih: ");

            try
            {
                int pilihMenu = int.Parse(Console.ReadLine());

                switch (pilihMenu)
                {

                    case 1:
                        Console.Clear();
                        InsertIntoEmployees();
                        MenuEmployees();
                        break;
                    case 2:
                        Console.Clear();
                        UpdateIntoEmployees();
                        MenuEmployees();
                        break;
                    case 3:
                        Console.Clear();
                        DeleteByEmployee();
                        MenuEmployees();
                        break;
                    case 4:
                        Console.Clear();
                        SearchEmployeeById();
                        MenuEmployees();
                        break;
                    case 5:
                        Console.WriteLine("5. Get All Employees");
                        GetEmployees();
                        MenuEmployees();
                        break;
                    case 6:
                        Console.WriteLine("MainMenu");
                        Program.MainMenu();
                        break;
                    default:
                        Console.WriteLine("Silahkan Pilih Nomor 1-6");
                        MenuEmployees();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("tidak ada pilihan");
                MenuEmployees();
            }
        }

    }
}
