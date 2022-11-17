using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class QuestionnaireDetailsReport
    {

        public string DbCode { get; set; }
        public string DistributorName { get; set; }
        public bool LogoOfTheTCC { get; set; }
        public bool StorageOfMemoForLastYear { get; set; }
        public bool GitInIdasAvailable { get; set; }
        public bool DayClosePending { get; set; }
        public bool StorageCondition { get; set; }
        public bool MarketReturnedProductAdjusted { get; set; }
        public bool FreeSchemeProductAvailable { get; set; }
        public string BBDName { get; set; }

      
    }
}
