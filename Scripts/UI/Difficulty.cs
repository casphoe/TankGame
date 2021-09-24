using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Difficulty : CCompoent
{
    private Text GameDifficultyText;

    public string Message;

    public float MessageSpeed;
    
    void Start()
    {
        GameDifficultyText = GetComponent<Text>();
        MessageSpeed = 0.2f;
        Message = "Congratulations on clearing the Easy difficulty game ";
        switch (GameManager.instance.difficulty)
        {
            case GameSetting.Difficulty.Easy:
                Message = "Congratulations on clearing the Easy difficulty game ";
                break;
            case GameSetting.Difficulty.Normal:
                Message = "Congratulations on clearing the Normal difficulty game ";
                break;
            case GameSetting.Difficulty.Hard:
                Message = "Congratulations on clearing the Hard difficulty game ";
                break;
        }
        StartCoroutine(Typing(GameDifficultyText, Message, MessageSpeed));
    }

    IEnumerator Typing(Text typeText, string message, float speed)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typeText.text = message.Substring(0, i * 1);
            yield return new WaitForSeconds(speed);
        }
    }
}
