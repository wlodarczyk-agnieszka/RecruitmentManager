using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentManager.UtilityClasses
{
    public class UtilDate
    {
        public string Today()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public string GetFormattedDate(DateTime Date)
        {
            return Date.ToString("yyyy-MM-dd");
        }
    }
}
