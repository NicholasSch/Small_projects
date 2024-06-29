using System;
using Extreme.Mathematics;
using Extreme.Mathematics.LinearAlgebra;
using Extreme.Mathematics.Optimization;


namespace Extreme.Numerics.QuickStart.CSharp
{
    class LinearProgramming
    {
        //função para ver se ha algum elemnto quebrado.
        static (bool,double,double) temquebrado (DenseVector<double> p)
        {
            double i = 0;
             foreach(double doub in p)
            {
            if ( Math.Round(doub,10) % 1 != 0)
            {
                return (true,doub,i);
            };
            i++;
            }
            return (false,0.0,0.0);
        }

        //função para fazer branch and bound.
        static (double,DenseVector<double>) Branchandbound (DenseVector<double> p, double[] c, DenseMatrix<double>? A, DenseVector<double>? b, double melhorvalorfinal, DenseVector<double> melhorvalorvar)
        {
            //salva o resultado do simplex relaxado da funcão objetivo
            LinearProgram lp3 = new LinearProgram(Vector.Create(c), A, b, 0);
            p = lp3.Solve();
            for(int i = 0;i<p.Length;i++)
            {
                p[i] = Math.Round(p[i],10);
            }

            //verifica se ha algum elemento não inteiro
            var quebra = temquebrado(p);
            //se ha elemento não inteiro
            if (quebra.Item1)
            {   
            double nvv = quebra.Item3;
            //chamaremos recursivamente a função com a matriz e a array de condições atualizada com a nova condição
            double nvc1 = Math.Floor(quebra.Item2);
            double nvc2 = Math.Ceiling(quebra.Item2);
            //cria novo vetor no lugar de b com o novo elemento da nova restrição;
            var d = Vector.Create(b.Length + 1, i =>
            {
                if (i == b.Length) return nvc1; // Right-hand side for W constraint
                if (i < b.Length) return b[i]; // Right-hand side for upper bounds
                return 0.0;
            });
            //cria nova matriz no lugar de A com o novo elemento da nova restrição;
                var e = Matrix.Create(A.RowCount+1, A.ColumnCount, (i, j) =>
            {
                if (i == A.RowCount) return (j == nvv) ? 1.0 : 0.0; // Right-hand side for W constraint
                if (i < A.RowCount) return A[i,j]; // Right-hand side for upper bounds
                return 0.0;
            });
            //cria novo vetor no lugar de b com o novo elemento da nova restrição;
                       var F = Vector.Create(b.Length + 1, i =>
            {
                if (i == b.Length) return -nvc2; // Right-hand side for W constraint
                if (i < b.Length) return b[i]; // Right-hand side for upper bounds
                return 0.0;
            });
            //cria nova matriz no lugar de A com o novo elemento da nova restrição;
                var G = Matrix.Create(A.RowCount+1, A.ColumnCount, (i, j) =>
            {
                if (i == A.RowCount) return (j == nvv) ? -1.0 : 0.0; // Right-hand side for W constraint
                if (i < A.RowCount) return A[i,j]; // Right-hand side for upper bounds
                return 0.0;
            });
                //chamamos a recursão com as novas condições e atribuimos os resultados em variaveis
                var result1 = Branchandbound(p,c,e,d,melhorvalorfinal,melhorvalorvar);
                var result2 = Branchandbound(p,c,G,F,melhorvalorfinal,melhorvalorvar);

                //confere se os resultados tem valor maior que a solução atual (os vetores resultados sempre contem apenas valores inteiros)
                if(result1.Item1>=melhorvalorfinal)
                {
                    Console.WriteLine("branch de " + melhorvalorfinal + " valor final apagado por ter lucro menor que " +result1.Item1);
                    melhorvalorvar = result1.Item2;
                    melhorvalorfinal = result1.Item1;
                }
                if(result2.Item1>=melhorvalorfinal)
                {
                    Console.WriteLine("branch de " + melhorvalorfinal + "valor final apagado por ter lucro menor que " + result2.Item1);
                    melhorvalorvar = result2.Item2;
                    melhorvalorfinal = result2.Item1;
                }
            }
            // se so há inteiros na solução imprime a solução parcial e retorna os valores
            else
            {
                Console.WriteLine("solução parcial " + p);
                var valorotimoatual = Math.Round(-lp3.OptimalValue,10);
                Console.WriteLine("Optimal value: {0:F1}", valorotimoatual);
                return (valorotimoatual,p);
            }
            
            //retorna resultado final
            return(melhorvalorfinal,melhorvalorvar);
        }

