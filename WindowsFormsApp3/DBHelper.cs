using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class DBHelper
    {
        public static MySqlConnection conn = new MySqlConnection();
        public static MySqlDataAdapter da;
        public static DataSet ds;
        public static DataTable dt;

        private static void ConnectDB()
        {
            try
            {
                var ConnectionString = "Server=localhost;Port=3306;Database=ack;Uid=root;Pwd=1234";

                conn = new MySqlConnection(ConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public static void ReadQuery()
        {
            ConnectDB();
            string name = "";
            string insertStatement = $"SELECT * FROM student";

            MySqlCommand command = new MySqlCommand(insertStatement, conn);
            command.ExecuteNonQuery();

            MySqlDataReader reader = command.ExecuteReader();

            //연결확인용
            if (reader.HasRows)
            {
                Console.WriteLine("db연결 됨");
                while (reader.Read())
                {
                    name = reader["name"].ToString();
                    Console.WriteLine(name);
                }
            }
            else
            {
                Console.WriteLine("db연결 안됨");
            }
            reader.Close();
            //끝

            da = new MySqlDataAdapter(insertStatement, conn);
            ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];

            //마지막 행 추가
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            conn.Close();
        }


        public static void AddQuery(Student student)
        {
            //AddCustomer 메소드
            ConnectDB();
            string insertStatement =
            "INSERT student (grade, cclass, no, name, score) VALUES (@grade, @cclass, @no, @name, @score)";

            MySqlCommand command = new MySqlCommand(insertStatement, conn);

            command.Parameters.AddWithValue("@grade",student.grade);
            command.Parameters.AddWithValue("@cclass", student.cclass);
            command.Parameters.AddWithValue("@no", student.no);
            command.Parameters.AddWithValue("@name",student.name);
            command.Parameters.AddWithValue("@score", student.score);

            command.CommandText = insertStatement;
            command.ExecuteNonQuery();

            conn.Close ();
        }

        public static void UpdateQuery(Student newCustomer)
        {
            ConnectDB();

            string updateStatement = "UPDATE Student SET grade = @NewGrade,cclass = @NewCclass, name = @NewName,score = @NewScore WHERE no = @NewNo";
            MySqlCommand command = new MySqlCommand(updateStatement, conn);

            command.Parameters.AddWithValue("@newGrade",newCustomer.grade);
            command.Parameters.AddWithValue("@NewCclass", newCustomer.cclass);
            command.Parameters.AddWithValue("@NewNo", newCustomer.no);
            command.Parameters.AddWithValue("@NewName", newCustomer.name);
            command.Parameters.AddWithValue("@NewScore", newCustomer.score);

            //command.Parameters.AddWithValue("@OldGrade", Student.grade);
            //command.Parameters.AddwithValue("@oldCclass", Student.cclass);
            //command.Parameters.AddwithValue("@oldName", Student.name);
            //command.Parameters.AddWithValue("@oldScore", Student.score);
            //command.Parameters.AddWithValue("@oldNo", oldCustomer.no);

            command.CommandText = updateStatement;
            command.ExecuteNonQuery();

            conn.Close();
        }

        public static Boolean ifNoExists(Student student)
        {
            bool result=false;
            ConnectDB();
            string insertStatement = $"SELECT name FROM student where no = @No";
            MySqlCommand command = new MySqlCommand(insertStatement, conn);

            command.Parameters.AddWithValue("@No", student.no);

            command.CommandText = insertStatement;
            command.ExecuteNonQuery();
            if (command.ExecuteNonQuery() != null)
            {
                result = true;
            }

            conn.Close();

            return result;
        }

        //internal static void updateQuery(int gradeText, int cclassText, int noText, string nameText, string scoreText)
        //{
        //    System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
        //}
        public static void DeleteQuery(string no)
        {
            ConnectDB();
            string insertStatement =
            "DELETE FROM student where no = @no";

            MySqlCommand command = new MySqlCommand(insertStatement, conn);

            command.Parameters.AddWithValue("@no", no);

            command.CommandText = insertStatement;
            command.ExecuteNonQuery();

            conn.Close();
        }

    }
}
