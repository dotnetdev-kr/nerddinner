using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NerdDinner.Core;
using NerdDinner.Core.Model;
using NerdDinner.Core.ModelRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NerdDinner.Core.Mobile.DataViewModel
{
    public class MobileDinnerRepository : DinnerRepository<JArray>
    {
        private readonly IDinnerRepository _dinnerRepository;
        private IQueryable<Dinner> _dinnerData;

        public MobileDinnerRepository()
        {
            _dinnerRepository = new DinnerRepository<JArray>();
        }

        public override IQueryable<Dinner> GetAll()
        {
            GetWebData();
            return _dinnerData;
        }

        protected async void GetWebData() 
        {
            List<Dinner> _data = new List<Dinner>();

            HttpClient httpDataClient = new HttpClient();
            HttpRequestMessage httpDataRequest = new HttpRequestMessage();
            HttpResponseMessage httpDatatResponse;

            httpDataClient.DefaultRequestHeaders.ExpectContinue = false;

            httpDataRequest.Method = HttpMethod.Get;
            httpDataRequest.RequestUri = new Uri(NerdDinnerAPI.RetrieveDinnersList);

            try
            {
                httpDatatResponse = await httpDataClient.SendAsync(httpDataRequest);

                if (httpDatatResponse.IsSuccessStatusCode) 
                {
                    string dataReturn = await httpDatatResponse.Content.ReadAsStringAsync();   
                    //Map Json Data..Here 
                }
            }
            catch 
            { 
                
            }
            _dinnerData = _data.AsQueryable<Dinner>();
        }

         
        //private readonly IDinnerRepository repository;
        //private ObservableCollection<Dinner> _dataItems;

        //public APIViewModel() 
        //{
        //    repository = new DinnerRepository<JArray>();
        //}


        ////public APIViewModel(IDinnerRepository repo)
        ////{
        ////    repository = repo;
        ////}

    }

    public struct NerdDinnerAPI
    {
        public static readonly string RetrieveDinnersList = "http://nerddinnerofficial.azurewebsites.net/api/dinners";
        public static readonly string RetrieveDinnersDetails = "http://nerddinnerofficial.azurewebsites.net/api/dinners/1";
        public static readonly string RetrieveDinnersByLoc = "http://nerddinnerofficial.azurewebsites.net/api/Search?latitude=30.0703&longitude=31.2036";
        public static readonly string SearchDinners = "http://nerddinnerofficial.azurewebsites.net/api/Search?limit=10";
    }
}
