using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact_Application.econtactClasses
{
    internal class contactClass
    {
        public int ContactID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String ContactNo { get; set; }
        public String Adress { get; set; }
        public String Gender { get; set; }

        static String myconnString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;


        //selecting data from database
        public DataTable Select()
        {
            //step1: Database connection
            SqlConnection conn = new SqlConnection(myconnString);
            DataTable dt = new DataTable();
            try
            {
                //Step 2 :Writing sql query
                String sql = "SELECT * FROM tbl_contact";

                //creating CMD and writing sql and conns
                SqlCommand cmd = new SqlCommand(sql, conn);

                //creting sql database using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //inserting data into database
        public bool Insert(contactClass c)
        {
            bool isSuccess = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(myconnString))
                {
                    string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Adress, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Adress, @Gender)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", c.LastName);
                        cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                        cmd.Parameters.AddWithValue("@Adress", c.Adress);
                        cmd.Parameters.AddWithValue("@Gender", c.Gender);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        isSuccess = rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insert error: " + ex.Message);
                // Optionally log the error or rethrow
            }

            return isSuccess;
        }



        //Updating data into database
        public bool Update(contactClass c)
        {
            //step1: Database connection
            SqlConnection conn = new SqlConnection(myconnString);
            bool isSuccess = false;
            try
            {
                //Step 2 :Writing sql query
                String sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Adress=@Adress, Gender=@Gender WHERE ContactID=@ContactID  ";

                //creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //creating parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Adress", c.Adress);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;

                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to delete data from database
        public bool delete(contactClass c)
        {
            //step1: Database connection
            SqlConnection conn = new SqlConnection(myconnString);
            bool isSuccess = false;
            try
            {
                //Step 2 :Writing sql query
                String sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID ";

                //creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //creating parameters to add data
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;

                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
