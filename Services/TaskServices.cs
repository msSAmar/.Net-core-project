using updateApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
//using updateApi.interfaces;
namespace updateApi.Services

{   
    public class TaskServices : IOTaskServices
    {   
        private  List<Assiment> Tasks {get;}
        private IWebHostEnvironment  webHost;
        private string filePath;
        public TaskServices(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "Assiment.json");
            
            using (var jsonFile = File.OpenText(filePath))
            {
                Tasks = JsonSerializer.Deserialize<List<Assiment>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(Tasks));
        }
        
        public  List<Assiment> GetAll() => Tasks;
        public List<Assiment> Get(string mail)
        {
            List<Assiment> assiment=Tasks.FindAll(p =>p.Mail.Equals(mail, StringComparison.OrdinalIgnoreCase));
            return assiment;
        }

        public bool Update(string mail,int idTask, Assiment newTask)
        {
            
            List<Assiment> assiment=Tasks.FindAll(p => p.Mail.Equals(mail, StringComparison.OrdinalIgnoreCase));
            // if (newTask.Id != id)
            //     return false;
            
            var task = assiment.FirstOrDefault(t => t.IdTask == idTask);

            task.Name = newTask.Name;
            task.Description = newTask.Description;
            task.Done=newTask.Done;
            task.Deadline=newTask.Deadline;
            saveToFile();
            return true;
            
        }

        public  bool Delete(string mail,int idTask)
        {
            
             List<Assiment> assiment=Tasks.FindAll(p => p.Mail.Equals(mail, StringComparison.OrdinalIgnoreCase));
            var task = assiment.FirstOrDefault(t => t.IdTask == idTask);
            if (task == null)
                return false;
            Tasks.Remove(task);
            saveToFile();
            return true;
        }
         public  void Add(Assiment assiment)
        {
            assiment.IdTask = Tasks.Max(p => p.IdTask) + 1;
            Tasks.Add(assiment);
            saveToFile();
        }

        }

    }
