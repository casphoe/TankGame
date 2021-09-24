using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreText : CCompoent
{

    private Text Best;

    void Start()
    {
        Best = GetComponent<Text>();

        if(GameManager.instance.difficulty == GameSetting.Difficulty.Easy)
        {
            Best.text = "BestScore : " + string.Format("{0:#,0}", GameSetting.BestScore + 100);
        }
        else if(GameManager.instance.difficulty == GameSetting.Difficulty.Normal)
        {
            Best.text = "BestScore : " + string.Format("{0:#,0}", GameSetting.BestScore + 200);
        }
        else if(GameManager.instance.difficulty == GameSetting.Difficulty.Hard)
        {
            Best.text = "BestScore : " + string.Format("{0:#,0}", GameSetting.BestScore + 300);
        }
    }

}
