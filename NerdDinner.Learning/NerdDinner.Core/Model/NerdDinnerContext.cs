using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NerdDinner.Core.Model
{
    //Pass in DataContext Type for all Types
    //Web -> DBContext
    //Mobile-> JObject
    

    
    class NerdDinnerContext<T> where T : class, IDisposable
    {
        private T _dataSource;
        

    }
}
