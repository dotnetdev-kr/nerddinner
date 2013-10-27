using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdDinner.Core.Common;
using NerdDinner.Core.ModelRepository;
using Newtonsoft.Json.Linq;

namespace NerdDinner.Learning
{
    class Win8TestImplementation : DinnerRepository<JArray>
    {
        public Win8TestImplementation() 
        {
            //May not use this
            PlatformInstance = this;
        }

        public override void Add(Core.Model.Dinner entity)
        {
            //base.Add(entity);
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
