using System.Text.Json;
using WebProjeleri2022.Models;

namespace WebProjeleri2022.Services
{
    public class JsonProjectService
    {
       
        public JsonProjectService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment;

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "projects.json"); }

        }


        public List<ProjectModel> GetProjects()
        {
            using var json = File.OpenText(JsonFileName);
            
            //return JsonSerializer.Deserialize<List<ProjectModel>>(json.Re());

            return JsonSerializer.Deserialize<ProjectModel[]>(json.ReadToEnd()).ToList();

        }

        public void AddProject(ProjectModel newproject)
        {
            List<ProjectModel> projeler = GetProjects();

            projeler.Add(newproject);
            
            using var json = File.OpenWrite(JsonFileName);
            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented= true});
            JsonSerializer.Serialize<List<ProjectModel>>(jsonwriter, projeler);
        }

        

    }
}
