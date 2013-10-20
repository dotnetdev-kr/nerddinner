using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdDinner.Core.Common;
using Newtonsoft.Json.Linq;

namespace NerdDinner.Learning
{
    class Win8TestImplementation : CoreGenericRepository<JArray,NerdDinner.Core.Model.Dinner>
    {
        public Win8TestImplementation() 
        {
            PlatformInstance = this;
        }

        public override IQueryable<Core.Model.Dinner> GetAll()
        {
            
            IQueryable<Core.Model.Dinner> query;
            //var test 
            query = this.DataContext.Select(q => q.Cast<Core.Model.Dinner>()).Cast<Core.Model.Dinner>().AsQueryable();
            return query;
            //throw new NotImplementedException();
        }
    }


}
