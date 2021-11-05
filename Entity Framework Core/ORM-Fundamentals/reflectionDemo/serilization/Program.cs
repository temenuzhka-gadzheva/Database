using Newtonsoft.Json;


namespace serilization
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            player.Id = 1;
            player.Name = "Marty";
            player.Age = 21;
            player.Score = new Score();
            player.Score.Amount = 50;
            player.Score.IsWinner = false;
            player.Games = new int[] { 1, 2, 3, 4 };

            // to view better use nuget packages newtonSoft json
            string json = JsonConvert.SerializeObject(player, Formatting.Indented);

            System.Console.WriteLine(json);
        }
    }
}
