using updateApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace updateApi
{   
    public interface IOUserService
    {  
       public void add(User user);
       public User Exist(User user);
       public List<User> GetAll();
       public bool Delete(string id);
    }
}