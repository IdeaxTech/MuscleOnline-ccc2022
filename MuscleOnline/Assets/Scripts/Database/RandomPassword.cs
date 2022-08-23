using UnityEngine;
using System.Linq;


public class RandomPassword : MonoBehaviour
{
    private const string ASCII_NUMBER = "0123456789";                       //数字
    private const string ASCII_UPPER_ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";  //英字大文字
    private const string ASCII_LOWER_ALPHA = "abcdefghijklmnopqrstuvwxyz";  //英字小文字
    private const string ASCII_MARK = "!\"#$%&'()*+-/<=>?@;[]^";            //記号

    private static readonly System.Random rng = new System.Random(); //ランダムクラス

    /// <summary>
    /// ランダムな文字列を生成する。
    /// </summary>
    /// <param name="length">文字列の長さ</param>
    /// <returns>数字、大文字、小文字および記号を１文字以上含むランダムな文字列</returns>
    public static string Generate(int length)
    {
        string allSource = ASCII_NUMBER + ASCII_UPPER_ALPHA + ASCII_LOWER_ALPHA + ASCII_MARK;

        //各文字種を最低１文字含める
        string password = Choice(ASCII_NUMBER)
                        + Choice(ASCII_UPPER_ALPHA)
                        + Choice(ASCII_LOWER_ALPHA)
                        + Choice(ASCII_MARK);

        //任意の文字種でランダムな文字を生成
        int cnt = length - password.Length;
        for (int i = 0; i < cnt; i++)
        {
            password += Choice(allSource);
        }

        //生成した文字列をシャッフルして返す
        return string.Join("", password.OrderBy(n => rng.Next()));
    }

    /// <summary>
    /// 指定された文字列ソースからランダムに１文字選択する
    /// </summary>
    /// <param name="source">文字列ソース</param>
    /// <returns>ランダムに選択した文字</returns>
    private static string Choice(string source)
    {
        return source[rng.Next(0, source.Length - 1)].ToString();
    }
}
