using System; // Espaço:O(1), Tempo:O(1)
using System.Collections.Generic; // Espaço:O(1), Tempo:O(1)
using System.Linq;  // Espaço:O(1), Tempo:O(1)

class Program  // Espaço:O(1), Tempo:O(1)
{
    public class Project  // Espaço:O(1), Tempo:O(1)
    {
        public DateTime startdate;  // Espaço:O(1), Tempo:O(1)
        public DateTime enddate;  // Espaço:O(1), Tempo:O(1)
        public Double profit;  // Espaço:O(1), Tempo:O(1)

        public Project(DateTime startdate,DateTime enddate, double profit)  // Espaço:O(1), Tempo:O(1)
        {
            this.startdate = startdate;  // Espaço:O(1), Tempo:O(1)
            this.enddate = enddate;  // Espaço:O(1), Tempo:O(1)
            this.profit = profit; // Espaço:O(1), Tempo:O(1)
        }
    }
    static void Main()
    {
        Project project1 = new Project(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15), 1000);  // Espaço:O(1), Tempo:O(1)
        Project project2 = new Project(new DateTime(2024, 2, 1), new DateTime(2024, 2, 20), 1500);  // Espaço:O(1), Tempo:O(1)
        Project project3 = new Project(new DateTime(2024, 3, 1), new DateTime(2024, 3, 10), 800);  // Espaço:O(1), Tempo:O(1)
        Project project4 = new Project(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15), 2000);  // Espaço:O(1), Tempo:O(1)

        List<Project> projects = new List<Project>([project1,project2,project3,project4]);  // Espaço:O(N), Tempo:O(1)

        List<Project> bestprojects = AlgoritmoGuloso(projects);  // Espaço:O(N), Tempo:O(1)

        Console.WriteLine("Projetos selecionados:");  // Espaço:O(1), Tempo:O(1)
        foreach (Project project in bestprojects)  //Espaço:O(K),  Tempo:O(K)
        {
            Console.WriteLine($"Data inicial: {project.startdate}, Data final: {project.enddate}, Lucro: {project.profit}");  // Espaço:O(1), Tempo:O(1)
        } 
    }

    static List<Project> AlgoritmoGuloso(List<Project> projects)  // Espaço:O(1), Tempo:O(1)
    {
        projects = projects.OrderByDescending(p => p.profit).ToList();  // Espaço:O(N), Tempo:O(N)

        List<Project> bestprojects = new List<Project>();  // Espaço:O(1), Tempo:O(1)

        foreach (Project project in projects)  // Espaço:O(N), Tempo:O(N)
        {
            if (!Timeconflict (project, bestprojects))  // Espaço:O(1), Tempo:O(1)
            {
                bestprojects.Add(project);  // Espaço:O(1), Tempo:O(1)
            }
        }

        return bestprojects;  // Espaço:O(1),Tempo:O(1)
    }

    static bool Timeconflict(Project project, List<Project> bestprojects)  // Espaço:O(1),Tempo:O(1)
    {
        foreach (Project pro in bestprojects)  // Espaço:O(K),Tempo:O(K)
        {
            if (project.startdate < pro.enddate && project.enddate > pro.startdate)  // Espaço:O(1),Tempo:O(1)
            {
                return true;  // Espaço:O(1),Tempo:O(1)
            }
        }
        return false;  // Espaço:O(1),Tempo:O(1)
    }
}


//Total Espaço:O(N*K),Tempo:O(N*K)