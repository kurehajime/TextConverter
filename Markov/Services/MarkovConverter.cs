using NMeCab;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Navigation;
using TextConverter.Lib.Interfaces;

namespace Markov.Services
{
    public class MarkovConverter : ITextConverterService, ISampleTextService
    {
        #region ITextConverterService

        public List<string> Convert(List<string> inputString)
        {
            throw new NotImplementedException();
        }

        public string Name()
        {
            return "ワードサラダ";
        }

        #endregion

        #region ISampleTextService

        public List<string> SampleText()
        {
            throw new NotImplementedException();
        }

        #endregion

        private MeCabIpaDicTagger tagger;


        public MarkovConverter()
        {
            var dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var dicPath = Path.Combine(Path.GetDirectoryName(dllPath), "IpaDic");
            this.tagger = MeCabIpaDicTagger.Create(dicPath);
        }

        private Dictionary<string, List<string>> makeDictionary(string text)
        {

            var dict = new Dictionary<string, List<string>>();
            text = Regex.Replace(text, " ", "");
            text = Regex.Replace(text, "　", "");
            text = Regex.Replace(text, "\r\n", "。");
            text = Regex.Replace(text, "\n", "。");
            text = Regex.Replace(text, "「", "。");
            text = Regex.Replace(text, "」", "。");
            text = Regex.Replace(text, ",", "。");
            text = Regex.Replace(text, ")", "。");
            text = Regex.Replace(text, "(", "。");
            text = Regex.Replace(text, "?", "。");
            text = Regex.Replace(text, "？", "。");
            text = Regex.Replace(text, "!", "。");
            text = Regex.Replace(text, "！", "。");

            var tokens = tagger.Parse(text);
            var EOSKEYWORD = ".\n。".ToCharArray();


            //辞書を作る
            //dict[前の単語]=["次の単語","次の単語","次の単語","次の単語"]
            for (var i = 0; i < tokens.Length; i++)
            {
                string now = tokens[i].Surface;
                string next;
                if (i + 1 < tokens.Length)
                {
                    next = tokens[i + 1].Surface;
                }
                else
                {
                    next = "EOS";
                }

                //該当キーワードが最終文字だったらEOSに置き換え、BOSに次のキーワードを追加。
                if (now.IndexOfAny(EOSKEYWORD) != -1)
                {
                    if (dict["BOS"] == null)
                    {
                        dict["BOS"] = new List<string>();
                    }
                    if (next.IndexOfAny(EOSKEYWORD) != -1)
                    {
                        dict["BOS"].Add(next);

                    }
                    continue;
                }
                //次の文字が最終文字ならEOSに置き換え
                if (next.IndexOfAny(EOSKEYWORD) != -1)
                {
                    next = "EOS";
                }
                //辞書に追加
                if (dict[now] == null)
                {
                    dict[now] = new List<string>();
                }
                dict[now].Add(next);
            }
            return dict;
        }
    }
}
