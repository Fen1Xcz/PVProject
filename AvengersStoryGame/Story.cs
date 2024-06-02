using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Story
{
    private Character _character;
    private Timer _timer;

    public Story(Character character)
    {
        _character = character;
        _timer = new Timer();
    }

    public void Start()
    {
        Console.WriteLine("Welcome " + _character.Name + " the " + _character.Type + " to the Avengers Initiative!");

        Decision("Do you want to join the Avengers?", () =>
        {
            Console.WriteLine("Great! You are now a part of the Avengers.");
            MissionOne();
        }, () =>
        {
            Console.WriteLine("You chose not to join the Avengers. The world needs you, maybe next time.");
        });
    }

    private void MissionOne()
    {
        Decision("Loki is attacking New York. Will you help the Avengers to stop him?", () =>
        {
            Console.WriteLine("You joined the battle in New York and helped defeat Loki.");
            MissionTwo();
        }, () =>
        {
            Console.WriteLine("You decided not to help, and the battle rages on without you.");
        });
    }

    private void MissionTwo()
    {
        Decision("There's an incoming alien invasion. Will you join the fight?", () =>
        {
            Console.WriteLine("You fought bravely and helped repel the alien invasion.");
        }, () =>
        {
            Console.WriteLine("You chose to stay back, but the Avengers prevailed without you.");
        });
    }

    private void Decision(string question, Action yesAction, Action noAction)
    {
        Console.WriteLine(question);
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        Console.WriteLine("3. I'll think about it");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            yesAction();
        }
        else if (choice == "2")
        {
            noAction();
        }
        else if (choice == "3")
        {
            _timer.Start(() =>
            {
                Console.WriteLine("It's time to decide.");
                Decision(question, yesAction, noAction);
            });
        }
        else
        {
            Console.WriteLine("Invalid choice, please choose again.");
            Decision(question, yesAction, noAction);
        }
    }
}

