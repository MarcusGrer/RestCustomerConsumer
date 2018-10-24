using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestCustomerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {

            // GET
            //int a = 0;
            //IList<Customer> mylist = GetCustomersAsync().GetAwaiter().GetResult();

            //foreach (var obj in mylist)
            //{

            //    Console.WriteLine(mylist[a].ToString());
            //    a++;
            //}
            //

            // GET id             
            //Console.WriteLine("Select Id");
            //string myId = Console.ReadLine();
            //Customer cus = GetCustomerbyIdAsync(myId).GetAwaiter().GetResult();
            //Console.WriteLine(cus.ToString());
            //

            //DELETE
            Console.WriteLine("Select Id");
            string myId = Console.ReadLine();
            var status = DeleteAsync(myId);

            int a = 0;
            IList<Customer> mylist = GetCustomersAsync().GetAwaiter().GetResult();

            foreach (var obj in mylist)
            {

                Console.WriteLine(mylist[a].ToString());
                a++;
            }

            Console.ReadKey();

        }      
        public static async Task<HttpStatusCode> DeleteAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string Uri = "https://localhost:44364/api/ApiWithActions/";
                string path = Uri + id;
                HttpResponseMessage content = await client.DeleteAsync(path);
                //Customer obj = JsonConvert.DeserializeObject<Customer>(content);
                return content.StatusCode;
            }
        }

        public static async Task<IList<Customer>> GetCustomersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string CustomersUri = "https://localhost:44364/api/Customers";
                string content = await client.GetStringAsync(CustomersUri);
                IList<Customer> cList = JsonConvert.DeserializeObject<IList<Customer>>(content);
                return cList;
            }
        }

        public static async Task<Customer> GetCustomerbyIdAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string Uri = "https://localhost:44364/api/Customers/";
                string path = Uri + id;
                string content = await client.GetStringAsync(path);
                Customer obj = JsonConvert.DeserializeObject<Customer>(content);
                return obj;
            }
        }
    }

    class Customer
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private int _yearOfRegistration;

        public int Id { get => _id; set => _id = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public int YearOfRegistration { get => _yearOfRegistration; set => _yearOfRegistration = value; }

        public Customer(int id, string firstName, string lastName, int yearOfRegistration)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            YearOfRegistration = yearOfRegistration;
        }

        public Customer()
        {

        }

        public override string ToString()
        {
            return ("Name: "+FirstName+" Surname: "+LastName + " Id: "+ Id+ " Date: "+YearOfRegistration);
        }
    }
}
