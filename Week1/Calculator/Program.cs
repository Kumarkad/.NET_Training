internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Calculator");
        Console.Write("Enter the first number : ");
        int num1=Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the second number : ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"Options : \n1. Add\n2. Subtraction\n3. Multiply\n4. Division\n5. Modulus");
        Console.Write("Enter the option (1/2/3/4/5): ");
        int option = Convert.ToInt32(Console.ReadLine());
        double result = 0;
        switch (option)
        {
            case 1:
                result = num1 + num2;
                break;
            case 2:
                result = num1 - num2;
                break;
            case 3:
                result = num1 * num2;
                break;
            case 4:
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                else
                {
                    Console.WriteLine("Error : Division by zero !!!");
                }
                break;
            case 5:
                if (num2 != 0)
                {
                    result = num1 % num2;
                }
                else
                {
                    Console.WriteLine("Error : Modulus by zero !!!");
                }
                break;
            default :
                Console.WriteLine("Invalid Option !!!");
                return;
        }
        Console.WriteLine($"Result : {result}");


    }
}