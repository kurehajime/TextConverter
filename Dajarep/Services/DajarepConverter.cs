using Dajarep.Models;
using Microsoft.VisualBasic;
using NMeCab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TextConverter.Lib.Interfaces;

namespace Dajarep.Services
{
    public class DajarepConverter : ITextConverterService, ISampleTextService
    {
        #region ITextConverterService

        public List<string> Convert(List<string> inputString)
        {
            return Dajarep(Strings.Join(inputString.ToArray(), Environment.NewLine));
        }

        public string Name()
        {
            return "ダジャレ抽出";
        }


        #endregion

        #region ISampleTextService

        public List<string> SampleText()
        {
            return @"人民の人民による人民のための政治
アルミ缶の上にあるミカン
トンネルを抜けるとそこは雪国であった
智代子のチョコ
布団が吹っ飛んだ
我輩は猫である
猫が寝転んだ
その意見にはついていけん
靴を靴箱に入れる
傘を貸さない
イカは如何なものか
親譲りの無鉄砲で子供の時から損ばかりしている
マイケル・ジョーダンが冗談を言った
知事が縮む
鶏には取り憑かない
破壊についての和解".Split(Environment.NewLine).ToList();
        }

        #endregion

        private MeCabIpaDicTagger tagger;

        public DajarepConverter()
        {
            var dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var dicPath = Path.Combine(Path.GetDirectoryName(dllPath), "IpaDic");
            this.tagger = MeCabIpaDicTagger.Create(dicPath);
        }

        /// <summary>
        /// 本文から省略可能文字を消したパターンを返す。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string fixSentence(string text)
        {
            text = Strings.Replace(text, "ッ", "");
            text = Strings.Replace(text, "ー", "");
            text = Strings.Replace(text, "、", "");
            text = Strings.Replace(text, ",", "");
            text = Strings.Replace(text, "　", "");
            text = Strings.Replace(text, " ", "");
            return text;
        }

        /// <summary>
        /// 置き換え可能な文字を考慮した正規表現を返す。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string fixWord(string text)
        {
            text = Strings.Replace(text, "ッ", "[ツッ]?");
            text = Strings.Replace(text, "ァ", "[アァ]?");
            text = Strings.Replace(text, "ィ", "[イィ]?");
            text = Strings.Replace(text, "ゥ", "[ウゥ]?");
            text = Strings.Replace(text, "ェ", "[エェ]?");
            text = Strings.Replace(text, "ォ", "[オォ]?");
            text = Strings.Replace(text, "ズ", "[ズヅ]");
            text = Strings.Replace(text, "ヅ", "[ズヅ]");
            text = Strings.Replace(text, "ヂ", "[ジヂ]");
            text = Strings.Replace(text, "ジ", "[ジヂ]");
            text = Regex.Replace(text, "([アカサタナハマヤラワャ])ー", "$1[アァ]?");
            text = Regex.Replace(text, "([イキシチニヒミリ])ー", "$1[イィ]?");
            text = Regex.Replace(text, "([ウクスツヌフムユルュ])ー", "$1[ウゥ]?");
            text = Regex.Replace(text, "([エケセテネへメレ])ー", "$1[エェ]?");
            text = Regex.Replace(text, "([オコソトノホモヨロヲョ])ー", "$1[ウゥオォ]?");
            text = Strings.Replace(text, "ャ", "[ヤャ]");
            text = Strings.Replace(text, "ュ", "[ユュ]");
            text = Strings.Replace(text, "ョ", "[ヨョ]");
            text = Strings.Replace(text, "ー", "[ー]?");

            return text;
        }

        /// <summary>
        /// テキストからsentenceオブジェクトを作る。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<Sentence> getSentences(string text)
        {
            var sentences = new List<Sentence>();

            text = Strings.Replace(text, "。", "\n");
            text = Strings.Replace(text, ".", "\n");
            text = Strings.Replace(text, "?", "?\n");
            text = Strings.Replace(text, "!", "!\n");
            text = Strings.Replace(text, "？", "？\n");
            text = Strings.Replace(text, "！", "！\n");
            var senstr = Strings.Split(text, "\n");

            for (var i = 0; i < senstr.Length; i++)
            {
                var tokens = tagger.Parse(senstr[i]);
                var words = new List<Word>();
                string kana = "";
                string yomi = "";

                for (var j = 0; j < tokens.Length; j++)
                {
                    var token = tokens[j];
                    if (token.Feature.Length > 7)
                    {
                        var w = new Word()
                        {
                            OriginalText = token.OriginalForm,
                            Kana = token.Reading,
                            WordType = token.PartsOfSpeech
                        };
                        words.Add(w);
                        kana += token.Reading;
                        yomi += token.Pronounciation;
                    }
                }
                sentences.Add(new Sentence()
                {
                    OriginalText = senstr[i],
                    Words = words,
                    Kana = kana,
                    Yomi = yomi
                });
            }
            return sentences;
        }

        /// <summary>
        /// 駄洒落かどうか
        /// </summary>
        /// <param name="sen"></param>
        /// <returns></returns>
        private bool isDajare(Sentence sen)
        {
            var words = sen.Words;

            for (var i = 0; i < words.Count; i++)
            {
                var word = words[i];

                if (word.WordType == "名詞" && word.Kana.Length > 1)
                {
                    var hitStr = Regex.Matches( sen.OriginalText, word.OriginalText);
                    var hitKana1 = Regex.Matches( sen.Kana, fixWord(word.Kana));
                    var hitKana2 = Regex.Matches( fixSentence(sen.Kana), fixWord(word.Kana));
                    var hitKana3 = Regex.Matches(sen.Yomi, fixWord(word.Kana));
                    var hitKana4 = Regex.Matches( fixSentence(sen.Yomi), fixWord(word.Kana));

                    //ある単語における　原文の一致文字列数<フリガナでの一致文字列数　→　駄洒落の読みが存在
                    if (hitStr.Count < most(hitKana1.Count, hitKana2.Count, hitKana3.Count, hitKana4.Count))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 最大値
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int most(params int[] num)
        {
            var max = 0;
            foreach (var n in num)
            {
                if (n > max)
                {
                    max = n;
                }
            }
            return max;
        }

        /// <summary>
        /// ダジャレ抽出
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<string> Dajarep(string text)
        {
            var dajares = new List<string>();
            var sentences = getSentences(text);

            for (var i = 0; i < sentences.Count; i++) {
                if (isDajare(sentences[i])) 
                {
                    dajares.Add(sentences[i].OriginalText.Trim());
                }
            }
            return dajares;
        }
    }
}
