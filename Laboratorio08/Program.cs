using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio08
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            JoiningLambda();
            Console.Read();
        }
        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach(int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        static void IntroToLINQLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            Func<int, bool> isEven = i => i % 2 == 0;
            var evensLambda = numbers.Where(isEven);
            foreach (int num in evensLambda)
            {
                Console.Write("{0,1} ", num);
            }
        }
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach(var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void DataSourceLambda()
        {
            var queryAllCustomers = context.clientes.ToList();

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(x => x.Ciudad == "Londres").ToList();

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void Ordering()
        {
            var queryLondonCustomers3 =
                    from cust in context.clientes
                    where cust.Ciudad == "Londres"
                    orderby cust.NombreCompañia ascending
                    select cust;

            foreach(var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void OrderingLambda()
        {
            var queryLondonCustomers3 = context.clientes.Where(x => x.Ciudad == "Londres")
                                        .OrderBy(clientes => clientes.NombreCompañia);

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach(var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach(clientes customer in customerGroup)
                {
                    Console.WriteLine("  {0}", customer.NombreCompañia);
                }
            }
        }
        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(clientes => clientes.Ciudad);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("  {0}", customer.NombreCompañia);
                }
            }
        }
        static void Grouping2()
        {
            var custQuery =
                    from cust in context.clientes
                    group cust by cust.Ciudad into custGroup
                    where custGroup.Count() > 2
                    orderby custGroup.Key
                    select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Grouping2Lambda()
        {
            var custQuery = context.clientes.GroupBy(clientes => clientes.Ciudad);
            var custQuery2 = custQuery.Where(c => c.Count() > 2).OrderBy(c => c.Key);

            foreach (var item in custQuery2)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes.Join(context.Pedidos,
                                        c => c.idCliente,
                                        p => p.IdCliente,
                                        (c, p) => 
                                        new { CustomerName = c.NombreCompañia, DistributorName = p.PaisDestinatario });
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
