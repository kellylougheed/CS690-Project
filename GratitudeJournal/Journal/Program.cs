namespace Journal;

using Spectre.Console;
using System.IO;
using System;
using System.Globalization;

class Program {
    static void Main(string[] args) {

        Console.WriteLine("Welcome to your Gratitude Journal!");

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Add Entry", "View Entries", "Get Ideas", 
                }));

        if (selection == "Add Entry") {
            AddEntry();
        } else if (selection == "View Entries") {
            ViewEntries();
        } else if (selection == "Get Ideas") {
            GetIdeas();
        }
    }

    static void AddEntry() {

        DateTime date = DateTime.Today;
        Console.WriteLine("Gratitude Entry for " + date.ToString(new CultureInfo("en-us")));

        string addSelection = "Add Another";

        List<string> items = new List<string>();

        while (addSelection == "Add Another") {

            Console.WriteLine("What are you grateful for?");
            string item = Console.ReadLine();

            if (item.Length > 0) {
                items.Add(item);
            }
            
            addSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Add Another", "Save and Exit", 
                }));
        }
        if (addSelection == "Save and Exit") {

            // TODO: SAVE DATE + LIST TO A FILE

            Console.WriteLine("Your list:");
            Console.WriteLine(items);
            Console.WriteLine("Exiting now");
        }
        

        
    }

     static void ViewEntries() {

    }

     static void GetIdeas() {


    }
}