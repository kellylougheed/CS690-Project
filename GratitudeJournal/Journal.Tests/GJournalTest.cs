namespace Journal.Tests;

using Journal;

public class GJournalTest
{

    GJournal testJournal;

    public GJournalTest() {
        testJournal = new GJournal("test-journal.txt");
    }

    [Fact]
    public void Test_GJournal_AddInputToFiles()
    {
        List<string> items = new List<string>{"family", "friends"};
        testJournal.addInputToFile(new DateTime(2000, 01, 01), items);

        string fileContents = File.ReadAllText("test-journal.txt");

        Assert.Contains("1/1/2000", fileContents);
        Assert.Contains("friends", fileContents);
        Assert.Contains("family", fileContents);
    }

    [Fact]
    public void Test_GJournal_AddInputToEntries()
    {
        testJournal.entries = new List<Entry>();
        List<string> items = new List<string>{"family", "friends"};
        testJournal.addInputToEntries(new DateTime(2000, 01, 01), items);

        Assert.True(testJournal.entries[0].date.Equals(new DateTime(2000, 01, 01)));
        Assert.Contains("family", testJournal.entries[0].items);
        Assert.Contains("friends", testJournal.entries[0].items);
    }

    [Fact]
    public void Test_GJournal_Update()
    {
        testJournal.entries = new List<Entry>();
        List<string> items = new List<string>{"family", "friends"};
        testJournal.addInputToEntries(new DateTime(2000, 01, 01), items);

        Assert.True(testJournal.entries[0].date.Equals(new DateTime(2000, 01, 01)));
        Assert.Contains("family", testJournal.entries[0].items);
        Assert.Contains("friends", testJournal.entries[0].items);

        string fileContents = File.ReadAllText("test-journal.txt");

        Assert.Contains("1/1/2000", fileContents);
        Assert.Contains("friends", fileContents);
        Assert.Contains("family", fileContents);
    }

    [Fact]
    public void Test_GJournal_ConvertFileToEntries()
    {
        testJournal.entries = new List<Entry>();
        File.AppendAllText("test-journal.txt", "1/1/2000 12:00:00 AM||family|friends" + Environment.NewLine);

        testJournal.convertFileToEntries();

        Assert.True(testJournal.entries[0].date.Equals(new DateTime(2000, 01, 01)));
        Assert.Contains("family", testJournal.entries[0].items);
        Assert.Contains("friends", testJournal.entries[0].items);

        Assert.False(testJournal.entries[0].date.Equals(new DateTime(2001, 01, 01)));
    }

    [Fact]
    public void Test_GJournal_ContainsEntryForDate()
    {
        testJournal.entries = new List<Entry>();
        List<string> items = new List<string>{"family", "friends"};
        testJournal.addInputToEntries(new DateTime(2000, 01, 01), items);

        Assert.True(testJournal.containsEntryForDate(new DateTime(2000, 01, 01)));
        Assert.False(testJournal.containsEntryForDate(new DateTime(2001, 01, 01)));
    }

    [Fact]
    public void Test_GJournal_GetEntryForDate()
    {
        testJournal.entries = new List<Entry>();
        List<string> items = new List<string>{"family", "friends"};
        testJournal.addInputToEntries(new DateTime(2000, 01, 01), items);

        Assert.True(testJournal.entries[0].date.Equals(new DateTime(2000, 01, 01)));
        Assert.Contains("family", testJournal.entries[0].items);
        Assert.Contains("friends", testJournal.entries[0].items);
    }

    [Fact]
    public void Test_GJournal_DisplayEntriesForDate()
    {
        testJournal.entries = new List<Entry>();
        List<string> items = new List<string>{"family", "friends"};
        testJournal.addInputToEntries(new DateTime(2000, 01, 01), items);

        Assert.True(testJournal.displayEntriesForDate(new DateTime(2000, 01, 01)));
        Assert.False(testJournal.displayEntriesForDate(new DateTime(2001, 01, 01)));
    }
      
}