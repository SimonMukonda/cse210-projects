using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise2 Project.");

        Console.Write("Please place your grade percentage for evaluation: ");
        string studentInput = Console.ReadLine();
        int percent = int.Parse(studentInput);

        string letter = "";

        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your current grade is: {letter}");
        
        if (percent >= 70)
        {
            Console.WriteLine("You passed, Well done!");
        }
        else
        {
            Console.WriteLine("Dont beat yourself up, Aim higher next term.");
        }
    }
}