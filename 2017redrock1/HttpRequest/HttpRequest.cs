using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2017redrock1.HttpRequest
{
    class HttpRequest
    {
        public static async Task<string> GetRequest(string url)
        {
            try
            {
                HttpClient httpClint = new HttpClient();
                string request = await httpClint.GetStringAsync(new Uri(url));
                return request;
            }
            catch
            {
                return null;
            }
        }

        public static List<Song> GetList(List<Song> list, string content)
        {
            try
            {
                content = content.Replace("\"", "");
                //Console.WriteLine(json);
                string pat = @"(list:\[{).*(}\])";
                Match match = Regex.Match(content, pat);
                string json1 = match.Value;
                json1 = json1.Replace("list:[", "");
                json1 = json1.Remove(json1.Length - 1, 1);
                //Console.WriteLine(json1);
                Regex R = new Regex(@"{albumid:.*?vid:}");
                //Match A = R.Match(json1);
                //Console.WriteLine(A.Value);
                MatchCollection matchs = R.Matches(json1);
                foreach (Match m in matchs)
                {
                    Song song = new Song();
                    string json2 = m.Value;
                    //匹配后得到的songname
                    Regex regexName = new Regex(@"songname:.*?,");
                    Match matchSongname = regexName.Match(json2);
                    string _songname = matchSongname.Value.Replace("songname:", "").Replace(",", "");
                    song.SongName = _songname;
                    //匹配后的后的singer
                    Regex regexSinger = new Regex(@"\[.*\]");
                    Match matchSinger = regexSinger.Match(json2);
                    Regex singername = new Regex(@"name:\s*.*,");
                    Match matchSingername = singername.Match(matchSinger.Value);
                    string _songer = matchSingername.Value.Replace("name:", "").Replace(",", "");
                    song.SingerName = _songer;
                    list.Add(song);
                }
                return list;
            }
            catch
            {
                return null;
            }
            return list;
        }
    }
}
