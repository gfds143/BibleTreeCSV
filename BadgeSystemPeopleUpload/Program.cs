using System;
using System.Collections.Generic;
using BibleTree.Models;

namespace BadgeSystemPeopleUpload
{
    class Program
    {
        static string[] adminUsers = { "Gage Ervin", "Tyler Kerr", "Ante Susic", "Chance Turner", "Andy Harbert" };
        static string[] facultyUsers = { "Jesus Arredondo", "Weston Buck" };


        static void Main(string[] args)
        {

            #region read people from file
            //Console.WriteLine("Input file path.");
            //string filePath = Console.ReadLine();

            string filePath = @"..\..\SourceFiles\BadgeSystemPeople.csv";

            List<User> people = PeopleReadFile(filePath);

            List<Administrator> administrators = new List<Administrator>();
            List<Student> students = new List<Student>();
            List<Faculty> faculty = new List<Faculty>();

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
                else if(IsIn(U.user_name, facultyUsers))
                {
                    Faculty tempFaculty = new Faculty();

                    tempFaculty.mapUser(U);

                    tempFaculty.user_type = 'f';
                    tempFaculty.user_token = "Unused";

                    tempFaculty.faculty_department = "Computer Science";
                    tempFaculty.faculty_position = "Professor";

                    faculty.Add(tempFaculty);
                }
                else
                {
                    Student tempStu = new Student();
                    tempStu.mapUser(U);

                    tempStu.user_type = 's';
                    tempStu.user_token = "Unused";

                    tempStu.student_id = "";                                // ??? 
                    tempStu.student_dateEnrolled = enrollDate;
                    tempStu.student_dateGraduated = graduateDate;

                    students.Add(tempStu);
                }

            }
            #endregion

            // insert the person into the database

            BibleTree.Services.SQLService db = new BibleTree.Services.SQLService();

            Console.Write("Do you want to reset the database? (Y/N) :");
            string cont = Console.ReadLine();

            if (cont.ToUpper() == "Y")
                db.Rebuild();
            try
            {
                foreach (Student item in students)
                {
                    db.AddStudent(item);
                    Console.WriteLine("Added Student :{0}", item);
                }

                foreach (Administrator item in administrators)
                {
                    db.AddAdministrator(item);
                    Console.WriteLine("Added Admin :{0}", item);
                }

                foreach(Faculty item in faculty)
                {
                    db.AddFaculty(item);
                    Console.WriteLine("Added Faculty :{0}", item);
                }

                Console.WriteLine("Finished loading from people csv.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(ex.Message);
            }



            string bankfilePath = @"..\..\SourceFiles\BadgeBank.csv";

            List<BadgeType> badges = BadgeReadFile(bankfilePath);
            // load the badges into the database

            try
            {
                foreach (BadgeType badge in badges)
                {
                    db.AddBadge(badge);
                    Console.WriteLine("Added badge :'{0}'", badge.badge_name);
                }

                Console.WriteLine("Finished loading badges from csv.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Finished Running");
        }

        static List<User> PeopleReadFile(string filePath)
        {
            List<User> users = new List<User>();

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);

            int count = 1000;
            string line;

            // ignore the first line
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


        static List<BadgeType> BadgeReadFile(string filePath)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);

            /*
             * Badge#;                  0
             * Badge Name;              1
             * Badge Summary;           2
             * Badge Category;          3
             * Badge Give Type;         4
             * Date Activated;          5
             * Date Retired;            6
             * Notes;                   7
             * Image website address    8
            */


            string line;
            List<BadgeType> badges = new List<BadgeType>();

            file.ReadLine();
            while((line = file.ReadLine()) != null)
            {
                string[] item = line.Split(';');

                BadgeType badge = new BadgeType();

                badge.badge_id = Convert.ToInt32(item[0]);

                badge.badge_name = item[1];

                badge.badge_description = item[2];


                badge.badge_level = (BadgeType.Badge_Level)Convert.ToInt32(item[3]);


                string startDate = item[5].Replace('-', '/');
                string endDate = item[6].Replace('-', '/');
                
                badge.badge_activeDate = (startDate == "") ? 
                        new DateTime(1753,1,1) : Convert.ToDateTime(startDate);

                badge.badge_expirationDate = (endDate == "") ? 
                        new DateTime(9999,12,31) : Convert.ToDateTime(endDate);

                badge.badge_gifURL = item[8];

                badges.Add(badge);
            }

            return badges;
        }
    }
}
