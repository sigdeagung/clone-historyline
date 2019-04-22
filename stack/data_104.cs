MyScript myScriptInstance = FindObjectOfType<MyScript>();
var go = new GameObject();
var btn = go.AddComponent<Button>();

var targetinfo = UnityEvent.GetValidMethodInfo(myScriptInstance,
"OnButtonClick", new Type[] { typeof(GameObject) });

UnityAction<GameObject> action = Delegate.CreateDelegate(typeof(UnityAction<GameObject>), myScriptInstance, targetinfo, false) as UnityAction<GameObject>;

UnityEventTools.AddObjectPersistentListener<GameObject>(btn.onClick, action, go);
MyScript myScriptInstance = FindObjectOfType<MyScript>();
var go = new GameObject();
var btn = go.AddComponent<Button>();

UnityAction<GameObject> action = new UnityAction<GameObject>(myScriptInstance.OnButtonClick);
UnityEventTools.AddObjectPersistentListener<GameObject>(btn.onClick, action, go);
void Start () {

    MyScript myScriptInstance = FindObjectOfType<MyScript> ();

    GameObject go = DefaultControls.CreateButton (new DefaultControls.Resources());
    var btn = go.GetComponent<Button> ();

    btn.onClick.AddListener (myScriptInstance.TestMethod);
}
public void TestMethod ()
{
    Debug.Log ("TestMethod");   
}