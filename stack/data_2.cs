[Serializable]
public class Player
{

    public string playerId;
    public string playerLoc;
    public string playerNick;
}

Player playerInstance = new Player();
playerInstance.playerId = "8484239823";
playerInstance.playerLoc = "Powai";
playerInstance.playerNick = "Random Nick";

//Convert to Jason
string playerToJason = JsonUtility.ToJson(playerInstance);
Debug.Log(playerToJason);

Player playerInstance = new Player();
playerInstance.playerId = "8484239823";
playerInstance.playerLoc = "Powai";
playerInstance.playerNick = "Random Nick";

//Convert to Jason
string playerToJason = JsonUtility.ToJson(playerInstance, true);
Debug.Log(playerToJason);

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

Player[] playerInstance = new Player[2];

playerInstance[0] = new Player();
playerInstance[0].playerId = "8484239823";
playerInstance[0].playerLoc = "Powai";
playerInstance[0].playerNick = "Random Nick";

playerInstance[1] = new Player();
playerInstance[1].playerId = "512343283";
playerInstance[1].playerLoc = "User2";
playerInstance[1].playerNick = "Rand Nick 2";

//Convert to Jason
string playerToJason = JsonHelper.ToJson(playerInstance, true);
Debug.Log(playerToJason);