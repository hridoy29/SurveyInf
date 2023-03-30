using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class LU_AssetConfigReport_GetPaged
    {
        public List<Asset_configReports> asset_configReports { get; set; }
        public int DataCount { get; set; }
    }
}
