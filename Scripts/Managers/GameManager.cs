using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
[System.Serializable]
public class PlayerSetting
{
    public string TankName;
    public int Hp;
    public int Power;
    public int Defense;
    public float Speed;
    public float AttackRate;

    public void PlayerDB(string Name,int tankhp,int attack,int defencse,float speed,float Rate)
    {
        TankName = Name;
        Hp = tankhp;
        Power = attack;
        Defense = defencse;
        Speed = speed;
        AttackRate = Rate;
    }

    public enum TankDB
    {
        LightTank,MediumTank,HeavyTank
    }
}
[System.Serializable]
public class Upgrades
{
    public string UpgradeName;
    public int AddDefnece;
    public float AddSpeed;
    public float AddAttackRate;
    public int NeedScrewValue;

    public void UpgradeDB(string Name,int defnece,float Speed,float Rate,int value)
    {
        UpgradeName = Name;
        AddDefnece = defnece;
        AddSpeed = Speed;
        AddAttackRate = Rate;
        NeedScrewValue = value;
    }

    public enum Upgrade
    {
        Track,Turret,TankArmor
    }
}
[System.Serializable]
public class EnemySetting
{
    public string EnemyName;
    public int Hp;
    public int attack;
    public int defence;
    public float Speed;
    public float AttackRate;
    public float ScanValue;
    public float CreateTime;
    public int AddScore;
    public float RotateSpeed;
    public float RotateTime;

    public void EnemyDB(string Name,int EnemyHp,int EnemyPower,int EnemyDefence,float speed,float rate,float scan,float Time,int Score,float Rotate,float RT)
    {
        EnemyName = Name;
        Hp = EnemyHp;
        attack = EnemyPower;
        defence = EnemyDefence;
        Speed = speed;
        AttackRate = rate;
        ScanValue = scan;
        CreateTime = Time;
        AddScore = Score;
        RotateSpeed = Rotate;
        RotateTime = RT;
    }
}
[System.Serializable]
public class CrewSetting
{
    public string CrewName;
    public int AddAttack;
    public int AddDefnece;
    public float AddSpeed;
    public float AddAttackRate;
    public int Purchase;

    public void CrewDB(string Name,int attack,int defence,float speed,float rate,int count)
    {
        CrewName = Name;
        AddAttack = attack;
        AddDefnece = defence;
        AddSpeed = speed;
        AddAttackRate = rate;
        Purchase = count;
    }

    public enum Crew
    {
        Beginner, Pilot, Profiient
    }
}
[System.Serializable]
public class GameSetting
{

    public static int PlayerLevel = 1;
    public static int Score;
    public static int BestScore;
    public static float BGM;
    public static float Effect;
    public static int CoinCount = 0;
    public static int ScrewValue = 0;

    public static int NormalCrew = 0;
    public static int SkilledCrew = 0;

    public static bool TrackUpgrade = false;
    public static bool TurretUpgrade = false;
    public static bool TankArmorUpgrade = false;

    public static bool IsGameOver = false;
    public static bool ISGameStart = false;
    public static bool StageClear = false;
    public static bool isLevelUp = false;

    public static int StageClearNum = 0;

    public static int Exp = 0;
    public static int MaxEnemyCreate = 0;


    public static float GameTime = 0;


    public enum Stage
    {
        Stage1,Stage2,Stage3
    }

    public enum Difficulty
    {
        Easy,Normal,Hard
    }

    public static void DifficultyStageClearEnemyCount()
    {
        switch(GameManager.instance.difficulty)
        {
            case Difficulty.Easy:
                GameTime = 420f;
                MaxEnemyCreate = 10;
                break;
            case Difficulty.Normal:
                GameTime = 320f;
                MaxEnemyCreate = 14;
                break;
            case Difficulty.Hard:
                GameTime = 250f;
                MaxEnemyCreate = 18;
                break;
        }
    }
}

public class GameManager : Singleton<GameManager>
{

    public Dictionary<string, PlayerSetting> Players = new Dictionary<string, PlayerSetting>();
    public Dictionary<string, EnemySetting> Enemys = new Dictionary<string, EnemySetting>();
    public Dictionary<string, Upgrades> Upgrade = new Dictionary<string, Upgrades>();
    public Dictionary<string, CrewSetting> Crews = new Dictionary<string, CrewSetting>();

    public CrewSetting.Crew CrewBase;
    public GameSetting.Stage StageSelect;
    public PlayerSetting.TankDB TankBase;
    public Upgrades.Upgrade upgrades;
    public GameSetting.Difficulty difficulty;

    public static float UpgradeTracks;
    public static float UpgradeTurrets;
    public static int UpgradeTankArmors;

