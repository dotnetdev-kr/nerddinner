using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenSearchToolkit;
using NerdDinner.Models;

namespace NerdDinner
{
    /// <summary>
    /// Summary description for OpenSearch
    /// </summary>
    public class OpenSearch : OpenSearchHandler
    {
        protected override Description Description
        {
            get
            {
                return new Description
                {
                    DisplayName = "Nerd Dinner",
                    LongDescription = "Nerd Dinner - Organizing the world's nerds and helping them eat in packs",
                    SearchPathTemplate = "/#where={0}",
                    IconPath = "~/favicon.ico"
                };
            }
        }

        protected override IEnumerable<SearchResult> GetResults(string term)
        {
            var dinnerRepository = new DinnerRepository();
            var dinners = dinnerRepository.FindAllDinners().Where(d => d.Title.Contains(term)).AsEnumerable();

            return from dinner in dinners
                   select new
                       SearchResult
                       {
                           Description = dinner.Description,
                           Title = dinner.Title,
                           Path = "/" + dinner.DinnerID
                       };
        }

        protected override IEnumerable<SearchSuggestion> GetSuggestions(string term)
        {
            throw new NotImplementedException();
        }

        protected override bool SupportsSuggestions
        {
            get { return false; }
        }
    }
}