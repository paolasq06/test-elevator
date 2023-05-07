using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModel.Summary
{
    public class ServiceSummaryVM
    {
        public int TotalService { get; set; }

        public IList<ProviderServiceSummaryVM> ProviderServicesSummary { get; set; }
        public IList<CountryServiceSummaryVM> CountryServicesSummary { get; set; }
    }

    public class ProviderServiceSummaryVM
    {
        public int Total { get; set; }

        public string Provider { get; set; }
    }

    public class CountryServiceSummaryVM
    {
        public int Total { get; set; }

        public string Country { get; set; }
    }


}
