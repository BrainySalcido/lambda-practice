using lamda_practice.Data;
using System;
using System.Globalization;
using System.Linq;

namespace lambda_practice
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = new DatabaseContext())
            {

                //1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua
                var query1 = ctx.Employees

                    .Where(e => e.City.Name == "Chihuahua")
                    .Select(s => new { s.Id, s.FirstName, s.LastName, s.City });

                foreach (var employee in query1)
                {

                    Console.WriteLine("{0}  {1}  {2}  {3}",
                        employee.Id, employee.FirstName, employee.LastName, employee.City.Name);


                }
                Console.ReadKey();
                Console.Clear();



                //2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.

                var query2 = from employee in ctx.Employees
                             group employee by employee.DepartmentId
                    into deparmentGroup


                             select new
                             {
                                 N = deparmentGroup.Select(e => e.Department.Name).FirstOrDefault(),
                                 C = deparmentGroup.Count()
                             };
                foreach (var department in query2)
                {

                    Console.WriteLine("{0} {1}",
                     department.N, department.C);


                }
                Console.ReadKey();
                Console.Clear();
                

                //4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.

                var query4 = ctx.Employees
                    .Where(e => e.HireDate.Month == DateTime.Today.Month + 1);
                foreach (var employee in query4)
                {
                    Console.WriteLine("{0} {1} {2}",
                      employee.FirstName, employee.LastName, employee.HireDate);
                }

                Console.ReadKey();
                Console.Clear();









                //5.Listar los 12 meses del año y el numero de empleados contratados por cada mes.

                var query5 = from employee in ctx.Employees
                             group employee by employee.HireDate.Month
                    into hireGroup


                             select new
                             {
                                 N = hireGroup.Select(e => e.HireDate).FirstOrDefault(),
                                 C = hireGroup.Count()
                             };


                foreach (var employee in query5.OrderBy(e => e.N.Month))
                {
                    Console.WriteLine("{0} {1}",
                      employee.N.ToString("MMM"), employee.C);
                }

                Console.ReadKey();
                Console.Clear();

            }


            Console.Read();
        }
    }
}
