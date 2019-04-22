public struct CappedString
{
    int Max_Length;
    string val;

    public CappedString(string str, int maxLength = 20)
    {
        Max_Length = maxLength;
        val = (string.IsNullOrEmpty(str)) ? "" :
              (str.Length <= Max_Length) ? str : str.Substring(0, Max_Length);
    }

    // From string to CappedString
    public static implicit operator CappedString(string str)
    {
        return new CappedString(str);
    }

    // From CappedString to string
    public static implicit operator string(CappedString str)
    {
        return str.val;
    }

    // To making using Debug.Log() more convenient
    public override string ToString()
    {
        return val;
    }

    // Then overload the rest of your operators for other common string operations
}

lass Superstring
{
    int max_Length = 20;
    string theString;

    public Superstring() { }
    public Superstring(int maxLength) { max_Length = maxLength; }
    public Superstring(string initialValue) { Value = initialValue; }
    public Superstring(int maxLength, string initialValue) { max_Length = maxLength; Value = initialValue; }

    public string Value { get { return theString; } set { theString = string.IsNullOrEmpty(value) ? value : value.Substring(0, Math.Min(max_Length, value.Length)); } }
}

Superstring s = new Superstring("z");
s.Value = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
string s2 = s.Value;