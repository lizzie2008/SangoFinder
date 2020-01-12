using System.Collections.Generic;

namespace SangoFinder.Models
{
    public class SangoInfo
    {
        public SangoInfo()
        {
            Citys = new List<City>();
            Generals = new List<General>();
        }


        public SG3_ODIN SG3_ODIN { set; get; }
        public NA_MAN NA_MAN { set; get; }
        public NATION NATION { set; get; }

        public IList<City> Citys { set; get; }
        public IList<General> Generals { set; get; }
    }
}
