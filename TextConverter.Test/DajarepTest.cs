using Dajarep.Services;
using NUnit.Framework;
using System;
using System.Linq;

namespace TextConverter.Test
{
    public class DajarepTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ダジャレ抽出()
        {
            var converter = new DajarepConverter();
            var result = converter.Convert(@"人民の人民による人民のための政治
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
破壊についての和解"
.Split(Environment.NewLine).ToList());

            var expect = @"アルミ缶の上にあるミカン
智代子のチョコ
布団が吹っ飛んだ
猫が寝転んだ
その意見にはついていけん
傘を貸さない
イカは如何なものか
マイケル・ジョーダンが冗談を言った
知事が縮む
鶏には取り憑かない"
.Split(Environment.NewLine);

            Assert.AreEqual(result, expect);
        }
    }
}