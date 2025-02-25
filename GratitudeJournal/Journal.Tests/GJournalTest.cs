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
        testJournal.addInputToFile()
        Assert.True(e.items.Contains("friends"));
        Assert.True(e.items.Contains("family"));
    }

    [Fact]
    public void Test_Entry_FormattedDate()
    {
        Assert.Equal("1/1/2000", e.formattedDate());
    }

    [Fact]
    public void Test_Entry_ToString()
    {
        Assert.True(e.ToString().Contains("Gratitude Entry"));
        Assert.True(e.ToString().Contains("1/1/2000"));
        Assert.True(e.ToString().Contains("friends"));
        Assert.True(e.ToString().Contains("family"));
    }
      
}