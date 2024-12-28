using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CustomerOperations customerOperations = new CustomerOperations();
        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount = int.Parse(txtCustomerShoppingCount.Text),
            };

            customerOperations.AddCustomer(customer);
            MessageBox.Show("Musteri Ekleme Islemi Basarili.", "Uyari", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            List<Customer> customers = customerOperations.GetAllCustomer();
            dataGridView1.DataSource = customers;
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string customerId = txtCustomerId.Text;

                customerOperations.DeleteCustomer(customerId);

                MessageBox.Show("Müşteri başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;
            var updateCustomer = new Customer
            {
                CustomerName = txtCustomerName.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount = int.Parse(txtCustomerShoppingCount.Text),
                CustomerId = customerId
            };
            customerOperations.UpdateCustomer(updateCustomer);
            MessageBox.Show("Müşteri başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnCustomerGet_Click(object sender, EventArgs e)
        {
            var customerId = txtCustomerId.Text.Trim(); // Kullanıcıdan ID al
            var customerName = txtCustomerName.Text.Trim(); // Kullanıcıdan isim al
            var customerSurname = txtCustomerSurname.Text.Trim(); // Kullanıcıdan soyisim al


            // Müşteri getir
            Customer customerGet = customerOperations.GetCustomer(customerId, customerName, customerSurname);

            if (customerGet != null)
            {
                // Müşteri bilgilerini DataGridView'e ata
                dataGridView1.DataSource = new List<Customer> { customerGet };
            }
            else
            {
                MessageBox.Show("Aranan müşteri bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
