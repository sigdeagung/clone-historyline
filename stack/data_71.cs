public int success_fail

IEnumerator POST(string username, string passw)
{
    WWWForm form = new WWWForm();
    form.AddField("usr", username);
    form.AddField("pass", passw);

    WWW www = new WWW(url, form);

    yield return StartCoroutine(WaitForRequest(www));
}

private IEnumerator WaitForRequest(WWW www)
{
    yield return www;
    if (www.error == null)
    {
        if(www.text.Contains("user exists"))
            {
                success_fail = 2;
            }
            else
            {
                success_fail=1;
            }
    } else {
        success_fail=0;
    }    
}
WWW www = new WWW("http://google.com");

StartCoroutine(WaitForRequest(www,(status)=>{
    print(status.ToString());
}));
private IEnumerator WaitForRequest(WWW www,Action<int> callback) {
    int tempInt = 0;
    yield return www;
    if (string.IsNullOrEmpty(www.error)) {
        if(!string.IsNullOrEmpty(www.text)) {
            tempInt = 3;
        }
        else {
            tempInt=2;
        }
    } else {
        print(www.error);
        tempInt=1;
    }
    callback(tempInt);
}
StartCoroutine(WaitForRequest(www,(status)=>{
    print(status.ToString());
    Awake(); // we can call other functions within the callback to use other codeblocks and logic.
    if(status != 0)
        print("yay!");
    }
));