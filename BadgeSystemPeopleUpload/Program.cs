using System;
using System.Collections.Generic;
using BibleTree.Models;

namespace BadgeSystemPeopleUpload
{
    class Program
    {
<<<<<<< HEAD
        static string[] adminUsers = { "Gage Ervin", "Aaron Holmes", "Tyler Kerr", "Ante Susic", "Chance Turner", "Andy Harbert" };
=======
        const string CONNECTION_STRING =
                @"Data Source=.;" +

            // added this comment to test merge on github
                    //@"Data Source=.\SQLEXPRESS;" +
                    "Initial Catalog=Sales;Integrated Security=True;Connect Timeout=15;" +
                    "Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
>>>>>>> refs/remotes/origin/master

        static void Main(string[] args)
        {

            //Console.WriteLine("Input file path.");
            //string filePath = Console.ReadLine();

            string filePath = @"..\..\SourceFiles\BadgeSystemPeople.csv";

            List<User> people = ReadFile(filePath);

            List<Administrator> administrators = new List<Administrator>();
            List<Student> students = new List<Student>();

            DateTime enrollDate = new DateTime(2014, 8, 1);
            DateTime graduateDate = new DateTime(2019, 4, 30);

            foreach (User U in people)
            {
                if( IsIn(U.user_name, adminUsers)){

                    Administrator tempAdmin = new Administrator();
                    tempAdmin.mapUser(U);

                    tempAdmin.user_type = 'a';
                    tempAdmin.user_token = "Unused";

                    tempAdmin.administrator_permLevel = 1;

                    administrators.Add(tempAdmin);
                }
                else
                {
                    Student tempStu = new Student();
                    tempStu.mapUser(U);

                    tempStu.user_type = 's';
                    tempStu.user_token = "Unused";

                    //tempStu.student_id = students.Count;   ??? what is student ID and why is it a string
                    tempStu.student_dateEnrolled = enrollDate;
                    tempStu.student_dateGraduated = graduateDate;

                    students.Add(tempStu);
                }

            }

            // insert the person into the database
        }

        static List<User> ReadFile(string filePath)
        {
            List<User> users = new List<User>();

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);

            int count = 0;
            string line;

            file.ReadLine();

            while ((line = file.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                //Person temp = new Person(items[0], items[1]);

                User temp = new User();
                temp.user_name = items[0];
                temp.user_email = items[1];
                temp.user_id = count;

                count++;

                users.Add(temp);
            }

            file.Close();

            return users;
        }

        static bool IsIn(string A, string[] B)
        {
            foreach (string S in B)
            {
                if (S == A)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
