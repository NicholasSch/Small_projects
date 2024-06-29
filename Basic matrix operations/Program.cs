using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;
public class av2ExtraMat
{



static void Main(string[] args)
{

int[,] matrix =
{
    {5,3,1,4,7},
    {5,1,9,3,5},
    {54,31,12,43,71},
    {15,13,49,23,51},
    {1,2,7,5,3}
};

int[,] transpose()
{
    int[,] transposedmatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
    for (int i = 0;i<transposedmatrix.GetLength(0);i++)
    {
        for (int j = 0;j<transposedmatrix.GetLength(1);j++)
        {
            transposedmatrix[j,i]=matrix[i,j];
        }
    }
    return transposedmatrix;
}

int[,] multiplymatrix (int K)
{
    int[,] Multipliedmatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
    for (int i = 0;i<Multipliedmatrix.GetLength(0);i++)
    {
        for (int j = 0;j<Multipliedmatrix.GetLength(1);j++)
        {
            Multipliedmatrix[i,j]= matrix[i,j]*K;
        }
    }
    return Multipliedmatrix;
}

void printmatrix (int[,] matrixprint)
{
    for (int i = 0;i<matrixprint.GetLength(0);i++)
    {
        for (int j = 0;j<matrixprint.GetLength(1);j++)
        {
            Console.Write(matrixprint[i,j] + " ");
        }
        Console.WriteLine();
    }
}

int[,] multiplyaddedmatrix (int K)
{
    int[,] Multiplieaddeddmatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
    for (int i = 0;i<Multiplieaddeddmatrix.GetLength(0);i++)
    {
        for (int j = 0;j<Multiplieaddeddmatrix.GetLength(1);j++)
        {
            Multiplieaddeddmatrix[i,j]= matrix[i,j]+ K*matrix[j,i];
        }
    }
    return Multiplieaddeddmatrix;
}


Console.WriteLine("Original matrix");
printmatrix(matrix);

Console.WriteLine("Transpost matrix");
printmatrix(transpose());

Console.WriteLine("Multiplyed matryx");
printmatrix(multiplymatrix(5));

Console.WriteLine("Original matrix sumed with the transpost of it multiplyed");
printmatrix(multiplyaddedmatrix(2));


}
}