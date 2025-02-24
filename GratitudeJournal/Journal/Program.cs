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

        GJournal myJournal = new GJournal("gratitude-journal.txt");
        myJournal.displayEntries();

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

                if (myJournal.containsEntryForDate(parsedDate)) {
                    myJournal.displayEntriesForDate(parsedDate);
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