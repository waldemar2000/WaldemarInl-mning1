using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WaldemarInlämning1.Entities;

namespace CustomerRegisterDatabase.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private DatabaseContext databaseContext;

        public CustomerController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
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

    }
}
