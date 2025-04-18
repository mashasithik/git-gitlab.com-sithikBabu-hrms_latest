namespace HRMS.Management.WinForms
{
    partial class EmployeeDetails
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeDetails));
            dataGridView1 = new DataGridView();
            jobGridView = new DataGridView();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            txtName = new TextBox();
            txtPhone = new TextBox();
            cmbRole = new ComboBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            dtDob = new DateTimePicker();
            cmbGender = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            label1 = new Label();
            label2 = new Label();
            txtempID = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            label11 = new Label();
            label12 = new Label();
            btnClear = new Button();
            btnFind = new Button();
            label7 = new Label();
            cmbDept = new ComboBox();
            label13 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)jobGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 54);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1253, 251);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // jobGridView
            // 
            jobGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            jobGridView.Enabled = false;
            jobGridView.Location = new Point(5, 507);
            jobGridView.Name = "jobGridView";
            jobGridView.RowHeadersWidth = 51;
            jobGridView.Size = new Size(1260, 251);
            jobGridView.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 339);
            label3.Name = "label3";
            label3.Size = new Size(49, 20);
            label3.TabIndex = 5;
            label3.Text = "Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(781, 336);
            label4.Name = "label4";
            label4.Size = new Size(40, 20);
            label4.TabIndex = 6;
            label4.Text = "DOB";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(512, 340);
            label5.Name = "label5";
            label5.Size = new Size(57, 20);
            label5.TabIndex = 7;
            label5.Text = "Gender";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 785);
            label6.Name = "label6";
            label6.Size = new Size(39, 20);
            label6.TabIndex = 8;
            label6.Text = "Role";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(250, 339);
            label8.Name = "label8";
            label8.Size = new Size(50, 20);
            label8.TabIndex = 10;
            label8.Text = "Phone";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(25, 387);
            label9.Name = "label9";
            label9.Size = new Size(46, 20);
            label9.TabIndex = 11;
            label9.Text = "Email";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(282, 387);
            label10.Name = "label10";
            label10.Size = new Size(62, 20);
            label10.TabIndex = 12;
            label10.Text = "Address";
            // 
            // txtName
            // 
            txtName.Location = new Point(94, 332);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 13;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(319, 336);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(172, 27);
            txtPhone.TabIndex = 15;
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(54, 783);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(151, 28);
            cmbRole.TabIndex = 16;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(94, 384);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(172, 27);
            txtEmail.TabIndex = 17;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(351, 387);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(434, 27);
            txtAddress.TabIndex = 18;
            // 
            // dtDob
            // 
            dtDob.Location = new Point(854, 330);
            dtDob.Name = "dtDob";
            dtDob.Size = new Size(157, 27);
            dtDob.TabIndex = 19;
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female" });
            cmbGender.Location = new Point(589, 337);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(151, 28);
            cmbGender.TabIndex = 20;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(813, 387);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 21;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(917, 385);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 29);
            btnUpdate.TabIndex = 22;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1017, 386);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 23;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            label1.ForeColor = Color.Coral;
            label1.Location = new Point(1008, 8);
            label1.Name = "label1";
            label1.Size = new Size(174, 28);
            label1.TabIndex = 24;
            label1.Text = "Employee Details";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic);
            label2.ForeColor = Color.Coral;
            label2.Location = new Point(1008, 473);
            label2.Name = "label2";
            label2.Size = new Size(192, 28);
            label2.TabIndex = 25;
            label2.Text = "Job History Details";
            // 
            // txtempID
            // 
            txtempID.Enabled = false;
            txtempID.ForeColor = SystemColors.WindowFrame;
            txtempID.Location = new Point(1203, 12);
            txtempID.Name = "txtempID";
            txtempID.Size = new Size(59, 27);
            txtempID.TabIndex = 26;
            txtempID.Visible = false;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(604, 783);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(157, 27);
            dateTimePicker1.TabIndex = 27;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(604, 816);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(157, 27);
            dateTimePicker2.TabIndex = 28;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(513, 790);
            label11.Name = "label11";
            label11.Size = new Size(79, 20);
            label11.TabIndex = 29;
            label11.Text = "From Date";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(513, 821);
            label12.Name = "label12";
            label12.Size = new Size(61, 20);
            label12.TabIndex = 30;
            label12.Text = "To Date";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(1117, 386);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 31;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnFind
            // 
            btnFind.Location = new Point(794, 788);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(94, 29);
            btnFind.TabIndex = 32;
            btnFind.Text = "Search";
            btnFind.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(245, 788);
            label7.Name = "label7";
            label7.Size = new Size(89, 20);
            label7.TabIndex = 34;
            label7.Text = "Department";
            // 
            // cmbDept
            // 
            cmbDept.FormattingEnabled = true;
            cmbDept.Location = new Point(340, 784);
            cmbDept.Name = "cmbDept";
            cmbDept.Size = new Size(151, 28);
            cmbDept.TabIndex = 33;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label13.ForeColor = Color.Black;
            label13.Location = new Point(-15, 450);
            label13.Name = "label13";
            label13.Size = new Size(1641, 23);
            label13.TabIndex = 35;
            label13.Text = resources.GetString("label13.Text");
            // 
            // EmployeeDetails
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1315, 850);
            Controls.Add(label13);
            Controls.Add(label7);
            Controls.Add(cmbDept);
            Controls.Add(btnFind);
            Controls.Add(btnClear);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(txtempID);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(cmbGender);
            Controls.Add(dtDob);
            Controls.Add(txtAddress);
            Controls.Add(txtEmail);
            Controls.Add(cmbRole);
            Controls.Add(txtPhone);
            Controls.Add(txtName);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(jobGridView);
            Controls.Add(dataGridView1);
            Name = "EmployeeDetails";
            Text = "Human Resource (HR) Management System";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)jobGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridView jobGridView;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox txtName;
        private TextBox textBox2;
        private TextBox txtPhone;
        private ComboBox cmbRole;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private DateTimePicker dtDob;
        private ComboBox cmbGender;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Label label1;
        private Label label2;
        private TextBox txtempID;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private Label label11;
        private Label label12;
        private Button btnClear;
        private Button btnFind;
        private Label label7;
        private ComboBox cmbDept;
        private Label label13;
    }
}
