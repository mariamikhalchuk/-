using lab7; 
using System; using System.Data;
using System.Windows.Forms;

namespace lab7
{
public partial class MainForm : Form
{
private DatabaseManager databaseManager; private DataGridView dataGridView1; private Button btnAdd;
private Button btnEdit; private Button btnDelete; private DataRow selectedRow;

public MainForm()
{
InitializeComponents();
databaseManager = new DatabaseManager(); LoadData();
dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
}

private void InitializeComponents()
{
dataGridView1 = new DataGridView(); btnAdd = new Button();
btnEdit = new Button(); btnDelete = new Button();

40);

dataGridView1.ReadOnly = true; dataGridView1.AllowUserToAddRows = false; dataGridView1.AllowUserToDeleteRows = false;
dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.Size = new System.Drawing.Size(565, 400);
btnAdd.Text = "Add"; btnAdd.Click += btnAdd_Click;

btnEdit.Text = "Edit"; btnEdit.Click += btnEdit_Click;

btnDelete.Text = "Delete"; btnDelete.Click += btnDelete_Click;
Controls.Add(dataGridView1); Controls.Add(btnAdd); Controls.Add(btnEdit); Controls.Add(btnDelete);

btnAdd.Location = new System.Drawing.Point(10, dataGridView1.Bottom +

btnEdit.Location = new System.Drawing.Point(btnAdd.Right + 40,
 
dataGridView1.Bottom + 40);
btnDelete.Location = new System.Drawing.Point(btnEdit.Right + 70, dataGridView1.Bottom + 40);

btnAdd.Size = new System.Drawing.Size(90, 50); btnEdit.Size = new System.Drawing.Size(120, 50); btnDelete.Size = new System.Drawing.Size(80, 50);

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
using (AddLotteryForm addForm = new AddLotteryForm())
{
if (addForm.ShowDialog() == DialogResult.OK)
{
databaseManager.InsertTicket(addForm.LotteryName, addForm.Amount, addForm.Price);
LoadData();
}
}
}

private void btnEdit_Click(object sender, EventArgs e)
{
if (selectedRow != null)
{
int ticketId = Convert.ToInt32(selectedRow["ID"]);
string PCType = selectedRow["Type"].ToString(); string amount = selectedRow["Amount"].ToString();
float price = Convert.ToSingle(selectedRow["Price"]);

using (AddLotteryForm editForm = new AddLotteryForm(ticketId, lotteryName, amount, price))
{
if (editForm.ShowDialog() == DialogResult.OK)
{
databaseManager.UpdateTicket(ticketId, editForm.LotteryName, editForm.Amount, editForm.Price);
LoadData();
}
}
}
else
{
MessageBox.Show("Choose a PC to edit”, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
}

private void btnDelete_Click(object sender, EventArgs e)
{
if (selectedRow != null)
{
 


}
else
{
 
int ticketId = Convert.ToInt32(selectedRow["ID"]); databaseManager.DeleteTicket(ticketId); LoadData();


MessageBox.Show("Choose a ticket to delete.", "Warning",
 
MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
}

protected override void OnFormClosing(FormClosingEventArgs e)
{
databaseManager.Dispose(); base.OnFormClosing(e);
}
}
}

