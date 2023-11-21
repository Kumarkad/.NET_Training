internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter First Number : ");
        int num1 = Convert.ToInt32(Console.ReadLine());
        
        Console.Write("Enter Second Number : ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        double sum = num1 + num2;
        double sumSqaure = sum * sum;
        Console.Write("Square of sum is : "+sumSqaure);


    }
}