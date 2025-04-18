using HRMS.Management.Common.Master;
using HRMS.Management.Common.Transaction;
using HRMS.Management.CommonLayer;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Text;
using System;
using System.Security.Policy;
using System.Windows.Forms;

namespace HRMS.Management.WinForms
{
    public partial class EmployeeDetails : Form
    {
        //private BindingList<Customer> customerList;
        readonly string _apiURL;

        public EmployeeDetails()
        {
            InitializeComponent();
            _apiURL = "https://localhost:44388/api";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataAsync();
            LoadEmplLocation();
            LoadRole();
        }

        private async Task LoadDataAsync()
        {
            string apiUrl = _apiURL + "/Transaction/Employee/getEmployeeDetails"; // Replace with your API URL
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<EmployeesCollectionDataModel>(responseBody);
                    dataGridView1.DataSource = data.employeeModelList;
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Request error: {e.Message}");
                }
            }

        }

        private async Task LoadEmplLocation()
        {
            string apiUrl = _apiURL + "/Masters/Location/getLocationDetails"; // Replace with your API URL
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<LocationCollectionDataModel>(responseBody);
                    cmbDept.DataSource = data.LocationModelList;
                    cmbDept.DisplayMember = "Name";
                    cmbDept.ValueMember = "LocationID";
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Request error: {e.Message}");
                }
            }
        }
        private async Task LoadRole()
        {
            string apiUrl = _apiURL + "/Masters/Roles/getRolesDetails"; // Replace with your API URL
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<RolesCollectionDataModel>(responseBody);
                    cmbRole.DataSource = data.RolesModelList;
                    cmbRole.DisplayMember = "Name";
                    cmbRole.ValueMember = "RoleID";
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Request error: {e.Message}");
                }
            }
        }
        private async Task LoadJobHistoryDataAsync(int employeeID)
        {
            //int employeeID = 3;
            string apiUrl = _apiURL + "/Transaction/JobHistory/getJobHistoryDetails?employeeID=" + employeeID; // Replace with your API URL
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JobHistoryCollectionDataModel>(responseBody);
                    jobGridView.DataSource = data.JobHistoryModelList.Select(x =>
                    new JobHistoryDetail
                    {
                        EmployeeName = x.EmployeeName,
                        ManagerName = x.ManagerName,
                        RoleName = x.RoleName,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Status = x.Status,
                        Comments = x.Comments
                    }).ToList();
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Request error: {e.Message}");
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the click is not on the header row
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //Load textboxes
                txtempID.Text = row.Cells["EmployeeID"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                dtDob.Text = row.Cells["birthDate"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                cmbGender.Text = row.Cells["Gender"].Value.ToString();


                int employeeID = Convert.ToInt32(row.Cells["EmployeeID"].Value);
                LoadJobHistoryDataAsync(employeeID);
                //string message = $"You clicked on row with ID: {row.Cells["ID"].Value} and Name: {row.Cells["Name"].Value}";
                //MessageBox.Show(message, "Row Clicked", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeesModel emplst = new EmployeesModel();
                emplst.EmployeeID = 0;
                emplst.Name = txtName.Text;
                emplst.BirthDate = dtDob.Text;
                emplst.Phone = txtPhone.Text;
                emplst.Email = txtEmail.Text;
                emplst.Address = txtAddress.Text;
                emplst.Gender = cmbGender.Text;
                emplst.Status = "Active";

                InsertUpdateEmployee(emplst);


            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task InsertUpdateEmployee(EmployeesModel emplst)
        {
            //int employeeID = 3;
            string apiUrl = _apiURL + "/Transaction/Employee/postEmployeeDetails"; // Replace with your API URL
            try
            {
               
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44388/api/Transaction/Employee/postEmployeeDetails");
                var json = JsonConvert.SerializeObject(emplst);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                var res = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CoreResponse>(res);
                if (data.Status == CoreResponseStatus.Success)
                { MessageBox.Show(data.Message); LoadDataAsync(); }
                else { MessageBox.Show($"Request error: {data.Message}"); }

            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}");
            }

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeesModel emplst = new EmployeesModel();
                emplst.EmployeeID = Convert.ToInt32(txtempID.Text.Trim());
                emplst.Name = txtName.Text;
                emplst.BirthDate = dtDob.Text;
                emplst.Phone = txtPhone.Text;
                emplst.Email = txtEmail.Text;
                emplst.Address = txtAddress.Text;
                emplst.Gender = cmbGender.Text;
                emplst.Status = "Active";

                InsertUpdateEmployee(emplst);                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
            $"Are you sure you want to delete '{txtName.Text}' Detail?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DeleteEmployee(Convert.ToInt32(txtempID.Text.Trim()));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting '{txtName.Text}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task DeleteEmployee(int employeeID)
        {
            //int employeeID = 3;
            string apiUrl = _apiURL + "/Transaction/Employee/deleteEmployeeDetails?employeeID=" + employeeID; // Replace with your API URL
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);
                    //var json = JsonConvert.SerializeObject(employeeID);
                    //var content = new StringContent(json, null, "application/json");
                    //request.Content = content;
                    var response = await client.SendAsync(request);
                    //response.EnsureSuccessStatusCode();
                    var res = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CoreResponse>(res);


                    if (data.Status == CoreResponseStatus.Success)
                    { MessageBox.Show(data.Message); LoadDataAsync(); }
                    else { MessageBox.Show($"Request error: {data.Message}"); }
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Request error: {e.Message}");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
        }
    }

    public class JobHistoryDetail
    {
        public string EmployeeName { get; set; } = "";
        public string ManagerName { get; set; } = "";
        public string RoleName { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
        public string Status { get; set; } = "";
        public string Comments { get; set; } = "";
    }
}
