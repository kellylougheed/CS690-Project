namespace Journal;

class GJournal {
    public string filePath { get; }
    public List<Entry> entries { get; }

    public GJournal(string filePath) {
        this.filePath = filePath;

        if (!File.Exists(filePath)) {
                File.Create(filePath).Close();
        }

        entries = new List<Entry>();

        convertFileToEntries();
    }

    private void convertFileToEntries() {
        string contents = File.ReadAllText(filePath);
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

    public void displayEntriesForDate(DateTime dateToDisplay) {
        if (containsEntryForDate(dateToDisplay)) {
            Entry e = getEntryForDate(dateToDisplay);
            Console.WriteLine(e);
        } else {
            Console.WriteLine("Sorry, no entries to display.");
        }
    }

    
}