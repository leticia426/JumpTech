using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool[] completedFases;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}
