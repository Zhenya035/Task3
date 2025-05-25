namespace Task3;

class Program
{
    static void Main(string[] args)
    {
        var parsedDices = args.Select(arg => arg.Split(',').ToList()).ToList();
        if (!CheckDicesCount(parsedDices) || !CheckSidesCount(parsedDices) || !CheckValuesAreIntegers(parsedDices))
        {
            Console.WriteLine("Write, for example: dotnet run -- 1,2,3,4,5,6 1,2,3,4,5,6 1,2,3,4,5,6");
            return;
        }
        
        var dices = new List<Dice>();
        dices.AddRange(parsedDices.Select(arg => new Dice(arg)));
    }

    private static bool CheckDicesCount(List<List<string>> dices)
    {
        if(dices.Count >= 3)
            return true;
        Console.WriteLine("Specify at least three bones");
        return false;
    }
        
    private static bool CheckSidesCount(List<List<string>> dices){
        if(dices.All(dice => dice.Count== 6))
            return true;
        Console.WriteLine("Specify 6 faces in each cube");
        return false;
    }
    
    private static bool CheckValuesAreIntegers(List<List<string>> dices)
    {
        if(dices.All(dice => dice.All(num => int.TryParse(num, out _))))
            return true;
        Console.WriteLine("Just give me the numbers");
        return false;
    }
}