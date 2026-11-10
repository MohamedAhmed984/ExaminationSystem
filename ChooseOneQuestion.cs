using System;

public class ChooseOneQuestion : Question
{
    public string[] Choices { get; set; }
    public string CorrectAnswer { get; set; }

    public ChooseOneQuestion(string header, int marks, QuestionLevel level, string[] choices, string correctAnswer) 
        : base(header, marks, level)
    {
        Choices = choices;
        CorrectAnswer = correctAnswer;
    }

    public override void Display()
    {
        Console.WriteLine($"[Choose One] {Header} (Marks: {Marks})");
        for (int i = 0; i < Choices.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Choices[i]}");
        }
    }

    public override bool CheckAnswer(string studentAnswer)
    {
        return studentAnswer.Trim().Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
    }
}