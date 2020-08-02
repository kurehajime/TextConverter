using System.Collections.Generic;

namespace TextConverter.Lib.Interfaces
{
    public interface ITextConverterService
    {
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string Name();

        /// <summary>
        /// 文字列変換
        /// </summary>
        /// <param name="inputString">入力</param>
        /// <returns>出力</returns>
        public List<string> Convert(List<string> inputString);

    }
}
