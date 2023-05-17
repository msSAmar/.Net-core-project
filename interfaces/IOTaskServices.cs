using updateApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace updateApi
{   
    public interface IOTaskServices
    {    
        

    public List<Assiment> GetAll() ;
    public List<Assiment> Get(int id);
       

    public bool Update(int id,int idTask, Assiment newTask);

    public bool Delete(int id,int idTask );
   
    public void Add(Assiment assiment);
       

        }

    }
