class Program { // Espaço:O(1), Tempo:O(1)

public class City  // Espaço:O(1), Tempo:O(1)
{
    public string cityname; // Espaço:O(1), Tempo:O(1)
    public int shortesttimeto; // Espaço:O(1), Tempo:O(1)
    public bool explored; // Espaço:O(1), Tempo:O(1)
    public List<Edge> edges; // Espaço:O(1), Tempo:O(1)
    public City? previouscity; // Espaço:O(1), Tempo:O(1)
    public bool firstiteration; // Espaço:O(1), Tempo:O(1)

    public City(string city_name) // Espaço:O(1), Tempo:O(1)
    {
        this.cityname=city_name;  // Espaço:O(1), Tempo:O(1)
        this.shortesttimeto=int.MaxValue; // Espaço:O(1), Tempo:O(1)
        this.explored=false; // Espaço:O(1), Tempo:O(1)
        this.edges= new List<Edge>(); // Espaço:O(K), Tempo:O(1)
        this.previouscity=null; // Espaço:O(1), Tempo:O(1)
        this.firstiteration=true; // Espaço:O(1), Tempo:O(1)
    }
}

public class Edge { // Espaço:O(1), Tempo:O(1)
    public City leftcity; // Espaço:O(1), Tempo:O(1)
    public City rightcity; // Espaço:O(1), Tempo:O(1)
    public int pathtime; // Espaço:O(1), Tempo:O(1)

