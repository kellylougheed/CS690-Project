namespace Journal.Tests;

using Journal;

public class EntryTest
{

    Entry e;

    public EntryTest() {
        DateTime date = new DateTime(2000, 01, 01);
        List<string> items = new List<string>{"friends", "family"};
        e = new Entry(date, items);
    }

    [Fact]
    public void Test_Entry_Construction()
    {
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