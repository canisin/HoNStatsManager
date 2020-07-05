namespace HonStatsManager
{
    internal class Hero
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Hero()
        {
        }

        public Hero(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}