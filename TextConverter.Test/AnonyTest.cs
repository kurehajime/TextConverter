using Anony.Services;
using NUnit.Framework;
using System;
using System.Linq;

namespace TextConverter.Test
{
    public class AnonyTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void 匿名化()
        {
            var converter = new AnonyConverter();
            var result = converter.Convert(@"筒井康隆は、日本の小説家・劇作家・俳優である。
ホリプロ所属。身長166cm。小松左京、星新一と並んで「SF御三家」とも称される。
パロディやスラップスティックな笑いを得意とし、初期にはナンセンスなSF作品を多数発表。
1970年代よりメタフィクションの手法を用いた前衛的な作品が増え、エンターテインメントや純文学といった境界を越える実験作を多数発表している。"
.Split(Environment.NewLine).ToList());

            var expect = @"T・Yは、日本の小説家・劇作家・俳優である。
ホリプロ所属。身長166cm。K・S、H・Sと並んで「SF御三家」とも称される。
パロディやスラップスティックな笑いを得意とし、初期にはナンセンスなSF作品を多数発表。
1970年代よりメタフィクションの手法を用いた前衛的な作品が増え、エンターテインメントや純文学といった境界を越える実験作を多数発表している。"
.Split(Environment.NewLine);

            Assert.AreEqual(result, expect);
        }
    }
}