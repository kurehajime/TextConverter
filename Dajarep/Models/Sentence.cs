using System.Collections.Generic;

namespace Dajarep.Models
{
    class Sentence
    {
        public string Str { get; set; }
        public string Kana { get; set; }
        public string Yomi { get; set; }
        public List<Word> Words { get; set; }
    }
}
