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
        Console.WriteLine("Avengers\n");
        Console.WriteLine("Welcome " + _character.Name + " the " + _character.Type + " to the Avengers Initiative!\n");

        Decision("Do you want to continue working for " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + "?\n", () =>
        {
            Console.WriteLine("You chose to continue working for " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + ".\n");
            _decisions.Add("Do you want to continue working for " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + "? Yes");
            Console.WriteLine("You are ready to comply with their missions.\n");
            _score -= 20;
            AssassinationMissions();
        }, () =>
        {
            _decisions.Add("Do you want to continue working for " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + "? No");
            Decision("Do you want to join the Avengers?\n", () =>
            {
                Console.WriteLine("Great! You are now a part of the Avengers.\n");
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
            Decision("You were sent to kill the " + target + ". Do you comply?\n", () =>
            {
                Console.WriteLine("You chose to kill the " + target + ".\n");
                _decisions.Add("You were sent to kill the " + target + ". Do you comply? Yes");
                _score -= 10;
            }, () =>
            {
                Console.WriteLine("You refused to kill the " + target + ".\n");
                _decisions.Add("You were sent to kill the " + target + ". Do you comply? No");
                _score += 10;
                _noCount++;

                if (_noCount > 6)
                {
                    Console.WriteLine((_character.Type == "Widow" ? "The Red Room" : "Hydra") + " decided you are a traitor and killed you. Game over.\n");
                    DisplayScore();
                    return;
                }
                else
                {
                    Console.WriteLine("You are tortured by " + (_character.Type == "Widow" ? "the Red Room" : "Hydra") + " for your refusal.\n");
                    _score -= 10;
                }
            });

            if (_noCount > 6)
            {
                return;
            }
        }

        Console.WriteLine("You managed to survive the 10 missions and escaped.\n");
        _decisions.Add("You survived the 10 missions and escaped.");
        BountyHunterMissions();
    }

    private void BountyHunterMissions()
    {
        string[] targets = { "a corrupt politician", "a crime lord", "a rival assassin", "a traitorous general", "a powerful businessman" };

        foreach (var target in targets)
        {
            Decision("You received a contract to kill " + target + ". Do you accept?\n", () =>
            {
                Console.WriteLine("You accepted the contract to kill " + target + ".\n");
                _decisions.Add("You received a contract to kill " + target + ". Do you accept? Yes");
                _score += 10;
            }, () =>
            {
                Console.WriteLine("You refused the contract to kill " + target + ".\n");
                _decisions.Add("You received a contract to kill " + target + ". Do you accept? No");
                _score -= 10;
            });
        }

        Console.WriteLine("You started taking missions to protect people as a bodyguard.\n");
        _decisions.Add("Started taking missions to protect people as a bodyguard.");
        BodyguardMissions();
    }

    private void BodyguardMissions()
    {
        string[] clients = { "a high-profile diplomat", "a celebrity", "a wealthy CEO", "a political activist", "a scientist with important research" };

        foreach (var client in clients)
        {
            Decision("You received a request to protect " + client + ". Do you accept?\n", () =>
            {
                Console.WriteLine("You accepted the mission to protect " + client + ".\n");
                _decisions.Add("You received a request to protect " + client + ". Do you accept? Yes");
                _score += 10;
            }, () =>
            {
                Console.WriteLine("You refused the mission to protect " + client + ".\n");
                _decisions.Add("You received a request to protect " + client + ". Do you accept? No");
                _score -= 10;
            });
        }

        Console.WriteLine("You got noticed by the Avengers.\n");
        Decision("Do you want to join the Avengers?\n", () =>
        {
            Console.WriteLine("You decided to join the Avengers.\n");
            _decisions.Add("Do you want to join the Avengers? Yes");
            _joinedAvengersAtStart = true;
            _score += 10;
            MissionHydraBases();
        }, () =>
        {
            Console.WriteLine("You refused to join the Avengers.\n");
            _decisions.Add("Do you want to join the Avengers? No");
            VigilanteOrBodyguard();
        });
    }

    private void VigilanteOrBodyguard()
    {
        Decision("Do you want to become a vigilante or continue as a bodyguard?\n", () =>
        {
            Console.WriteLine("You chose to become a vigilante.\n");
            _decisions.Add("Do you want to become a vigilante or continue as a bodyguard? Vigilante");
            VigilantePath();
        }, () =>
        {
            Console.WriteLine("You chose to continue as a bodyguard.\n");
            _decisions.Add("Do you want to become a vigilante or continue as a bodyguard? Bodyguard");
            AdditionalBodyguardMissions();
        });
    }

    private void AdditionalBodyguardMissions()
    {
        string[] clients = { "a famous athlete", "a prominent lawyer", "a tech mogul", "a political figure", "an artist with controversial work" };

        foreach (var client in clients)
        {
            Decision("You received a request to protect " + client + ". Do you accept?\n", () =>
            {
                Console.WriteLine("You accepted the mission to protect " + client + ".\n");
                _decisions.Add("You received a request to protect " + client + ". Do you accept? Yes");
                _score += 10;
            }, () =>
            {
                Console.WriteLine("You refused the mission to protect " + client + ".\n");
                _decisions.Add("You received a request to protect " + client + ". Do you accept? No");
                _score -= 10;
            });
        }

        Decision("Do you want to join the Avengers?\n", () =>
        {
            Console.WriteLine("You decided to join the Avengers.\n");
            _decisions.Add("Do you want to join the Avengers? Yes");
            _joinedAvengersAtStart = true;
            _score += 10;
            MissionHydraBases();
        }, () =>
        {
            Console.WriteLine("You refused to join the Avengers and continued as a bodyguard for the rest of your career.\n");
            _decisions.Add("Do you want to join the Avengers? No");
            DisplayScore();
        });
    }

    private void VigilantePath()
    {
        Decision("Do you want to be a vigilante?\n", () =>
        {
            Console.WriteLine("You chose to be a vigilante. The Alien invasion is still happening.\n");
            _decisions.Add("Do you want to be a vigilante? Yes");
            _score += 5;
            Decision("Do you want to help the Avengers?\n", () =>
            {
                Console.WriteLine("You decided to help the Avengers as a vigilante.\n");
                _decisions.Add("Do you want to help the Avengers? Yes");
                _score += 10;
                AlienInvasionAsVigilante();
            }, () =>
            {
                Console.WriteLine("You go around helping whoever and wherever you can.\n");
                _decisions.Add("Do you want to help the Avengers? No");
                _score += 5;
                AlienInvasionAsVigilante();
            });
        }, () =>
        {
            Console.WriteLine("You chose a normal life. Game over.\n");
            _decisions.Add("Do you want to be a vigilante? No");
            _score -= 5;
            DisplayScore();
        });
    }

    private void AlienInvasionAsVigilante()
    {
        Console.WriteLine("You dealt with the alien invasion.\n");
        Decision("Do you want to join the Avengers now?\n", () =>
        {
            Console.WriteLine("You decided to join the Avengers after all.\n");
            _decisions.Add("Do you want to join the Avengers now? Yes");
            _score += 10;
            UltronStoryline();
        }, () =>
        {
            Console.WriteLine("You continue as a vigilante and move to Sokovia.\n");
            _decisions.Add("Do you want to join the Avengers now? No");
            _score += 5;
            SokoviaPath();
        });
    }
    private void UltronStoryline()
    {
        Decision("Do you want to help the Avengers look for Loki's scepter?\n", () =>
        {
            Console.WriteLine("You decided to help the Avengers look for the scepter.\n");
            _decisions.Add("Do you want to help the Avengers look for Loki's scepter? Yes");
            _score += 10;
            IndiaMission();
        }, () =>
        {
            Console.WriteLine("You are an opportunist and were kicked out. Game over.\n");
            _decisions.Add("Do you want to help the Avengers look for Loki's scepter? No");
            _score -= 20;
            DisplayScore();
        });
    }

    private void IndiaMission()
    {
        Console.WriteLine("You go to India with the Avengers and encounter Ultron and the Maximoff twins again.\n");
        Decision("Do you fight?\n", () =>
        {
            Console.WriteLine("You decided to fight against Ultron and the Maximoffs.\n");
            _decisions.Add("Do you fight? Yes");
            Decision("Do you use your superpowers to stop Wanda from showing everyone their worst fears?\n", () =>
            {
                Console.WriteLine("You used your superpowers to stop Wanda.\n");
                _decisions.Add("Do you use your superpowers to stop Wanda from showing everyone their worst fears? Yes");
                _score += 20;
                ClintsHouse();
            }, () =>
            {
                Console.WriteLine("You didn't use your superpowers to stop Wanda.\n");
                _decisions.Add("Do you use your superpowers to stop Wanda from showing everyone their worst fears? No");
                _score -= 20;
                ClintsHouse();
            });
        }, () =>
        {
            Console.WriteLine("You chose not to fight and were killed by Ultron. Game over.\n");
            _decisions.Add("Do you fight? No");
            _score -= 20;
            DisplayScore();
        });
    }

    private void ClintsHouse()
    {
        Console.WriteLine("You get to Clint's house where Nick Fury appears.\n");
        Decision("Do you help him and the rest of the Avengers track down Ultron again?\n", () =>
        {
            Console.WriteLine("You decided to help Nick Fury and the Avengers track down Ultron.\n");
            _decisions.Add("Do you help Nick Fury and the rest of the Avengers track down Ultron again? Yes");
            _score += 10;
            SalemBattle();
        }, () =>
        {
            Console.WriteLine("You are an opportunist and were kicked out. Game over.\n");
            _decisions.Add("Do you help Nick Fury and the rest of the Avengers track down Ultron again? No");
            _score -= 20;
            DisplayScore();
        });
    }

    private void SalemBattle()
    {
        Console.WriteLine("You go to Salem and battle with Ultron, ending up fighting him in the train.\n");
        Decision("Do you use your superpowers to help get people out of the way?\n", () =>
        {
            Console.WriteLine("You used your superpowers to help get people out of the way.\n");
            _decisions.Add("Do you use your superpowers to help get people out of the way? Yes");
            _score += 20;
            ReturnToTower();
        }, () =>
        {
            Console.WriteLine("You didn't use your superpowers and let people suffer. You are kicked out. Game over.\n");
            _decisions.Add("Do you use your superpowers to help get people out of the way? No");
            _score -= 20;
            DisplayScore();
        });
    }

    private void ReturnToTower()
    {
        Console.WriteLine("You return to the Avengers tower.\n");
        Decision("Do you let Tony and Bruce work?\n", () =>
        {
            Console.WriteLine("You chose to destroy the device containing Ultron's new body.\n");
            _decisions.Add("Do you let Tony and Bruce work? No");
            _score += 10;
            DisplayScore();
        }, () =>
        {
            Console.WriteLine("You let Tony and Bruce work, resulting in Vision coming to life.\n");
            _decisions.Add("Do you let Tony and Bruce work? Yes");
            _score += 20;
            DisplayScore();
        });
    }


    private void MissionOne()
    {
        Decision("Loki is attacking New York. Will you help the Avengers to stop him?\n", () =>
        {
            Console.WriteLine("You joined the battle in New York and helped defeat Loki.\n");
            _decisions.Add("Loki is attacking New York. Will you help the Avengers to stop him? Yes");
            _score += 10;
            MissionTwo();
        }, () =>
        {
            Console.WriteLine("You decided not to help, and the battle rages on without you.\n");
            _decisions.Add("Loki is attacking New York. Will you help the Avengers to stop him? No");
            _score -= 10;
            CheckOpportunist();
        });
    }

    private void MissionTwo()
    {
        Decision("There's an incoming alien invasion. Will you join the fight?\n", () =>
        {
            Console.WriteLine("You fought bravely and helped repel the alien invasion.\n");
            _decisions.Add("There's an incoming alien invasion. Will you join the fight? Yes");
            _score += 10;
            MissionHydraBases();
        }, () =>
        {
            Console.WriteLine("You chose to stay back, but the Avengers prevailed without you.\n");
            _decisions.Add("There's an incoming alien invasion. Will you join the fight? No");
            _score -= 5;
            CheckOpportunist();
        });
    }

    private void MissionHydraBases()
    {
        Decision("The Avengers are going on missions to find and raid Hydra bases. Do you want to help?\n", () =>
        {
            Console.WriteLine("You chose to help the Avengers raid Hydra bases.\n");
            _decisions.Add("Do you want to help raid Hydra bases? Yes");
            _score += 10;
            SokoviaBaseMission();
        }, () =>
        {
            Console.WriteLine("You chose not to help with the Hydra base raids.\n");
            _decisions.Add("Do you want to help raid Hydra bases? No");
            _score -= 10;
            SokoviaBaseMission();
        });
    }

    private void SokoviaBaseMission()
    {
        Decision("The Avengers are heading to the Sokovia base. Do you want to join?\n", () =>
        {
            Console.WriteLine("You chose to join the mission to the Sokovia base.\n");
            _decisions.Add("Do you want to join the mission to the Sokovia base? Yes");
            _score += 10;
            EncounterMaximoff();
        }, () =>
        {
            Console.WriteLine("You chose not to join the Sokovia mission. You are kicked out of the Avengers for being an opportunist.\n");
            _decisions.Add("Do you want to join the mission to the Sokovia base? No");
            _score -= 20;
            DisplayScore();
        });
    }

    private void EncounterMaximoff()
    {
        Console.WriteLine("You meet Pietro and Wanda Maximoff at the Sokovia base.\n");
        if (_character.Gender == "Male")
        {
            Console.WriteLine("You talk and fight against Pietro.\n");
            FightOutcome("Pietro");
        }
        else
        {
            Console.WriteLine("You talk and fight against Wanda.\n");
            FightOutcome("Wanda");
        }
        Console.WriteLine("You regroup with the Avengers and complete the mission.\n");
        UltronStoryline();
    }

    private void FightOutcome(string opponent)
    {
        Random random = new Random();
        bool win = random.Next(2) == 0;

        if (win)
        {
            Console.WriteLine("You win the fight against " + opponent + ".\n");
            _decisions.Add("Fight against " + opponent + "? Won");
            _score += 10;
        }
        else
        {
            Console.WriteLine("You lose the fight against " + opponent + ".\n");
            _decisions.Add("Fight against " + opponent + "? Lost");
            _score -= 10;
        }
    }

    private void SokoviaPath()
    {
        Console.WriteLine("You met Pietro and Wanda before the Avengers arrived. You get taken into Hydra and receive a new power.\n");
        Random random = new Random();
        string[] powers = { "Super Speed", "Telekinesis", "Telepathy" };
        string newPower = powers[random.Next(powers.Length)];
        _character.Superpower += ", " + newPower;
        Console.WriteLine("Your new power is " + newPower + ".\n");
        _decisions.Add("Received new power from Hydra: " + newPower);
        Decision("Do you want to help Pietro and Wanda against the Avengers?\n", () =>
        {
            Console.WriteLine("You chose to help Pietro and Wanda against the Avengers.\n");
            _decisions.Add("Help Pietro and Wanda against the Avengers? Yes");
            FightAgainstAvengers();
        }, () =>
        {
            Console.WriteLine("You chose not to help Pietro and Wanda. You leave the scene.\n");
            _decisions.Add("Help Pietro and Wanda against the Avengers? No");
            DisplayScore();
        });
    }

    private void FightAgainstAvengers()
    {
        Console.WriteLine("You fight against the Avengers alongside Pietro and Wanda.\n");
        _decisions.Add("Fought against the Avengers alongside Pietro and Wanda.");
        _score += 10;
        Console.WriteLine("After the fight, you regroup and are called to the church to talk with Ultron.\n");
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
                    Console.WriteLine("It's time to decide.\n");
                    Decision(question, yesAction, noAction);
                });
            }
            else
            {
                Console.WriteLine("You can only choose 'I'll think about it' twice. Please choose again.\n");
                Decision(question, yesAction, noAction);
            }
        }
        else
        {
            Console.WriteLine("Invalid choice, please choose again.\n");
            Decision(question, yesAction, noAction);
        }
    }

    private void CheckOpportunist()
    {
        if (_joinedAvengersAtStart)
        {
            Console.WriteLine("You are an opportunist and you were kicked out. Game over.\n");
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
        if (_score >= -100 && _score < 30)
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

        Console.WriteLine("Here are your decisions:\n");
        foreach (var decision in _decisions)
        {
            Console.WriteLine(decision + "\n");
        }
    }
}
