using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private Student student;
        public Form1()
        {
            InitializeComponent();


            try
            {
                DataTable dataTable = new DataTable();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            //시작과 동시에 데이터 출력
            dataGridView1.DataSource = DataManager.students;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var currentInputData = dataGridView1.CurrentCell.Value;
                if (currentInputData != null)
                {
                    //실행확인 라벨
                    //label1.Text = currentInputData.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //화면 불러오기
        public void reLoad()
        {
            DataManager.Load();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.students;
        }
        //화면 불러오기
        private void button1_Click(object sender, EventArgs e)
        {
            reLoad();
        }
        //추가
        private void button2_Click(object sender, EventArgs e)
        {
            Student student = new Student();

            student.grade = (string)dataGridView1.CurrentRow.Cells[0].Value;
            student.cclass = (string)dataGridView1.CurrentRow.Cells[1].Value;
            student.no = (string)dataGridView1.CurrentRow.Cells[2].Value;
            student.name = (string)dataGridView1.CurrentRow.Cells[3].Value;
            student.score = (string)dataGridView1.CurrentRow.Cells[4].Value;

            DBHelper.AddQuery(student);
            reLoad();
        }
        //갱신
        private void button3_Click(object sender, EventArgs e)
        {
            DBHelper.ReadQuery();
            Student newstudent = new Student();
            newstudent.grade = (string)dataGridView1.CurrentRow.Cells[0].Value;
            newstudent.cclass = (string)dataGridView1.CurrentRow.Cells[1].Value;
            newstudent.no = (string)dataGridView1.CurrentRow.Cells[2].Value;
            newstudent.name = (string)dataGridView1.CurrentRow.Cells[3].Value;
            newstudent.score = (string)dataGridView1.CurrentRow.Cells[4].Value;

            // 이미 있는 no 값이 있다면 수정 불가하게 하자...
            if (DBHelper.ifNoExists(newstudent) == false)
            {
                DBHelper.UpdateQuery(newstudent);
                reLoad();
            }
            else
            {
                MessageBox.Show("이미 존재하는 no 입니다.");
                reLoad();
            }
        }
        //삭제
        private void button4_Click(object sender, EventArgs e)
        {
            //선택한 row하나 no값 가져오기
            Student student = new Student();

            student.no = (string)dataGridView1.CurrentRow.Cells[2].Value;
            Console.WriteLine(student.no);

            DBHelper.DeleteQuery(student.no);
            reLoad();
        }
        private void HandleDatabaseError(SqlException ex)
        {
            // in real life, would do something with error like log it
            MessageBox.Show(ex.Message, ex.GetType().ToString());
        }
        private void HandleGeneralError(Exception ex)
        {
            MessageBox.Show(ex.Message, ex.GetType().ToString());
        }
    }
}
