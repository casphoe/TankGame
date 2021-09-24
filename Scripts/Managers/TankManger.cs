using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Turn
{
    Left,Right
}

public class TankManger : CCompoent
{

    public Dictionary<string, PlayerSetting> Tanks;
    public Dictionary<string, Upgrades> Upgrades;
    public Dictionary<string, CrewSetting> Crews;

    public Upgrades Up;
    public PlayerSetting T;
    public CrewSetting C;
    public GameObject[] CreateTanks = new GameObject[3];
    public GameObject TankCreatePosition;

    public Transform CreateShellPoistion;

    public GameObject TankTurret;

    public Sprite[] CrewSprite = new Sprite[3];
    public Image CrewImage;

    public bool IsTurn = false;
    public bool MoveFlag = false;

    public Vector3 TankPosition;

    public int Hp;
    public int Attack;
    public int Deffence;
    public float Speed;
    public float AttackRate;

    public float ChargeValue;
    public float TurnValue;

    public float ShotTimer;

    public float ChargeCount;

    public Slider HpSlider;
    public Text HpText;

    public Slider ChargeSlider;

    public GameObject GameOverPanel;
    public Text BestScoreText;

    public Text LevelEffectText;

    public Camera PlayerCamera;

    private float TankCreateTime;
    GameObject Player;

    public static TankManger instance;

    public Turn TurnTank;

