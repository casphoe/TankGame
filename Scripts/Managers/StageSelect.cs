using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : CCompoent
{
    public GameObject[] CrewS = new GameObject[2];

    public GameObject[] StageStarImage = new GameObject[3];

    public GameObject CrewSelectPanel;
    public GameObject GameStartButton;

    public Dictionary<string, PlayerSetting> Tanks;

    public Dictionary<string, Upgrades> TankUpgrades;
    public Dictionary<string, CrewSetting> crew;

    public Upgrades Up;
    public PlayerSetting T;
    public CrewSetting C;

    public Slider[] StatSlider = new Slider[5];
    public Text[] StatText = new Text[5];

    public GameObject GameDifficultyPanel;

    public Text PlayerLevelText;

    private void Start()
    {
        CrewSelectPanel.SetActive(false);
        GameStartButton.SetActive(false);
        if (GameSetting.SkilledCrew == 1 || GameSetting.NormalCrew == 1)
        {
            if(GameSetting.NormalCrew == 1)
            {
                CrewS[0].SetActive(true);
            }
            if(GameSetting.SkilledCrew == 1)
            {
                CrewS[1].SetActive(true);
            }
        }
        Tanks = GameManager.instance.PlayerMGR();
        TankUpgrades = GameManager.instance.UpgradeMGR();
        crew = GameManager.instance.CrewMGR();
        PlayerLevelText.text = "PlayerLevel : " + GameSetting.PlayerLevel;
        GameDifficultyPanel.SetActive(false);
    }

    public void StageStart()
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


        CrewSelectPanel.SetActive(true);
    }

    public void Cancel()
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

        CrewSelectPanel.SetActive(false);
    }

    public void Crewselect()
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

        GameStartButton.SetActive(true);
        if(GameManager.instance.TankBase == PlayerSetting.TankDB.LightTank)
        {
            TankStat("LightTank");
            switch (GameManager.instance.CrewBase)
            {
                case CrewSetting.Crew.Beginner:                   
                    CrewAddStat("Beginner");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
                case CrewSetting.Crew.Pilot:
                    
                    CrewAddStat("Pilot");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
                case CrewSetting.Crew.Profiient:
                    CrewAddStat("Profiient");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
            }
        }
        else if(GameManager.instance.TankBase == PlayerSetting.TankDB.MediumTank)
        {
            TankStat("MediumTank");

            switch (GameManager.instance.CrewBase)
            {
                case CrewSetting.Crew.Beginner:
                    
                    CrewAddStat("Beginner");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
                case CrewSetting.Crew.Pilot:
                    
                    CrewAddStat("Pilot");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
                case CrewSetting.Crew.Profiient:
                    
                    CrewAddStat("Profiient");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
            }
        }
        else if(GameManager.instance.TankBase == PlayerSetting.TankDB.HeavyTank)
        {
            TankStat("HeavyTank");

            switch (GameManager.instance.CrewBase)
            {
                case CrewSetting.Crew.Beginner:
                    
                    CrewAddStat("Beginner");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
                case CrewSetting.Crew.Pilot:
                    
                    CrewAddStat("Pilot");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
                case CrewSetting.Crew.Profiient:
                    
                    CrewAddStat("Profiient");
                    StatSlider[0].value = T.Hp;
                    StatText[0].text = "" + T.Hp;
                    StatSlider[1].value = T.Power + C.AddAttack;
                    StatText[1].text = "" + (T.Power + C.AddAttack);
                    if (GameSetting.TurretUpgrade == true || GameSetting.TrackUpgrade == true || GameSetting.TankArmorUpgrade)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            TankUpgradeStat("Track");
                            StatSlider[3].value = T.Speed + GameManager.UpgradeTracks + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + GameManager.UpgradeTracks + C.AddSpeed);
                        }
                        else
                        {
                            StatSlider[3].value = T.Speed + C.AddSpeed;
                            StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            TankUpgradeStat("Turret");
                            StatSlider[4].value = (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                            StatText[4].text = "" + (T.AttackRate - GameManager.UpgradeTurrets - C.AddAttackRate);
                        }
                        else
                        {
                            StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                            StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            TankUpgradeStat("TankArmor");
                            StatSlider[2].value = T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + GameManager.UpgradeTankArmors + C.AddDefnece);
                        }
                        else
                        {
                            StatSlider[2].value = T.Defense + C.AddDefnece;
                            StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        }
                    }
                    else
                    {
                        StatSlider[2].value = T.Defense + C.AddDefnece;
                        StatText[2].text = "" + (T.Defense + C.AddDefnece);
                        StatSlider[3].value = T.Speed + C.AddSpeed;
                        StatText[3].text = "" + (T.Speed + C.AddSpeed);
                        StatSlider[4].value = T.AttackRate - C.AddAttackRate;
                        StatText[4].text = "" + (T.AttackRate - C.AddAttackRate);
                    }
                    break;
            }
        }
    }

    public void DifficutlyStart()
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

        switch (StageUI.instacne.GetStage)
        {
            case GameSetting.Stage.Stage1:
                GameManager.instance.StageSelect = GameSetting.Stage.Stage1;
                break;
            case GameSetting.Stage.Stage2:
                GameManager.instance.StageSelect = GameSetting.Stage.Stage2;
                break;
        }

        GameDifficultyPanel.SetActive(true);
    }

    public void EasyButton()
    {
        GameManager.instance.difficulty = GameSetting.Difficulty.Easy;
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
    }

    public void NormalButton()
    {
        GameManager.instance.difficulty = GameSetting.Difficulty.Normal;
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
    }

    public void HardButton()
    {
        GameManager.instance.difficulty = GameSetting.Difficulty.Hard;
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
    }

    public void ExitButton()
    {
        GameDifficultyPanel.SetActive(false);
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
    }

    public void GameStart()
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

        switch (StageUI.instacne.GetStage)
        {
            case GameSetting.Stage.Stage1:               
                ScneLoader.instance.LoadScene(CDefine.SCENE_NAME_TANKGAME_GAMESCENE);
                break;
            case GameSetting.Stage.Stage2:
                break;
        }
    }

    void TankStat(string Name)
    {
        Tanks.TryGetValue(string.Format("{0}", Name), out T);        
    }

    void TankUpgradeStat(string Name)
    {
        TankUpgrades.TryGetValue(string.Format("{0}", Name), out Up);
    }

    void CrewAddStat(string Name)
    {
        crew.TryGetValue(string.Format("{0}", Name), out C);
    }
}