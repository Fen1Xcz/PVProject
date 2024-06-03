using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.Collections.Generic;

class Story
{
    private Character _character;
    private Timer _timer;
    private int _score;
    private bool _joinedAvengersAtStart;
    private List<string> _decisions;

    public Story(Character character)
    {
        _character = character;
        _timer = new Timer();
        _score = 0;
        _joinedAvengersAtStart = false;
        _decisions = new List<string>();
    }

    public void Start()
    {
        Console.WriteLine("Avengers");
        Console.WriteLine("Welcome " + _character.Name + " the " + _character.Type + " to the Avengers Initiative!");

        Decision("Do you want to continue working for " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + "?", () =>
        {
            Console.WriteLine("You chose to continue working for " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + ".");
            _decisions.Add("Do you want to continue working for " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + "? Yes");
            Console.WriteLine("You are ready to comply with their missions.");
            _score -= 20;
            DisplayScore();
        }, () =>
        {
            _decisions.Add("Do you want to continue working for " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + "? No");
            Decision("Do you want to join the Avengers?", () =>
            {
                Console.WriteLine("Great! You are now a part of the Avengers.");
                _decisions.Add("Do you want to join the Avengers? Yes");
                _joinedAvengersAtStart = true;
                _score += 10;
                MissionOne();
            }, () =>
            {
                _decisions.Add("Do you want to join the Avengers? No");
                VigilantePath();
            });
        });
    }

    private void VigilantePath()
    {
        Decision("Do you want to be a vigilante?", () =>
        {
            Console.WriteLine("You chose to be a vigilante. The Alien invasion is still happening.");
            _decisions.Add("Do you want to be a vigilante? Yes");
            _score += 5;
            Decision("Do you want to help the Avengers?", () =>
            {
                Console.WriteLine("You decided to help the Avengers as a vigilante.");
                _decisions.Add("Do you want to help the Avengers? Yes");
                _score += 10;
                AlienInvasionAsVigilante();
            }, () =>
            {
                Console.WriteLine("You go around helping whoever and wherever you can.");
                _decisions.Add("Do you want to help the Avengers? No");
                _score += 5;
                AlienInvasionAsVigilante();
            });
        }, () =>
        {
            Console.WriteLine("You chose a normal life. Game over.");
            _decisions.Add("Do you want to be a vigilante? No");
            _score -= 5;
            DisplayScore();
        });
    }

    private void AlienInvasionAsVigilante()
    {
        Console.WriteLine("You dealt with the alien invasion.");
        Decision("Do you want to join the Avengers now?", () =>
        {
            Console.WriteLine("You decided to join the Avengers after all.");
            _decisions.Add("Do you want to join the Avengers now? Yes");
            _score += 10;
            StayAtAvengersTower();
        }, () =>
        {
            Console.WriteLine("You continue as a vigilante and move to Sokovia.");
            _decisions.Add("Do you want to join the Avengers now? No");
            _score += 5;
            DisplayScore();
        });
    }

    private void StayAtAvengersTower()
    {
        Console.WriteLine("You stayed with the Avengers in the Avengers tower for now.");
        DisplayScore();
    }

    private void MissionOne()
    {
        Decision("Loki is attacking New York. Will you help the Avengers to stop him?", () =>
        {
            Console.WriteLine("You joined the battle in New York and helped defeat Loki.");
            _decisions.Add("Loki is attacking New York. Will you help the Avengers to stop him? Yes");
            _score += 10;
            MissionTwo();
        }, () =>
        {
            Console.WriteLine("You decided not to help, and the battle rages on without you.");
            _decisions.Add("Loki is attacking New York. Will you help the Avengers to stop him? No");
            _score -= 10;
            CheckOpportunist();
        });
    }

    private void MissionTwo()
    {
        Decision("There's an incoming alien invasion. Will you join the fight?", () =>
        {
            Console.WriteLine("You fought bravely and helped repel the alien invasion.");
            _decisions.Add("There's an incoming alien invasion. Will you join the fight? Yes");
            _score += 10;
            DisplayScore();
        }, () =>
        {
            Console.WriteLine("You chose to stay back, but the Avengers prevailed without you.");
            _decisions.Add("There's an incoming alien invasion. Will you join the fight? No");
            _score -= 5;
            CheckOpportunist();
        });
    }

    private void CheckOpportunist()
    {
        if (_joinedAvengersAtStart)
        {
            Console.WriteLine("You are an opportunist and you were kicked out. Game over.");
            DisplayScore();
        }
        else
        {
            DisplayScore();
        }
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

    private void DisplayScore()
    {
        Console.WriteLine("Your current score is: " + _score);
        if (_score >= 0 && _score < 10)
        {
            Console.WriteLine("You are a villain.");
        }
        else if (_score >= 10 && _score < 20)
        {
            Console.WriteLine("You are a decent hero.");
        }
        else if (_score >= 20 && _score < 30)
        {
            Console.WriteLine("You are a good hero.");
        }
        else if (_score >= 30 && _score < 40)
        {
            Console.WriteLine("You are an excellent hero.");
        }

        Console.WriteLine("Here are your decisions:");
        foreach (var decision in _decisions)
        {
            Console.WriteLine(decision);
        }
    }
}
