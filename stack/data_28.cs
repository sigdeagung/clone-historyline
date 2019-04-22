void ActivateBuff1(){
    gun.equippedGun.msPerShot /= 2;
    gun.equippedGun.shotsLeftInMag += 10;
    Invoke("ResetPlayerRage", powerUpDuration);
}

void ActivateBuff2(){
    player.speedModifier *= 1.5f;
    Invoke("ResetPlayerSpeed", powerUpDuration);
}

void ResetPlayerRage(){
    gun.equippedGun.msPerShot *= 2;
}

void ResetPlayerSpeed(){
    player.speedModifier /= 1.5f;
}
void ActivateBuff1(){
    gun.equippedGun.msPerShot /= 2;
    gun.equippedGun.shotsLeftInMag += 10;
    gameObject.SetActive(false);
    Invoke("ResetPlayerRage", powerUpDuration);
}

void ResetPlayerRage(){
    gun.equippedGun.msPerShot *= 2;
    Destroy(gameObject);
}
public class RageController : BufferBase
{
    public static RageController instance;

    public static bool IsActive { get { return instance._isBufferActive; } }

    #region Static Methods
    internal static void AddRage(float value)
    {
        instance.StartOrExtendBuffer(value);
    }

    internal static void Reset()
    {
        instance.ResetBuffer();
    }
    #endregion

    #region Overriden Methods
    protected override void StartOrExtendBuffer(float value)
    {
        base.StartOrExtendBuffer(value);

        //----
        //add speed etc..
        //----
    }

    protected override void EndBuffer()
    {
        base.EndBuffer();

        //----
        //remove speed etc..
        //----
    }
    #endregion   

    #region Unity Methods
    void Awake()
    {
        instance = this;
    }
    void FixedUpdate()
    {
        FixedUpdateBuffer();

        if (_isBufferActive)
        {
            //----
            //anything that changes by time
            //----
        }
    }
    #endregion
}