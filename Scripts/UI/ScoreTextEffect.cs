using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextEffect : CCompoent
{
    Vector3 dir;
    Vector3 PrevDir;
    float time = 0f;
    private Text Effect;


    private void Start()
    {
        dir = gameObject.transform.position;
        PrevDir = dir;
        Effect = GetComponent<Text>();
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        dir.y += 0.3f;
        transform.position = dir;
        if(time < 3f)
        {
            Effect.color = new Color(1, 1, 1, time / 3);
        }
        else
        {
            time = 0;
            gameObject.SetActive(false);
            dir = PrevDir;
        }
        time += Time.deltaTime;
    }
}
