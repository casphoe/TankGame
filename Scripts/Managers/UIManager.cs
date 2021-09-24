using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : CCompoent
{
    public bool IsSoundOn = true;

    public Sprite SoundOff;
    public Sprite SoundOn;

    public GameObject SoundImage;

    public Text CrewNameText;
    public Slider CAddAttack;
    public Slider CAddDefnce;
    public Slider CAddSpeed;
    public Slider CAddAttackRate;

    public Slider[] UpgradeSliders = new Slider[3];
    public Text[] UpgradeText = new Text[3];

    public Text Purchace;

    private Dictionary<string, CrewSetting> Crews;

    public GameObject CrewPanel;
    public GameObject UpgradePanel;
    public GameObject PurchasePanel;

    public int CrewPurchasevalue;

    public static UIManager instance;

    public GameObject[] CrewPurchasePanel = new GameObject[2];
    public CrewSetting crew;
    public Sprite[] PurchaseBool = new Sprite[2];

    public Image[] PurchaseYesImage = new Image[2];

    public Text[] CrewPurchaseSetting = new Text[4];

    public override void Awake()
    {
        base.Awake();
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            return;
        }
    }

    public void SoundOnOff()
    {
        IsSoundOn = !IsSoundOn;

        if(IsSoundOn)
        {
            SoundImage.GetComponent<Image>().sprite = SoundOn;
            GameSetting.BGM = 1;
            GameSetting.Effect = 1;

            if(!SoundManager.instance.BackGroundAudio.isPlaying)
            {
                SoundManager.instance.BackGroundAudio.Play();
            }
        }
        else
        {
            SoundImage.GetComponent<Image>().sprite = SoundOff;
            GameSetting.BGM = 0;
            GameSetting.Effect = 0;
            SoundManager.instance.BackGroundAudio.Stop();

            for(int i = 0; i < SoundManager.instance.EffectAudioList.Count; i++)
            {
                SoundManager.instance.EffectAudio.gameObject.transform.GetChild(i).GetComponent<AudioSource>().Stop();
            }
        }
        PlayerPrefs.SetFloat("BGM", GameSetting.BGM);
        PlayerPrefs.SetFloat("Effect", GameSetting.Effect);
    }

    public void BackGameScene()
    {
        ScneLoader.instance.LoadScene(CDefine.SCENE_NAME_TANKGAME_PLAYERSELECT);
    }

    public void CrewButton()
    {
        if(UpgradePanel.activeSelf == true)
        {
            UpgradePanel.SetActive(false);
        }

        if(CrewPanel.activeSelf == false)
        {
            CrewPanel.SetActive(true);
        }
        else
        {
            return;
        }
    }
    
    public void UpgradeButton()
    {
        if (CrewPanel.activeSelf == true)
        {
            CrewPanel.SetActive(false);
        }

        if (UpgradePanel.activeSelf == false)
        {
            UpgradePanel.SetActive(true);
            
        }
        else
        {
            return;
        }
    }


    public void CrewStat(string Name)
    {
        Crews = GameManager.instance.CrewMGR();

        Crews.TryGetValue(string.Format("{0}", Name), out crew);

        CrewNameText.text = "CrewName :  " + crew.CrewName;
        CAddAttack.value = crew.AddAttack + UpgradeUI.instance.T.Power;
        CAddAttack.maxValue = 16;

        CAddDefnce.maxValue = 16;


        CAddSpeed.maxValue = 15;


        CAddAttackRate.maxValue = 3.5f;


        Purchace.text = "Purchase : " + crew.Purchase;
        CrewPurchasevalue = crew.Purchase;

        CrewPurchaseSetting[0].text = string.Format("{0} + {1} = {2}", UpgradeUI.instance.T.Power, crew.AddAttack, UpgradeUI.instance.T.Power + crew.AddAttack);

        if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
        {
            if(GameSetting.TrackUpgrade == true)
            {
                CAddSpeed.value = crew.AddSpeed + UpgradeUI.instance.T.Speed + GameManager.UpgradeTracks;
                CrewPurchaseSetting[2].text = string.Format("{0} + {1} + {2} = {3}", UpgradeUI.instance.T.Speed, crew.AddSpeed,GameManager.UpgradeTracks, UpgradeUI.instance.T.Speed + crew.AddSpeed + GameManager.UpgradeTracks);                
            }
            if(GameSetting.TurretUpgrade == true)
            {
                CAddAttackRate.value = UpgradeUI.instance.T.AttackRate - crew.AddAttackRate - GameManager.UpgradeTurrets;
                CrewPurchaseSetting[3].text = string.Format("{0} - {1} - {2} = {3}", UpgradeUI.instance.T.AttackRate, crew.AddAttackRate,GameManager.UpgradeTurrets, UpgradeUI.instance.T.AttackRate - crew.AddAttackRate - GameManager.UpgradeTurrets);
            }
            if(GameSetting.TankArmorUpgrade == true)
            {
                CAddDefnce.value = crew.AddDefnece + UpgradeUI.instance.T.Defense + GameManager.UpgradeTankArmors;
                CrewPurchaseSetting[1].text = string.Format("{0} + {1} + {2} = {3}", UpgradeUI.instance.T.Defense, crew.AddDefnece,GameManager.UpgradeTankArmors, UpgradeUI.instance.T.Defense + crew.AddDefnece + GameManager.UpgradeTankArmors);
            }
        }
        else
        {
            CAddDefnce.value = crew.AddDefnece + UpgradeUI.instance.T.Defense;
            CAddSpeed.value = crew.AddSpeed + UpgradeUI.instance.T.Speed;
            CAddAttackRate.value = UpgradeUI.instance.T.AttackRate - crew.AddAttackRate;
            CrewPurchaseSetting[1].text = string.Format("{0} + {1} = {2}", UpgradeUI.instance.T.Defense, crew.AddDefnece, UpgradeUI.instance.T.Defense + crew.AddDefnece);
            CrewPurchaseSetting[2].text = string.Format("{0} + {1} = {2}", UpgradeUI.instance.T.Speed, crew.AddSpeed, UpgradeUI.instance.T.Speed + crew.AddSpeed);
            CrewPurchaseSetting[3].text = string.Format("{0} - {1} = {2}", UpgradeUI.instance.T.AttackRate, crew.AddAttackRate, UpgradeUI.instance.T.AttackRate - crew.AddAttackRate);
        }
    }

    public void CrewPurchase()
    {
        if(PurchasePanel.activeSelf == false)
        {
            PurchasePanel.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void YesButton()
    {
        if(GameManager.instance.CrewBase == CrewSetting.Crew.Pilot)
        {
            if(GameSetting.CoinCount >= CrewPurchasevalue)
            {
                GameSetting.CoinCount -= CrewPurchasevalue;
                GameSetting.NormalCrew = 1;
                PlayerPrefs.SetInt("NormalCrew",GameSetting.NormalCrew);
                PlayerPrefs.SetInt("CoinCount", GameSetting.CoinCount);
                PurchaseYesImage[0].gameObject.SetActive(true);

                if (GameSetting.Effect == 0)
                {
                    SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Stop();
                }
                else
                {
                    if (!SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().isPlaying)
                    {
                        SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Play();
                    }
                }
            }
            else
            {
                return;
            }
        }
        else if (GameManager.instance.CrewBase == CrewSetting.Crew.Profiient)
        {
            if (GameSetting.CoinCount >= CrewPurchasevalue)
            {
                GameSetting.CoinCount -= CrewPurchasevalue;
                GameSetting.SkilledCrew = 1;
                PlayerPrefs.SetInt("BestCrew", GameSetting.SkilledCrew);
                PlayerPrefs.SetInt("CoinCount", GameSetting.CoinCount);
                PurchaseYesImage[1].gameObject.SetActive(true);

                if (GameSetting.Effect == 0)
                {
                    SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Stop();
                }
                else
                {
                    if (!SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().isPlaying)
                    {
                        SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Play();
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

    public void NoButton()
    {
        PurchasePanel.SetActive(false);
    }
}