using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

public class av3grafos
{

    public class Graph 
    {
        public int[] vertexes;
        public List<Edge> edges;
        
        public double[] vertexesWeights;

        public List<int>[] dependencies;


        public void addEdge(int i, int j, double weight)
        {
            edges.Add(new Edge(i, j,weight));
            dependencies[i].Add(j);
        }

        public Graph (int qntvertexes)
        {
            this.vertexes = new int[qntvertexes];
            this.edges = new List<Edge>();
            this. vertexesWeights  = new double[qntvertexes]; 
            this. dependencies = new List<int>[qntvertexes];
            for (int i = 0; i < qntvertexes; i++)
            {
                dependencies[i] = new List<int>();
            }
        }
    }
    public class Edge
    {
        public int i;
        public int j;
        public double weight;
        
        public Edge (int i, int j, double weight)
        {
            this.i = i;
            this.j = j;
            this.weight = weight;

        }
    }

    static (List<int>, bool) BFS (int raiz, Graph graph)
    {
        List<int> alldependencies = new List<int>();
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(raiz);
        while(queue.Count()>0)
        {
            int current = queue.Dequeue();
            if(!alldependencies.Contains(current))
            {
            alldependencies.Add(current);
            foreach (int vert in graph.dependencies[current])
            {   
                if(vert == raiz)
                {
                return (alldependencies,false);
                }
                queue.Enqueue(vert);
            }
            }
        }
        return (alldependencies,true);
    }
 public static IEnumerable<IEnumerable<T>> GetPowerSet<T>(List<T> list)
    {
        return from m in Enumerable.Range(0,(int)Math.Pow(2,list.Count))
               select
                   from i in Enumerable.Range(0, list.Count)
                   where (m & (1 << i)) != 0
                   select list[i];
    }

    public static List<List<int>> PowerSetOfListOfLists(List<List<int>> listOfLists)
    {
        var powerSet = GetPowerSet(listOfLists)
            .Select(subset => subset.SelectMany(innerList => innerList).ToList())
            .Where(subset => subset.Count > 0)
            .ToList();
        return powerSet;
    }
    

    static void Main (string [] args)
    {
        //Change path file for it to work
        //First line contains vertex quantity
        //Next n lines contain the adjacency graph 
        //Final line contain the weights of the vertexes
        string[] lines = File.ReadAllLines("D:\\Facul\\VSCODE\\av3parte2\\Project.txt");
        int vertexess = int.Parse(lines[0]);
        Graph graph = new Graph(vertexess);
        bool possible = true;
        string [] sepaartedlines;
        for(int i = 1;i<=vertexess;i++)
        {
            sepaartedlines = lines[i].Split(' ');
            for(int j = 0; j<vertexess;j++)
            {
                if(int.Parse(sepaartedlines[j])!= 0)
                graph.addEdge(i-1,j,double.Parse(sepaartedlines[j]));
            }
        }
        sepaartedlines = lines[vertexess+1].Split(' ');
        for(int j = 0; j<vertexess;j++)
        {
            graph.vertexesWeights[j] = double.Parse(sepaartedlines[j]);
        }


        List<List<int>> sets = new List<List<int>>();

        for(int i = 0;i<graph.vertexes.Count();i++)
        {
            var BFSresult = BFS(i,graph);
            sets.Add(BFSresult.Item1);
            if(!BFSresult.Item2)
            {
                possible=false;
                break;
            }
        }


        if (possible)
        {
            double finalsum = 0;
            var result = PowerSetOfListOfLists(sets);
            var conjuntofinal = new List<int>();
            List<int> resultWithoutDuplicates;
            for (int z = 0; z < result.Count(); z++)
            {
                double sum = 0;
                resultWithoutDuplicates = result[z].Distinct().ToList();
                foreach (int i in resultWithoutDuplicates)
                {
                    sum += graph.vertexesWeights[i];
                }
                finalsum = Math.Max(finalsum, sum);
                
                if (finalsum == sum)
                {   
                    conjuntofinal = resultWithoutDuplicates;
                }
            }     
            if(finalsum>0)
            {
            Console.Write("[");
            foreach(int p in conjuntofinal)
            {
                Console.Write(p+1 + ",");
            }
            Console.WriteLine("]");
            Console.WriteLine("Final sum " +finalsum);
            }
            else
            {
                Console.WriteLine("No project will give profit");
            }
        }
        else
        {
            Console.WriteLine("Invalid graph");
        }
    }
}