        static void Main(string[] args)
        {
            // Caminho do arquivo de entrada
            string caminhoArquivo = "D:\\Facul\\Mochila\\entrada.txt";

            // Lê todas as linhas do arquivo
            string[] linhas = File.ReadAllLines(caminhoArquivo);

            // Lê a primeira linha para obter o número de variáveis
            int n = int.Parse(linhas[0]);

            // Lê o vetor c
            double[] c = Array.ConvertAll(linhas[1].Split(' '), double.Parse);

            // Lê o vetor w
            double[] w = Array.ConvertAll(linhas[2].Split(' '), double.Parse);

            // Lê o vetor M
            double[] M = Array.ConvertAll(linhas[3].Split(' '), double.Parse);

            // Lê o vetor m
            double[] m = Array.ConvertAll(linhas[4].Split(' '), double.Parse);

            // Lê o peso máximo W
            double W = double.Parse(linhas[5]);

            // Exibe as informações lidas do arquivo
            Console.WriteLine("Número de variáveis (n): " + n);
            Console.WriteLine("Vetor c: " + string.Join(", ", c));
            Console.WriteLine("Vetor w: " + string.Join(", ", w));
            Console.WriteLine("Vetor M: " + string.Join(", ", M));
            Console.WriteLine("Vetor m: " + string.Join(", ", m));
            Console.WriteLine("Peso máximo (W): " + W);

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = -c[i];
            }
            Extreme.License.Verify("62206-21901-59158-30313");
            // Now we construct matrix A and vector b based on the inequalities

            // The coefficients of the constraints:
            var A = Matrix.Create(2 * n + 1, n, (i, j) =>
            {
                if (i < n)
                    return (i == j) ? 1.0 : 0.0; // Coefficients for variable bounds (upper bound)
                else if (i < 2 * n)
                    return (i - n == j) ? -1.0 : 0.0; // Coefficients for variable bounds (lower bound)
                else
                    return w[j]; // Coefficients for the sum constraint
            });

            // The right-hand sides of the constraints:
            var b = Vector.Create(2 * n + 1, i =>
            {
                if (i == 2 * n) return W; // Right-hand side for W constraint
                if (i < n) return M[i]; // Right-hand side for lower bounds
                if (i < 2 * n) return -m[i-n]; // Right-hand side for upper bounds
                return 0.0; // Right-hand side for the sum constraint
            });

            Console.WriteLine("matriz de condiçoes " );
            Console.WriteLine(A);
            Console.WriteLine("array de resultados das inequalidades ");
            Console.WriteLine(b);
            // We're now ready to call the constructor.
            // The last parameter specifies the number of equality constraints.
            LinearProgram lp1 = new LinearProgram(Vector.Create(c), A, b, 0);

            // Now we can call the Solve method to run the Revised
            // Simplex algorithm:
            var x = lp1.Solve();
            Console.WriteLine("resultado do problema relaxado");
            Console.WriteLine(x);

            // The optimal value is returned by the Extremum property:
            Console.WriteLine("valor otimo: {0:F1}", -lp1.OptimalValue);
            Console.WriteLine();

            var result = Branchandbound(x,c,A,b,0.0,Vector.Create(0.0));
            if (result.Item1!=0.0)
            {
            Console.WriteLine("solução final:");
            Console.WriteLine("lucro de {0:F1}", result.Item1);
            Console.WriteLine("usando os itens {0}",result.Item2);
            }
            else
            Console.WriteLine("Solução Inviável");
        }
    }
}