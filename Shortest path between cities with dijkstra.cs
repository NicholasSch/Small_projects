using System.IO;
using System.Runtime.CompilerServices;

public class Djikistra
{
    public class City
    {
        public string city_name { get; set; }
        public int shortest_time_to { get; set; }

        public City? closest_city { get; set; }

        public List<Edge> edges { get; set; }

        public bool explored { get; set; }


        public City(string cityname)
        {
            this.city_name = cityname;
            this.shortest_time_to = int.MaxValue;
            this.closest_city = null;
            this.edges = new List<Edge>();
            this.explored = false;
        }

    }

    public class Edge
    {
        public City city1 { get; set; }
        public City city2 { get; set; }

        public int time_taken { get; set; }

        public Edge(City city1, City city2, int time_taken)
        {
            this.city1 = city1;
            this.city2 = city2;
            this.time_taken = time_taken;
        }
    }

    public static (List<City>, int) findshortestpathto(List<City> allcities, City startcity, City endcity)
    {
        startcity.shortest_time_to = 0;
        City currentcity;
        int totaltime = int.MaxValue;
        while (true)
        {
            currentcity = findsmallestunexploredcity(allcities);
            if (currentcity==null)
            {
                break;
            }
            currentcity.explored = true;

            int time;
            foreach (Edge edge in currentcity.edges)
            {
                time = edge.time_taken + currentcity.shortest_time_to;
                if (edge.city2.shortest_time_to > time)
                {
                    edge.city2.shortest_time_to = time; 
                    edge.city2.closest_city = edge.city1;
                    Console.WriteLine("Closest city from " + edge.city2.city_name + " starting from " + startcity.city_name + " is now " + edge.city1.city_name);
                }
                Console.WriteLine("closes city from " + edge.city2.city_name + " is " + edge.city2.closest_city.city_name);
            }
        }
        List<City> path = new List<City>();
        totaltime = endcity.shortest_time_to;
        if (totaltime < int.MaxValue)
        {
            City current = endcity;
            while (current != null)
            {
                path.Insert(0, current);
                current = current.closest_city;
            }
        }
        return (path, totaltime);
    }

private static City findsmallestunexploredcity(List<City> allcities)
    {
        int minTime = int.MaxValue;
        City smallestCity = null;

        foreach (City city in allcities)
        {
            if (!city.explored && city.shortest_time_to < minTime)
            {
                minTime = city.shortest_time_to;
                smallestCity = city;
            }
        }

        return smallestCity;
    }


    static public void Main(String[] args)
    {

        City cityA = new City("A");
        City cityB = new City("B");
        City cityC = new City("C");
        City cityD = new City("D");
        City cityE = new City("E");
        City cityF = new City("F"); 
        City cityG = new City("G");
        City cityH = new City("H");
        City cityI = new City("I");

        Edge edgeAB = new Edge(cityA, cityB, 2);
        Edge edgeAC = new Edge(cityA, cityC, 4);
        Edge edgeBD = new Edge(cityB, cityD, 3);
        Edge edgeBE = new Edge(cityB, cityE, 7);
        Edge edgeCD = new Edge(cityC, cityD, 1);
        Edge edgeCE = new Edge(cityC, cityE, 5);
        Edge edgeDF = new Edge(cityD, cityF, 2);
        Edge edgeEG = new Edge(cityE, cityG, 1);
        Edge edgeFI = new Edge(cityF, cityI, 4);
        Edge edgeGH = new Edge(cityG, cityH, 3);
        Edge edgeHI = new Edge(cityH, cityI, 2);

        Edge edgeAD = new Edge(cityA, cityD, 5);
        Edge edgeBF = new Edge(cityB, cityF, 1);
        Edge edgeCG = new Edge(cityC, cityG, 2);
        Edge edgeDH = new Edge(cityD, cityH, 6);
        Edge edgeEI = new Edge(cityE, cityI, 3);
        Edge edgeAH = new Edge(cityA, cityH, 8);
        Edge edgeCF = new Edge(cityC, cityF, 6);
        Edge edgeAE = new Edge(cityA, cityE, 9);

        cityA.edges.AddRange(new[] { edgeAB, edgeAC, edgeAD, edgeAH, edgeAE });
        cityB.edges.AddRange(new[] { edgeBD, edgeBE, edgeBF});
        cityC.edges.AddRange(new[] { edgeCD, edgeCE, edgeCG, edgeCF });
        cityD.edges.AddRange(new[] { edgeDF, edgeDH });
        cityE.edges.AddRange(new[] { edgeEG, edgeEI });
        cityF.edges.AddRange(new[] { edgeFI });
        cityG.edges.AddRange(new[] { edgeGH });
        cityH.edges.AddRange(new[] { edgeHI });
        cityI.edges.AddRange(new[] { edgeEI });


        List<City> AllCities = new List<City> { cityA, cityB, cityC, cityD, cityE, cityF, cityG, cityH, cityI };

        var result = findshortestpathto(AllCities,cityA,cityI);



        Console.WriteLine("Menor caminho");
        foreach (City city in result.Item1)
        {
            Console.Write(city.city_name + " -> ");
        }
        Console.WriteLine($"Total Time: {result.Item2}");

    }
}

