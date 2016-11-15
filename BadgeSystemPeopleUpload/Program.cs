using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace BadgeSystemPeopleUpload
{
    class Program
    {
        const string CONNECTION_STRING =
                @"Data Source=.;" +

            // added this comment to test merge on github
                    //@"Data Source=.\SQLEXPRESS;" +
                    "Initial Catalog=Sales;Integrated Security=True;Connect Timeout=15;" +
                    "Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static void Main(string[] args)
        {

            Console.WriteLine("Input file path.");
            string filePath = Console.ReadLine();

            List<Person> people = ReadFile(filePath);

            upload(people);
        }

        static List<Person> ReadFile(string filePath)
        {
            List<Person> people = new List<Person>();

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);

            string line;
            while((line = file.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                Person temp = new Person(items[0], items[1]);

                people.Add(temp);
            }

            file.Close();

            return people;
        }

        static void upload(List<Person> people)
        {
            using (IDbConnection db = new SqlConnection(CONNECTION_STRING))
            {
                //var temp = InsertSQL();

                foreach (Person p in people)
                {
                    //db.Execute(InsertSQL(), p);
                }

            }
        }
    }
}
