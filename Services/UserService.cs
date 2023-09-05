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
        public List<User> GetAll()
{
    // Filter the Users list based on the condition where IsActive field is true
    List<User> filteredUsers = Users.Where(user => user.IsAdmin == "false").ToList();

    return filteredUsers;
}
        public void add(User user){
            //user.Mail = Users.Max(p => p.Mail) + 1;
            Users.Add(user);
            saveToFile();
        }
         public  bool Delete(string mail)
    {
        var user = Users.FirstOrDefault(t => t.Mail.Equals(mail, StringComparison.OrdinalIgnoreCase));
        
        if (user == null)
            return false;
        Users.Remove(user);
        saveToFile();
        return true;
    }
        public User Exist (User user){
           foreach (User item in Users)
        {
            if(
                item.Mail==user.Mail&&
                item.Password==user.Password)
                return item;
        }
        return null;
    }
        } 
     
}