    public override void Awake()
    {
        base.Awake();
        PlayerData();
        EnemyData();
        CrewData();
        UpgradeData();

        GameSetting.PlayerLevel = PlayerPrefs.GetInt("Level", GameSetting.PlayerLevel);
        GameSetting.BestScore = PlayerPrefs.GetInt("BestScore", GameSetting.BestScore);
        GameSetting.BGM = PlayerPrefs.GetFloat("BGM",GameSetting.BGM);
        GameSetting.Effect = PlayerPrefs.GetFloat("Effect",GameSetting.Effect);
        GameSetting.NormalCrew = PlayerPrefs.GetInt("NormalCrew", GameSetting.NormalCrew);
        GameSetting.SkilledCrew = PlayerPrefs.GetInt("BestCrew", GameSetting.SkilledCrew);
        GameSetting.CoinCount = PlayerPrefs.GetInt("CoinCount", GameSetting.CoinCount);
        GameSetting.ScrewValue = PlayerPrefs.GetInt("ScrewValue", GameSetting.ScrewValue);
        GameSetting.Exp = PlayerPrefs.GetInt("Exp", GameSetting.Exp);

        UpgradeTracks = PlayerPrefs.GetFloat("UpgradeTracks",GameManager.UpgradeTracks);
        UpgradeTurrets = PlayerPrefs.GetFloat("UpgradeTurrets",GameManager.UpgradeTurrets);
        UpgradeTankArmors = PlayerPrefs.GetInt("UpgradeTankArmors",GameManager.UpgradeTankArmors);

        string UpgradeTrack = PlayerPrefs.GetString("TrackUpgrade",GameSetting.TrackUpgrade.ToString());
        GameSetting.TrackUpgrade = System.Convert.ToBoolean(UpgradeTrack); //UpgradeTrack의 스트링 값을 불값으로 전환

        string UpgradeTurret = PlayerPrefs.GetString("TurretUpgrade",GameSetting.TurretUpgrade.ToString());
        GameSetting.TurretUpgrade = System.Convert.ToBoolean(UpgradeTrack);

        string UpgradeTankArmor = PlayerPrefs.GetString("TankArmorUpgrade", GameSetting.TankArmorUpgrade.ToString());
        GameSetting.TankArmorUpgrade = System.Convert.ToBoolean(UpgradeTankArmor);

        


    }

    void PlayerData()
    {
        PlayerSetting Tank1 = new PlayerSetting();
        Tank1.PlayerDB("LightTank", 180, 2, 2, 7, 2.5f);
        Players.Add(Tank1.TankName, Tank1);

        PlayerSetting Tank2 = new PlayerSetting();
        Tank2.PlayerDB("MediumTank", 340, 4, 4, 5, 3f);
        Players.Add(Tank2.TankName, Tank2);

        PlayerSetting Tank3 = new PlayerSetting();
        Tank3.PlayerDB("HeavyTank", 500, 8, 8, 3, 3.5f);
        Players.Add(Tank3.TankName, Tank3);
    }

    void EnemyData()
    {
        EnemySetting Enemy1 = new EnemySetting();
        //이름,체력,공격력,방어력,스피드,공격 시간,탐지범위,생성시간,점수,회전 속도
        Enemy1.EnemyDB("WeakTank", 18, 18, 1, 3, 2f, 20f, 2.5f, 10, 0.002f, 12.5f);
        Enemys.Add(Enemy1.EnemyName, Enemy1);

        EnemySetting Enemy2 = new EnemySetting();
        Enemy2.EnemyDB("Tank", 25, 20, 2, 6, 3f, 1.5f, 23f, 20, 0.004f, 15f);
        Enemys.Add(Enemy2.EnemyName, Enemy2);

        EnemySetting Enemy3 = new EnemySetting();
        Enemy3.EnemyDB("HardTank", 35, 22, 3, 9, 1f, 25f, 1.5f, 40, 0.008f , 17.5f);
        Enemys.Add(Enemy3.EnemyName, Enemy3);
    }

    void CrewData()
    {
        CrewSetting Crew1 = new CrewSetting();
        Crew1.CrewDB("Beginner", 2, 2, 2, 0.2f, 0);
        Crews.Add(Crew1.CrewName, Crew1);

        CrewSetting Crew2 = new CrewSetting();
        Crew2.CrewDB("Pilot", 3, 3, 3f, 0.3f, 400);
        Crews.Add(Crew2.CrewName, Crew2);

        CrewSetting Crew3 = new CrewSetting();
        Crew3.CrewDB("Profiient", 4, 4, 5f, 0.4f, 800);
        Crews.Add(Crew3.CrewName, Crew3);
    }

    void UpgradeData()
    {
        Upgrades Upgrade1 = new Upgrades();
        Upgrade1.UpgradeDB("Track",0,2f,0,200);
        Upgrade.Add(Upgrade1.UpgradeName, Upgrade1);

        Upgrades Upgrade2 = new Upgrades();
        Upgrade2.UpgradeDB("Turret", 0, 0, 0.2f, 300);
        Upgrade.Add(Upgrade2.UpgradeName, Upgrade2);

        Upgrades Upgrade3 = new Upgrades();
        Upgrade3.UpgradeDB("TankArmor", 4, 0, 0, 400);
        Upgrade.Add(Upgrade3.UpgradeName, Upgrade3);

    }

    public Dictionary<string,PlayerSetting> PlayerMGR()
    {
        return Players;
    }

    public Dictionary<string,EnemySetting> EnemyMGR()
    {
        return Enemys;
    }

    public Dictionary<string,Upgrades> UpgradeMGR()
    {
        return Upgrade;
    }

    public Dictionary<string,CrewSetting> CrewMGR()
    {
        return Crews;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}