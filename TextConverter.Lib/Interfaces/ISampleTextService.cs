using System.Collections.Generic;

namespace TextConverter.Lib.Interfaces
{
    public interface ISampleTextService
    {

        /// <summary>
        /// サンプルテキスト
        /// </summary>
        /// <returns></returns>
        public List<string> SampleText();
    }
}
