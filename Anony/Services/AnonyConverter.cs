using NMeCab;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TextConverter.Lib.Interfaces;

namespace Anony.Services
{
    class AnonyConverter : ITextConverterService, ISampleTextService
    {
        #region ITextConverterService

        public List<string> Convert(List<string> inputString)
        {
            return this.Anony(string.Join("\n", inputString)).Split("\n").ToList();
        }

        public string Name()
        {
            return "匿名化";
        }

        #endregion

        #region ISampleTextService

        public List<string> SampleText()
        {
            return @"筒井康隆は、日本の小説家・劇作家・俳優である。
ホリプロ所属。身長166cm。小松左京、星新一と並んで「SF御三家」とも称される。
パロディやスラップスティックな笑いを得意とし、初期にはナンセンスなSF作品を多数発表。
1970年代よりメタフィクションの手法を用いた前衛的な作品が増え、エンターテインメントや純文学といった境界を越える実験作を多数発表している。".Split("\n").ToList();
        }

        #endregion

        private MeCabIpaDicTagger tagger;

        public AnonyConverter()
        {
            var dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var dicPath = Path.Combine(Path.GetDirectoryName(dllPath), "IpaDic");
            this.tagger = MeCabIpaDicTagger.Create(dicPath);
        }

        /// <summary>
        /// 匿名化
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string Anony(string text)
        {
            var tokens = tagger.Parse(text);

            var anonyText = "";
            var nameCount = 0;

            for (var j = 0; j < tokens.Length; j++)
            {
                var token = tokens[j];
                if (token.Feature.Length > 7)
                {
                    if (token.PartsOfSpeechSection2 == "人名" && token.PartsOfSpeechSection1 == "固有名詞")
                    {
                        if (nameCount == 0)
                        {
                            anonyText += word2Initial(token.Reading);

                        }
                        else if (nameCount == 1)
                        {
                            anonyText += "・";
                            anonyText += word2Initial(token.Reading);
                        }
                        nameCount++;
                    }
                    else
                    {
                        anonyText += token.Surface;
                        nameCount = 0;

                    }
                }
                else if (token.Feature.Length > 0)
                {
                    anonyText += token.Surface;
                    nameCount = 0;
                }
            }
            return anonyText;
        }

        /// <summary>
        /// イニシャル変換
        /// </summary>
        /// <param name="kana"></param>
        /// <returns></returns>
        private string word2Initial(string kana)
        {
            kana = kana.First().ToString();
            kana = Regex.Replace(kana, "[アァ]", "A");
            kana = Regex.Replace(kana, "[イィ]", "I");
            kana = Regex.Replace(kana, "[ウゥ]", "U");
            kana = Regex.Replace(kana, "[エェ]", "E");
            kana = Regex.Replace(kana, "[オォ]", "O");
            kana = Regex.Replace(kana, "[カキクケコ]", "K");
            kana = Regex.Replace(kana, "[サシスセソ]", "S");
            kana = Regex.Replace(kana, "[タツテト]", "T");
            kana = Regex.Replace(kana, "[チ]", "T");
            kana = Regex.Replace(kana, "[ナニヌネノ]", "N");
            kana = Regex.Replace(kana, "[ハヒヘホ]", "H");
            kana = Regex.Replace(kana, "[フ]", "F");
            kana = Regex.Replace(kana, "[マミムメモ]", "M");
            kana = Regex.Replace(kana, "[ヤユヨ]", "Y");
            kana = Regex.Replace(kana, "[ラリルレロ]", "R");
            kana = Regex.Replace(kana, "[ワヲ]", "W");
            kana = Regex.Replace(kana, "[ン]", "N");
            kana = Regex.Replace(kana, "[ガギグゲゴ]", "G");
            kana = Regex.Replace(kana, "[ザズゼゾ]", "Z");
            kana = Regex.Replace(kana, "[ダヂヅデド]", "D");
            kana = Regex.Replace(kana, "[ジ]", "J");
            kana = Regex.Replace(kana, "[パピプペポ]", "P");
            kana = Regex.Replace(kana, "[バビブベボ]", "B");
            return kana;
        }
    }
}
