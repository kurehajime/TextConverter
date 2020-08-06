﻿using Microsoft.VisualBasic;
using NMeCab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TextConverter.Lib.Interfaces;

namespace Markov.Services
{
    public class MarkovConverter : ITextConverterService, ISampleTextService
    {
        #region ITextConverterService

        public List<string> Convert(List<string> inputString)
        {
            var bs = new byte[4];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bs);
            }
            var seed = BitConverter.ToInt32(bs, 0);
            return makeMarkov(Strings.Join(inputString.ToArray(),Environment.NewLine), seed, 30, 10)
                .Split(Environment.NewLine).ToList();
        }

        public List<string> Convert(List<string> inputString,int seed,int betterLen, int retryCount)
        {
            return makeMarkov(Strings.Join(inputString.ToArray(), Environment.NewLine), seed, betterLen, retryCount)
                .Split(Environment.NewLine).ToList();
        }

        public string Name()
        {
            return "ワードサラダ";
        }

        #endregion

        #region ISampleTextService

        public List<string> SampleText()
        {
            return @"人間はひとくきの葦にすぎない
自然の中で最も弱いものである
だが、それは考える葦である
その日その日が一年中の最善の日である
チャンスは貯蓄できない
希望さえあればどんな所にでも辿りつけると決心している
死者にたいする最高の手向けは、悲しみではなく感謝だ
人生は道路のようなものだ
一番の近道は、たいてい一番悪い道だ
世の中は、君の理解する以上に栄光に満ちている
人付き合いがうまいというのは、人を許せるということだ
生きるとは呼吸することではない
行動することだ
人生は学校である
そこでは幸福より不幸の方が良い教師である
若い女は美しい
しかし、老いた女はもっと美しい
今日という日は、残りの人生の最初の一日
卵を割らなければ、オムレツは作れない
至上の処世術は、妥協することなく適応することである
人間、志を立てるのに遅すぎるということはない
確かに世の中は不公平だ
美人や美青年がいる一方で、あなたがいる
だからなんだ！？それを嘆いてどうするの？太陽が輝くかぎり、希望もまた輝く
行動は必ずしも幸福をもたらさないかも知れないが、行動のない所に、幸福は、生まれない
成し遂げんとした志をただ一回の敗北によって捨ててはいけない
花を与えるのは自然であり、それを編んで花輪にするのが芸術である
やってみせて、言って聞かせて、やらせてみて、ほめてやらねば人は動かじ
話し合い、耳を傾け、承認し、任せてやらねば、人は育たず
やっている、姿を感謝で見守って、信頼せねば、人は実らず
「みんなと同じ事はしたくない」という、みんなと同じセリフ
どこに行こうとしているのかわかっていなければ、どの道を通ってもどこにも行けない
一生の間に一人の人間でも幸福にすることが出来れば自分の幸福なのだ自分の道を進む人は、誰でも英雄です
過去も未来も存在せず、あるのは現在と言う瞬間だけだ知は力なり下手糞の上級者への道のりは己が下手さを知りて一歩目人生は一箱のマッチに似ている
重大に扱うのはばかばかしい
重大に扱わねば危険である
社長なんて偉くもなんともない
課長、部長、包丁、盲腸と同じだ
要するに命令系統をはっきりさせる記号にすぎない
人生はクローズアップで見れば悲劇ロングショットで見れば喜劇善行は悪行と同じように、人の憎悪を招くものである
何も咲かない寒い日は、下へ下へと根をのばせ、やがて大きな花が咲く
昨日は昨日
大事なのは明日だ
よく聞け、金を残して死ぬ者は下だ
仕事を残して死ぬ者は中だ
人を残して死ぬ者は上だ
よく覚えておけ人は死ぬかもしれないし、国は興亡するかもしれないが、理念は生き続ける
諸君は必ず失敗する
成功があるかもしれませぬけど、成功より失敗が多い
失敗に落胆しなさるな
失敗に打ち勝たねばならぬ
叱ってくれる人を持つことは大きな幸福であるPKを外す事ができるのは、PKを蹴る勇気を持った者だけだ私は先輩のギャルソンに、お客様は王様であると教えられました
しかし、先輩は言いました
王様の中には首をはねられた奴も大勢いると例え、例えですね、明日死ぬとしても、やり直しちゃいけないって、誰が決めたんですか？誰が決めたんですか？学生時代に大事なのは、何を学んだかではなくて、どうやって学んだかということ
勝つことは、人を止める
負けることは、人を進める
イジメは絶対悪や絶対になくならへん!あんなおもろい事誰がやめんねん!お前な、イジメられてるって事はチャンスなんやぞ!?なんで笑いにもっていかん!?死ぬことに意味を持つな
生きるんだ！あのね、立派な人になんかにならなくてもいいの
感じの良い人になって下さい
「Iloveyou」という言葉に初めて二葉亭四迷がぶつかったとき、どう訳すか悩んだらしいんですよ
今みたいに「好き」「愛している」使わない時代ですから
それで、何と訳したと思います？「私は死んでもいい」・・・と
あのね、立派な人になんかにならなくてもいいの
感じの良い人になって下さい
何でも謝って済むことではないけれど謝れない人間は最低だ正しいという字は「一つ」「止まる」と書きます
「どうか一つ止まって判断できる人になって下さい大きな志を持つ者は小さな屈辱に耐えよ、耐えられるはずだ
人生は勝ち負けじゃない負けたって言わない人が勝ちなのよ幸せになろうと思わないで下さい幸せをつかみに行って幸せをつかんだ人は１人もいません
幸せは感じるものです人生で起こることは、すべて、皿の上でも起こる
人生で大事なことは、何を食べるか、ではなく、どこで食べるか、である人生とオムレツは、タイミングが大事
トマトに塩をかければ、サラダになる
まずい食材はない
まずい料理があるだけだ
智に働けば角が立つ
情に棹せば流される
意地を通せば窮屈だ
兎角にこの世は住みにくい
結婚は顔を赤くするほど嬉しいものでもなければ、恥ずかしいものでもないよ
彼らにとって絶対に必要なものはお互いだけで、お互いだけが、彼らにはまた充分であった
彼らは山の中にいる心を抱いて、都会に住んでいた
恋は罪悪ですよ
運命は神の考えることだ
人間は人間らしく働けばそれで結構である
如何に至徳の人でもどこかしらに悪いところがあるように、人も解釈し自分でも認めつつあるのは疑いもない真実だろうと思う
吾人は自由を欲して自由を得た
自由を得た結果、不自由を感じて困っている
戦場でおびえたことを、恥じることは決してない
恥ずべきは、人間の尊厳を根こそぎ奪い取る、戦争や社会体制なのだ人生を賭けるに値するのは、夢だけだと思いませんか？あきらめたらそこで試合終了だよ
「負けたことがある」というのがいつか大きな財産になる
いいか！これから先、お前から夢や希望を奪おうとする事がたくさんある
両親がお前の大事なものを捨てたようにだ！でも、負けるんじゃないぞ
夢や希望ってやつは捨てるのは簡単だが、取り戻すのはすごく難しいんだ
あんまりHな目をしていると女の子に嫌われちゃうぞ
温室育ちのバラより、野生のバラの方が強いんだぜ
初めて流す悔し涙か
男は何度か泣いて本当の男になるのさ
さあ、我が腕の中で息絶えるがよい
もはや再び生き返らぬよう、そなたらのはらわたを喰らい尽くしてやるわ
買わない奴は日本を去れ
あいつの場合に限って常に最悪のケースを想定しろ
奴は必ずその少し斜め上を行く
あの御二人から何故このような悪魔が生まれてこなければならなかったのか
それを思うと…それはもう残念でならない女の涙は信用してはいけない
これは数少ない宇宙共通の真理だ二人の人間が愛し合えば、ハッピーエンドはあり得ない
二人の間に恋がなくなったとき、愛し愛された昔を恥ずかしく思わない人はほとんどいない恋愛は幸福を殺し、幸福は恋愛を殺す
恋の喜びが結局悲しみをもたらすということは、もういろいろな女の例ではっきりしているんですもの
私は恋も悩みも両方捨てますから、悪いことも起こらないでしょう
恋の悲しみを知らぬものに恋の味は話せない
恋の苦しみは、あらゆるほかの悦びよりずっと愉しい
恋わずらいの人は、ある種の病人のように自分自身が医者になる
苦悩の原因をなした相手から癒してもらえることはないのだから、結局は、その苦悩の中に薬を見出すのである
恋とは巨大な矛盾であります
それなくしては生きられず、しかもそれによって傷つく
男は別れの言い方が分からない
女はそれを言うべき時が分からない
私の愛人が他の男によって幸せになるのを見るくらいなら、私はその女が不幸になるのを見たほうがマシだ
恋という奴は一度失敗してみるのもいいかも知れぬ、そこで初めて味がつくような気がするね
女性が綺麗になる方法は二つあります
「いい恋をすること」と「悪い恋をやめてしまうこと」です
恋をすることは苦しむことだ
苦しみたくないなら、恋をしてはいけない
でもそうすると、恋をしていないことでまた苦しむことになる
嫉妬は常に恋と共に生まれる
しかし必ずしも恋と共には滅びない
恋の味を痛烈に味わいたいならば、それは片思いか失恋する以外にないだろう"
.Split(Environment.NewLine).ToList();
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
            text = Regex.Replace(text, "\\)", "。");
            text = Regex.Replace(text, "\\(", "。");
            text = Regex.Replace(text, "\\?", "。");
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
                    if (!dict.ContainsKey("BOS"))
                    {
                        dict["BOS"] = new List<string>();
                    }
                    if (next.IndexOfAny(EOSKEYWORD) == -1)
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
                if (!dict.ContainsKey(now))
                {
                    dict[now] = new List<string>();
                }
                dict[now].Add(next);
            }
            return dict;
        }
        private string makeWord(Dictionary<string, List<string>> dict, Random rand)
        {
            var rtn = "";

            var now = dict["BOS"][rand.Next(0, dict["BOS"].Count)];

            for (var i = 0; i < 100; i++)
            {
                if (now == "EOS")
                {
                    break;
                }
                rtn += now;

                if (dict.ContainsKey(now) && dict[now].Count != 0)
                {
                    now = dict[now][rand.Next(dict[now].Count)];
                }

            }
            return rtn.Replace("EOS", "");
        }

        private string makeMarkov(string text, int seed, int betterLen, int retryCount)
        {

            var rand = new Random(seed);
            var dict = makeDictionary(text);
            var rtn = "";

            for (var i = 0; i <= retryCount; i++)
            {
                var line = makeWord(dict, rand);
                if (Math.Abs(rtn.Length - betterLen) > Math.Abs(line.Length - betterLen))
                {
                    rtn = line;
                }
            }
            return rtn;
        }
    }
}
