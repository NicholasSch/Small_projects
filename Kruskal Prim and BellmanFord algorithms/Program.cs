using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
class Program
{
//Objeto das arestas
public class Edge : IComparable<Edge>
{   
     public int Source { get; }
    public int Destination { get; }
    public double Weight { get; }

    public Edge(int source, int destination, double weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }
    
    //Função de comparação de pesos das arestas para se usar o sort
    public int CompareTo(Edge other)
    {
            return Weight.CompareTo(other.Weight);
    }
}

// classe principal do grafo e seus algoritmos
class Graph
{
    public int Vertices { get; }
    public List<Edge> Edges { get; }

    //Construtor do grafo
    public Graph(int vertices)
    {
        Vertices = vertices;
        Edges = new List<Edge>();
    }
    
    //Função de adição de arestas
    public void AddEdge(int source, int destination, double weight)
    {
        Edges.Add(new Edge(source, destination, weight));
    }

    //Classe do set disjunto (não lembro o nome em português)
    class DisjointSet
    {
    private int[] parent;

    //No construtor do objeto seta os elementos como sendo os proprios pais ja que os elementos são numerós de 0 a n
    public DisjointSet(int size)
    {
        parent = new int[size];
        for (int i = 0; i < size; i++)
        {
            parent[i] = i;
        }
    }

    //Acha o nó raiz a partir de um elemnto
    public int Find(int element)
    {
        if (parent[element] != element)
        {
            parent[element] = Find(parent[element]);
        }
        return parent[element];
    }

    //Une as sub arvores
    public void Union(int set1, int set2)
    {
        parent[Find(set1)] = Find(set2);
    }
}

    //logica principal do Kruskal
    public List<Edge> KruskalMST()
    {
        List<Edge> mst = new List<Edge>();
        DisjointSet ds = new DisjointSet(Vertices);

        //Ordena as arestas por peso
        Edges.Sort();

        //Para cada aresta
        foreach (Edge edge in Edges)
        {
            //Verifica se os dois verticés da aresta possuem o mesmo nó raiz
            int root1 = ds.Find(edge.Source);
            int root2 = ds.Find(edge.Destination);

            //Se for diferente os adiciona na arvoré e junta suas raizes
            if (root1 != root2)
            {
                mst.Add(edge);
                ds.Union(root1, root2);
            }
        }
        return mst;
    }


    public (List<int>,List<Edge>) PrimMST(int root)
    {
        //Inicia uma lista de predecessores com todos os valores como -2, uma lista de visitados vazia e uma lista pro mst vazia
        List<int> predecessors = Enumerable.Repeat(-2, Vertices).ToList();
        List<int> visited = new List<int>();
        List<Edge> mst = new List<Edge>();

        //Seta o predecessor da raiz como -1 e a adiciona na lista de visitados
        predecessors[root] = -1;
        visited.Add(root);

        //Enquanto a quantidade de visitados for menor que a quantidade de vértices
        while (visited.Count < Vertices)
        {
            //Seta valores iniciais de comparação
            Edge minEdge = null;
            double minWeight = double.PositiveInfinity;

            //Verifica todas as arestas do grafo
            foreach (Edge edge in Edges)
            {
                //Se visitados contém o Vértice inicial e não contém o verticé destino e tem um peso menor que o menor achado até agora 
                if (visited.Contains(edge.Source) && !visited.Contains(edge.Destination) && edge.Weight < minWeight)
                {
                    //seta menor como o seguinte
                    minWeight = edge.Weight;
                    minEdge = edge;
                }
            }

            //Se o menor for diferente de nulo
            if (minEdge != null)
            {
                //Define o predecessor do destino como o inicial e o adiciona a visitados
                predecessors[minEdge.Destination] = minEdge.Source;
                visited.Add(minEdge.Destination);
                mst.Add(minEdge);
            }
            //Se ha algum vértice solto break
            else
            {
                break;
            }
        }

        return (predecessors,mst);
    }

