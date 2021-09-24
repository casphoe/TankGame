using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverCount : CCompoent
{
    Text gameovercount;

    int Count = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameovercount = GetComponent<Text>();
        StartCoroutine(FadeTextToZero());
        gameovercount.text = "" + Count;
    }

    IEnumerator FadeTextToFullAlpha() //알파값 0에서 1로 전환
    {
        gameovercount.color = new Color(gameovercount.color.r, gameovercount.color.g, gameovercount.color.b, 0);
        while(gameovercount.color.a < 1.0f)
        {
            gameovercount.color = new Color(gameovercount.color.r, gameovercount.color.g, gameovercount.color.b, gameovercount.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
        Count -= 1;
        StartCoroutine(FadeTextToZero());
    }

    IEnumerator FadeTextToZero() //알파값 1에서 0으로 전환
    {
        gameovercount.color = new Color(gameovercount.color.r, gameovercount.color.g, gameovercount.color.b, 1);
        while(gameovercount.color.a > 0.0f)
        {
            gameovercount.color = new Color(gameovercount.color.r, gameovercount.color.g, gameovercount.color.b, gameovercount.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha());
    }

    public void Update()
    {
        gameovercount.text = "" + Count;
        if(Count <= 0)
        {
            ScneLoader.instance.LoadScene(CDefine.SCENE_NAME_TANKGAME_STARTSCENE);
        }
    }
}
