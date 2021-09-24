using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : CCompoent
{
    public int Hp;
    public int Power;
    public int EnemyDefence;
    public float Speed;
    public float AttackRate;
    public float PlayerScanValue;
    public float CreateTime;
    public int AddScore;
    public int StageClearEnemyCount;
    public float RotateSpeed;
    public float RT;

    public Text EnemyScoreText;

    public static EnemyManager instance;

    public Dictionary<string, EnemySetting> Enemys;
    public EnemySetting enemys;

    public GameObject EnemyTank;
    public Transform[] EnemyCreatePosition = new Transform[3];
    public Transform Create;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        Enemys = GameManager.instance.EnemyMGR();
        GameSetting.DifficultyStageClearEnemyCount();
        switch (GameManager.instance.difficulty)
        {
            case GameSetting.Difficulty.Easy:
                StageClearEnemyCount = GameSetting.MaxEnemyCreate;
                EnemyStat("WeakTank");
                PoolingManager.instance.EnemyBulletManager(10 * StageClearEnemyCount);
                if(TankManger.instance.Deffence >= Power)
                {
                    Power = TankManger.instance.Deffence + 2;
                }
                else
                {
                    Power = enemys.attack;
                }
                InvokeRepeating("CreateEnemy", CreateTime, CreateTime);
                //Invoke("CreateEnemy",CreateTime);
                break;
            case GameSetting.Difficulty.Normal:
                EnemyStat("Tank");
                StageClearEnemyCount = GameSetting.MaxEnemyCreate;
                PoolingManager.instance.EnemyBulletManager(10 * StageClearEnemyCount);
                if (TankManger.instance.Deffence >= Power)
                {
                    Power = TankManger.instance.Deffence + 4;
                }
                else
                {
                    Power = enemys.attack;
                }
                //Invoke("CreateEnemy", CreateTime);
                InvokeRepeating("CreateEnemy", CreateTime, CreateTime);
                break;
            case GameSetting.Difficulty.Hard:
                EnemyStat("HardTank");               
                StageClearEnemyCount = GameSetting.MaxEnemyCreate;
                PoolingManager.instance.EnemyBulletManager(10 * StageClearEnemyCount);
                if (TankManger.instance.Deffence >= Power)
                {
                    Power = TankManger.instance.Deffence + 8;
                }
                else
                {
                    Power = enemys.attack;
                }
                //Invoke("CreateEnemy", CreateTime);
                InvokeRepeating("CreateEnemy", CreateTime, CreateTime);
                break;
        }
    }

    

    void EnemyStat(string Name)
    {
        Enemys.TryGetValue(string.Format("{0}", Name), out enemys);
        Hp = enemys.Hp;
        EnemyDefence = enemys.defence;
        Speed = enemys.Speed;
        AttackRate = enemys.AttackRate;
        PlayerScanValue = enemys.ScanValue;
        CreateTime = enemys.CreateTime;
        AddScore = enemys.AddScore;
        RotateSpeed = enemys.RotateSpeed;
        RT = enemys.RotateTime;
    }

    void CreateEnemy()
    {
        if (StageClearEnemyCount > 0 && GameSetting.IsGameOver == false)
        {

            int RandomCreate = Random.Range(0, 2);

            if (RandomCreate == 0)
            {
                Create = EnemyCreatePosition[0];
            }
            else if (RandomCreate == 1)
            {
                Create = EnemyCreatePosition[1];
            }
            else
            {
                Create = EnemyCreatePosition[2];
            }
            GameObject TankEnemy = Instantiate(EnemyTank, new Vector3(Create.position.x, 9f, Create.position.z), Quaternion.identity);
            TankEnemy.transform.SetParent(this.gameObject.transform);
            StageClearEnemyCount--;
            CreateTime += 0.5f;
        }
        else
        {
            return;
        }
    }

}
