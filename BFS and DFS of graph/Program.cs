using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;

public class av2extra 
{
 
 public class Vertex
 {
  public string name;
  public bool Explored;
  public List<Vertex> vertexchildren;
	public Vertex? predecessor;
	public int distance;

    public Vertex(string name)
    {
    this.name = name;
    this.Explored = false;
    this.vertexchildren = new List<Vertex>();
		this.predecessor=null;
		this.distance=0;
    }

    //BFS function starting from this vertex
    public void BFS()
    {   
     Queue<Vertex> fifoqueue = new Queue<Vertex>();
     fifoqueue.Enqueue(this);   

      while (fifoqueue.Count > 0)
      {
        Vertex currentvertex = fifoqueue.Dequeue();
        currentvertex.Explored = true;
        Console.WriteLine("Vertice " + currentvertex.name);
        if(currentvertex.predecessor!=null)
        {
        Console.WriteLine("Predecessor " + currentvertex.predecessor.name);
        }
        else
        {
          Console.WriteLine("Predecessor ninguém");
        }  
        Console.WriteLine("Distancia ao vertice inicial " + currentvertex.distance);
        Console.WriteLine("");
    
        foreach(Vertex vertex in currentvertex.vertexchildren)
        {   
          if (!vertex.Explored)
          {
            vertex.Explored=true;
            fifoqueue.Enqueue(vertex);
            vertex.predecessor=currentvertex;
            vertex.distance=currentvertex.distance+1;
          }
        }
      }
    }
    //DFS function starting from this vertex
    public void DFS()
    {
      Stack<Vertex> vertexes = new Stack<Vertex>();
      vertexes.Push(this);

      while (vertexes.Count > 0)
      {
        Vertex currentvertex = vertexes.Pop();
        currentvertex.Explored = true;
        Console.WriteLine("Vertice " + currentvertex.name);
        if(currentvertex.predecessor!=null)
        {
          Console.WriteLine("Predecessor " + currentvertex.predecessor.name);
        }
        else
        {
          Console.WriteLine("Predecessor ninguém");
        }  
        Console.WriteLine("Distancia ao vertice inicial " + currentvertex.distance);
        Console.WriteLine("");

        foreach(Vertex vertex in currentvertex.vertexchildren) 
        {
            if (!vertex.Explored)
            {
              vertex.Explored=true;
                vertexes.Push(vertex);
					      vertex.predecessor=currentvertex;
					      vertex.distance=currentvertex.distance+1;
            }
        }
      }
    }
 }

 static void Main(string[] args)
 {
       Console.WriteLine("type the matriz size");
        int size = Int32.Parse(Console.ReadLine());
        int[,] adjacencymatrix = new int[size,size];
        for (int i = 0;i<adjacencymatrix.GetLongLength(0);i++)
        {
            for (int j = 0;j<adjacencymatrix.GetLength(1); j++)
            {
                Console.WriteLine("type the lement from line " + i + " and colunm " + j );
                adjacencymatrix[i,j] = (Int32.Parse(Console.ReadLine()));
            }
        }

   //Creating vertexes
   Dictionary<string, Vertex> vertices = new Dictionary<string, Vertex>();
   int asciiValue = (int)'A';
   for (int i = 0;i<adjacencymatrix.GetLength(0);i++)
   {
    char vertexName = (char)(asciiValue + i);
    Vertex vertex = new Vertex(vertexName.ToString());
    vertices[vertex.name] = vertex;
   }

   //Creating Vertexes connections
   for(int i = 0;i<adjacencymatrix.GetLength(0);i++)
   {
        for (int j = 0;j<adjacencymatrix.GetLength(1);j++)
        {   
            if (adjacencymatrix[i,j] != 0)
            {
                char vertexName = (char)(asciiValue + i);
                char vertexchildrenname = (char)(asciiValue + j);
                vertices[vertexName.ToString()].vertexchildren.Add(vertices[vertexchildrenname.ToString()]);
            }
        }
   }
   //function calls
   int op = 1;
   while(op!=0)
   {
    foreach (Vertex vertex in vertices.Values)
    {
      vertex.Explored=false;
      vertex.predecessor=null;
      vertex.distance=0;
    }
    Console.WriteLine("Choose initial vertex between");
      foreach (Vertex vertex in vertices.Values)
    {
      Console.WriteLine(vertex.name);
    }
    string verticeobj = Console.ReadLine();
    Console.WriteLine("BFS");
    vertices[verticeobj].BFS();
    foreach (Vertex vertex in vertices.Values)
    {
      vertex.Explored=false;
      vertex.predecessor=null;
      vertex.distance=0;
    }
    Console.WriteLine("DFS");
    vertices[verticeobj].DFS();
    Console.WriteLine("Type 0 to stop or other number if you want to change the initial vertex");
    op = Int32.Parse(Console.ReadLine());
   }
 }
}