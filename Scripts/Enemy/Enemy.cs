using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Enemy : CCompoent
{
    public int Hp;
    public int Defence;
    public float Speed;
    public float AttackRate;
    public float Scan;

    public GameObject TurnTurret;
    public GameObject ShellPoistion;

    public float RotateSpeed;

    public int Num = 0;

    public float RotateTime = 0f;

    public float distance;

    public Slider HpSlider;
    public Image FillImage;
    public Text ScoreText;

    public Color FullHealthColor = Color.green; //체력바가 풀일 때 green색상
    public Color ZeroHealthColor = Color.red; //체력바가 없을 때 red 색상

    public Vector3 Target;
    public NavMeshAgent Navi;

    private Quaternion Rotate = Quaternion.identity;
    float Rt;

    private float ShotTimer;

    // Start is called before the first frame update
    void Start()
    {
        Hp = EnemyManager.instance.Hp;
        Defence = EnemyManager.instance.EnemyDefence;
        Speed = EnemyManager.instance.Speed;
        AttackRate = EnemyManager.instance.AttackRate;
        Scan = EnemyManager.instance.PlayerScanValue;
        RotateSpeed = EnemyManager.instance.RotateSpeed;
        HpSlider.maxValue = EnemyManager.instance.Hp;
        HpUI();
        RotateTime = EnemyManager.instance.RT;
        Navi = GetComponent<NavMeshAgent>();
        Navi.speed = Speed;
        Navi.stoppingDistance = Scan;
        ScoreText = EnemyManager.instance.EnemyScoreText;
    }

    void HpUI()
    {
        HpSlider.value = Hp;

        //FillImage의 색깔을 ZeroHealthColor에서 FullHealthColor 변경
        FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, Hp / EnemyManager.instance.Hp);
    }

    private void Update()
    {
        Target = TankManger.instance.TankPosition;
        distance = Vector3.Distance(Target, transform.position);
        
        PlayerScan();
    }

    void PlayerScan()
    {
        if(GameSetting.IsGameOver == false)
        {
            switch(Num)
            {
                case 0:
                    LeftTurretTurn();
                    break;
                case 1:
                    RightTurretTurn();
                    break;
                case 2:
                    TurretReturn();
                    break;
                case 3:
                    PlayerDisanceMove();
                    break;
                case 4:
                    Find();
                    break;
            }
        }
    }

    void LeftTurretTurn()
    {
        Rotate.eulerAngles = new Vector3(0f, -45f, 0f);
        TurnTurret.transform.rotation = Quaternion.Slerp(TurnTurret.transform.rotation, Rotate, RotateSpeed + Time.deltaTime);
        Rt += RotateTime * Time.deltaTime;
        if(Rt >= 90)
        {
            Rt = 0f;
            Num++;
        }
    }

    void RightTurretTurn()
    {
        Rotate.eulerAngles = new Vector3(0f, 360-125f, 0f);
        TurnTurret.transform.rotation = Quaternion.Slerp(TurnTurret.transform.rotation, Rotate, RotateSpeed + Time.deltaTime);
        Rt += RotateTime * Time.deltaTime;
        if (Rt >= 90)
        {
            Rt = 0f;
            Num++;
        }
    }

    void TurretReturn()
    {
        RotateTime = EnemyManager.instance.RT;
        Rotate.eulerAngles = new Vector3(0f, 360-90f, 0f);
        TurnTurret.transform.rotation = Quaternion.Slerp(TurnTurret.transform.rotation, Rotate, RotateSpeed + Time.deltaTime);
        Rt += RotateTime * Time.deltaTime;
        if(Rt >= 90)
        {
            Rt = 0f;
            Num++;
        }
    }
    
    //플레이어 방향으로 일정 방향 이동시킴
    void PlayerDisanceMove()
    {
        if(Navi.isStopped == true)
        {
            Navi.isStopped = false;
        }

        Navi.destination = new Vector3(Target.x, Target.y, Target.z);

        gameObject.transform.GetChild(0).transform.LookAt(Target);


        if(distance <= Scan)
        {
            Num += 1;
        }
    }

    //플레이어를 찾았을 경우
    void Find()
    {
        Navi.isStopped = true;

        //총알 생성
        ShotTimer += Time.deltaTime;
        gameObject.transform.GetChild(0).transform.LookAt(Target);
        EnemyShellShot();

        if(distance > Scan)
        {
            Num = 3;
        }

        
    }

    public void EnemyDamage(int amout)
    {

        if (GameSetting.Effect == 0)
        {
            SoundManager.instance.EffectAudio.transform.GetChild(1).GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (!SoundManager.instance.EffectAudio.transform.GetChild(1).GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(1).GetComponent<AudioSource>().Play();
            }
        }

        amout = amout - Defence;
        Hp -= amout;
        HpUI();
        if (Hp <= 0)
        {
            EnemyManager.instance.StageClearEnemyCount -= 1;
            GameSetting.Exp += EnemyManager.instance.AddScore / 2;
            GameSetting.CoinCount += EnemyManager.instance.AddScore / 2;
            GameSetting.ScrewValue += EnemyManager.instance.AddScore / 2;
            GameSetting.Score += EnemyManager.instance.AddScore;
            ScoreText.gameObject.SetActive(true);
            ScoreText.text = "+ " + EnemyManager.instance.AddScore;

            PlayerPrefs.SetInt("Exp", GameSetting.Exp);

            if(GameSetting.PlayerLevel < 9)
            {
                LevelManager.instance.LevelUp();

                if (GameSetting.isLevelUp == true)
                {
                    TankManger.instance.LevelEffectText.gameObject.SetActive(true);
                    if (GameSetting.Effect == 0)
                    {
                        SoundManager.instance.EffectAudio.transform.GetChild(10).GetComponent<AudioSource>().Stop();
                    }
                    else
                    {
                        if (!SoundManager.instance.EffectAudio.transform.GetChild(10).GetComponent<AudioSource>().isPlaying)
                        {
                            SoundManager.instance.EffectAudio.transform.GetChild(10).GetComponent<AudioSource>().Play();
                        }
                    }
                }
            }


            if (GameSetting.Effect == 0)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(5).GetComponent<AudioSource>().Stop();
            }
            else
            {
                if (!SoundManager.instance.EffectAudio.transform.GetChild(5).GetComponent<AudioSource>().isPlaying)
                {
                    SoundManager.instance.EffectAudio.transform.GetChild(5).GetComponent<AudioSource>().Play();
                }
            }

            if(EnemyManager.instance.StageClearEnemyCount <= 0)
            {
                GameSetting.StageClear = true;
            }

            Function.LateCallFunc(this, 0.2f, (CCompoent) =>
              {
                  gameObject.SetActive(false);
              });
        }
    }

    private void EnemyShellShot()
    {
        if(ShotTimer >= AttackRate)
        {
            EnemyBulletCreate(new Vector3(ShellPoistion.transform.position.x, ShellPoistion.transform.position.y + 1.3f, ShellPoistion.transform.position.z));

            ShotTimer = 0f;
        }
    }

    private GameObject EnemyBulletCreate(Vector3 pos)
    {
        GameObject Es = null;

        for(int i = 0; i < PoolingManager.instance.EnemyShells.Count; i++)
        {
            if(PoolingManager.instance.EnemyShells[i].activeSelf == false)
            {
                Es = PoolingManager.instance.EnemyShells[i];
                break;
            }
        }

        if(Es == null)
        {
            GameObject newEshell = Instantiate(PoolingManager.instance.EnemyShell, pos, TurnTurret.transform.rotation);

            newEshell.transform.parent = PoolingManager.instance.gameObject.transform.GetChild(1).transform;

            PoolingManager.instance.EnemyShells.Add(newEshell);

            Es = newEshell;
        }

        Es.SetActive(true);

        Es.transform.position = pos;

        Rigidbody enemyShell = Es.GetComponent<Rigidbody>();

        enemyShell.velocity = ShellPoistion.transform.forward * 125;

        Vector3 EnemyShellRotate = enemyShell.velocity;

        var Rotation = Quaternion.LookRotation(EnemyShellRotate);

        enemyShell.transform.rotation = Rotation;
        return Es;
    }
}
