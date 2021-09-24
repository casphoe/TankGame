using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpEffect : CCompoent
{
    public float AddExp;
    public float MessageSpeed;

    private Text Exp;
    private string Message;

    private void Start()
    {
        Exp = GetComponent<Text>();
        MessageSpeed = 0.3f;
        AddExp = 100 + 100;
        Message = AddExp.ToString();
        StartCoroutine(Type(Exp, Message, MessageSpeed));
    }

    IEnumerator Type(Text type,string message, float speed)
    {
        for(int i = 0; i < message.Length; i++)
        {
            type.text = message.Substring(0, i * 1);
            yield return new WaitForSeconds(speed);
        }
    }

    
}
