namespace Task3;

public class Dice(List<string> sides)
{
    public List<int> Sides { get; } = sides.Select(int.Parse).ToList();

    public override string ToString()
    {
        var response = string.Empty;
        response = "[";
        response += string.Join(", ", Sides);
        response += "]";
        return response;
    }
}