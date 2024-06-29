using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Game
{


    // Vertex object declclaration
    public class Vertex
    {
        public string VertexName;
        public List<Edge> Edges;
        public int ShortestTimeTo;
        public bool Explored;

        public Vertex? closestvertex;


        public Vertex(string vertexname)
        {
            this.VertexName = vertexname;
            this.Edges = new List<Edge>();
            this.ShortestTimeTo = int.MaxValue;
            this.Explored = false;
            this.closestvertex = null;
        }

    }

    //Edge object declaration
    public class Edge
    {
        public Vertex LeftVertex;
        public Vertex RightVertex;
        public int EdgeTime;

        public Edge(Vertex leftvertex, Vertex rightvertex, int edgetime)
        {
            this.LeftVertex = leftvertex;
            this.RightVertex = rightvertex;
            this.EdgeTime = edgetime;
        }
    }

    //Board to keep track of current police and thief positions
    public class Board
    {
        public List<Vertex> AllVertices;
        public List<Vertex> PolicePositions;
        public Vertex ThiefPosition;

        public Board(List<Vertex> allvertices, List<Vertex> StartPolicePositions, Vertex StartThiefPosition)
        {
            this.AllVertices = allvertices;
            this.PolicePositions = StartPolicePositions;
            this.ThiefPosition = StartThiefPosition;
        }

        public void DrawBoard()
        {
            // Check if the console is available
            if (!ConsoleIsAvailable())
            {
                Console.WriteLine("Console is not available.");
                return;
            }

            // Draw the game board
            Console.WriteLine("Game Board:");
            Console.WriteLine("----------------------------------------");

            // Fixed size for the game board
            int boardWidth = 80;
            int boardHeight = 20;

            // Draw vertices and edges
            Dictionary<Vertex, int[]> vertexPositions = new Dictionary<Vertex, int[]>();
            int startX = 10;
            int startY = 5;
            int offsetX = 10;
            int offsetY = 5;
            foreach (var vertex in AllVertices)
            {
                // Assign positions to vertices
                int[] position = new int[] { startX, startY };
                vertexPositions[vertex] = position;

                // Draw vertex
                Console.SetCursorPosition(startX, startY);
                Console.Write("● " + vertex.VertexName);

                // Draw edges
                foreach (var edge in vertex.Edges)
                {
                    if (vertexPositions.ContainsKey(edge.RightVertex))
                    {
                        int[] targetPosition = vertexPositions[edge.RightVertex];
                        int targetX = targetPosition[0];
                        int targetY = targetPosition[1];
                        DrawLine(startX + 1, startY, targetX, targetY);
                    }
                }

                // Update start position for next vertex
                startX += offsetX;
                if (startX >= boardWidth - offsetX)
                {
                    startX = 10;
                    startY += offsetY;
                }
            }

            // Mark police positions
            foreach (var policePosition in PolicePositions)
            {
                if (vertexPositions.ContainsKey(policePosition))
                {
                    int[] position = vertexPositions[policePosition];
                    Console.SetCursorPosition(position[0], position[1]);
                    Console.Write("👮");
                }
            }

            // Mark thief position
            if (vertexPositions.ContainsKey(ThiefPosition))
            {
                int[] thiefPosition = vertexPositions[ThiefPosition];
                Console.SetCursorPosition(thiefPosition[0], thiefPosition[1]);
                Console.Write("🕵️");
            }

            Console.WriteLine("\n----------------------------------------");
        }

        // Check if the console is available
        private bool ConsoleIsAvailable()
        {
            try
            {
                return !Console.IsOutputRedirected && Console.CursorLeft >= 0 && Console.CursorTop >= 0;
            }
            catch
            {
                return false;
            }
        }
        // Helper method to draw a line between two points
        private void DrawLine(int x1, int y1, int x2, int y2)
        {
            double deltaX = x2 - x1;
            double deltaY = y2 - y1;
            double length = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            double unitX = deltaX / length;
            double unitY = deltaY / length;

            for (int i = 0; i <= (int)length; i++)
            {
                int x = (int)(x1 + unitX * i);
                int y = (int)(y1 + unitY * i);
                Console.SetCursorPosition(x, y);
                Console.Write("-");
            }
        }
    }


    //find minimum distance to end vertex
    public static int smallestvalue(Vertex startvertex, Vertex finalvertex, List<Vertex> allvertexes)
    {
        int smallestvalue = int.MaxValue;
        startvertex.ShortestTimeTo = 0;
        Vertex currentvertex;
        while (smallestvertex(allvertexes) != null)
        {
            currentvertex = smallestvertex(allvertexes);
            currentvertex.Explored = true;

            foreach (Edge edge in currentvertex.Edges)
            {
                if (edge.RightVertex.ShortestTimeTo > edge.LeftVertex.ShortestTimeTo + edge.EdgeTime)
                {
                    edge.RightVertex.ShortestTimeTo = edge.LeftVertex.ShortestTimeTo + edge.EdgeTime;
                    edge.RightVertex.closestvertex = edge.LeftVertex;
                }

            }
        }
        smallestvalue = finalvertex.ShortestTimeTo;

        // resset Vertexes to original state
        foreach (Vertex vertex in allvertexes)
        {
            vertex.Explored = false;
            vertex.ShortestTimeTo = int.MaxValue;
            vertex.closestvertex = null;
        }


        return smallestvalue;
    }

    //Find unexplored vertex with smallest distance to
    public static Vertex smallestvertex(List<Vertex> allvertexes)
    {
        int smallestnumber = int.MaxValue;
        Vertex smallestvertex = null;
        foreach (Vertex vertex in allvertexes)
        {
            if (vertex.Explored != true && vertex.ShortestTimeTo < smallestnumber)
            {
                smallestvertex = vertex;
                smallestnumber = vertex.ShortestTimeTo;
            }
        }
        return smallestvertex;
    }

    //Thief AI main logic find which of the edge options would get the thief further to the nearest police
    public static Vertex closestvertex(Vertex currentvertex, List<Vertex> allvertexes, Board board)
    {
        //find nearest police officer
        Vertex nearvertex = null;
        int timetaken;
        int smallesttimetaken = int.MaxValue;
        foreach (Vertex policevertex in board.PolicePositions)
        {
            timetaken = smallestvalue(currentvertex, policevertex, allvertexes);
            if (timetaken < smallesttimetaken)
            {
                nearvertex = policevertex;
                smallesttimetaken = timetaken;
            }
        }

        //chooose the furthest adjacent Vertex from the nearest police officer
        Vertex farvertex = null;
        int biggesttime = int.MinValue;
        foreach (Edge edge in currentvertex.Edges)
        {
            timetaken = smallestvalue(edge.RightVertex, nearvertex, allvertexes);
            if (timetaken > biggesttime)
            {
                farvertex = edge.RightVertex;
                biggesttime = timetaken;
            }
        }

        return farvertex;
    }

    public static void AddEdge(Vertex v1, Vertex v2, int time)
    {
        Edge e1 = new Edge(v1, v2, time);
        Edge e2 = new Edge(v2, v1, time); // Ensure edges are both ways
        v1.Edges.Add(e1);
        v2.Edges.Add(e2);
    }



    static void Main(string[] args)
    {
        // Creating vertices
        Vertex vertexA = new Vertex("A");
        Vertex vertexB = new Vertex("B");
        Vertex vertexC = new Vertex("C");
        Vertex vertexD = new Vertex("D");
        Vertex vertexE = new Vertex("E");
        Vertex vertexF = new Vertex("F");
        Vertex vertexG = new Vertex("G");
        Vertex vertexH = new Vertex("H");
        Vertex vertexI = new Vertex("I");
        Vertex vertexJ = new Vertex("J");

        // Creating edges
        AddEdge(vertexA, vertexE, 1);
        AddEdge(vertexA, vertexJ, 1);
        AddEdge(vertexB, vertexD, 1);
        AddEdge(vertexC, vertexE, 1);
        AddEdge(vertexJ, vertexF, 1);
        AddEdge(vertexB, vertexG, 1);
        AddEdge(vertexF, vertexH, 1);
        AddEdge(vertexG, vertexI, 1);
        AddEdge(vertexH, vertexD, 1);
        AddEdge(vertexI, vertexA, 1);
        AddEdge(vertexB, vertexF, 1);
        AddEdge(vertexB, vertexC, 1);
        AddEdge(vertexF, vertexC, 1);


        // Creating the board 
        List<Vertex> allVertices = new List<Vertex> { vertexA, vertexB, vertexC, vertexD, vertexE, vertexF, vertexG, vertexH, vertexI, vertexJ };
        List<Vertex> policePositions = new List<Vertex> { vertexB, vertexH };
        Vertex thiefPosition = vertexJ;
        Board mainBoard = new Board(allVertices, policePositions, thiefPosition);

        //Main game logic
        bool thiefcatched = false;
        while (!thiefcatched)
        {

            mainBoard.DrawBoard();
            //Thief turn
            Console.WriteLine("Thief turn");
            thiefPosition = closestvertex(thiefPosition, allVertices, mainBoard);
            foreach (Vertex position in policePositions)
            {
                if (position == thiefPosition)
                {
                    break;
                }
            }
            Console.WriteLine("Thief moved to vertex" + thiefPosition.VertexName);

            //Police turn
            Console.WriteLine("Police turn");
            Console.WriteLine("Choose police officer to move");
            Console.WriteLine("1. Police1");
            Console.WriteLine("2. police2");
            int movingpolice = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Choose where to move");
            if (movingpolice == 1)
            {
                int i = 1;
                foreach (Edge edge in policePositions[0].Edges)
                {
                    Console.WriteLine(i + ". move officer to " + edge.RightVertex.VertexName);
                    i++;
                }
                int movetarget = Int32.Parse(Console.ReadLine());
                policePositions[0] = policePositions[0].Edges[movetarget - 1].RightVertex;
            }
            else if (movingpolice == 2)
            {
                int i = 1;
                foreach (Edge edge in policePositions[1].Edges)
                {
                    Console.WriteLine(i + ". move officer to " + edge.RightVertex.VertexName);
                    i++;
                }
                int movetarget = Int32.Parse(Console.ReadLine());
                policePositions[1] = policePositions[1].Edges[movetarget - 1].RightVertex;
            }
            if (policePositions[0] == thiefPosition)
            {
                break;
            }
            if (policePositions[1] == thiefPosition)
            {
                break;
            }

        }
        //game ended 
        Console.WriteLine("Game over");

    }
}