using System;
using System.Collections.Generic;

namespace StudentGradeManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the student database with predefined names and grades
            Dictionary<string, int> ssdb = new Dictionary<string, int>()
            {
                {"Alfred", 48}, {"Alison", 54}, {"Allan", 42},
                {"Audrey", 49}, {"Barry", 66}, {"Beth", 67},
                {"Billy", 70}, {"Calvin", 86}, {"Charlotte", 43},
                {"Chris", 72}, {"Claire", 63}, {"Clara", 59},
                {"Clifford", 87}, {"Dean", 95}, {"Edgar",96},
                {"Edna",66}, {"Eileen",100}, {"Franklin",59},
                {"Frederick",97}, {"Glen",70}, {"Gordon",88},
                {"Jean",48}, {"Jeff",54}, {"Joanna",43},
                {"Joanne",78}, {"Julian",99}, {"Keith",97},
                {"Ken",80}, {"Kim",75}, {"Kristen",44},
                {"Louise",67}, {"Luis",85}, {"Margaret",76},
                {"Martin",61}, {"Mary",95}, {"Nina",73},
                {"Pauline",73}, {"Penny",49}, {"Peter",75},
                {"Rhonda",94}, {"Richard",76}, {"Robyn",45},
                {"Samantha",89}, {"Sara",80}, {"Stephen",84},
                {"Tammy",62},{"Vincent",79}, {"William",99}
            };

            // Main program loop - displays menu and handles user input
            while (true)
            {
                Console.WriteLine("\nStudent Grade Management System");
                Console.WriteLine("1. Show all grades");
                Console.WriteLine("2. Add a student");
                Console.WriteLine("3. Delete a student");
                Console.WriteLine("4. Change a student's grade");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                // Read user input and check for null value to avoid null reference errors
                string? input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Error: No input provided.");
                    continue; // Skip to the next iteration if input is null
                }
                string choice = input;

                // Menu options using switch-case based on user input
                switch (choice)
                {
                    case "1":
                        ShowAllGrades(ssdb); // Display all grades
                        break;
                    case "2":
                        ssdb = AddStudent(ssdb); // Add a new student
                        break;
                    case "3":
                        ssdb = DeleteStudent(ssdb); // Delete a student
                        break;
                    case "4":
                        ssdb = ChangeStudentGrade(ssdb); // Change a student's grade
                        break;
                    case "5":
                        Console.WriteLine("Exiting the system.");
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        // Method to display all students and their grades
        static void ShowAllGrades(Dictionary<string, int> ssdb)
        {
            Console.WriteLine("\nGrades for all students:");
            foreach (var student in ssdb)
            {
                Console.WriteLine($"{student.Key}: {student.Value}"); // Print each student's name and grade
            }
        }

        // Method to add a new student to the database
        static Dictionary<string, int> AddStudent(Dictionary<string, int> ssdb)
        {
            Console.Write("Enter the student's name: ");
            string? nameInput = Console.ReadLine(); // Read the student's name and check for null
            if (nameInput == null)
            {
                Console.WriteLine("Error: No name provided.");
                return ssdb; // Return original database if name input is null
            }
            string name = nameInput;

            Console.Write("Enter the student's grade: ");
            string? gradeInput = Console.ReadLine(); // Read the student's grade and check for null
            if (int.TryParse(gradeInput, out int grade))
            {
                if (!ssdb.ContainsKey(name))
                {
                    ssdb[name] = grade; // Add student if not already in the database
                    Console.WriteLine("Student added successfully.");
                }
                else
                {
                    Console.WriteLine("Student already exists.");
                }
            }
            else
            {
                Console.WriteLine("Invalid grade input. Please enter a number.");
            }
            return ssdb; // Return updated database
        }

        // Method to delete a student from the database
        static Dictionary<string, int> DeleteStudent(Dictionary<string, int> ssdb)
        {
            Console.Write("Enter the student's name to delete: ");
            string? nameInput = Console.ReadLine(); // Read the student's name and check for null
            if (nameInput == null)
            {
                Console.WriteLine("Error: No name provided.");
                return ssdb; // Return original database if name input is null
            }
            string name = nameInput;

            if (ssdb.ContainsKey(name))
            {
                ssdb.Remove(name); // Remove student if they exist in the database
                Console.WriteLine("Student deleted successfully.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            return ssdb; // Return updated database
        }

        // Method to change the grade of an existing student
        static Dictionary<string, int> ChangeStudentGrade(Dictionary<string, int> ssdb)
        {
            Console.Write("Enter the student's name to change the grade: ");
            string? nameInput = Console.ReadLine(); // Read the student's name and check for null
            if (nameInput == null)
            {
                Console.WriteLine("Error: No name provided.");
                return ssdb; // Return original database if name input is null
            }
            string name = nameInput;

            if (ssdb.ContainsKey(name))
            {
                Console.Write("Enter the new grade: ");
                string? gradeInput = Console.ReadLine(); // Read the new grade and check for null
                if (int.TryParse(gradeInput, out int newGrade))
                {
                    ssdb[name] = newGrade; // Update the student's grade if input is valid
                    Console.WriteLine("Grade updated successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid grade input. Please enter a number.");
                }
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            return ssdb; // Return updated database
        }
    }
}
