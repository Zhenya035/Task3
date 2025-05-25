namespace Task3;

public class Game(List<Dice> dices)
{
    private List<Dice> Dices { get;} = dices;

    public void Run()
    {
        MakeFirstMove();
    }

    private void MakeFirstMove()
    {
        Console.WriteLine("Let's determine who makes the first move.");
        Console.WriteLine("I selected a random value in the range 0..1 ");

        var signedData = GenerateInterval.Generate(0, 1);
        Console.WriteLine($"HMAC = {signedData.Hmac}");
        
        var isSelected = false;
        var choice = string.Empty;
        
        while (!isSelected)
        {
            isSelected = true;
            Console.WriteLine("Try to guess my selection.");
            Console.WriteLine("0 - 0");
            Console.WriteLine("1 - 1");
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");

            choice = Console.ReadLine();
            
            if(choice == "X")
                return;
            else if(choice == "?")
                return;
            else if (choice != "0" && choice != "1")
            {
                Console.WriteLine("Incorrect value");
                isSelected = false;
            }
        }

        Console.WriteLine($"Your selection: {choice}");
        Console.WriteLine($"My selection: {signedData.Number} (Key: {BitConverter.ToString(signedData.SecretKey).Replace("-", "")})");
    }
}