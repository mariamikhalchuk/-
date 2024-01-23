using lab6;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab6
{
    public partial class MainForm : Form
    {
        private DatabaseHelper databaseManager;
        private DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private DataRow selectedRow;

        public MainForm()
        {
            InitializeComponents();
            databaseManager = new DatabaseHelper();
            LoadData();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void InitializeComponents()
        {
            dataGridView1 = new DataGridView();
            btnAdd = new System.Windows.Forms.Button();
            btnEdit = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();

            
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(565, 400);
           
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;

            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;

            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;

            
            Controls.Add(dataGridView1);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);

            
            btnAdd.Location = new System.Drawing.Point(10, dataGridView1.Bottom + 40);
            btnEdit.Location = new System.Drawing.Point(btnAdd.Right + 40, dataGridView1.Bottom + 40);
            btnDelete.Location = new System.Drawing.Point(btnEdit.Right + 70, dataGridView1.Bottom + 40);

            btnAdd.Size = new System.Drawing.Size(90, 50);
            btnEdit.Size = new System.Drawing.Size(120, 50);
            btnDelete.Size = new System.Drawing.Size(80, 50);

        }

        private void LoadData()
        {
            dataGridView1.DataSource = databaseManager.LoadData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                selectedRow = ((DataRowView)dataGridView1.SelectedRows[0].DataBoundItem).Row;
            }
            else
            {
                selectedRow = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (AddEditForm addForm = new AddEditForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    databaseManager.InsertTicket(addForm.Type, addForm.Model, addForm.Price);
                    LoadData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedRow != null)
            {
                int pcId = Convert.ToInt32(selectedRow["ID"]);
                string Type = selectedRow["Type"].ToString();
                string Model = selectedRow["Model"].ToString();
                float Price = Convert.ToSingle(selectedRow["Price"]);

                using (AddEditForm editForm = new AddEditForm(pcId, Type, Model, Price))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        databaseManager.Update(pcId, editForm.Type, editForm.Model, editForm.Price);
                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose a ticket to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRow != null)
            {
                int pcId = Convert.ToInt32(selectedRow["ID"]);
                databaseManager.Delete(pcId);
                LoadData();
            }
            else
            {
                MessageBox.Show("Select for delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            databaseManager.Dispose();
            base.OnFormClosing(e);
        }
    }
}
