if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
{
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
    PlayerData data = (PlayerData)bf.Deserialize(file);
    file.Close();

    //These lines overwrites the loaded data
    data.score = score;
    data.levelcount = levelcount;
}

if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
{
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
    PlayerData data = (PlayerData)bf.Deserialize(file);
    file.Close();

    score = data.score;
    levelcount= data.levelcount;
}