    public Edge(City left_city, City right_city, int path_time){ // Espaço:O(1), Tempo:O(1)
    this.leftcity = left_city; // Espaço:O(1), Tempo:O(1)
    this.rightcity = right_city; // Espaço:O(1), Tempo:O(1)
    this.pathtime = path_time; // Espaço:O(1), Tempo:O(1)
    }

}
public static int Calculate(City startingcity,City currentcity) // Espaço:O(1), Tempo:O(1)
{
int totaltime = 0; // Espaço:O(1), Tempo:O(1)
int shortestime = int.MaxValue; // Espaço:O(1), Tempo:O(1)
startingcity.explored = true; // Espaço:O(1), Tempo:O(1)
startingcity.shortesttimeto=0; // Espaço:O(1), Tempo:O(1)
return Calculatetime(startingcity,currentcity); // Espaço:O(1), Tempo:O(1)

int Calculatetime(City startingcity,City currentcity) // Espaço:O(1), Tempo:O(1)
{
foreach(Edge edge in currentcity.edges) // Espaço:O(K), Tempo:O(K)
{
    Console.WriteLine(currentcity.cityname); // Espaço:O(1), Tempo:O(1)
    currentcity=edge.leftcity; // Espaço:O(1), Tempo:O(1)
    if(edge.rightcity.explored!=true && edge.rightcity!=startingcity) // Espaço:O(1), Tempo:O(1)
    {
        totaltime+=edge.pathtime; // Espaço:O(1), Tempo:O(1)
        if(edge.rightcity.shortesttimeto>edge.leftcity.shortesttimeto+edge.pathtime) // Espaço:O(1), Tempo:O(1)
        {
            edge.rightcity.shortesttimeto = edge.leftcity.shortesttimeto+edge.pathtime; // Espaço:O(1), Tempo:O(1)
        }
        currentcity.explored=true; // Espaço:O(1), Tempo:O(1)
        if(startingcity.firstiteration) // Espaço:O(1), Tempo:O(1)
        {
        edge.rightcity.previouscity=currentcity; // Espaço:O(1), Tempo:O(1)
        }
        currentcity=edge.rightcity; // Espaço:O(1), Tempo:O(1)
        
        Calculatetime(startingcity,currentcity); // Espaço:O(N), Tempo:O(N)
    }
    else if (edge.rightcity==startingcity) // Espaço:O(1), Tempo:O(1)
    {    
    if(totaltime-edge.pathtime>0) // Espaço:O(1), Tempo:O(1)
    {
     totaltime+=edge.pathtime;  // Espaço:O(1), Tempo:O(1)   
     if(totaltime<shortestime) // Espaço:O(1), Tempo:O(1)
     {
      shortestime=totaltime; // Espaço:O(1), Tempo:O(1)
     } 
     Console.WriteLine(shortestime); // Espaço:O(1), Tempo:O(1)
     currentcity.explored=false; // Espaço:O(1), Tempo:O(1)
     totaltime-=edge.pathtime; // Espaço:O(1), Tempo:O(1)
    }
    else if(startingcity.firstiteration) // Espaço:O(1), Tempo:O(1)
    {
        totaltime-=edge.pathtime; // Espaço:O(1), Tempo:O(1)
        currentcity.explored=false; // Espaço:O(1), Tempo:O(1)
        currentcity = edge.rightcity; // Espaço:O(1), Tempo:O(1)
        startingcity.firstiteration=false; // Espaço:O(1), Tempo:O(1)
    }
    }
    else if(edge.rightcity.explored == true && edge.rightcity!=startingcity) // Espaço:O(1), Tempo:O(1)
    {
        if(currentcity.previouscity==edge.rightcity) // Espaço:O(1), Tempo:O(1)
        {
        totaltime-=edge.pathtime; // Espaço:O(1), Tempo:O(1)
        currentcity.explored=false; // Espaço:O(1), Tempo:O(1)
        }
    }
}
return shortestime; // Espaço:O(1), Tempo:O(1)
}
}

static void Main(string[] args) // Espaço:O(1), Tempo:O(1)
{
 City citya = new City("A"); // Espaço:O(1), Tempo:O(1)
 City cityb = new City("B");  // Espaço:O(1), Tempo:O(1)
 City cityc = new City("C"); // Espaço:O(1), Tempo:O(1)
 City cityd = new City("D"); // Espaço:O(1), Tempo:O(1)
 City citye = new City("E"); // Espaço:O(1), Tempo:O(1)
 City cityf = new City ("F"); // Espaço:O(1), Tempo:O(1)
 City cityg = new City("G"); // Espaço:O(1), Tempo:O(1)
 City cityh = new City("H"); // Espaço:O(1), Tempo:O(1)
 City cityi = new City ("I"); // Espaço:O(1), Tempo:O(1)

Random random = new Random(); // Espaço:O(1), Tempo:O(1)

Edge edgeab = new Edge(citya, cityb, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgeba = new Edge(cityb, citya, edgeab.pathtime); // Espaço:O(1), Tempo:O(1)

Edge edgebc = new Edge(cityb, cityc, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgecb = new Edge(cityc, cityb, edgebc.pathtime); // Espaço:O(1), Tempo:O(1)

Edge edgecd = new Edge(cityc, cityd, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgedc = new Edge(cityd, cityc, edgecd.pathtime); // Espaço:O(1), Tempo:O(1)

Edge edgede = new Edge(cityd, citye, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgeed = new Edge(citye, cityd, edgede.pathtime); // Espaço:O(1), Tempo:O(1)

Edge edgeef = new Edge(citye, cityf, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgefe = new Edge(cityf, citye, edgeef.pathtime); // Espaço:O(1), Tempo:O(1)

Edge edgefg = new Edge(cityf, cityg, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgegf = new Edge(cityg, cityf, edgefg.pathtime); // Espaço:O(1), Tempo:O(1)

Edge edgegh = new Edge(cityg, cityh, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgehg = new Edge(cityh, cityg, edgegh.pathtime); // Espaço:O(1), Tempo:O(1)

Edge edgehi = new Edge(cityh, cityi, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgeih = new Edge(cityi, cityh, edgehi.pathtime); // Espaço:O(1), Tempo:O(1)

Edge edgeia = new Edge(cityi, citya, random.Next(1, 10)); // Espaço:O(1), Tempo:O(1)
Edge edgeai = new Edge(citya, cityi, edgeia.pathtime); // Espaço:O(1), Tempo:O(1)

citya.edges.AddRange(new List<Edge> { edgeab, edgeai }); // Espaço:O(1), Tempo:O(1)
cityb.edges.AddRange(new List<Edge> { edgebc, edgeba }); // Espaço:O(1), Tempo:O(1)
cityc.edges.AddRange(new List<Edge> { edgecd, edgecb }); // Espaço:O(1), Tempo:O(1)
cityd.edges.AddRange(new List<Edge> { edgede, edgedc }); // Espaço:O(1), Tempo:O(1)
citye.edges.AddRange(new List<Edge> { edgeef, edgeed }); // Espaço:O(1), Tempo:O(1)
cityf.edges.AddRange(new List<Edge> { edgefg, edgefe }); // Espaço:O(1), Tempo:O(1)
cityg.edges.AddRange(new List<Edge> { edgegh, edgegf }); // Espaço:O(1), Tempo:O(1)
cityh.edges.AddRange(new List<Edge> { edgehi, edgehg }); // Espaço:O(1), Tempo:O(1)
cityi.edges.AddRange(new List<Edge> { edgeia, edgeih }); // Espaço:O(1), Tempo:O(1)

Console.WriteLine("Tempo minimo" + Calculate(citya,citya)); // Espaço:O(1), Tempo:O(1)

}
}

//Total Espaço O(N*K), Tempo O(N*K)