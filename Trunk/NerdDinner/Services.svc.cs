using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using NerdDinner.Models;

namespace NerdDinner
{
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Services : DataService<NerdDinnerEntities>
    {
        IDinnerRepository dinnerRepository;
        //
        // Dependency Injection enabled constructors

        public Services()
            : this(new DinnerRepository()) {
        }

        public Services(IDinnerRepository repository)
        {
            dinnerRepository = repository;
        }

        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // We're exposing both Dinners and RSVPs for read
            config.SetEntitySetAccessRule("Dinners", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("RSVPs", EntitySetRights.AllRead);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            #if DEBUG
            config.UseVerboseErrors = true;
            #endif
        }

        [WebGet]
        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return dinnerRepository.FindUpcomingDinners();
        }
    }
}
