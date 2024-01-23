using System;
using System.Windows.Forms;

namespace lab6
{
    public partial class AddEditForm : Form
    {
        private TextBox txtType;
        private TextBox txtModel;
        private TextBox txtPrice;
        private Button btnSave;

        public string Type { get; private set; }
        public string Model { get; private set; }
        public float Price { get; private set; }

        public AddEditForm()
        {
            InitializeComponents();
        }

        public AddEditForm(int pcId, string Type, string Model, float Price)
        {
            InitializeComponents();
            txtType.Text = Type;
            txtModel.Text = Model;
            txtPrice.Text = Price.ToString();
        }

        private void InitializeComponents()
        {
            txtType = new TextBox();
            txtModel = new TextBox();
            txtPrice = new TextBox();
            btnSave = new Button();

            // TextBox setup
            txtType.Location = new System.Drawing.Point(10, 10);
            txtModel.Location = new System.Drawing.Point(10, 40);
            txtPrice.Location = new System.Drawing.Point(10, 70);

            // Настройка кнопки
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            btnSave.Location = new System.Drawing.Point(10, 100);

            // Добавление элементов на форму
            Controls.Add(txtType);
            Controls.Add(txtModel);
            Controls.Add(txtPrice);
            Controls.Add(btnSave);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Type = txtType.Text;
                Model = txtModel.Text;
                Price = float.Parse(txtPrice.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtType.Text) || string.IsNullOrWhiteSpace(txtModel.Text) || !float.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Fill all required fields", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
