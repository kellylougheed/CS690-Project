namespace Journal;

using Spectre.Console;
using System.IO;
using System;
using System.Globalization;

class ConsoleUI {

    IdeaGenerator ig = new IdeaGenerator("idea-database.txt");
    GJournal myJournal = new GJournal("gratitude-journal.txt");
    
    public void showMainMenu() {
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

    public void AddEntry() {
        
        DateTime date = DateTime.Today;
        List<string> items = new List<string>();

        Console.WriteLine("\n✨ Gratitude Entry for " + formattedDate(date));

        string addSelection = "Add Another";
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

            // Use today's date or enter another date
            string dateSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(new[] {
                        "Use Today's Date", "Enter Another Date"
                    }));

            if (dateSelection == "Enter Another Date") {
                date = getValidDate();  
            }

            GJournal myJournal = new GJournal("gratitude-journal.txt");
            myJournal.update(date, items);
            myJournal.printEntry(date);
            Console.WriteLine("✅ Saved.\n");
        }
        
    }

     public void ViewEntries() {

        // Need this or it won't recognize the last entry added
        myJournal.convertFileToEntries();

        myJournal.displayEntries();

        var viewSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Enter Date to View", "Exit", 
                }));

        while (viewSelection == "Enter Date to View") {

            DateTime date = getValidDate();
            if (myJournal.containsEntryForDate(date)) {
                myJournal.displayEntriesForDate(date);
            } else {
                Console.WriteLine("Sorry, no entries for that date.\n");
            }

            viewSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Enter Date to View", "Exit", 
                }));
        }
    }

     public void GetIdeas() {
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

    private string formattedDate(DateTime date) {
        string strDate = date.ToString(new CultureInfo("en-us"));
        string dateOnly = strDate.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
        return dateOnly;
    }

    private DateTime getValidDate() {
        // https://www.bytehide.com/blog/string-to-datetime-csharp
        
        DateTime date = DateTime.Today;
        bool isValid = false;

        while (!isValid) {
            Console.Write("Enter a date in mm/dd/yyyy format: ");
            string stringDate = Console.ReadLine();

            isValid = DateTime.TryParse(stringDate, out date);

            if (!isValid) {
                Console.WriteLine("Invalid date. Try again.");
            }
        }
        return date;
    }
}