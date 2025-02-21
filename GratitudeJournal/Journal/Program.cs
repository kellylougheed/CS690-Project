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

                Console.WriteLine("Random Gratitude Idea: " + getRandomIdea());

            }
        }
        if (addSelection == "Save and Exit") {

            string filePath = "gratitude-journal.txt";

            if (!File.Exists(filePath)) {
                File.Create(filePath).Close();
            }

            // FILE FORMAT: Date||Item|Item|Item

            string contents = date.ToString(new CultureInfo("en-us")) + "||";
            contents += String.Join("|", items);

            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine(contents);
            sw.Close();

            Console.WriteLine("Your entry: " + contents);
            Console.WriteLine("Saved.");
        }
        
    }

     static void ViewEntries() {

        // TODO: READ IN FILE AS DICTIONARY OF TYPE DATETIME:LIST OF STRINGS

        Dictionary<DateTime, List<string>> journal = new Dictionary<DateTime, List<string>>();
        journal.Add(DateTime.Today.Date, new List<string> { "Thing 1", "Thing 2" });
        
        // testing
        foreach (KeyValuePair<DateTime, List<string>> kvp in journal)
        {
            Console.WriteLine(kvp.Key);
            foreach (string value in kvp.Value) {
                Console.WriteLine(value);
            }
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

        var random = new Random();

        Console.WriteLine("Random Gratitude Idea:");

        string ideaSelection = "New Idea";

        while (ideaSelection == "New Idea") {

            Console.WriteLine(getRandomIdea());
            
            ideaSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "New Idea", "Exit", 
                }));
        }

    }

    static string getRandomIdea() {
        string filePath = "idea-database.txt";
        string contents = File.ReadAllText(filePath);
        string[] ideas = contents.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        Random random = new Random();
        int index = random.Next(0, ideas.Length);

        return ideas[index];
    }
}