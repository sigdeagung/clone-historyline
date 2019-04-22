void Start () 
{
    // Register this object to the current game controller.
    // This is important so that all clients have a reference to this object.
    GameController.Instance.RegisterMoon (this);
}
public Transform CameraTransform;

    private void Start ()
    {
        CameraTransform = FindObjectOfType ().transform;
        // Register this player to the GameController. 
        // Important: all clients must have a reference to this player.
        GameController.Instance.RegisterPlayer (this);

        // Hide your own model if you are the local client.
        if (photonView.isMine)
            gameObject.transform.GetChild (0).gameObject.SetActive (false);
    }

    void Update () 
    {
        // If this player is not being controlled by the local client
        // then do not update its position. Each client is responsible to update
        // its own player.
        if (!photonView.isMine && PhotonNetwork.connected)
            return;

        // The player should have the same transform as the camera
        gameObject.transform.position = CameraTransform.position;
        gameObject.transform.rotation = CameraTransform.rotation;
    }
}