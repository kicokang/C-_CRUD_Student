using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class DataManager
    {
        public static List<@Student> students = new List<@Student>();
        static DataManager()
        {
            Load();
        }
        public static void Load()
        {
            try
            {
                DBHelper.ReadQuery();
                students.Clear();
                foreach (DataRow item in DBHelper.ds.Tables[0].Rows)
                {
                    Student stu = new Student();
                    stu.grade = item["grade"].ToString();
                    stu.cclass = item["cclass"].ToString();
                    stu.no = item["no"].ToString();
                    stu.name = item["name"].ToString();
                    stu.score= item["score"].ToString();
                    students.Add(stu);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
       
    }
}
