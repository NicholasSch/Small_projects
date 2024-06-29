using System.Security.Cryptography.X509Certificates;

public class fib
{
    public static int fibo (int n)
    {
        int num1 = 0;
        int num2 = 1;
        int aux;
        for (int i = 2;i <= n;i++)
        {
            aux = num2;
            num2 = num1 +num2;
            num1 = aux;
        }
        return num2;
    }
    static void Main (string[] args)
    {
        Console.WriteLine(fibo (6));
    }
}
