﻿using CinemaAdminApp.Models;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace CinemaAdminApp.Controllers
{
    public class OrderController : Controller
    {
        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44392/api/Admin/GetAllActiveOrders";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Order>>().Result;
            return View(data);
        }

        public IActionResult Details(Guid orderId)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44392/api/Admin/GetDetailsForOrder";
            var model = new
            {
                Id = orderId
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); //parsira vo json

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;
            return View(data);
        }

        public IActionResult CreateInvoice(Guid id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44392/api/Admin/GetDetailsForOrder";
            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); //parsira vo json

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath); //loading template
            document.Content.Replace("{{OrderNumber}}", data.Id.ToString());
            document.Content.Replace("{{UserName}}", data.Owner.UserName);

            StringBuilder sb = new StringBuilder();

            var total = 0.0;

            foreach (var item in data.ProductInOrders)
            {
                total += item.Quantity * item.Product.ProductPrice;
                sb.AppendLine(item.Product.ProductName + " with quantity of: " + item.Quantity + " and price of: $" + item.Product.ProductPrice);
            }

            document.Content.Replace("{{ProductList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", "$" + total.ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());


            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
        public FileContentResult ExportAllOrders(Guid id)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44386/api/Admin/GetOrders";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var result = response.Content.ReadAsAsync<List<Order>>().Result;

            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workBook = new XLWorkbook()) //create excel document
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("All Orders"); //create worksheet

                worksheet.Cell(1, 1).Value = "Order Id";
                worksheet.Cell(1, 2).Value = "Costumer Name";
                worksheet.Cell(1, 3).Value = "Costumer Last Name";
                worksheet.Cell(1, 4).Value = "Costumer Email";

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.Owner.FirstName;
                    worksheet.Cell(i + 1, 3).Value = item.Owner.LastName;
                    worksheet.Cell(i + 1, 4).Value = item.Owner.Email;

                    for (int p = 1; p <= item.ProductInOrders.Count(); p++)
                    {
                        worksheet.Cell(1, p + 4).Value = "Product-" + (p + 1);
                        worksheet.Cell(i + 1, p + 4).Value = item.ProductInOrders.ElementAt(p - 1).Product.ProductName;
                    }

                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }

        }
    }
}



