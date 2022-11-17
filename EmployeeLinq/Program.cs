using EmployeeLinq.Entities;
using System.Globalization;
using System.Linq;

static void Print<T>(string message, IEnumerable<T> collection) {
    Console.WriteLine(message);
    foreach (T obj in collection) {
        Console.WriteLine(obj);
    }
    Console.WriteLine();
}

Console.Write("Enter full file path: ");
string path = Console.ReadLine();
List<Employee> list = new List<Employee>();
try {
    using (StreamReader sr = File.OpenText(path)) {
        while(!sr.EndOfStream) {
            string[] vect = sr.ReadLine().Split(",");
            string name = vect[0];
            string email = vect[1];
            double salary = double.Parse(vect[2], CultureInfo.InvariantCulture);
            list.Add(new Employee(name, email, salary));
        }
    }
} catch (Exception ex) { 
    Console.WriteLine(ex.Message);
}

Console.Write("Enter salary: ");
double salaryFilter = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);


var emails = list.Where(p => p.Salary > salaryFilter).OrderBy(p => p.Email).Select(p => p.Email);
var sum = list.Where(p => p.Name[0] == 'M').Select(p => p.Salary).Sum();


Console.WriteLine("Email of people whose salary is more than: " + salaryFilter.ToString("F2", CultureInfo.InvariantCulture));
Print("", emails);

Console.WriteLine("Sum of salary of people whose name starts with 'M': ");
Console.WriteLine(sum.ToString("F2", CultureInfo.InvariantCulture));