    public override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            return;
        }
        Tanks = GameManager.instance.PlayerMGR();
        Upgrades = GameManager.instance.UpgradeMGR();
        Crews = GameManager.instance.CrewMGR();
        GameSetting.ISGameStart = false;
        CreateTankAndStat();
        CreateTank(TankCreatePosition.transform);
        ChargeValue = 0.007f;
        TurnValue = 0.08f;
        ChargeCount = 0f;
    }

    void CreateTankAndStat()
    {
        if(GameManager.instance.TankBase == PlayerSetting.TankDB.LightTank)
        {
            TankStat("LightTank");
            Hp = T.Hp;
            switch(GameManager.instance.CrewBase)
            {
                case CrewSetting.Crew.Beginner:
                    CrewStat("Beginner");
                    CrewImage.sprite = CrewSprite[0];
                    Attack = T.Power + C.AddAttack;
                    if(GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if(GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if(GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
                case CrewSetting.Crew.Pilot:
                    CrewStat("Pilot");
                    CrewImage.sprite = CrewSprite[1];
                    Attack = T.Power + C.AddAttack;
                    if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
                case CrewSetting.Crew.Profiient:
                    CrewStat("Profiient");
                    CrewImage.sprite = CrewSprite[2];
                    Attack = T.Power + C.AddAttack;
                    if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
            }
        }
        else if(GameManager.instance.TankBase == PlayerSetting.TankDB.MediumTank)
        {
            TankStat("MediumTank");
            Hp = T.Hp;
            switch (GameManager.instance.CrewBase)
            {
                case CrewSetting.Crew.Beginner:
                    CrewStat("Beginner");
                    CrewImage.sprite = CrewSprite[0];
                    Attack = T.Power + C.AddAttack;
                    if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
                case CrewSetting.Crew.Pilot:
                    CrewStat("Pilot");
                    CrewImage.sprite = CrewSprite[1];
                    Attack = T.Power + C.AddAttack;
                    if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
                case CrewSetting.Crew.Profiient:
                    CrewStat("Profiient");
                    CrewImage.sprite = CrewSprite[2];
                    Attack = T.Power + C.AddAttack;
                    if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
            }
        }
        else if(GameManager.instance.TankBase == PlayerSetting.TankDB.HeavyTank)
        {
            TankStat("HeavyTank");
            Hp = T.Hp;
            switch (GameManager.instance.CrewBase)
            {
                case CrewSetting.Crew.Beginner:
                    CrewStat("Beginner");
                    CrewImage.sprite = CrewSprite[0];
                    Attack = T.Power + C.AddAttack;
                    if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
                case CrewSetting.Crew.Pilot:
                    CrewStat("Pilot");
                    CrewImage.sprite = CrewSprite[1];
                    Attack = T.Power + C.AddAttack;
                    if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
                case CrewSetting.Crew.Profiient:
                    CrewStat("Profiient");
                    CrewImage.sprite = CrewSprite[2];
                    Attack = T.Power + C.AddAttack;
                    if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
                    {
                        if (GameSetting.TrackUpgrade == true)
                        {
                            UpgradeStat("Track");
                            Speed = T.Speed + C.AddSpeed + GameManager.UpgradeTracks;
                        }
                        else
                        {
                            Speed = T.Speed + C.AddSpeed;
                        }
                        if (GameSetting.TurretUpgrade == true)
                        {
                            UpgradeStat("Turret");
                            AttackRate = T.AttackRate - C.AddAttackRate - GameManager.UpgradeTurrets;
                        }
                        else
                        {
                            AttackRate = T.AttackRate - C.AddAttackRate;
                        }
                        if (GameSetting.TankArmorUpgrade == true)
                        {
                            UpgradeStat("TankArmor");
                            Deffence = T.Defense + C.AddDefnece + GameManager.UpgradeTankArmors;
                        }
                        else
                        {
                            Deffence = T.Defense + C.AddDefnece;
                        }
                    }
                    else
                    {
                        Deffence = T.Defense + C.AddDefnece;
                        Speed = T.Speed + C.AddSpeed;
                        AttackRate = T.AttackRate - C.AddAttackRate;
                    }
                    break;
            }
        }
    }

    void TankStat(string Name)
    {
        Tanks.TryGetValue(string.Format("{0}", Name), out T);
    }

    void UpgradeStat(string Name)
    {
        Upgrades.TryGetValue(string.Format("{0}", Name), out Up);
    }

    void CrewStat(string Name)
    {
        Crews.TryGetValue(string.Format("{0}", Name), out C);
    }

    void CreateTank(Transform Pos)
    {
        GameSetting.ISGameStart = true;
        if(GameManager.instance.TankBase == PlayerSetting.TankDB.LightTank)
        {
            Player = Instantiate(CreateTanks[0],Pos.position,Quaternion.identity);
        }
        else if (GameManager.instance.TankBase == PlayerSetting.TankDB.MediumTank)
        {
            Player = Instantiate(CreateTanks[1], Pos.position, Quaternion.identity);
        }
        else if (GameManager.instance.TankBase == PlayerSetting.TankDB.HeavyTank)
        {
            Player = Instantiate(CreateTanks[2], Pos.position, Quaternion.identity);
        }
    }

    public void GameOver()
    {
        CancelInvoke();
        PlayerPrefs.SetInt("CoinCount", GameSetting.CoinCount);
        PlayerPrefs.SetInt("ScrewValue", GameSetting.ScrewValue);
        PlayerPrefs.SetInt("Level", GameSetting.PlayerLevel);
        //게임오버
        GameOverPanel.SetActive(true);
        if (GameSetting.BestScore >= GameSetting.Score)
        {
            GameSetting.BestScore = GameSetting.Score;

            PlayerPrefs.SetInt("BestScore", GameSetting.BestScore);
        }
        BestScoreText.text = "BestScore : " + string.Format("{0:#,0}", GameSetting.BestScore);

        if (GameSetting.Effect == 0)
        {
            SoundManager.instance.EffectAudio.transform.GetChild(2).GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (!SoundManager.instance.EffectAudio.transform.GetChild(2).GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(2).GetComponent<AudioSource>().Play();
            }
        }
    }

    public void ShellShot()
    {
        if(ShotTimer >= AttackRate)
        {
            PoolingManager.instance.PlayerBulletCreate(new Vector3(CreateShellPoistion.transform.position.x,CreateShellPoistion.transform.position.y + 1.4f,CreateShellPoistion.position.z));

            ShotTimer = 0f;
        }
    }
}