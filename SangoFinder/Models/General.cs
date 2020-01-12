using System.Collections.Generic;

namespace SangoFinder.Models
{
    public class General
    {
        public short 忠诚度 { get; set; }
        public short 当前生命 { get; set; }
        public short 当前技力 { get; set; }
        public short 最大生命 { get; set; }
        public short 最大技力 { get; set; }
        public short 智力 { get; set; }
        public short 等级 { get; set; }
        public short 士气 { get; set; }
        public short 武力 { get; set; }
        public short 功勋 { get; set; }
        public short 经验 { get; set; }
        public string 武将名 { get; set; }
        public string 所属 { get; set; }
        public string 所在 { get; set; }
        public string 坐骑 { get; set; }
        public string 兵器 { get; set; }
        public string 书籍 { get; set; }

        public IList<PlaceInfo> PlaceInfos { get; set; }

        public General()
        {
            PlaceInfos=new List<PlaceInfo>();
        }
    }

    public class PlaceInfo
    {
        public string 时期 { get; set; }
        public string 地点 { get; set; }
        public string 在野 { get; set; }
    }
}
