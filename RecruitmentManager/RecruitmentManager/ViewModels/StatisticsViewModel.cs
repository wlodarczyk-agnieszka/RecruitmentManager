using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentManager.ViewModels
{
    public class StatisticsViewModel
    {
        public Dictionary<string, int> Actual { get; set; }
        public Dictionary<string, int> Archives { get; set; }
        public Dictionary<string, int> All { get; set; }
    }
}
