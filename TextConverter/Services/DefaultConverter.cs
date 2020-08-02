using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextConverter.Lib.Interfaces;

namespace TextConverter.Services
{
    class DefaultConverter : ITextConverterService, ISampleTextService
    {

        public string Name()
        {
            return "無変換";
        }

        public List<string> Convert(List<string> inputString)
        {
            return inputString;
        }


        public List<string> SampleText()
        {
            return $@"色は匂へど　散りぬるを
我が世誰そ　常ならむ
有為の奥山　今日越えて
浅き夢見じ　酔ひもせず".Split("\n").ToList<string>();
        }
    }
}
