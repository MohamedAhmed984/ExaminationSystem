using System;
using System.Collections.Generic;
using System.Linq;

public class ExaminationSystem
{
    private static List<Question> questionBank = new List<Question>();

    public static void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("================================");
            Console.WriteLine("     EXAMINATION SYSTEM         ");
            Console.WriteLine("================================");
            Console.WriteLine("1. Doctor Mode");
            Console.WriteLine("2. Student Mode");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                DoctorMode();
            }
            else if (choice == "2")
            {
                StudentMode();
            }
            else if (choice == "3")
            {
                break;
            }
        }
    }

    private static void DoctorMode()
    {
        Console.Clear();
        Console.WriteLine("--- Doctor Mode ---");
        Console.Write("Enter number of questions to add: ");
        if (!int.TryParse(Console.ReadLine(), out int count)) return;

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"\n--- Question {i + 1} ---");
            Console.WriteLine("Select Type (1. True/False, 2. Choose One):");
            string type = Console.ReadLine();

            Console.Write("Enter Question Header: ");
            string header = Console.ReadLine();

            Console.Write("Enter Marks: ");
            int marks = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Level (1. Easy, 2. Medium, 3. Hard):");
            QuestionLevel level = (QuestionLevel)int.Parse(Console.ReadLine());

            if (type == "1")
            {
                Console.Write("Enter Correct Answer (True/False): ");
                string ans = Console.ReadLine();
                questionBank.Add(new TrueFalseQuestion(header, marks, level, ans));
            }
            else
            {
                string[] choices = new string[4];
                for (int c = 0; c < 4; c++)
                {
                    Console.Write($"Enter choice {c + 1}: ");
                    choices[c] = Console.ReadLine();
                }
                Console.Write("Enter correct choice number (1-4): ");
                string ans = Console.ReadLine();
                questionBank.Add(new ChooseOneQuestion(header, marks, level, choices, ans));
            }
        }

        Console.WriteLine("\nSaved successfully! Press any key to return.");
        Console.ReadKey();
    }

    private static void StudentMode()
    {
        Console.Clear();
        if (questionBank.Count == 0)
        {
            Console.WriteLine("No questions available! Press any key.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("--- Student Mode ---");
        Console.WriteLine("1. Practical Exam (Half questions)");
        Console.WriteLine("2. Final Exam (All questions)");
        string examType = Console.ReadLine();

        Console.WriteLine("Enter Level (1. Easy, 2. Medium, 3. Hard):");
        QuestionLevel level = (QuestionLevel)int.Parse(Console.ReadLine());

        List<Question> examQuestions = questionBank.Where(q => q.Level == level).ToList();

        if (examQuestions.Count == 0)
        {
            Console.WriteLine("No questions found for this level! Press any key.");
            Console.ReadKey();
            return;
        }

        if (examType == "1")
        {
            int take = (examQuestions.Count + 1) / 2;
            examQuestions = examQuestions.Take(take).ToList();
        }

        int totalMarks = examQuestions.Sum(q => q.Marks);
        int score = 0;

        Console.Clear();
        for (int i = 0; i < examQuestions.Count; i++)
        {
            Console.WriteLine($"\nQ{i + 1}:");
            examQuestions[i].Display();

            Console.Write("Your Answer: ");
            string ans = Console.ReadLine();

            if (examQuestions[i].CheckAnswer(ans))
            {
                score += examQuestions[i].Marks;
            }
        }

        Console.Clear();
        Console.WriteLine("================================");
        Console.WriteLine($"Your Result: {score} / {totalMarks}");
        Console.WriteLine("================================");
        Console.WriteLine("Press any key to return.");
        Console.ReadKey();
    }
}