using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStartUI : CCompoent
{
    private float Timer = 0f;
    private Text GameCoutText;
    private Image CanvasEffects;

    public GameObject MessageCanvas;
    public GameObject StartEffectCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
        GameCoutText = GetComponent<Text>();
        CanvasEffects = StartEffectCanvas.GetComponent<Image>();
        StartCoroutine(EffectCanvas());
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer == 0)
        {
            Time.timeScale = 0f;
        }

        if(Timer <= 225)
        {
            Timer++;

            if(Timer < 45)
            {
                GameCoutText.text = "3";
            }

            if(Timer > 90)
            {
                GameCoutText.text = "2";
            }

            if(Timer > 135)
            {
                GameCoutText.text = "1";
            }

            if(Timer > 180)
            {
                GameCoutText.text = "GAMESTART";
            }

            if(Timer >= 225)
            {
                GameCoutText.text = "" + GameManager.instance.StageSelect + " Start";
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f;
            }
        }
    }

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        MessageCanvas.SetActive(false);
    }

    IEnumerator EffectCanvas()
    {
        CanvasEffects.color = new Color(CanvasEffects.color.r, CanvasEffects.color.g, CanvasEffects.color.b, 1);

        while(CanvasEffects.color.a > 0.0f)
        {
            CanvasEffects.color = new Color(CanvasEffects.color.r, CanvasEffects.color.g, CanvasEffects.color.b, CanvasEffects.color.a -(Timer / 9600f));
            yield return null;
        }
    }
}
