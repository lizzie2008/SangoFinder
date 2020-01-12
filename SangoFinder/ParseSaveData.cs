using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SangoFinder.Models;

namespace SangoFinder
{
    public class ParseSaveData
    {
        public static SangoInfo Parse(string fileName)
        {
            var bytes = File.ReadAllBytes(fileName);

            var result = new SangoInfo();

            #region 城市
            var citySettings = IniReader.GetCollecions(@"Settings\City.ini");
            var indexCity = IndexOf(bytes, Encoding.GetEncoding("BIG5").GetBytes("CT_MAN"));
            var cityNum = 70;
            indexCity += 40;
            for (int i = 0; i < cityNum; i++)
            {
                var data = bytes.Skip(indexCity).Take(80).ToArray();
                var city = ParseCity(data);
                city.城市名 = citySettings[i].Get("Name");
                result.Citys.Add(city);
                indexCity += 80;
            }
            #endregion

            #region 武将
            var indexGeneral = IndexOf(bytes, Encoding.GetEncoding("BIG5").GetBytes("GN_MAN"));
            var generalNum = BitConverter.ToInt32(bytes, indexGeneral + 64);
            indexGeneral += 68;
            for (int i = 0; i < generalNum; i++)
            {
                var data = bytes.Skip(indexGeneral).Take(464).ToArray();
                result.Generals.Add(ParseGeneral(data));
                indexGeneral += 464;
            }
            #endregion

            return result;
        }
        private static City ParseCity(byte[] data)
        {
            var result = new City();

            result.所属 = Encoding.GetEncoding("BIG5").GetString(data, 30, 16).Split('\0')[0];
            result.太守 = Encoding.GetEncoding("BIG5").GetString(data, 46, 16).Split('\0')[0];
            result.军师 = Encoding.GetEncoding("BIG5").GetString(data, 62, 16).Split('\0')[0];
            result.开发 = BitConverter.ToInt32(data, 16);
            result.金钱 = BitConverter.ToInt32(data, 20);
            result.人口 = BitConverter.ToInt32(data, 24);
            result.预备兵 = BitConverter.ToInt16(data, 26);

            return result;
        }
        private static General ParseGeneral(byte[] data)
        {
            var result = new General();

            result.功勋 = BitConverter.ToInt16(data, 12);
            result.忠诚度 = BitConverter.ToInt16(data, 14);
            result.当前生命 = BitConverter.ToInt16(data, 16);
            result.当前技力 = BitConverter.ToInt16(data, 18);
            result.最大生命 = BitConverter.ToInt16(data, 20);
            result.最大技力 = BitConverter.ToInt16(data, 22);
            result.智力 = BitConverter.ToInt16(data, 24);
            result.等级 = BitConverter.ToInt16(data, 26);
            result.士气 = BitConverter.ToInt16(data, 28);
            result.武力 = BitConverter.ToInt16(data, 30);
            result.经验 = BitConverter.ToInt16(data, 56);
            result.武将名 = Encoding.GetEncoding("BIG5").GetString(data, 215, 16).Split('\0')[0];
            result.所在 = Encoding.GetEncoding("BIG5").GetString(data, 231, 16).Split('\0')[0];
            result.书籍 = Encoding.GetEncoding("BIG5").GetString(data, 247, 16).Split('\0')[0];
            result.坐骑 = Encoding.GetEncoding("BIG5").GetString(data, 263, 16).Split('\0')[0];
            result.所属 = Encoding.GetEncoding("BIG5").GetString(data, 279, 16).Split('\0')[0];
            result.兵器 = Encoding.GetEncoding("BIG5").GetString(data, 295, 16).Split('\0')[0];

            var periodNum = 7;
            var periodNames = new[] { "黄巾之乱-184年", "讨伐董卓-189年", "群雄割据-196年", "官渡之战-200年", "赤壁之战-208年", "三国鼎立-219年", "天下归魏-230年" };
            var index = 343;
            for (int i = 0; i < periodNum; i++)
            {
                var info = new PlaceInfo();
                info.时期 = periodNames[i];
                info.地点 = Encoding.GetEncoding("BIG5").GetString(data, index + 1, 16).Split('\0')[0];
                info.在野 = !string.IsNullOrEmpty(info.地点) && !BitConverter.ToBoolean(data, index) ? "在野" : "";
                result.PlaceInfos.Add(info);
                index += 17;
            }

            return result;
        }

        private static NATION ParseNATION(byte[] buff)
        {
            return new NATION();
        }

        private static NA_MAN ParseNA_MAN(byte[] buff)
        {
            return new NA_MAN();
        }

        private static SG3_ODIN ParseSG3_ODIN(byte[] buff)
        {
            return new SG3_ODIN();
        }

        private static string GetBlockName(byte[] buff, int index)
        {
            return Encoding.GetEncoding("BIG5").GetString(buff.Skip(index).Take(12).ToArray());
        }

        private static int IndexOf(byte[] srcBytes, byte[] searchBytes)
        {
            if (srcBytes == null) { return -1; }
            if (searchBytes == null) { return -1; }
            if (srcBytes.Length == 0) { return -1; }
            if (searchBytes.Length == 0) { return -1; }
            if (srcBytes.Length < searchBytes.Length) { return -1; }
            for (int i = 0; i < srcBytes.Length - searchBytes.Length; i++)
            {
                if (srcBytes[i] == searchBytes[0])
                {
                    if (searchBytes.Length == 1) { return i; }
                    bool flag = true;
                    for (int j = 1; j < searchBytes.Length; j++)
                    {
                        if (srcBytes[i + j] != searchBytes[j])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) { return i; }
                }
            }
            return -1;
        }
    }
}
