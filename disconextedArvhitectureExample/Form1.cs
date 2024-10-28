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

namespace disconextedArvhitectureExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=ROHITSALVI020;Initial Catalog=ROHIT;Integrated Security=True;";
        private void button1_Click(object sender, EventArgs e)
        { 
            UpdateData(1,textBox1.Text);
        }

        public void UpdateData(int employeeId, string newName)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees WHERE id = @id", connection);
                adapter.SelectCommand.Parameters.AddWithValue("@id", employeeId);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Employees");

                if (dataSet.Tables["Employees"].Rows.Count > 0)
                {
                    DataRow row = dataSet.Tables["Employees"].Rows[0];
                    row["Name"] = newName;

                    adapter.Update(dataSet, "Employees");
                   
                    Console.WriteLine("Row updated in dataset.");
                }

            }
        }

        private void insertClick(object sender, EventArgs e)
        {
            InsertData(1, textBox1.Text);
        }
        public void InsertData(int employeeId, string name)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Employees");

                DataRow newRow = dataSet.Tables["Employees"].NewRow();
                newRow["id"] =(dataSet.Tables["Employees"].Rows.Count)+1;
                newRow["name"] = name;
                dataSet.Tables["Employees"].Rows.Add(newRow);
                adapter.Update(dataSet, "Employees");

                Console.WriteLine("Row inserted into dataset.");
            }
        }
        public void DeleteData(int employeeId)
        {
            string connectionString = "YourConnectionString";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees WHERE EmployeeID = @EmployeeID", connection);
                adapter.SelectCommand.Parameters.AddWithValue("@EmployeeID", employeeId);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Employees");

                if (dataSet.Tables["Employees"].Rows.Count > 0)
                {
                    DataRow row = dataSet.Tables["Employees"].Rows[0];
                    row.Delete();
                    adapter.Update(dataSet, "Employees");
                    Console.WriteLine("Row deleted from dataset.");
                }
            }
        }

    }
}

