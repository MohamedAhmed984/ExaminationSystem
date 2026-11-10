using System;

public class TrueFalseQuestion : Question
{
    public string CorrectAnswer { get; set; }

    public TrueFalseQuestion(string header, int marks, QuestionLevel level, string correctAnswer) 
        : base(header, marks, level)
    {
        CorrectAnswer = correctAnswer;
    }

    public override void Display()
    {
        Console.WriteLine($"[True/False] {Header} (Marks: {Marks})");
        Console.WriteLine("1. True");
        Console.WriteLine("2. False");
    }

    public override bool CheckAnswer(string studentAnswer)
    {
        return studentAnswer.Trim().Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
    }
}