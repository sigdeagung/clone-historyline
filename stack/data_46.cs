 public static string Translate(string input, string fromLanguage, string toLanguage)
    {
        using (WebClient webClient = new WebClient())
        {
            string url = string.Format("http://translate.google.com/translate_a/t?client=j&text={0}&sl={1}&tl={2}", Uri.EscapeUriString(input), fromLanguage, toLanguage);
            string result = webClient.DownloadString(url);

            // I used JavaScriptSerializer but another JSON parser would work
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)serializer.DeserializeObject(result);
            Dictionary<string, object> sentences = (Dictionary<string, object>)((object[])dic["sentences"])[0];
            return (string)sentences["trans"];
        }
    }
    try
{
    //Don't use UtF Encoding 
    // use default webclient encoding

    var url = String.Format("http://www.google.com/translate_t?hl=en&text={0}&langpair={1}", "►" + txtNewResourceValue.Text.Trim() + "◄", "en|" + item.Text.Substring(0, 2));                    

     var webClient = new WebClient();
     string result = webClient.DownloadString(url); //get all data from google translate in UTF8 coding..

      int start = result.IndexOf("id=result_box");
      int end = result.IndexOf("id=spell-place-holder");
      int length = end - start;
      result = result.Substring(start, length);
      result = reverseString(result);

      start = result.IndexOf(";8669#&");//◄
      end = result.IndexOf(";8569#&");  //►
      length = end - start;

      result = result.Substring(start +7 , length - 8);
      objDic2.Text =  reverseString(result);

       //hard code substring; finding the correct translation within the string.
        dictList.Add(objDic2);
}
catch (Exception ex)
 {
  lblMessages.InnerHtml = "<strong>Google translate exception occured no resource   saved..." + ex.Message + "</strong>";
                error = true;
}

public static string reverseString(string s)
{
    char[] arr = s.ToCharArray();
    Array.Reverse(arr);
    return new string(arr);

}
 try
    {


        //Don't use UtF Encoding 
        // use default webclient encoding
        //input        [string to translate]
        //Languagepair [eg|es]

        var url = String.Format("http://www.google.com/translate_t?hl=en&text={0}&langpair={1}", "►" + input.Trim() + "◄", languagePair);

        var webClient = new WebClient();
        string result = webClient.DownloadString(url); //get all data from google translate 

        int start = result.IndexOf("id=result_box");
        int end = result.IndexOf("id=spell-place-holder");
        int length = end - start;
        result = result.Substring(start, length);
        result = reverseString(result);

        start = result.IndexOf(";8669#&");//◄
        end = result.IndexOf(";8569#&");  //►
        length = end - start;

        result = result.Substring(start + 7, length - 8);

        //return transalted string
        return reverseString(result); 


    }
    catch (Exception ex)
    {
        return "Google translate exception occured no resource   saved..." + ex.Message";

    }
}