class Character
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Gender { get; set; }
    public string Superpower { get; set; }

    public static Character CreateCharacter()
    {
        Character character = new Character();

        while (true)
        {
            Console.WriteLine("Choose your character type:\n");
            Console.WriteLine("1. Widow from the Red Room");
            Console.WriteLine("2. Soldier from Hydra");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                character.Type = "Widow";
                character.Gender = "Female"; 
                break;
            }
            else if (choice == "2")
            {
                character.Type = "Soldier";
                character.Gender = ChooseGender();
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice, please choose again.");
            }
        }

        Console.Write("Enter your character's name: ");
        character.Name = Console.ReadLine();

        if (character.Type == "Widow")
        {
            character.Superpower = "Enchanted speed, agility, and strength";
            Console.WriteLine("As a Widow, you have enhanced speed, agility, and strength.");
        }
        else if (character.Type == "Soldier")
        {
            character.Superpower = ChooseSuperpower();
        }

        return character;
    }

    private static string ChooseGender()
    {
        while (true)
        {
            Console.WriteLine("Choose your gender:\n");
            Console.WriteLine("1. Male");
            Console.WriteLine("2. Female");
            string genderChoice = Console.ReadLine();

            if (genderChoice == "1")
            {
                return "Male";
            }
            else if (genderChoice == "2")
            {
                return "Female";
            }
            else
            {
                Console.WriteLine("Invalid choice, please choose again.");
            }
        }
    }

    private static string ChooseSuperpower()
    {
        while (true)
        {
            Console.WriteLine("You can choose one superpower:\n");
            Console.WriteLine("1. Super Speed");
            Console.WriteLine("2. Telekinesis");
            Console.WriteLine("3. Telepathy (mess with people's heads)");
            string powerChoice = Console.ReadLine();

            if (powerChoice == "1")
            {
                return "Super Speed";
            }
            else if (powerChoice == "2")
            {
                return "Telekinesis";
            }
            else if (powerChoice == "3")
            {
                return "Telepathy";
            }
            else
            {
                Console.WriteLine("Invalid choice, please choose again.");
            }
        }
    }
}
