using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NerdDinner.Core.Common;
using NerdDinner.Core.Model;
using System.Threading.Tasks;

namespace NerdDinner.Core.ModelRepository
{
    public interface IAccountRepository<T>  where T : class
    { 
        
    }

    //In Web this is an ActionResult type, make abstract class with Generic Type to allow for Mobile to use
    //Auth method relevant to their platform in View Model and Web to use ActionResult in Controller
    public abstract class AccountRepository<T> : IAccountRepository<T> where T : class
    {
        public abstract T Login(string returnUrl);
        public abstract Task<T> Login(UserAccount model, string returnUrl);
        public abstract T Register();
        public abstract Task<T> Register(UserAccount model);
        public abstract T ChangePassword(UserAccount model);
        public abstract Task<T> ExternalLogin();

    }
}
