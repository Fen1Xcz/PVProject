using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Character
{
    public string Name { get; set; }
    public string Type { get; set; }

    public static Character CreateCharacter()
    {
        Console.WriteLine("Choose your character type:");
        Console.WriteLine("1. Widow from the Red Room");
        Console.WriteLine("2. Soldier from Hydra");

        string choice = Console.ReadLine();
        Character character = new Character();

        if (choice == "1")
        {
            character.Type = "Widow";
        }
        else if (choice == "2")
        {
            character.Type = "Soldier";
        }
        else
        {
            Console.WriteLine("Invalid choice, defaulting to Widow.");
            character.Type = "Widow";
        }

        Console.Write("Enter your character's name: ");
        character.Name = Console.ReadLine();
        return character;
    }
}
