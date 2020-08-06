using Markov.Services;
using NUnit.Framework;
using System;

namespace TextConverter.Test
{
    public class MarkovTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ランダム文字列()
        {
            var converter = new MarkovConverter();
            var result = converter.Convert(converter.SampleText(),123,30,10);

            var expect = @"だからなんだ二人はなく感謝でも咲かないことであるの葦で食べるか"
.Split(Environment.NewLine);

            Assert.AreEqual(result, expect);
        }
    }
}