using System.Text;
using Extreme.Mathematics;
using Extreme.Mathematics.LinearAlgebra;
using Extreme.Mathematics.Optimization;
public class av3_1
{
    public class Edge
    {
        public int leftvertex;
        public int rightvertex;
        public double weight;

        public Edge(int leftvertexs, int rightvertexs , double weights)
        {
            this.leftvertex =leftvertexs;
            this.rightvertex = rightvertexs;
            this.weight = weights;
        }

    }

public class Graph 
{
    public int vertex;
    public List<Edge> edges;

    public Graph(int qntdevertices)
    {
        this.vertex = qntdevertices;
        this.edges = new List<Edge>();
    }

    public void AddEdge(int leftvertexs, int rightvertexs , double weights)
    {
        edges.Add(new Edge(leftvertexs,rightvertexs ,weights));
    }
}
static void Main (string[] args)
{
    Extreme.License.Verify("62206-21901-59158-30313");

    string[] lines = File.ReadAllLines("D:\\Facul\\AV3\\1\\Av3_1.txt");
    Graph graph = new Graph(lines.Count());

    string [] separatedlines;
    for(int i = 0; i< lines.Count();i++)
    {
        separatedlines = lines[i].Split(' ');
        for(int j = 0;j<separatedlines.Count();j++)
        {
            if(double.Parse(separatedlines[j])!=0)
            {
                graph.AddEdge(i,j,double.Parse(separatedlines[j]));
            }
        }
    }

    var lp = new LinearProgram();
    for(int i = 0;i<graph.vertex;i++)
    {
        lp.AddBinaryVariable(i.ToString(),-1.0);
    }
    int d = 0;
    foreach(Edge edge in graph.edges)
    {   
        double[] constraintVector = new double[graph.vertex];
        constraintVector[edge.leftvertex]=1.0;
        constraintVector[edge.rightvertex] = 1.0;
        lp.AddLinearConstraint(d.ToString(),constraintVector,0.0,1.0);
        d++;
    }
    Console.WriteLine(lp.Solve());
}
}