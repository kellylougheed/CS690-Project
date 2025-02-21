namespace Journal;

using Spectre.Console;
using System.IO;
using System;
using System.Globalization;

class Program {
    static void Main(string[] args) {

        Console.WriteLine("Welcome to your Gratitude Journal!");

        var selection = "";

        while (selection != "Quit Program") {

            selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(new[] {
                        "Add Entry", "View Entries", "Get Ideas", "Quit Program"
                    }));

            if (selection == "Add Entry") {
                AddEntry();
            } else if (selection == "View Entries") {
                ViewEntries();
            } else if (selection == "Get Ideas") {
                GetIdeas();
            }

        }

        
    }

    static void AddEntry() {

        DateTime date = DateTime.Today;
        Console.WriteLine("Gratitude Entry for " + date.ToString(new CultureInfo("en-us")));

        string addSelection = "Add Another";

        List<string> items = new List<string>();

        while (addSelection == "Add Another" || addSelection == "Get an Idea") {

            Console.WriteLine("What are you grateful for?");
            string item = Console.ReadLine();

            if (item.Length > 0) {
                items.Add(item);
            }
            
            addSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Add Another", "Get an Idea", "Save and Exit", 
                }));

            if (addSelection == "Get an Idea") {

                // TODO: Generate random gratitude idea here

                Console.WriteLine("Random Gratitude Idea");

            }
        }
        if (addSelection == "Save and Exit") {

            // TODO: SAVE DATE + LIST TO A FILE

            Console.WriteLine("Your list:");
            Console.WriteLine(items);
            Console.WriteLine("Exiting now");
        }
        
    }

     static void ViewEntries() {

        // TODO: READ IN FILE AS DICTIONARY OF TYPE DATETIME:LIST OF STRINGS

        Dictionary<DateTime, List<string>> journal = new Dictionary<DateTime, List<string>>();
        journal.Add(DateTime.Today.Date, new List<string> { "Thing 1", "Thing 2" });
        
        // testing
        foreach (KeyValuePair<DateTime, List<string>> kvp in journal)
        {
            Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
        }

        Console.WriteLine("Latest Entry:");

        // TODO: Find latest date and print out the according entries

        var viewSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Enter Date to View", "Exit", 
                }));

        while (viewSelection == "Enter Date to View") {

            // https://www.bytehide.com/blog/string-to-datetime-csharp

            Console.Write("Enter a date in MM/dd/yyyy format: ");

            // TODO: Validate user input

            string stringDate = Console.ReadLine();

            DateTime parsedDate = DateTime.Parse(stringDate).Date;

            List<string> entries = journal[parsedDate];

            foreach (string entry in entries) {
                Console.WriteLine(entry);
            }
            
            viewSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Enter Date to View", "Exit", 
                }));
        }

    }

     static void GetIdeas() {

        // TODO: READ IN IDEAS FILE AS LIST OF STRINGS

        List<string> ideas = new List<string> { "Idea 1", "Idea 2", "Idea 3", "Idea 4", "Idea 5" };

        var random = new Random();

        Console.WriteLine("Random Gratitude Idea:");

        string ideaSelection = "New Idea";

        while (ideaSelection == "New Idea") {

            int index = random.Next(ideas.Count);
            Console.WriteLine(ideas[index]);
            
            ideaSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "New Idea", "Exit", 
                }));
        }

    }
}