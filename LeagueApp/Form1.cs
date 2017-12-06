using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;




namespace LeagueApp
{
    public partial class Form1 : Form 
    {
        string con = "Data Source=RYBICCY\\SQLEXPRESS;Initial Catalog=League;Integrated Security=True";
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.playersTableAdapter.Fill(this.leagueDataSet.Players);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            bool test1 = int.TryParse(textBox2.Text, out i);
            bool test2 = int.TryParse(textBox3.Text, out i);
            bool test3 = int.TryParse(textBox6.Text, out i);

            if (textBox1.Text.Length > 0 && int.TryParse(textBox1.Text, out i) &&
            (textBox2.Text is string) && !test1 && !test2 && !test3 &&
            textBox2.Text.Length > 0 && (textBox3.Text is string) &&
            textBox3.Text.Length > 0 && textBox4.Text.Length > 0 &&
            int.TryParse(textBox4.Text, out i) && textBox5.Text.Length > 0 &&
            int.TryParse(textBox5.Text, out i) && (textBox6.Text is string) &&
            textBox6.Text.Length > 0)
            {

                try
                {
                    sqlConnection = new SqlConnection("Data Source = RYBICCY\\SQLEXPRESS; Initial Catalog = League; Integrated Security = True");
                    string sql = "INSERT INTO Players(IDPlayer, Name, Surname, Heigh, Weight, Nation) VALUES (@param1, @param2, @param3, @param4, @param5, @param6)";

                    sqlCommand = new SqlCommand(sql, sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@param1", textBox1.Text);
                    sqlCommand.Parameters.AddWithValue("@param2", textBox2.Text);

                    sqlCommand.Parameters.AddWithValue("@param3", textBox3.Text);
                    sqlCommand.Parameters.AddWithValue("@param4", textBox4.Text);
                    sqlCommand.Parameters.AddWithValue("@param5", textBox5.Text);
                    sqlCommand.Parameters.AddWithValue("@param6", textBox6.Text);

                    sqlCommand.Connection = sqlConnection;

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                    MessageBox.Show("record inserted!");
                    display();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Dodanie tego rekordu do bazy danych naruszyło by unikalność klucza głownego!");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection = new SqlConnection("Data Source = RYBICCY\\SQLEXPRESS; Initial Catalog = League; Integrated Security = True");
                sqlConnection.Open();
                string sql = "delete from Players where IDPlayer =" + textBox1.Text;
                sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();

                //sqlCommand.Parameters.AddWithValue("@param1", textBox1.Text);

                MessageBox.Show("record deleted!");
                display();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(Convert.ToString(ex));
                var se = ex.InnerException as SqlException;
                var code = se.Number;
                if (code == 2627)
                {
                    MessageBox.Show("Dodanie tego rekordu do bazy danych naruszyło by unikalność klucza głownego!");
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = " ";
                sqlConnection = new SqlConnection("Data Source = RYBICCY\\SQLEXPRESS; Initial Catalog = League; Integrated Security = True");
                sqlConnection.Open();
                if (textBox2.TextLength > 0)
                {
                    sql = "update Players set Name=" + textBox2.Text + " where IDPlayer =" + textBox1.Text;
                }
                if (textBox3.TextLength > 0)
                {
                    sql = "update Players set Surname=" + textBox3.Text + " where IDPlayer =" + textBox1.Text;
                }
                if (textBox4.TextLength > 0)
                {
                    sql = "update Players set Heigh=" + textBox4.Text + " where IDPlayer =" + textBox1.Text;
                }
                if (textBox5.TextLength > 0)
                {
                    sql = "update Players set Weight=" + textBox5.Text + " where IDPlayer =" + textBox1.Text;
                }
                if (textBox6.TextLength > 0)
                {
                    sql = "update Players set Nation=" + textBox6.Text + " where IDPlayer =" + textBox1.Text;
                }

                sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("record changed!");
                display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));

            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string text = dataGridView1.SelectedRows.ToString();
            //textBox1.Text = dataGridView1.SelectedRows.ToString();
            Int32 selectedRowCount =
               dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int i = 0; i < selectedRowCount; i++)
                {
                    sb.Append("Row: ");
                    sb.Append(dataGridView1.SelectedRows[i].Index.ToString());
                    sb.Append(Environment.NewLine);
                }

                sb.Append("Total: " + selectedRowCount.ToString());
                MessageBox.Show(sb.ToString(), "Selected Rows");
            }
        }

        private void selectedRowsButton_Click(object sender, System.EventArgs e)
        {
            Int32 selectedRowCount =
                dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int i = 0; i < selectedRowCount; i++)
                {
                    sb.Append("Row: ");
                    sb.Append(dataGridView1.SelectedRows[i].Index.ToString());
                    sb.Append(Environment.NewLine);
                }

