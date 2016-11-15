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


        static void Main(string[] args)
        {

            Console.WriteLine("Input file path.");
            string filePath = Console.ReadLine();

            //List<Person> people = ReadFile(filePath);

            // insert the person into the database
        }

        //static List<Person> ReadFile(string filePath)
        //{
        //    List<Person> people = new List<Person>();

        //    System.IO.StreamReader file = new System.IO.StreamReader(filePath);

        //    string line;
        //    while((line = file.ReadLine()) != null)
        //    {
        //        string[] items = line.Split(',');

        //        Person temp = new Person(items[0], items[1]);

        //        people.Add(temp);
        //    }

        //    file.Close();

        //    return people;
        //}

    }
}
