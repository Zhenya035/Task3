namespace Task3;

public class HelpMenu
{
    public static void ShowHelpMenu(List<Dice> dices)
    {
        Console.WriteLine("Probability of winning for each pair of dice:");

        var probabilities = CalculateWinProbabilities(dices);
    
        Console.WriteLine("Dice 1 vs Dice 2 | Winning Probability Dice 1");
        Console.WriteLine("--------------------------------------------");
        foreach (var pair in probabilities)
        {
            Console.WriteLine($"{pair.Key.Item1} vs {pair.Key.Item2} | {pair.Value:P}");
        }
    }
    
    private static Dictionary<(Dice, Dice), double> CalculateWinProbabilities(List<Dice> dices)
    {
        var probabilities = new Dictionary<(Dice, Dice), double>();

        foreach (var dice1 in dices)
        {
            foreach (var dice2 in dices)
            {
                if (dice1 == dice2) continue;

                var totalRolls = dice1.Sides.Count * dice2.Sides.Count;

                var wins = (from side1 in dice1.Sides from side2 in dice2.Sides where side1 > side2 select side1).Count();

                probabilities[(dice1, dice2)] = (double)wins / totalRolls;
            }
        }

        return probabilities;
    }

}