using updateApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace updateApi
{   
    public interface IOTaskServices
    {    
        

    public List<Assiment> GetAll() ;
    public List<Assiment> Get(string id);
       

    public bool Update(string id,int idTask, Assiment newTask);

    public bool Delete(string id,int idTask );
   
    public void Add(Assiment assiment);
       

        }

    }
