using System;

public class UnicodeStringHelper
{
	public UnicodeStringHelper()
	{
	}

    // UnicodeString by Erdinç Barut 
    // Converts - String to  unicode Json string encoding(starts with \u )
    // Supporting all Unicode charachters like Emoji
    // Excepts Ascii charachters
    // Example: string test = UnicodeStr("ՏṖᴇ₵ꟾᾸḺ");
    //         MessageBox.Show("Encoded:"+test);
    //         string jsonStr = "{\"test\":\""+test+"\"}";
    //         JObject testJO = JObject.Parse(jsonStr);
    //          MessageBox.Show(testJO["test"].ToString());      
    // Easy for use direct convert strings into json unicode format



    public static string UnicodeString(string txt)
    {
        byte[] bytex = Encoding.Unicode.GetBytes(txt);
        string outStr = "";
        int charCode = 0;
        int curPage = 0;
        int pageSet = -1;

        for (int i = 0; i < bytex.Length; i++)
        {

            if (pageSet == -1)
            {
                charCode = bytex[i];
                pageSet = 0;
            }
            else
            {
                curPage = bytex[i];
                if (Convert.ToString(curPage) == "0")
                {
                    if (curPage != -1)
                    {
                        outStr += (char)charCode;

                        curPage = -1;
                    }
                    else
                    {
                        outStr += charCode.ToString("X");
                    }
                }
                else
                {
                    string encoding = "00";
                    encoding = encoding.Remove(1, 1).Insert(1, Convert.ToString(curPage));
                    if (curPage != -1)
                    {

                        if (Convert.ToInt32(encoding) > 9)
                        {
                            encoding = Convert.ToInt32(encoding).ToString("X");
                        }
                        outStr += (char)92 + "u" + encoding + charCode.ToString("X2");
                    }
                    else
                    {

                        outStr += (char)92 + "u" + charCode.ToString("X2");
                    }
                    string t = (charCode).ToString("X");
                    curPage = -1;

                }

                pageSet = -1;

            }
        }

        return outStr;
    }
}
