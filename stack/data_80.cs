class Player {

  private int currentLife = 100;

  public int CurrentLife {
    get { return currentLife; }
    set { currentLife = value; }
  }

}
class HealthBarGUI1 {

  public GameObject player;
  private Player playerScript;

  void Start() {
    playerScript = (Player)player.GetComponent(typeof(Player)); 
    Debug.Log(playerscript.CurrentLife);
  }

}
class HealthBarGUI1 {

  private Player player;

  void Start() {
    player = (Player)GameObject.Find("NameOfYourPlayerObject").GetComponent(typeof(Player));
    Debug.Log(player.CurrentLife);
  }

}
HealthBarGUI1.currentLife
gameObject.GetComponent<HealthBarGUI1>().varname;
gameObject.GetComponent<HealthBarGUI1>().varname;