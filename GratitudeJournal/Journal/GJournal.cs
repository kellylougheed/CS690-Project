namespace Journal;

using System.Globalization;

public class GJournal {
    public string filePath { get; }
    public List<Entry> entries { get; set; }

    public GJournal(string filePath) {
        this.filePath = filePath;

        if (!File.Exists(filePath)) {
                File.Create(filePath).Close();
        }

        entries = new List<Entry>();

        convertFileToEntries();
    }

    public void convertFileToEntries() {

        // reset entries
        entries = new List<Entry>();

        string contents = File.ReadAllText(filePath); // closes file
        string[] rawEntries = contents.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        foreach (string entry in rawEntries) {
            // Get date
            string[] dateAndList = entry.Split("||", StringSplitOptions.RemoveEmptyEntries);
            string dtString = dateAndList[0];
            // Final date form
            DateTime parsedDate = DateTime.Parse(dtString).Date;

            // Get items
            List<string> listItems = dateAndList[1].Split("|", StringSplitOptions.RemoveEmptyEntries).ToList();

            if (!this.containsEntryForDate(parsedDate)) {
                this.entries.Add(new Entry(parsedDate, listItems));
            } else {
                getEntryForDate(parsedDate).items.AddRange(listItems);
            }
        }
    }

    public void update(DateTime date, List<string> items) {
        addInputToFile(date, items);
        convertFileToEntries();
    }

    // FILE FORMAT: Date||Item|Item|Item
    public void addInputToFile(DateTime date, List<string> items) {
        string contents = date.Date.ToString(new CultureInfo("en-us")) + "||";
        contents += String.Join("|", items);

        StreamWriter sw = new StreamWriter(filePath, true);
        sw.WriteLine(contents);
        sw.Close();
    }

    public void addInputToEntries(DateTime date, List<string> items) {
        entries.Add(new Entry(date, items));
    }

    public void printEntry(DateTime date) {
        if (containsEntryForDate(date)) {
            Console.WriteLine(getEntryForDate(date));
        }
    }

    public bool containsEntryForDate(DateTime dateToSearch) {
        foreach(Entry e in entries) {
            if (e.date.Equals(dateToSearch)) {
                return true;
            }
        }
        return false;
    }

    public Entry getEntryForDate(DateTime dateToSearch) {
        if (containsEntryForDate(dateToSearch)) {
            foreach(Entry e in entries) {
                if (e.date.Equals(dateToSearch)) {
                    return e;
                }
            }
        }
        return new Entry(dateToSearch, new List<string>{"No entry found"});
    }

    public void displayEntries() {
        foreach (Entry e in entries) {
            Console.WriteLine(e);
        }
    }

    public bool displayEntriesForDate(DateTime dateToDisplay) {
        if (containsEntryForDate(dateToDisplay)) {
            Entry e = getEntryForDate(dateToDisplay);
            Console.WriteLine(e);
            return true;
        } else {
            Console.WriteLine("Sorry, no entries to display.");
            return false;
        }
    }

    
}