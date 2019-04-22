public string Name {
    get { return name; }
    set {
        name = value;
        var eh = NameChanged;   // avoid race condition.
        if (eh != null)
            eh(this, EventArgs.Empty);
    }
}
private string name;
public event EventHandler NameChanged;
public int RandomNumber { get; set; }
private int _iRandomNumber;

public int iRandomNumber
{
     get { return _iRandomNumber%10;} //you can do something like this mod function 
     set { _iRandomNumber = value+1000;} //you can manipulate the value being set 

} 