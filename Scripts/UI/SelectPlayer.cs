using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPlayer : CCompoent
{
    private bool TurnLeft = false;
    private bool TurnRight = false;

    private Quaternion turn = Quaternion.identity;

    private Dictionary<string, PlayerSetting> Players;

    public int value = 0;
    public int charactorNum = 0;

    public GameObject GameStartButton;

    public Text PlayerName;
    public Text Hp;
    public Text Power;
    public Text Defence;
    public Text Speed;
    public Text ShootRate;

    public Slider HpSlider;
    public Slider PowerSlider;
    public Slider DefenceSlider;
    public Slider SpeedSlider;
    public Slider ShootRateSlider;

    public Text PlayerLevel;

    private void Start()
    {
        //각도를 초기화 시킵니다.
        turn.eulerAngles = new Vector3(0, value, 0);

        PlayerLevel.text = "PlayerLevel : " + GameSetting.PlayerLevel;
    }

    private void Update()
    {
        if (TurnLeft)
        {
            
            charactorNum++;
            if(charactorNum == 3)
            {
                charactorNum = 0;
            }
            value -= 120;
            TurnLeft = false;
        }
        if(TurnRight)
        {
            
            charactorNum--;
            if(charactorNum == -1)
            {
                charactorNum = 2;
            }
            value += 120;
            TurnRight = false;
        }

        turn.eulerAngles = new Vector3(0, value, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, turn, Time.deltaTime * 5.0f);
        TankStat();
        TankGameStart();
    }

    public void LeftTurn()
    {
        TurnLeft = true;
        TurnRight = false;
    }

    public void RightTurn()
    {
        TurnLeft = false;
        TurnRight = true;
    }

    public void StageStartButton()
    {
        if (GameSetting.Effect == 0)
        {
            SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (!SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Play();
            }
        }

        ScneLoader.instance.LoadScene(CDefine.SCENE_NAME_TANKGAME_STAGESELECT);
    }

    public void PlayerStat(string Name)
    {
        Players = GameManager.instance.PlayerMGR();

        PlayerSetting PS;
        Players.TryGetValue(string.Format("{0}", Name), out PS);

        PlayerName.text = "Name : " + PS.TankName;
        Hp.text = "" + PS.Hp;
        Power.text = "" + PS.Power;
        Defence.text = "" + PS.Defense;
        Speed.text = "" + PS.Speed;
        ShootRate.text = "" + PS.AttackRate;

        HpSlider.maxValue = 500;
        HpSlider.value = PS.Hp;

        PowerSlider.maxValue = 16;
        PowerSlider.value = PS.Power;

        DefenceSlider.maxValue = 16;
        DefenceSlider.value = PS.Defense;

        SpeedSlider.maxValue = 15;
        SpeedSlider.value = PS.Speed;

        ShootRateSlider.maxValue = 3.5f;
        ShootRateSlider.value = PS.AttackRate;
        if (GameSetting.TankArmorUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true)
        {
            if(GameSetting.TankArmorUpgrade == true)
            {
                Defence.text = "" + (PS.Defense + GameManager.UpgradeTankArmors);
                DefenceSlider.value = PS.Defense + GameManager.UpgradeTankArmors;
            }
            if(GameSetting.TrackUpgrade == true)
            {
                Speed.text = "" + (PS.Speed + GameManager.UpgradeTracks);
                SpeedSlider.value = PS.Speed + GameManager.UpgradeTracks; 
            }
            if(GameSetting.TurretUpgrade == true)
            {
                ShootRate.text = "" + (PS.AttackRate - GameManager.UpgradeTurrets);
                ShootRateSlider.value = PS.AttackRate - GameManager.UpgradeTurrets;
            }
        }
        else
        {
            return;
        }
       
    }

    public void TankStat()
    {
        switch(charactorNum)
        {
            case 0:
                PlayerStat("LightTank");
                GameManager.instance.TankBase = PlayerSetting.TankDB.LightTank;
                break;
            case 1:
                PlayerStat("MediumTank");
                GameManager.instance.TankBase = PlayerSetting.TankDB.MediumTank;
                break;
            case 2:
                PlayerStat("HeavyTank");
                GameManager.instance.TankBase = PlayerSetting.TankDB.HeavyTank;
                break;
        }
    }
    //레벨의 따라서 플레이 가능한 탱크를 결정함
    private void TankGameStart()
    {
        if(GameSetting.PlayerLevel >= 9)
        {
            GameStartButton.SetActive(true);
        }
        else if(GameSetting.PlayerLevel >= 5)
        {
            if(charactorNum == 2)
            {
                GameStartButton.SetActive(false);
            }
            else
            {
                GameStartButton.SetActive(true);
            }
        }
        else if(GameSetting.PlayerLevel < 5)
        { 
            if(charactorNum == 2 || charactorNum == 1)
            {
                GameStartButton.SetActive(false);
            }
            else
            {
                GameStartButton.SetActive(true);
            }
        }
    }

    public void ShopButton()
    {

        if (GameSetting.Effect == 0)
        {
            SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (!SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Play();
            }
        }
        ScneLoader.instance.LoadScene(CDefine.SCENE_NAME_TANKGAME_SHOPSCENE);
    }
}