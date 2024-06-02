using System;
using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            Character character = Character.CreateCharacter();
            Story story = new Story(character);
            story.Start();
        }
}