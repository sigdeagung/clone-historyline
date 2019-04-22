public  string buildXMLRPCRequest(Hashtable FieldArray,string MethodName) 
{
    string  ReturnString = "";

    ReturnString    +=         "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>" +
                        "\n" + "<simpleRPC version=\"0.9\">" +
                        "\n" + "<methodCall>" +
                        "\n" + "<methodName>" + MethodName + "</methodName>" +
                        "\n" + "<vector type=\"struct\">";

    ReturnString    +=  buildNode(FieldArray);

    ReturnString    +=  "\n</vector>" +
                        "\n</methodCall>" +
                        "\n</simpleRPC>";
    return  ReturnString;
}

public  string buildNode(Hashtable FieldArray) 
{
    string  ReturnList = "";

    foreach (DictionaryEntry Item in FieldArray)    {

        string  TypeName    =   "int";
        string  NodeType    =   "scalar";

        Type myType =   Item.Value.GetType();
        string  fieldValue  =   "";

        if (myType == typeof(string) ) {
            TypeName    =   "string";
            fieldValue  =   Item.Value.ToString();
        }

        if (myType == typeof(Hashtable) ) {
            fieldValue  =   buildNode(Item.Value as Hashtable);
            NodeType    =   "vector";
            TypeName    =   "struct";
        }

        if (myType == typeof(int) ) {
            fieldValue  =   Item.Value.ToString();
            TypeName    = "int";
        }

        var ThisNode    =   "\n<" + NodeType + " type=\"" + TypeName + "\" id=\"" + Item.Key + "\">" + fieldValue + "</" + NodeType + ">";
        ReturnList +=   ThisNode;
    }

    return ReturnList;
}
private     void UnityPostXML(  int Staging,
                                        string WebServer,
                                        string MethodName,
                                        Hashtable   FieldArray)
    {
        string  WebServiceURL   =   "http://LIVESERVER/";
        if (Staging == 1) {
            WebServiceURL       =   "http://TESTSERVER";
        }

        // Encode the text to a UTF8 byte arrray

        string XMLRequest   =   buildXMLRPCRequest(FieldArray,MethodName);

        System.Text.Encoding enc = System.Text.Encoding.UTF8;
        byte[] myByteArray = enc.GetBytes(XMLRequest);


         // Get the Unity WWWForm object (a post version)


        var form = new WWWForm();
        var url = WebServiceURL;

        //  Add a custom header to the request.
        //  Change the content type to xml and set the character set
        var headers = form.headers;
        headers["Content-Type"]="text/xml;charset=UTF-8";

        // Post a request to an URL with our rawXMLData and custom headers
        var www = new WWW(WebServiceURL, myByteArray, headers);

        //  Start a co-routine which will wait until our servers comes back

        StartCoroutine(WaitForRequest(www));
}

IEnumerator WaitForRequest(WWW www)
{
    yield return www;

    // check for errors
    if (www.error == null)
    {
        Debug.Log("WWW Ok!: " + www.text);
    } else {
        Debug.Log("WWW Error: "+ www.error);
    }    
}
                                   string WebServer,
                                         string MethodName,
                                         Hashtable Fields)
    {
        //  Figure out who to call
        string  WebServiceURL   =   "http://LIVSERVER";
        if (Staging == 1) {
            WebServiceURL       =   "http://TESTSERVER";
        }

        WebServiceURL           +=  WebServer;

        //  Build the request

        XmlRpcParser    parser  =   new XmlRpcParser();
        string XMLRequest       = parser.buildXMLRPCRequest(Fields,MethodName);

        //  Fire it off

        HttpWebRequest httpRequest =(HttpWebRequest)WebRequest.Create(WebServiceURL);

        httpRequest.Method = "POST";

        //Defining the type of the posted data as XML
        httpRequest.ContentType = "text/xml";

        // string data = xmlDoc.InnerXml;
        byte[] bytedata = Encoding.UTF8.GetBytes(XMLRequest);

        // Get the request stream.
        Stream requestStream = httpRequest.GetRequestStream();

        // Write the data to the request stream.
        requestStream.Write(bytedata, 0, bytedata.Length);
        requestStream.Close();

        //Get Response
        HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

        // Get the stream associated with the response.
        Stream receiveStream = httpResponse.GetResponseStream ();

        // Pipes the stream to a higher level stream reader with the required encoding format. 
        StreamReader readStream = new StreamReader (receiveStream, Encoding.UTF8);

        string  ReceivedData    =   readStream.ReadToEnd ();
        httpResponse.Close ();
        readStream.Close ();

        return  ReceivedData;
    }
}