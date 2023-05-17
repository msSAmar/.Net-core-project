using updateApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
//using updateApi.interfaces;
namespace updateApi.Services

{   
    public class UserService : IOUserService
    {  
        private  List<User> Users {get;}
        private IWebHostEnvironment  webHost;
        private string filePath;
        public UserService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "User.json");
            
            using (var jsonFile = File.OpenText(filePath))
            {
                Users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(Users));
        }
        public void add(User user){
            user.Id = Users.Max(p => p.Id) + 1;
            Users.Add(user);
            saveToFile();
        }
        public User Exist (string num){
            return Users.FirstOrDefault(p => p.Password == num);
        } 
     }
}