                sb.Append("Total: " + selectedRowCount.ToString());
                MessageBox.Show(sb.ToString(), "Selected Rows");
            }
        }

        private void display()
        {
            try
            {
                sqlConnection = new SqlConnection("Data Source = RYBICCY\\SQLEXPRESS; Initial Catalog = League; Integrated Security = True");
                sqlConnection.Open();
                string sql = "select * from Players";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
                DataSet set = new DataSet();
                adapter.Fill(set);
                dataGridView1.DataSource = set;
                dataGridView1.DataMember = set.Tables[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));

            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            display();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=RYBICCY\\SQLEXPRESS;Initial Catalog=League;Integrated Security=True";
                try
                {
                    int i = 0;
                    bool test1 = int.TryParse(textBox2.Text, out i);
                    bool test2 = int.TryParse(textBox3.Text, out i);
                    bool test3 = int.TryParse(textBox6.Text, out i);

                    if (textBox1.Text.Length > 0 && int.TryParse(textBox1.Text, out i) &&
                    (textBox2.Text is string) && !test1 && !test2 && !test3 &&
                    textBox2.Text.Length > 0 && (textBox3.Text is string) &&
                    textBox3.Text.Length > 0 && textBox4.Text.Length > 0 &&
                    int.TryParse(textBox4.Text, out i) && textBox5.Text.Length > 0 &&
                    int.TryParse(textBox5.Text, out i) && (textBox6.Text is string) &&
                    textBox6.Text.Length > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("InsertKlient", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("IDPlayer", SqlDbType.Int).Value = textBox1.Text;
                            cmd.Parameters.Add("Name", SqlDbType.VarChar).Value = textBox2.Text;
                            cmd.Parameters.Add("Surname", SqlDbType.VarChar).Value = textBox3.Text;
                            cmd.Parameters.Add("Heigh", SqlDbType.Int).Value = textBox4.Text;
                            cmd.Parameters.Add("Weight", SqlDbType.Int).Value = textBox5.Text;
                            cmd.Parameters.Add("Nation", SqlDbType.VarChar).Value = textBox6.Text;
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            display();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Dodanie tego rekordu do bazy danych nie jest możliwe, ponieważ zostały wprowadzone błędne dane.");

                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Dodanie tego rekordu do bazy danych naruszyło by unikalność klucza głownego!");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=RYBICCY\\SQLEXPRESS;Initial Catalog=League;Integrated Security=True";
                using (SqlCommand cmd = new SqlCommand("DeleteKlient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("IDPlayer", SqlDbType.Int).Value = textBox1.Text;
                    cmd.Parameters.Add("Name", SqlDbType.VarChar).Value = textBox2.Text;
                    cmd.Parameters.Add("Surname", SqlDbType.VarChar).Value = textBox3.Text;
                    cmd.Parameters.Add("Heigh", SqlDbType.Int).Value = textBox4.Text;
                    cmd.Parameters.Add("Weight", SqlDbType.Int).Value = textBox5.Text;
                    cmd.Parameters.Add("Nation", SqlDbType.VarChar).Value = textBox6.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    display();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=RYBICCY\\SQLEXPRESS;Initial Catalog=League;Integrated Security=True";
                
                
                    int i = 0;
                    bool test1 = int.TryParse(textBox2.Text, out i);
                    bool test2 = int.TryParse(textBox3.Text, out i);
                    bool test3 = int.TryParse(textBox6.Text, out i);

                    if (textBox1.Text.Length > 0 && int.TryParse(textBox1.Text, out i) &&
                    (textBox2.Text is string) && !test1 && !test2 && !test3 &&
                    textBox2.Text.Length > 0 && (textBox3.Text is string) &&
                    textBox3.Text.Length > 0 && textBox4.Text.Length > 0 &&
                    int.TryParse(textBox4.Text, out i) && textBox5.Text.Length > 0 &&
                    int.TryParse(textBox5.Text, out i) && (textBox6.Text is string) &&
                    textBox6.Text.Length > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateKlient", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("IDPlayer", SqlDbType.Int).Value = textBox1.Text;
                            cmd.Parameters.Add("Name", SqlDbType.VarChar).Value = textBox2.Text;
                            cmd.Parameters.Add("Surname", SqlDbType.VarChar).Value = textBox3.Text;
                            cmd.Parameters.Add("Heigh", SqlDbType.Int).Value = textBox4.Text;
                            cmd.Parameters.Add("Weight", SqlDbType.Int).Value = textBox5.Text;
                            cmd.Parameters.Add("Nation", SqlDbType.VarChar).Value = textBox6.Text;
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            display();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Taka edycja rekordu nie jest możliwa, ponieważ zostały wprowadzone błędne dane.");

                    }
                
                
            }
        }

    }
}
