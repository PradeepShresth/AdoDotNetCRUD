using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MVC_ADO.Models;

namespace MVC_ADO.Controllers
{
    public class ProductController : Controller
    {
        public readonly string connectionString =
            "Data Source=(local);Initial Catalog=AdoCrud;"
            + "Integrated Security=true";

        public IActionResult Index()
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Product", connection);
                adapter.Fill(table);
            }
            return View(table);
        }

        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Product VALUES(@Name)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Product product = new Product();
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Product Where ProductID = @ProductID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                adapter.Fill(table);
            }

            if (table.Rows.Count == 1)
            {
                product.ProductID = Convert.ToInt32(table.Rows[0][0].ToString());
                product.Name = table.Rows[0][1].ToString();
                return View(product);
            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Product SET Name = @Name Where ProductID = @ProductID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Product product = new Product();
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Product Where ProductID = @ProductID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                adapter.Fill(table);
            }

            if (table.Rows.Count == 1)
            {
                product.ProductID = Convert.ToInt32(table.Rows[0][0].ToString());
                product.Name = table.Rows[0][1].ToString();
                return View(product);
            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Product Where ProductID = @ProductID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
