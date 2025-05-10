using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesManagment.BAL
{
    public class SeriesBAL
    {
        public int SeriesId { get; set; }
        //public int SeriesApiId { get; set; }
        public string SeriesName { get; set; }
        public int SeriesType { get; set; }
        public string SeriesStatus { get; set; }
        public string MatchStatus { get; set; }
        public string MatchFormat { get; set; }
        public int SeriesMatchType { get; set; }
        public int Gender { get; set; }
        public string Year { get; set; }
        public int TrophyType { get; set; }
        public DateTime SeriesStartDate { get; set; }
        public DateTime SeriesEndDate { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
