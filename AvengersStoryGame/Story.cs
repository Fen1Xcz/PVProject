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
    private int _noCount;
    private int _thinkCount;

    public Story(Character character)
    {
        _character = character;
        _timer = new Timer();
        _score = 0;
        _joinedAvengersAtStart = false;
        _decisions = new List<string>();
        _noCount = 0;
        _thinkCount = 0;
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
            AssassinationMissions();
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

    private void AssassinationMissions()
    {
        string[] targets = { "American president", "Prime Minister of the UK", "Chancellor of Germany", "President of Russia", "President of China", "Prime Minister of Japan", "President of France", "President of Brazil", "Prime Minister of Canada", "President of India" };

        foreach (var target in targets)
        {
            Decision("You were sent to kill the " + target + ". Do you comply?", () =>
            {
                Console.WriteLine("You chose to kill the " + target + ".");
                _decisions.Add("You were sent to kill the " + target + ". Do you comply? Yes");
                _score -= 10;
            }, () =>
            {
                Console.WriteLine("You refused to kill the " + target + ".");
                _decisions.Add("You were sent to kill the " + target + ". Do you comply? No");
                _score += 10;
                _noCount++;

                if (_noCount > 6)
                {
                    Console.WriteLine((_character.Type == "Widow" ? "The Red Room" : "Hydra") + " decided you are a traitor and killed you. Game over.");
                    DisplayScore();
                    return;
                }
                else
                {
                    Console.WriteLine("You are tortured by " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + " for your refusal.");
                    _score -= 10;
                }
            });

            if (_noCount > 6)
            {
                return;
            }
        }

        Console.WriteLine("You managed to survive the 10 missions and escaped.");
        _decisions.Add("You survived the 10 missions and escaped.");
        BountyHunterMissions();
    }

    private void BountyHunterMissions()
    {
        string[] targets = { "a corrupt politician", "a crime lord", "a rival assassin", "a traitorous general", "a powerful businessman" };

        foreach (var target in targets)
        {
            Decision("You received a contract to kill " + target + ". Do you accept?", () =>
            {
                Console.WriteLine("You accepted the contract to kill " + target + ".");
                _decisions.Add("You received a contract to kill " + target + ". Do you accept? Yes");
                _score += 10;
            }, () =>
            {
                Console.WriteLine("You refused the contract to kill " + target + ".");
                _decisions.Add("You received a contract to kill " + target + ". Do you accept? No");
                _score -= 10;
            });
        }

        Console.WriteLine("You started taking missions to protect people as a bodyguard.");
        _decisions.Add("Started taking missions to protect people as a bodyguard.");
        BodyguardMissions();
    }

    private void BodyguardMissions()
    {
        string[] clients = { "a high-profile diplomat", "a celebrity", "a wealthy CEO", "a political activist", "a scientist with important research" };

        foreach (var client in clients)
        {
            Decision("You received a request to protect " + client + ". Do you accept?", () =>
            {
                Console.WriteLine("You accepted the mission to protect " + client + ".");
                _decisions.Add("You received a request to protect " + client + ". Do you accept? Yes");
                _score += 10;
            }, () =>
            {
                Console.WriteLine("You refused the mission to protect " + client + ".");
                _decisions.Add("You received a request to protect " + client + ". Do you accept? No");
                _score -= 10;
            });
        }

        Console.WriteLine("You got noticed by the Avengers.");
        Decision("Do you want to join the Avengers?", () =>
        {
            Console.WriteLine("You decided to join the Avengers.");
            _decisions.Add("Do you want to join the Avengers? Yes");
            _joinedAvengersAtStart = true;
            _score += 10;
            MissionHydraBases();
        }, () =>
        {
            Console.WriteLine("You refused to join the Avengers and continue as a bounty hunter.");
            _decisions.Add("Do you want to join the Avengers? No");
            DisplayScore();
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
            SokoviaPath();
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
            MissionHydraBases();
        }, () =>
        {
            Console.WriteLine("You chose to stay back, but the Avengers prevailed without you.");
            _decisions.Add("There's an incoming alien invasion. Will you join the fight? No");
            _score -= 5;
            CheckOpportunist();
        });
    }

    private void MissionHydraBases()
    {
        Decision("The Avengers are going on missions to find and raid Hydra bases. Do you want to help?", () =>
        {
            Console.WriteLine("You chose to help the Avengers raid Hydra bases.");
            _decisions.Add("Do you want to help raid Hydra bases? Yes");
            _score += 10;
            SokoviaBaseMission();
        }, () =>
        {
            Console.WriteLine("You chose not to help with the Hydra base raids.");
            _decisions.Add("Do you want to help raid Hydra bases? No");
            _score -= 10;
            SokoviaBaseMission();
        });
    }

    private void SokoviaBaseMission()
    {
        Decision("The Avengers are heading to the Sokovia base. Do you want to join?", () =>
        {
            Console.WriteLine("You chose to join the mission to the Sokovia base.");
            _decisions.Add("Do you want to join the mission to the Sokovia base? Yes");
            _score += 10;
            EncounterMaximoff();
        }, () =>
        {
            Console.WriteLine("You chose not to join the Sokovia mission. You are kicked out of the Avengers for being an opportunist.");
            _decisions.Add("Do you want to join the mission to the Sokovia base? No");
            _score -= 20;
            DisplayScore();
        });
    }

    private void EncounterMaximoff()
    {
        Console.WriteLine("You meet Pietro and Wanda Maximoff at the Sokovia base.");
        if (_character.Gender == "Male")
        {
            Console.WriteLine("You talk and fight against Pietro.");
            FightOutcome("Pietro");
        }
        else
        {
            Console.WriteLine("You talk and fight against Wanda.");
            FightOutcome("Wanda");
        }
        Console.WriteLine("You regroup with the Avengers and complete the mission.");
        StayAtAvengersTower();
    }

    private void FightOutcome(string opponent)
    {
        Random random = new Random();
        bool win = random.Next(2) == 0;

        if (win)
        {
            Console.WriteLine("You win the fight against " + opponent + ".");
            _decisions.Add("Fight against " + opponent + "? Won");
            _score += 10;
        }
        else
        {
            Console.WriteLine("You lose the fight against " + opponent + ".");
            _decisions.Add("Fight against " + opponent + "? Lost");
            _score -= 10;
        }
    }

    private void SokoviaPath()
    {
        Console.WriteLine("You met Pietro and Wanda before the Avengers arrived. You get taken into Hydra and receive a new power.");
        Random random = new Random();
        string[] powers = { "Super Speed", "Telekinesis", "Telepathy" };
        string newPower = powers[random.Next(powers.Length)];
        _character.Superpower += ", " + newPower;
        Console.WriteLine("Your new power is " + newPower + ".");
        _decisions.Add("Received new power from Hydra: " + newPower);
        Decision("Do you want to help Pietro and Wanda against the Avengers?", () =>
        {
            Console.WriteLine("You chose to help Pietro and Wanda against the Avengers.");
            _decisions.Add("Help Pietro and Wanda against the Avengers? Yes");
            FightAgainstAvengers();
        }, () =>
        {
            Console.WriteLine("You chose not to help Pietro and Wanda. You leave the scene.");
            _decisions.Add("Help Pietro and Wanda against the Avengers? No");
            DisplayScore();
        });
    }

    private void FightAgainstAvengers()
    {
        Console.WriteLine("You fight against the Avengers alongside Pietro and Wanda.");
        _decisions.Add("Fought against the Avengers alongside Pietro and Wanda.");
        _score += 10;
        Console.WriteLine("After the fight, you regroup and are called to the church to talk with Ultron.");
        DisplayScore();
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
            _thinkCount = 0;
        }
        else if (choice == "2")
        {
            noAction();
            _thinkCount = 0;
        }
        else if (choice == "3")
        {
            if (_thinkCount < 2)
            {
                _thinkCount++;
                _timer.Start(() =>
                {
                    Console.WriteLine("It's time to decide.");
                    Decision(question, yesAction, noAction);
                });
            }
            else
            {
                Console.WriteLine("You can only choose 'I'll think about it' twice. Please choose again.");
                Decision(question, yesAction, noAction);
            }
        }
        else
        {
            Console.WriteLine("Invalid choice, please choose again.");
            Decision(question, yesAction, noAction);
        }
    }

    private void CheckOpportunist()
    {
        if (_joinedAvengersAtStart)
        {
            Console.WriteLine("You are an opportunist and you were kicked out. Game over.");
            _score -= 20;
            DisplayScore();
        }
        else
        {
            DisplayScore();
        }
    }

    private void DisplayScore()
    {
        Console.WriteLine("Your current score is: " + _score);
        if (_score >= -1000 && _score < 30)
        {
            Console.WriteLine("You are a villain.");
        }
        else if (_score >= 30 && _score < 50)
        {
            Console.WriteLine("You are a decent hero.");
        }
        else if (_score >= 50 && _score < 70)
        {
            Console.WriteLine("You are a good hero.");
        }
        else if (_score >= 70 && _score < 90)
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
