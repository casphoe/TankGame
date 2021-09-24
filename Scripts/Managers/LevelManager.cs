using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public override void Awake()
    {
        base.Awake();
        //LevelUp();
    }

    public void LevelUp()
    {
        switch(GameSetting.Exp / 10)
        {
            case 0:
                GameSetting.PlayerLevel = 1;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                break;
            case 1:
                GameSetting.PlayerLevel = 2;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                GameSetting.isLevelUp = true;
                break;
            case 3:
                GameSetting.PlayerLevel = 3;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                GameSetting.isLevelUp = true;
                break;
            case 5:
                GameSetting.PlayerLevel = 4;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                GameSetting.isLevelUp = true;
                break;
            case 8:
                GameSetting.PlayerLevel = 5;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                GameSetting.isLevelUp = true;
                break;
            case 10:
                GameSetting.PlayerLevel = 6;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                GameSetting.isLevelUp = true;
                break;
            case 14:
                GameSetting.PlayerLevel = 7;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                GameSetting.isLevelUp = true;
                break;
            case 18:
                GameSetting.PlayerLevel = 8;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                GameSetting.isLevelUp = true;
                break;
            case 25:
                GameSetting.PlayerLevel = 9;
                PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
                GameSetting.isLevelUp = true;
                break;
        }
    }
}
