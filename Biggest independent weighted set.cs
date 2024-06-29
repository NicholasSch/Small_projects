using System;
using System.Collections.Generic;
using System.IO;
using Google.OrTools.LinearSolver;

public class av3_1
{
    public class Edge
    {
        public int leftvertex;
        public int rightvertex;
        public double weight;

        public Edge(int leftvertex, int rightvertex, double weight)
        {
            this.leftvertex = leftvertex;
            this.rightvertex = rightvertex;
            this.weight = weight;
        }
    }

    public class Graph
    {
        public int vertex;
        public List<Edge> edges;

        public Graph(int numVertices)
        {
            this.vertex = numVertices;
            this.edges = new List<Edge>();
        }

        public void AddEdge(int leftvertex, int rightvertex, double weight)
        {
            edges.Add(new Edge(leftvertex, rightvertex, weight));
        }
    }

    static void Main(string[] args)
    {
        // Change input file location to file location on your pc
        // First line contain number of vertexes 
        // Other lines contain the adjacency matrix
        string[] lines = File.ReadAllLines("D:\\Facul\\VSCODE\\av3grafos\\entrada.txt");
        int vertexes = int.Parse(lines[0]);
        Graph graph = new Graph(vertexes);

        string[] separatedlines;
        for (int i = 1; i < vertexes; i++)
        {
            separatedlines = lines[i].Split(' ');
            for (int j = 0; j < vertexes; j++)
            {
                if (double.Parse(separatedlines[j]) != 0)
                {
                    graph.AddEdge(i-1, j, double.Parse(separatedlines[j]));
                }
            }
        }

        separatedlines = lines[vertexes+1].Split(' ');
        double[] weights = new double[vertexes];
        for(int i = 0;i<vertexes;i++)
        {
            weights[i]=double.Parse(separatedlines[i]);
        }


        Solver solver = Solver.CreateSolver("SCIP");

        Variable[] vars = new Variable[graph.vertex];
        for (int i = 0; i < graph.vertex; i++)
        {
            vars[i] = solver.MakeBoolVar(i.ToString());
        }

        foreach (Edge edge in graph.edges)
        {
            LinearExpr constraint = vars[edge.leftvertex] + vars[edge.rightvertex];
            solver.Add(constraint <= 1.0);
        }

        // Objective: Maximize sum of variables (equivalent to minimizing -1.0 * sum of variables)
        Objective objective = solver.Objective();
        int d = 0;
        foreach (var variable in vars)
        {
            objective.SetCoefficient(variable, weights[d]);
            d++;
        }
        objective.SetMaximization();

        Solver.ResultStatus resultStatus = solver.Solve();

        double nmrdeindep = 0;
        if (resultStatus == Solver.ResultStatus.OPTIMAL)
        {
            for (int i = 0; i < graph.vertex; i++)
            {
                Console.WriteLine($"Variavel {i+1} = {vars[i].SolutionValue()}");
                if(vars[i].SolutionValue()!=0)
                {
                    nmrdeindep+=weights[i];
                }
            }
            Console.WriteLine($"Biggest independent weighted set {nmrdeindep}");
        }

        else
        {
            Console.WriteLine("Result not possible");
        }
    }
}
