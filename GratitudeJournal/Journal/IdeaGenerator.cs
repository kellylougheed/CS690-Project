namespace Journal;

class IdeaGenerator {
    public string filePath { get; }
    public string[] ideas { get; }

    public IdeaGenerator(string filePath) {
        this.filePath = filePath;
        string contents = File.ReadAllText(filePath);
        this.ideas = contents.Split("\n", StringSplitOptions.RemoveEmptyEntries);
    }

    public string getRandomIdea() {
        Random random = new Random();
        int index = random.Next(0, ideas.Length);
        return ideas[index];
    }
}