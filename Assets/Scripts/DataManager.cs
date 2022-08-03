using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public float ReadBestTime()
    {
        return PlayerPrefs.GetFloat("BestTime");
    }

    public void WriteBestTime(float bestTime)
    {
        PlayerPrefs.SetFloat("BestTime",bestTime);
    }
}
