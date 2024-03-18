using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace isf_candidates
{
    static public class Utils_Str
    {

        /// <summary>
        /// Delete chars in the beginning (to divider), while the length was less than aMaxSize (To delete rows: aDivider="\n");
        /// </summary>
        /// <param name="aText"></param>
        /// <param name="aMaxSize"></param>
        /// <param name="aDivider">"": any char</param>
        /// <returns></returns>
        static public string DeleteToSize(string aText, UInt32 aMaxSize, string aDivider = "\n")
        {
            Int32 xDeleteLen = BeginPosOfSize(aText, aMaxSize, aDivider);
            if (xDeleteLen>0)
                aText = aText.Remove(0, xDeleteLen);
            return aText;
        }

        /// <summary>
        /// Return index of char after newline in the beginning, while the length was less than 100 aMaxSize
        /// </summary>
        /// <param name="aText"></param>
        /// <param name="aMaxSize"></param>
        /// <param name="aDivider"></param>
        /// <returns></returns>
        static public Int32 BeginPosOfSize(string aText, UInt32 aMaxSize, string aDivider = "\n")
        {
            Int32 xResult = 0;
            if (aText.Length >= aMaxSize)
            {
                xResult = 0;
                if (String.IsNullOrEmpty(aDivider))
                    xResult = (Int32)(aText.Length - aMaxSize);
                else
                    while ((aText.Length - xResult) >= aMaxSize)
                        xResult = aText.IndexOf(aDivider, xResult) + 1;
            }
            return xResult;
        }

        static public string RemoveControlCharacters(string aString)
        {
            if (aString == null) return null;

            StringBuilder newString = new StringBuilder();
            char ch;

            for (int i = 0; i < aString.Length; i++)
            {
                ch = aString[i];
                if (!char.IsControl(ch))
                {
                    newString.Append(ch);
                }
            }
            return newString.ToString();
        }

        static public string ReplaceControlCharacters(string aString, char aChar)
        {
            if (aString == null) return null;

            StringBuilder newString = new StringBuilder();
            char ch;

            for (int i = 0; i < aString.Length; i++)
            {
                ch = aString[i];
                if (char.IsControl(ch))
                {
                    ch = aChar;
                }
                newString.Append(ch);
            }
            return newString.ToString();
        }

        static public string ReplaceNonDigLetCharacters(string aString, char aChar)
        {
            if (aString == null) return null;

            StringBuilder newString = new StringBuilder();
            char ch;

            for (int i = 0; i < aString.Length; i++)
            {
                ch = aString[i];
                if (!char.IsLetterOrDigit(ch))
                {
                    ch = aChar;
                }
                newString.Append(ch);
            }
            return newString.ToString();
        }

        static public string StrList_GetMaxSubstring(IList<string> aList)
        {
            string xResult = null;
            if ((aList != null) && (aList.Count > 0))
            {
                xResult = aList[0];
                int i = 1;
                while (i < aList.Count)
                {
                    while (xResult.Length > 0)
                    {
                        if (aList[i].StartsWith(xResult))
                            break;
                        xResult = xResult.Remove(xResult.Length - 1);
                    }
                    i++;
                }
            }
            return xResult;
        }

        static public void StrList_DelMaxSubstring(IList<string> aList)
        {
            string xCommonSubstring = Utils_Str.StrList_GetMaxSubstring(aList);
            int xLenOfCommonSubstring = 0;
            if (!String.IsNullOrEmpty(xCommonSubstring))
            {
                xLenOfCommonSubstring = xCommonSubstring.Length;
                if (xLenOfCommonSubstring > 0)
                {
                    for (int i = 0; i < aList.Count; i++)
                    {
                        aList[i] = aList[i].Substring(xLenOfCommonSubstring);
                    }
                }
            }
        }

        static public string GetCompactedString( string stringToCompact, Font font, int maxWidth, TextFormatFlags аTextFormatFlags = TextFormatFlags.PathEllipsis | TextFormatFlags.ModifyString)
        {
            // Copy the string passed in since this string will be
            // modified in the TextRenderer's MeasureText method
            string compactedString = String.IsNullOrEmpty(stringToCompact) ? "" : string.Copy(stringToCompact);
            var maxSize = new Size(maxWidth, 0);
            TextRenderer.MeasureText(compactedString, font, maxSize, аTextFormatFlags);
            return compactedString;
        }

        static public string BytesToTextAndHex(Encoding aEncoding, byte[] aBytes, string aFormat = @" \0x{0:X2} ")
        {
            string sResult = null;
            if (aBytes!=null)
            {
                if (aEncoding.IsSingleByte)
                {
                    int i = 0;
                    foreach (byte b in aBytes)
                    {
                        if ((b >= 0x20) && ((aEncoding != Encoding.ASCII) || (b < 0x80)))
                        {
                            sResult += aEncoding.GetString(aBytes, i, 1);
                        }
                        else
                        {
                            sResult += String.Format(aFormat, b);
                        }
                        i++;
                    }
                }
                else
                {
                    int i = 0;
                    foreach (byte b in aBytes)
                    {
                        if ((b >= 0x20) && (b < 0x80))
                        {
                            sResult += aEncoding.GetString(aBytes, i, 1);
                        }
                        else
                        {
                            sResult += String.Format(aFormat, b);
                        }
                    }
                }
            }
            return sResult;
        }

        static public int FindText(string aText, string aTextToSearch, int aSearchStart)
        {
            // A valid ending index
            if (!String.IsNullOrEmpty(aText))
            {
                if ((aSearchStart >= 0) && (aSearchStart < aText.Length))
                {
                    // Find the position of search string in RichTextBox
                    return aText.IndexOf(aTextToSearch, aSearchStart);
                }
                else if ((-aSearchStart >= 0) && (-aSearchStart <= aText.Length))
                {
                    // Find the position of search string in RichTextBox
                    return aText.LastIndexOf(aTextToSearch, -aSearchStart);
                }
            }
            return -1;
        }


    } // class Utils_Str

} // namespace isf_candidates
