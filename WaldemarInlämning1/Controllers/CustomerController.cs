using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WaldemarInlämning1.Entities;
using WaldemarInlämning1.Models;

namespace CustomerRegisterDatabase.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private DatabaseContext databaseContext;
        private IHostingEnvironment env;
        private MailConfiguration mailConfiguration;

        public CustomerController(DatabaseContext databaseContext, IHostingEnvironment env, MailConfiguration mailConfiguration, IConfiguration iconfiguration)
        {
            this.databaseContext = databaseContext;
            this.env = env;
            this.mailConfiguration = mailConfiguration;
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            String AllowedEmail = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            String AllowedChars = @"^[a-zA-Z0-9]*$";
            if (Regex.IsMatch(customer.FirstName + customer.LastName + customer.Age + customer.Gender, AllowedChars) && Regex.IsMatch(customer.Email, AllowedEmail))
            {
                databaseContext.Add(customer);
                databaseContext.SaveChanges();

            }
            else
            {
                return BadRequest();
            }


            return Ok(customer.Id);
        }
        [HttpGet]

        public IActionResult ShowAll()
        {

            //List<Customer> customerList = new List<Customer>();
            //foreach (Customer customer in )
            //{
            //    customerList.Add(customer);
            //}
            return Ok(databaseContext.Customers);
        }

        [HttpDelete]
        public IActionResult Delete(int clickedId)
        {

            foreach (Customer customer in databaseContext.Customers)
            {
                if (customer.Id == clickedId)
                {
                    databaseContext.Remove(customer);
                }

            }
            databaseContext.SaveChanges();

            return Ok("kund med id: " + clickedId + " borttagen");
        }

        [HttpPut]
        public IActionResult Edit(int clickedId)
        {

            foreach (Customer customer in databaseContext.Customers)
            {
                if (customer.Id == clickedId)
                {
                    return Ok(customer);
                }

            }

            return Ok("ok");
        }

        [HttpPost, Route("saveEdit")]
        public IActionResult SaveEdit(Customer customer)
        {

            foreach (Customer cust in databaseContext.Customers)
            {
                if (cust.Id == customer.Id)
                {
                    cust.FirstName = customer.FirstName;
                    cust.LastName = customer.LastName;
                    cust.Age = customer.Age;
                    cust.Email = customer.Email;
                    cust.Gender = customer.Gender;
                }
            }
            String AllowedEmail = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            String AllowedChars = @"^[a-zA-Z0-9]*$";
            if (Regex.IsMatch(customer.FirstName + customer.LastName + customer.Age + customer.Gender, AllowedChars) && Regex.IsMatch(customer.Email, AllowedEmail))
            {
                databaseContext.SaveChanges();
                return Ok(customer.Id);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet, Route("env")]
        public IActionResult Env()
        {

            return Ok(new object[] {
                    "production mode: "+env.IsProduction(),
                    "development mode: "+env.IsDevelopment(),
                   "app name: "+env.ApplicationName,
                   "content root path: "+env.ContentRootPath,
                   "environment name: "+env.EnvironmentName,
                   "web root path: "+env.WebRootPath
                });
        }

        [HttpGet, Route("mailServerInfo")]
        public IActionResult MailServerInfo()
        {
           
            return Ok(mailConfiguration);
        }

    }
}
