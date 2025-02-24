namespace Journal;

using Spectre.Console;
using System.IO;
using System;
using System.Globalization;

class Program {
    
    static void Main(string[] args) {

        Console.WriteLine("\nWelcome to your Gratitude Journal!");

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
        
        IdeaGenerator ig = new IdeaGenerator("idea-database.txt");

        DateTime date = DateTime.Today;
        string strDate = date.ToString(new CultureInfo("en-us"));
        string formattedDate = strDate.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
        Console.WriteLine("\n✨ Gratitude Entry for " + formattedDate);

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

                Console.WriteLine("💡 Random Gratitude Idea: " + ig.getRandomIdea());

            }
        }
        if (addSelection == "Save and Exit") {

            // Verify date
            string dateSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(new[] {
                        "Use Today's Date", "Enter Another Date"
                    }));

            if (dateSelection == "Enter Another Date") {

                bool isValid = false;

                // https://www.bytehide.com/blog/string-to-datetime-csharp

                while (!isValid) {
                    Console.Write("Enter a date in mm/dd/yyyy format: ");
                    string stringDate = Console.ReadLine();

                    isValid = DateTime.TryParse(stringDate, out date);

                    if (!isValid) {
                        Console.WriteLine("Invalid date. Try again.");
                    }
                }
                
            }


            string filePath = "gratitude-journal.txt";

            if (!File.Exists(filePath)) {
                File.Create(filePath).Close();
            }

            // FILE FORMAT: Date||Item|Item|Item

            string contents = date.Date.ToString(new CultureInfo("en-us")) + "||";
            contents += String.Join("|", items);

            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine(contents);
            sw.Close();

            Console.WriteLine("\n✨ Gratitude Entry for " + date.Date.ToString(new CultureInfo("en-us")).Split(" ", StringSplitOptions.RemoveEmptyEntries)[0] + ":");
            foreach (string item in items) {
                Console.WriteLine(item);
            }
            Console.WriteLine("✅ Saved.\n");
        }
        
    }

     static void ViewEntries() {

        string filePath = "gratitude-journal.txt";

        Dictionary<DateTime, List<string>> journal = new Dictionary<DateTime, List<string>>();

        string contents = File.ReadAllText(filePath);
        string[] entries = contents.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        foreach (string entry in entries) {
            string[] dateAndList = entry.Split("||", StringSplitOptions.RemoveEmptyEntries);

            string dtString = dateAndList[0];

            string[] listItems = dateAndList[1].Split("|", StringSplitOptions.RemoveEmptyEntries);

            // Final date form
            DateTime parsedDate = DateTime.Parse(dtString).Date;

            if (journal.ContainsKey(parsedDate)) {
                journal[parsedDate].AddRange(listItems);
            } else {
                journal.Add(parsedDate, listItems.ToList());
            } 

        }
        
        foreach (KeyValuePair<DateTime, List<string>> kvp in journal) {
            string formattedDate = kvp.Key.ToString(new CultureInfo("en-us")).Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
            Console.WriteLine("✨ Gratitude Entry for " + formattedDate);
            foreach (string value in kvp.Value) {
                Console.WriteLine(value);
            }
            Console.WriteLine("");
        }

        // TODO: Find latest date and print out the according entries?

        var viewSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Enter Date to View", "Exit", 
                }));

        while (viewSelection == "Enter Date to View") {

            // https://www.bytehide.com/blog/string-to-datetime-csharp

            Console.Write("Enter a date in mm/dd/yyyy format: ");
            string stringDate = Console.ReadLine();

            DateTime parsedDate;
            bool isValid = DateTime.TryParse(stringDate, out parsedDate);

            if (!isValid) {
                Console.WriteLine("Invalid date. Try again.");
            } else {
                parsedDate = parsedDate.Date;

                List<string> entriesForDate;

                if (journal.ContainsKey(parsedDate)) {
                    string strDate = parsedDate.ToString(new CultureInfo("en-us"));
                    string formattedDate = strDate.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
                    Console.WriteLine("\n✨ Gratitude Entry for " + formattedDate);

                    entriesForDate = journal[parsedDate];
                    foreach (string entry in entriesForDate) {
                        Console.WriteLine(entry);
                    }
                    Console.WriteLine("");
                } else {
                    Console.WriteLine("Sorry, no entries for that date.\n");
                }
            }

            viewSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Enter Date to View", "Exit", 
                }));
        }

    }

     static void GetIdeas() {

        IdeaGenerator ig = new IdeaGenerator("idea-database.txt");

        Console.WriteLine("\n💡 Random Gratitude Idea");

        string ideaSelection = "New Idea";

        while (ideaSelection == "New Idea") {

            Console.WriteLine(ig.getRandomIdea());
            
            ideaSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "New Idea", "Exit", 
                }));
        }

    }
}