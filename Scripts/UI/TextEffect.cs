using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : CCompoent
{
    private Text StageClearText;

    public string Message;

    public float MessageSpeed;
    
    void Start()
    {
        MessageSpeed = 0.2f;
        StageClearText = GetComponent<Text>();
        StartCoroutine(Typing(StageClearText, Message, MessageSpeed));
    }

    
    IEnumerator Typing(Text typeText, string message,float speed)
    {
        for(int i = 0; i < message.Length; i++)
        {
            typeText.text = message.Substring(0, i * 1);
            yield return new WaitForSeconds(speed);
        }
    }
}
