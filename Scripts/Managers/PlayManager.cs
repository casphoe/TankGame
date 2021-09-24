using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : CCompoent
{
    public GameObject MessageCanvas;
    public GameObject PausePanel;

    public GameObject StageClearPanel;

    public Text TankName;
    public Text LevelText;


    public float GameTime = 0f;

    public Text GameClockText;
    public Text CoinCount;
    public Text ScoreText;
    public Text ScrewText;
    public Slider BGM;
    public Slider Effect;

    public Text difficultyText;


    private int Minute = 0; //분 표시

    private void Start()
    {
        TankManger.instance.HpSlider.maxValue = TankManger.instance.Hp;
        TankManger.instance.HpSlider.value = TankManger.instance.Hp;
        TankManger.instance.HpText.text = "Hp : " + TankManger.instance.Hp;
        PausePanel.SetActive(false);
        StageClearPanel.SetActive(false);
        TankManger.instance.GameOverPanel.SetActive(false);
        TankName.text = "Tank : " + GameManager.instance.TankBase;
        difficultyText.text = "Game difficulty : " + GameManager.instance.difficulty;
        BGM.value = GameSetting.BGM;
        Effect.value = GameSetting.Effect;

        Function.LateCallFunc(this, 1.6f, (ccom) =>
        {
            GameTime = GameSetting.GameTime;

            if(GameSetting.BGM == 0)
            {
                SoundManager.instance.BackGroundAudio.Stop();
            }
            else
            {
                if(!SoundManager.instance.BackGroundAudio.isPlaying)
                {
                    SoundManager.instance.BackGroundAudio.Play();
                }
            }
        });
    }

    private void Update()
    {
        if(GameSetting.StageClear == false)
        {
            if (GameTime != 0)
            {
                GameTime -= Time.deltaTime;
                if (GameTime >= 60)
                {
                    Minute++;
                    GameTime -= 60;
                }
                if (GameTime < 0)
                {
                    if (Minute > 0)
                    {
                        Minute--;
                        GameTime += 60;
                    }
                    if (GameTime <= 0)
                    {
                        GameTime = 0;
                        GameSetting.IsGameOver = true;
                        TankManger.instance.GameOver();
                    }
                }
            }
        }
        GameClockText.text = string.Format("{0:00} : {1:00}", Minute, (int)GameTime);
        //점수와 코인을 천단위 표시
        CoinCount.text = string.Format("{0:#,0}", GameSetting.CoinCount);
        ScoreText.text = "Score : " + string.Format("{0:#,0}", GameSetting.Score);
        ScrewText.text = ": " + string.Format("{0:#,0}", GameSetting.ScrewValue);
        LevelText.text = "PlayerLevel : " + GameSetting.PlayerLevel;
        SoundManager.instance.BackGroundAudio.volume = BGM.value;

        for (int i = 0; i < SoundManager.instance.EffectAudioList.Count; i++)
        {
            SoundManager.instance.EffectAudio.transform.GetChild(i).GetComponent<AudioSource>().volume = Effect.value;
        }
        StageClear();
    }

    public void PauseButton()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryButton()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        switch(GameManager.instance.StageSelect)
        {
            case GameSetting.Stage.Stage1:
                ScneLoader.instance.LoadScene(CDefine.SCENE_NAME_TANKGAME_GAMESCENE);
                break;
            case GameSetting.Stage.Stage2:
                break;
            case GameSetting.Stage.Stage3:
                break;
        }
    }

    public void PauseCancelButton()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PlayerSelectScene()
    {
        Time.timeScale = 1f;
        ScneLoader.instance.LoadScene(CDefine.SCENE_NAME_TANKGAME_PLAYERSELECT);
    }

    public void QameQuitButton()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void BGMSliderChange()
    {
        GameSetting.BGM = BGM.value;
        PlayerPrefs.SetFloat("BGM", GameSetting.BGM);
    }

    public void EffectSliderChange()
    {
        GameSetting.Effect = Effect.value;
        PlayerPrefs.SetFloat("Effect", GameSetting.Effect);
    }

    private void StageClear()
    {
        if(GameSetting.StageClear == true)
        {
            if (GameSetting.Effect == 0)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(6).GetComponent<AudioSource>().Stop();
            }
            else
            {
                if (!SoundManager.instance.EffectAudio.transform.GetChild(6).GetComponent<AudioSource>().isPlaying)
                {
                    SoundManager.instance.EffectAudio.transform.GetChild(6).GetComponent<AudioSource>().Play();
                }
            }
            StageClearPanel.SetActive(true);
        }
    }
}