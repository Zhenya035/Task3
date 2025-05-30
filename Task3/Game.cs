namespace Task3;

public class Game(List<Dice> dices)
{
    public void Run()
    {
        Console.WriteLine("Let's determine who makes the first move.");
        var signedData = GenerateAndPrintHmac(0, 1);
        var choice = GetUserChoice(["0", "1"], "Try to guess my selection.");
        
        Console.Write($"My selection: {signedData.Number}");
        PrintKey(signedData.SecretKey);

        var response = signedData.Number != int.Parse(choice)
            ?MakeMove(signedData)
            :MakeMove(signedData, true);

        IdentifyWinner(response);
    }

    private SelectedDice MakeMove(SignedData signedData, bool isUserFirst = false)
    {
        Console.WriteLine(isUserFirst ? "You make the first move." : "I make the first move.");
        var response = new SelectedDice();

        if (isUserFirst)
        {
            response.UserDice = GetUserDiceChoice();
            Console.WriteLine($"Your choice: {response.UserDice}");
            response.ComputerDice = GenerateComputerDice(signedData, response.UserDice);
            Console.Write($"My selection: {response.ComputerDice}");
            PrintKey(signedData.SecretKey);
        }
        else
        {
            response.ComputerDice = GenerateComputerDice(signedData);
            Console.Write($"My selection: {response.ComputerDice}");
            PrintKey(signedData.SecretKey);
            response.UserDice = GetUserDiceChoice(response.ComputerDice);
            Console.WriteLine($"Your choice: {response.UserDice}");
        }
        return response;
    }

    private Dice GetUserDiceChoice()
    {
        var availableDices = dices.Select(d => d.ToString()).ToList(); 
        return dices[int.Parse(GetUserChoice(availableDices, "Choose your dice:"))];
    }
    
    private Dice GetUserDiceChoice(Dice? computerDice)
    {
        var availableDices = dices.Where(d => d != computerDice).ToList();
        var options = availableDices.Select(d => d.ToString()).ToList();
        var userChoice = availableDices[int.Parse(GetUserChoice(options, "Choose your dice"))];
            
        var realIndex = dices.FindIndex(d => d == userChoice);
        
        return dices[realIndex];
    }

    private Dice GenerateComputerDice(SignedData signedData)
    {
        signedData = GenerateAndPrintHmac(0, dices.Count - 1);
        return dices[signedData.Number];
    }
    
    private Dice GenerateComputerDice(SignedData signedData, Dice? userResponse)
    {
        var availableDices = dices.Where(d => d != userResponse).ToList();
        
        signedData = GenerateAndPrintHmac(0, availableDices.Count - 1);
            
        var realIndex = dices.FindIndex(d => d == availableDices[signedData.Number]);
        
        return dices[realIndex];
    }

    private int MakeRoll(SelectedDice selectedDices, bool isUserMove)
    {
        Console.WriteLine(isUserMove ? "It's time for your roll." : "It's time for my roll.");
        var signedData = GenerateAndPrintHmac(0, 5);
        var choice = GetUserChoice(new List<string> { "0", "1", "2", "3", "4", "5" }, "Add your number modulo 6.");
        var number = (signedData.Number + int.Parse(choice)) % 6;
        Console.WriteLine($"My number is {signedData.Number}");
        PrintKey(signedData.SecretKey);
        
        Console.WriteLine(
            $"The fair number generation result is {signedData.Number} + {choice} = {number} (mod 6).");
        
        Console.WriteLine(isUserMove
            ? $"Your roll result is {selectedDices.UserDice.Sides[number]}"
            : $"My roll result is {selectedDices.ComputerDice.Sides[number]}");

        return isUserMove
            ? selectedDices.UserDice.Sides[number]
            : selectedDices.ComputerDice.Sides[number];
    }

    private void IdentifyWinner(SelectedDice response)
    {
        var computerNumber = MakeRoll(response, false);
        var userNumber = MakeRoll(response, true);
        
        if(userNumber > computerNumber) Console.WriteLine($"You win ({userNumber} > {computerNumber}).");
        else if(userNumber == computerNumber) Console.WriteLine($"Draw ({userNumber} = {computerNumber}).");
        else if(userNumber < computerNumber) Console.WriteLine($"You lost ({userNumber} < {computerNumber}).");
    }

    private string GetUserChoice(List<string> options, string message)
    {
        while (true)
        {
            List<string> choices = [];
            
            Console.WriteLine(message);
            for (var i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i} - {options[i]}");
                choices.Add(i.ToString());
            }
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");
            
            Console.Write($"Your selection: ");
            var choice = Console.ReadLine();
            if(choice.ToUpper() == "X") Environment.Exit(0);
            else if(choice == "?") Console.WriteLine(1);
            else if(choices.Contains(choice)) return choice;
            else Console.WriteLine("Invalid choice");
        }
    }

    private SignedData GenerateAndPrintHmac(int min, int max)
    {
        Console.Write($"I selected a random value in the range {min}..{max}");
        var signedData = GenerateInterval.Generate(min, max);
        Console.WriteLine($"(Hmac = {signedData.Hmac})");
        return signedData;
    }
    
    private void PrintKey(byte[] key) => Console.WriteLine($"(KEY={BitConverter.ToString(key).Replace("-", string.Empty)})");
}