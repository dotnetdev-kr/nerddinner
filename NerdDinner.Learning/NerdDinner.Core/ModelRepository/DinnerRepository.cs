using NerdDinner.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NerdDinner.Core.ModelRepository
{
    public interface IDinnerRepository : IGenericRepository<Core.Model.Dinner> { }

    public class DinnerRepository<C>: CoreGenericRepository<C,Core.Model.Dinner>, IDinnerRepository
        where C : class, new()
    {
    }
}