    public (List<int>,List<double>) BellmanFord(int source)
    {
        //Inicia uma lista de predecessores com todos os valores como -1, uma lista de distancias com valor inicial para cada vértice de infinito
        List<int> predecessors = Enumerable.Repeat(-1, Vertices).ToList();
        List<double> distances = Enumerable.Repeat(double.PositiveInfinity, Vertices).ToList();
        
        //Seta a distancia do nó inicial como 0 e deixa seu predecessor como -1
        distances[source] = 0;

        //Para cada vértice - 1 sendo o numero maximo de iterações necessárias
        for (int i = 0; i < Vertices - 1; i++)
        {
            //Para cada aresta
            foreach (Edge edge in Edges)
            {
                //Se a menor distancia encontrada para o vértice inicial + o peso da aresta for menor que a menor distancia encontrada para o vértice final da aresta
                if (distances[edge.Source] + edge.Weight < distances[edge.Destination])
                {
                    //Seta a nova menor distancia e o predecessor
                    distances[edge.Destination] = distances[edge.Source] + edge.Weight;
                    predecessors[edge.Destination] = edge.Source;
                }
            }
        }
        return (predecessors, distances);
    }
}


static void Main(string[] args)
{

    // Leitura do arquivo kruskal.txt
    string[] lines = File.ReadAllLines("D:\\Facul\\Grafos\\kruskal.txt");
    int qntvertices = int.Parse(lines[0]);

    //Cria o grafo com a quantidade de arestas
    Graph graph = new Graph(qntvertices);

    //Lê a qntdevertices linhas do texto e cria as arestas de acordo com os valores
    for (int i = 0; i < qntvertices; i++)
    {
        //Cria uma array com os valores da linha separando ops elementos a cada espaço
        string[] values = lines[i+1].Split(' ');
        for (int j = 0; j < qntvertices; j++)
        {
            //Converte o texto para um double
            double weight = double.Parse(values[j]);
            //Se o peso for diferente de 0 cria-se uma aresta
            if (weight != 0)
            {
                graph.AddEdge(i, j, weight);
            }
        }
    }

    //A execução do algoritmo de Kruskal retorna ujma lista com as arestas do mst
    List<Edge> kruskalMST = graph.KruskalMST();

    Console.WriteLine("Resultado de Kruskel");
    //Se ha qntvertices-1 arestas adicionada então é conexo
    if (kruskalMST.Count == qntvertices - 1)
    {
        Console.WriteLine("VERDADEIRO");
        //Imprime as arestas adcionadas
        foreach (var edge in kruskalMST)
        {
            Console.WriteLine($"({edge.Source}, {edge.Destination}): {edge.Weight}");
        }
    }
     else
    {
        Console.WriteLine("FALSO");
    }


    // Leitura do arquivo prim.txt
    lines = File.ReadAllLines("D:\\Facul\\Grafos\\prim.txt");
    qntvertices = int.Parse(lines[0]);
    graph = new Graph(qntvertices);

    //Lê a qntdevertices linhas do texto e cria as arestas de acordo com os valores
    for (int i = 0; i < qntvertices; i++)
    {
        //Cria uma array com os valores da linha separando ops elementos a cada espaço
        string[] values = lines[i+1].Split(' ');
        for (int j = 0; j < qntvertices; j++)
        {
            //Converte o texto para um double
            double weight = double.Parse(values[j]);
            //Se o peso for diferente de 0 cria-se uma aresta
            if (weight != 0)
            {
                graph.AddEdge(i, j, weight);
            }
        }
    }

    //Define o root como o elemento da ultima linha
    int root = int.Parse(lines[qntvertices + 1]);

    // Execução do algoritmo de Prim
    var resultprim = graph.PrimMST(root);
    List<int> primpredecessors = resultprim.Item1;
    List<Edge>primMSt = resultprim.Item2;

    Console.WriteLine("\nResultado de PRIM");
    //Se todos os elementos da lista são diferentes de -2 escreve o resultado
    if (primpredecessors.All(x => x != -2))
    {
        
        Console.WriteLine("VERDADEIRO");
        
        for (int i = 0; i < qntvertices; i++)
        {
                Console.WriteLine($"Predecessor de {i}: {primpredecessors[i]}");
        }
        foreach (var edge in primMSt)
        {
            Console.WriteLine($"({edge.Source}, {edge.Destination}): {edge.Weight}");
        }
    }
    //Se ha algum vértice desconexo
    else
    {
        Console.WriteLine("FALSO");
    }

    // Leitura do arquivo BellmanFord.txt
    lines = File.ReadAllLines("D:\\Facul\\Grafos\\BellmanFord.txt");
    qntvertices = int.Parse(lines[0]);
    graph = new Graph(qntvertices);

    //Lê a qntdevertices linhas do texto e cria as arestas de acordo com os valores
    for (int i = 0; i < qntvertices; i++)
    {
        //Cria uma array com os valores da linha separando ops elementos a cada espaço
        string[] values = lines[i+1].Split(' ');
        for (int j = 0; j < qntvertices; j++)
        {
            //Converte o texto para um double
            double weight = double.Parse(values[j]);
            //Se o peso for diferente de 0 cria-se uma aresta
            if (weight != 0)
            {
                graph.AddEdge(i, j, weight);
            }
        }
    }

    //Define o root como o elemento da ultima linha
    int root2 = int.Parse(lines[qntvertices + 1]);

    // Execução do algoritmo de BellmanFord
    var result = graph.BellmanFord(root2);
    List<int> bfPredecessors = result.Item1;
    List<double> bfDistances = result.Item2;

    Console.WriteLine("\nResultado de BellmanFord");
    Console.WriteLine("Predecessores:");

    //imprime os resultados
    foreach (var pred in bfPredecessors)
    {
        Console.Write(pred + " ");
    }
    Console.WriteLine("\nDistâncias:");
    foreach (var dist in bfDistances)
    {
        Console.Write(dist + " ");
    }
}
}
