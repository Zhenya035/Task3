namespace Task3;

public class Dice(List<string> sides)
{
    public List<int> Sides { get; } = sides.Select(int.Parse).ToList();
}