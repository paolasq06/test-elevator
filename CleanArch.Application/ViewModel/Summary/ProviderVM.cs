using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModel.Summary
{
    public class ProviderSummaryVM
    {
        public int Total { get; set; }

        public IList<ProviderSummaryDayVM> ProviderSummaryDays { get; set; }
    }

    public class ProviderSummaryDayVM
    {
        public int Total { get; set; }

        public string Day { get; set; }
    }


}
