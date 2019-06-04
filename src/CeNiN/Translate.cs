using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CeNiN
{
    public class Translate
    {
        public Dictionary<string,string> GetResult(string[] originArr)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var content = GetJson(originArr);
            TransResult res = JsonConvert.DeserializeObject<TransResult>(content);
            var resultStrOrigin = res.trans_result[0].dst;
            resultStrOrigin = HttpUtility.UrlDecode(resultStrOrigin);
            foreach (var item in res.trans_result)
            {
                if (!dic.ContainsKey(item.src.ToString()))
                {
                    dic.Add(item.src.ToString(), item.dst.ToString());
                }
            }
            return dic;
        }
        public string GetJson(string[] originArr)
        {
            var client = new RestClient("http://api.fanyi.baidu.com");
            var request = new RestRequest("/api/trans/vip/translate", Method.POST);
            string originStr = "";
            foreach (var item in originArr)
            {
                originStr += item.Split(',')[0] + "\n";
            }
            request.AddParameter("q", originStr);
            request.AddParameter("from", "en");
            request.AddParameter("to", "zh");
            request.AddParameter("appid", "20190604000304734");
            request.AddParameter("salt", "1435660288");
            request.AddParameter("sign", getMd5("20190604000304734" + originStr + "1435660288" + "wBEFcBPotrQ8OHIPWRBI"));
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString());
        }

        public string getMd5(string allStr)
        {
            var md5 = new MD5CryptoServiceProvider();
            var result = Encoding.UTF8.GetBytes(allStr);
            var output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
    }
    public class TransResult
    {
        public String from { get; set; }
        public String to { get; set; }
        public List<Trans_result> trans_result { get; set; }

    }
    public class Trans_result
    {
        public String src { get; set; }
        public String dst { get; set; }
    }
}
