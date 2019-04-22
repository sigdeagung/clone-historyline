ublic class CMonoBehavour : MonoBehaviour
    {
        public int SomeHelperProperty1 { get; private set; }
        public int SomeHelperProperty2 { get; private set; }
        public int SomeHelperProperty3 { get; private set; }
        public int SomeHelperProperty4 { get; private set; }
    }

    public class ThirtPartyLibraryWrapperClass : MonoBehaviour
    {
        public int SomeHelperProperty5 { get; private set; }
        public int SomeHelperProperty6 { get; private set; }
    }

    //COMPOSITE SOLUTION

    public class CompositeMonoBehaviour : CMonoBehaviour
    {
        private ThirtPartyLibraryWrapperClass _thirdParty;

        public int SomeHelperProperty5 { get{ return _thirdParty.SomeHelperProperty5; } }
        public int SomeHelperProperty6 { get{ return _thirdParty.SomeHelperProperty6; } }
    }

    public class ExampleUseage1 : CompositeMonoBehaviour
    {
          //you have all your properties, great news!
    }

    public class ExampleUseage2 : CompositeMonoBehaviour
    {
        //you have all your properties, great news!
    }

    public class CMonoBehavour : MonoBehaviour
{
    public int SomeHelperProperty1 { get; private set; }
    public int SomeHelperProperty2 { get; private set; }
    public int SomeHelperProperty3 { get; private set; }
    public int SomeHelperProperty4 { get; private set; }
}

public class ThirtPartyLibraryWrapperClass : CMonoBehavour
{
    public int SomeHelperProperty5 { get; private set; }
    public int SomeHelperProperty6 { get; private set; }
}


//The first class have 4 properties
public class ExampleUseage1 : CMonoBehavour
{

}
    //The first class have 6 properties
public class ExampleUseage2 : ThirtPartyLibraryWrapperClass
{

}