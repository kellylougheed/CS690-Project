namespace Journal;

using System.Globalization;

public class Entry {
    public DateTime date { get; }
    public List<string> items { get; }

    public Entry(DateTime date, List<string> items) {
        this.date = date;
        this.items = items;
    }

    public string formattedDate() {
        string strDate = date.ToString(new CultureInfo("en-us"));
        string formattedDateNoTime = strDate.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
        return formattedDateNoTime;
    }

    public override string ToString() {
        string result = "\n✨ Gratitude Entry for " + formattedDate();
        foreach (string item in items) {
            result += "\n" + item;
        }
        return result + "\n";
    }
}