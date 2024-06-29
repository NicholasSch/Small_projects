using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;
using System.Data;

public class av2extramat2()
{
    static void Main(string[] args)
    {
        Console.WriteLine("Type the quantity of lines of the matrix");
        int rows = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Type the quantity of colunms of the matrix");
        int cols = Int32.Parse(Console.ReadLine());
        int[,] matrix = new int[rows, cols];
        for (int i = 0;i<matrix.GetLongLength(0);i++)
        {
            for (int j = 0;j<matrix.GetLength(1); j++)
            {
                Console.WriteLine("Type the lement from line " + i + " and colunm " + j );
                matrix[i,j] = (Int32.Parse(Console.ReadLine()));
            }
        }

        int[,] op1 (int i,int j)
        {
            int[,] op1matrix = new int[matrix.GetLength(0),matrix.GetLength(1)];
            for (int k = 0;k<matrix.GetLength(0);k++)
            {
                if (k==i)
                {
                    for (int d = 0;d<matrix.GetLength(1);d++)
                    {
                        op1matrix[i,d] = matrix[j,d];
                    }
                }
                else if (k==j)
                {
                    for (int d = 0;d<matrix.GetLength(1);d++)
                    {
                        op1matrix[j,d] = matrix[i,d];
                    }
                }
                else
                {
                    for (int d = 0;d<matrix.GetLength(1);d++)
                    {
                        op1matrix[k,d] = matrix[k,d];
                    }
                }
            }
            return op1matrix;
        }

         int[,] op2 (int i,int k)
        {
            int[,] op2matrix = new int[matrix.GetLength(0),matrix.GetLength(1)];
            for (int j = 0;j<matrix.GetLength(0);j++)
            {
                if (j==i)
                {
                    for (int d = 0;d<matrix.GetLength(1);d++)
                    {
                        op2matrix[j,d] = matrix[j,d]*k;
                    }
                }
                else
                {
                    for (int d = 0;d<matrix.GetLength(1);d++)
                    {
                        op2matrix[j,d] = matrix[j,d];
                    }
                }
            }
            return op2matrix;
        }

        int[,] op3 (int i,int j,int k)
        {
            int[,] op3matrix = new int[matrix.GetLength(0),matrix.GetLength(1)];
            for (int z = 0;z<matrix.GetLength(0);z++)
            {
                if (z==i)
                {
                    for (int d = 0;d<matrix.GetLength(1);d++)
                    {
                        op3matrix[i,d] = matrix[i,d]+matrix[j,d]*k;
                    }
                }
                else
                {
                    for (int d = 0;d<matrix.GetLength(1);d++)
                    {
                        op3matrix[z,d] = matrix[z,d];
                    }
                }
            }
            return op3matrix;
        }

        void printmatrix (int[,] printmatrix)
        {
            for(int i = 0;i<printmatrix.GetLength(0);i++)
            {
                for(int j = 0;j<printmatrix.GetLength(1);j++)
                {
                    Console.Write(printmatrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        int ope = 1;

        while (ope != 0)
        {
            Console.WriteLine ("Select between operation 1 2 or 3 or type 0 to end");
            ope = Int32.Parse(Console.ReadLine());
            if (ope == 1)
            {
            Console.WriteLine("First line to swap, starting from 0");
            int trocada1 = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Second line to swap, starting from 0");
            int trocada2 = Int32.Parse(Console.ReadLine());
            Console.WriteLine ("Orignal matrix");
            printmatrix(matrix);
            Console.WriteLine ();
            Console.WriteLine ("OP1 matrix");
            printmatrix(op1(trocada1,trocada2));
            }
            else if (ope == 2)
            {
            Console.WriteLine("Line to be multiplyed, starting from 0");
            int multiplicada1 = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Constant to multiply for");
            int constante1 = Int32.Parse(Console.ReadLine());
            Console.WriteLine ("Orignal matrix");
            printmatrix(matrix);
            Console.WriteLine ();
            Console.WriteLine ("OP2 matrix");
            printmatrix(op2(multiplicada1,constante1));
            }
            else if (ope == 3)
            {
            Console.WriteLine("Line to operate, starting from 0");
            int trocadai = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Line to be added to the first multiplyed, starting from 0");
            int trocadaj = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Constant to multiply for");
            int constantek = Int32.Parse(Console.ReadLine());
            Console.WriteLine ("Orignal matrix");
            printmatrix(matrix);
            Console.WriteLine ();
            Console.WriteLine ("OP3 matrix");
            printmatrix(op3(trocadai,trocadaj,constantek));
            }
        }
    }
}