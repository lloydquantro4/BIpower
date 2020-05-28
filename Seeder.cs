using System;
using System.Linq;
using BIpower.Models;
using System.Collections.Generic;

namespace BIpower
{
    public class Seeder
    {
        private readonly biContext _db;
        List<Customer> customerList = new List<Customer>();
        List<Order> orderList = new List<Order>();
        List<Server> serverList = new List<Server>();
        public Seeder(biContext db)
        {

            _db = db;
        }

        public void Seed()
        {
            if (!_db.Customers.Any())
            {
                SeedCustomers();

            }
            if (!_db.Orders.Any())
            {
                SeedOrders();

            }
            if (!_db.Servers.Any())
            {
                SeedServers();

            }

            _db.SaveChanges();
        }

        private void SeedCustomers()
        {

            customerList.Add(new Customer { Name = "Lloyd", Province = "WC", Email = "test1@gmail.com" });
            customerList.Add(new Customer { Name = "Taf", Province = "NC", Email = "taf@gmail.com" });
            customerList.Add(new Customer { Name = "Taw", Province = "EC", Email = "taw@gmail.com" });
            customerList.Add(new Customer { Name = "Tal", Province = "KZN", Email = "tal@gmail.com" });
            customerList.Add(new Customer { Name = "Dad", Province = "LM", Email = "dad@gmail.com" });

            foreach (var cust in customerList)
            {
                _db.Customers.Add(cust);
            }
        }
        private void SeedOrders()
        {

            orderList.Add(new Order { Customer = customerList[0], Total = 200, OrderPlaced = new DateTime(2008, 3, 1, 7, 0, 0) });
            orderList.Add(new Order { Customer = customerList[1], Total = 3400, OrderPlaced = new DateTime(2018, 3, 1, 7, 0, 0) });
            orderList.Add(new Order { Customer = customerList[2], Total = 100, OrderPlaced = new DateTime(2018, 3, 1, 10, 0, 0) });
            orderList.Add(new Order { Customer = customerList[3], Total = 300, OrderPlaced = new DateTime(2019, 3, 1, 9, 0, 0) });
            orderList.Add(new Order { Customer = customerList[4], Total = 400, OrderPlaced = new DateTime(2020, 3, 1, 8, 0, 0) });

            foreach (var item in orderList)
            {
                _db.Orders.Add(item);
            }


        }
        private void SeedServers()
        {
            serverList.Add(new Server{Name= "WC HUB", isOnline = true});
            serverList.Add(new Server{Name="Gauteng HUB", isOnline = true});
            serverList.Add(new Server{Name="KZN HUB", isOnline= false});
            serverList.Add(new Server{Name="EC HUB", isOnline=false});

            foreach (var serverItem in serverList)
            {
                _db.Servers.Add(serverItem);
            }
        }
    }
}