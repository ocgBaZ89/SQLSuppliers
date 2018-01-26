using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace SQLSuppliers
{
    class Program
    {
        static void Main(string[] args)
        {


            string menuOption;
            string[] validMenuOptions = new string[] { "1", "2", "3", "4", "5" };

            do
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Select an Option");
                Console.WriteLine("--------------------\n\n");
                Console.WriteLine("1. View All Suppliers");
                Console.WriteLine("2. View By ID");
                Console.WriteLine("3. Add Supplier");
                Console.WriteLine("4. Update Supplier");
                Console.WriteLine("5. Delete Supplier");
                Console.WriteLine("6. Quit");
                menuOption = Console.ReadLine();
                Console.Clear();

                switch (menuOption)
                {
                    case "1":
                        //read/view all

                        try
                        {
                            ReadAll();
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                        }

                        catch (Exception ex)
                        {
                            string exMessage = ex.Message;
                            Console.WriteLine(exMessage);

                            MethodBase site = ex.TargetSite;
                            string methodName = site.ToString();

                            ExceptionLog(methodName, exMessage);
                        }

                        finally
                        {
                        }
                        break;

                    case "2":
                        //view supplier by id

                        try
                        {
                            ViewById();
                        }
                        catch (Exception ex)
                        {
                            string exMessage = ex.Message;
                            Console.WriteLine(exMessage);

                            MethodBase site = ex.TargetSite;
                            string methodName = site.ToString();

                            ExceptionLog(methodName, exMessage);
                        }
                        finally
                        {
                        }
                        break;

                    case "3":
                        //create
                        try
                        {
                            AddSupplier();
                        }
                        catch (Exception ex)
                        {
                            string exMessage = ex.Message;
                            Console.WriteLine(exMessage);

                            MethodBase site = ex.TargetSite;
                            string methodName = site.ToString();

                            ExceptionLog(methodName, exMessage);
                        }
                        finally
                        {
                        }
                        break;

                    case "4":
                        //update
                        try
                        {
                            UpdateSupplier();
                        }
                        catch (Exception ex)
                        {
                            string exMessage = ex.Message;
                            Console.WriteLine(exMessage);

                            MethodBase site = ex.TargetSite;
                            string methodName = site.ToString();

                            ExceptionLog(methodName, exMessage);
                        }
                        finally
                        {
                        }
                        break;

                    case "5":
                        //delete
                        try
                        {
                            DeleteSupplier();
                        }
                        catch (Exception ex)
                        {
                            string exMessage = ex.Message;
                            Console.WriteLine(exMessage);

                            MethodBase site = ex.TargetSite;
                            string methodName = site.ToString();

                            ExceptionLog(methodName, exMessage);
                        }
                        finally
                        {
                        }
                        break;
                }
            }
            while (menuOption != "6");
        }

        public static void ExceptionLog(string methodName, string exMessage)
        {
            string timeStamp = DateTime.Now.ToString();
            string logLevel = "fatal";
            string logPath = ConfigurationManager.AppSettings["LogPath"];

            StreamWriter Writer = new StreamWriter(logPath, true);
            Writer.WriteLine("{0} {1} {2} {3}", timeStamp, logLevel, methodName, exMessage);

            Writer.Close();
            Writer.Dispose();

            return;
        }

        //Method Read All
        private static void ReadAll()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            DataTable suppliers = new DataTable();
            SqlDataAdapter adapter = null;

            try
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Viewing all suppliers");
                Console.WriteLine("--------------------\n\n");

                connectionToSql = new SqlConnection(connectionString);
                storedProcedure = new SqlCommand("PULL_SUPPLIERS", connectionToSql);

                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(suppliers);

                foreach (DataRow row in suppliers.Rows)
                {
                    Console.WriteLine("{0}{1}:{2}", "SupplierID", new string(' ', 11 - "SupplierID".Length), row["SupplierId"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Company", new string(' ', 11 - "Company".Length), row["CompanyName"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Title", new string(' ', 11 - "Title".Length), row["ContactTitle"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Name", new string(' ', 11 - "Name".Length), row["ContactName"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Country", new string(' ', 11 - "Country".Length), row["Country"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Phone", new string(' ', 11 - "Phone".Length), row["Phone"].ToString().Trim());

                    Console.WriteLine();
                }
            }

            catch (Exception ex)
            {
                //Exception Message
                Console.WriteLine(ex.Message);
                throw ex;
            }

            finally
            {
                if (connectionToSql != null)
                {
                    adapter.Dispose();
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
        }

        //Method View by ID
        private static void ViewById()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            DataTable suppliers = new DataTable();
            SqlDataAdapter adapter = null;

            try
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Enter an ID");
                Console.WriteLine("--------------------\n\n");
                string inputID = Console.ReadLine();
                Console.Clear();

                connectionToSql = new SqlConnection(connectionString);
                storedProcedure = new SqlCommand("PULL_BY_ID", connectionToSql);

                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@SupplierID", inputID);

                connectionToSql.Open();
                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(suppliers);

                foreach (DataRow row in suppliers.Rows)
                {
                    Console.WriteLine("{0}{1}:{2}", "SupplierID", new string(' ', 11 - "SupplierID".Length), row["SupplierId"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Company", new string(' ', 11 - "Company".Length), row["CompanyName"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Title", new string(' ', 11 - "Title".Length), row["ContactTitle"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Name", new string(' ', 11 - "Name".Length), row["ContactName"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Country", new string(' ', 11 - "Country".Length), row["Country"].ToString().Trim());
                    Console.WriteLine("{0}{1}:{2}", "Phone", new string(' ', 11 - "Phone".Length), row["Phone"].ToString().Trim());

                    Console.WriteLine();
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            finally
            {
                if (connectionToSql != null)
                {
                    adapter.Dispose();
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
            Console.Clear();
        }

        //Method Add Supplier
        private static void AddSupplier()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Add a Supplier");
                Console.WriteLine("--------------------\n\n");

                Console.WriteLine("Enter Company Name");
                string company = Console.ReadLine();
                Console.WriteLine("Enter Title");
                string title = Console.ReadLine();
                Console.WriteLine("Enter Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Country");
                string country = Console.ReadLine();
                Console.WriteLine("Enter Phone Number");
                string phone = Console.ReadLine();

                Console.WriteLine("\n\nAre you certain that you'd like to add supplier? Y/N");
                string confirmation = Console.ReadKey().KeyChar.ToString();
                Console.Clear();

                if (confirmation == "y")
                {
                    connectionToSql = new SqlConnection(connectionString);
                    storedProcedure = new SqlCommand("ADD_SUPPLIER", connectionToSql);

                    storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                    storedProcedure.Parameters.AddWithValue("@CompanyName", company);
                    storedProcedure.Parameters.AddWithValue("@ContactTitle", title);
                    storedProcedure.Parameters.AddWithValue("@ContactName", name);
                    storedProcedure.Parameters.AddWithValue("@Country", country);
                    storedProcedure.Parameters.AddWithValue("@Phone", phone);

                    storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                    connectionToSql.Open();
                    storedProcedure.ExecuteNonQuery();

                    Console.WriteLine("Operation Successful");
                }

                else
                {
                    Console.WriteLine("Operation Aborted");
                }
                Console.ReadKey();
                Console.Clear();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            finally
            {
                if (connectionToSql != null && connectionToSql.State == ConnectionState.Open)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
            Console.Clear();
        }

        //Method Update Supplier
        private static void UpdateSupplier()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;



            try
            {
                //view all the suppliers before selection
                ReadAll();

                Console.WriteLine("Press any key when ready");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("--------------------");
                Console.WriteLine("Enter Supplier Info");
                Console.WriteLine("--------------------");
                Console.WriteLine();

                Console.WriteLine("Supplier ID Number");
                string idInput = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Company Name");
                string companyName = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Contact Title");
                string contactTitle = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Name");
                string contactName = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Country:");
                string country = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Phone number");
                string phone = Console.ReadLine();



                Console.WriteLine("Confirm update");
                Console.WriteLine();
                Console.WriteLine("Supplier ID: {0}", idInput);
                Console.WriteLine("Company: {0}", companyName);
                Console.WriteLine("Contact position: {0}", contactTitle);
                Console.WriteLine("Contact name: {0}", contactName);
                Console.WriteLine("Country: {0}", country);
                Console.WriteLine("Phone: {0}", phone);
                Console.WriteLine();
                Console.WriteLine("Update supplier? Y/N");
                string confirmation = Console.ReadKey().KeyChar.ToString();
                Console.Clear();

                if (string.Equals(confirmation, "y", StringComparison.CurrentCultureIgnoreCase))
                {
                    connectionToSql = new SqlConnection(connectionString);
                    storedProcedure = new SqlCommand("UPDATE_BY_ID", connectionToSql);

                    storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                    storedProcedure.Parameters.AddWithValue("@SupplierID", idInput);
                    storedProcedure.Parameters.AddWithValue("@CompanyName", companyName);
                    storedProcedure.Parameters.AddWithValue("@ContactTitle", contactTitle);
                    storedProcedure.Parameters.AddWithValue("@ContactName", contactName);
                    storedProcedure.Parameters.AddWithValue("@Country", country);
                    storedProcedure.Parameters.AddWithValue("@Phone", phone);

                    connectionToSql.Open();
                    storedProcedure.ExecuteNonQuery();

                    Console.Clear();
                    Console.WriteLine("Supplier updated");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Operation aborted.");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
            return;
        }

        private static void DeleteSupplier()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                //view all the suppliers before selection
                ReadAll();

                Console.WriteLine("Press any key when ready");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("--------------------");
                Console.WriteLine("Delete a Supplier");
                Console.WriteLine("--------------------\n\n");
                string idInput = Console.ReadLine();

                Console.WriteLine("\n\nAre you certain that you'd like to delete supplier? Y/N");
                string confirmation = Console.ReadKey().KeyChar.ToString();
                Console.Clear();

                if (confirmation == "y")
                {
                    connectionToSql = new SqlConnection(connectionString);
                    storedProcedure = new SqlCommand("DELETE_ID", connectionToSql);

                    storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                    storedProcedure.Parameters.AddWithValue("@SupplierID", idInput);

                    storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                    connectionToSql.Open();
                    storedProcedure.ExecuteNonQuery();

                    Console.WriteLine("Operation Successful");
                }

                else
                {
                    Console.WriteLine("Operation Aborted");
                }
                Console.ReadKey();
                Console.Clear();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            finally
            {
                connectionToSql.Close();
                connectionToSql.Dispose();
            }
        }

    }
}
