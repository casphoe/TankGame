using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CCompoent
{

    public int Hp;
    public int Defence;

    public float Speed;
    public float AttackRate;

    public GameObject TankTurret;
    public GameObject ShellPoistion;

    public float RotateSpeed;

    private Quaternion PlayerRotate = Quaternion.identity;

    private JoyStick joystick;

    private void Start()
    {
        Hp = TankManger.instance.Hp;
        Defence = TankManger.instance.Deffence;
        Speed = TankManger.instance.Speed;
        AttackRate = TankManger.instance.AttackRate;
        
        RotateSpeed = TankManger.instance.TurnValue;

        PoolingManager.instance.PlayerBulletManager(30);
        joystick = GameObject.Find("Joystick").GetComponent<JoyStick>();

        if (GameSetting.Effect == 0)
        {
            SoundManager.instance.EffectAudio.transform.GetChild(4).GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (!SoundManager.instance.EffectAudio.transform.GetChild(4).GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(4).GetComponent<AudioSource>().Play();
            }
        }

        if(GameManager.instance.TankBase == PlayerSetting.TankDB.MediumTank)
        {
            PlayerRotate.eulerAngles = new Vector3(0f, 90f, 0f);
            transform.rotation = PlayerRotate;
        }
        else if(GameManager.instance.TankBase == PlayerSetting.TankDB.HeavyTank)
        {
            PlayerRotate.eulerAngles = new Vector3(0f, 90f, 0f);
            transform.rotation = PlayerRotate;
        }
    }

    public void PlayerDamage(int amout)
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
        TankManger.instance.HpSlider.value -= amout;
        TankManger.instance.HpText.text = "Hp : " + Hp;
        if(Hp <= 0)
        {

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

            GameSetting.IsGameOver = true;
            TankManger.instance.GameOver();

            Function.LateCallFunc(this, 0.2f, (CCompoent) =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    private void Update()
    {
        turn();
        TankManger.instance.TankPosition = this.gameObject.transform.position;
        TankManger.instance.CreateShellPoistion = ShellPoistion.gameObject.transform;
        TankManger.instance.TankTurret = TankTurret;
        TankManger.instance.ShotTimer += Time.deltaTime;
        Move();
    }

    void turn()
    {
        if(TankManger.instance.IsTurn == true)
        {
            if(TankManger.instance.TurnTank == Turn.Left)
            {
                //Vector3.down = vector3(0,-1,0) , Vector3.up = vector3(0,1,0)
                TankTurret.transform.Rotate(Vector3.down, RotateSpeed + Time.deltaTime); //y축 - 방향으로 계속 회전
            }
            else if(TankManger.instance.TurnTank == Turn.Right)
            {
                TankTurret.transform.Rotate(Vector3.up, RotateSpeed + Time.deltaTime);
            }
        }
    }

    public void Move()
    {

#if UNITY_STANDALONE

        float MoveZ = 0f; //vector(0,0,MoveZ) 왼쪽 오른쪽
        float MoveX = 0f; //vector(Movex,0,0) 위 아래

        if(Input.GetKey(KeyCode.W))
        {
            MoveX += 1f;
        }

        if(Input.GetKey(KeyCode.S))
        {
            MoveX -= 1f;
        }

        if(Input.GetKey(KeyCode.A))
        {
            MoveZ += 1f;
        }

        if(Input.GetKey(KeyCode.D))
        {
            MoveZ -= 1f;
        }

        transform.Translate(new Vector3(MoveX, 0f, MoveZ) * (Speed / 75f));

#elif UNITY_ANDROID
        if(TankManger.instance.MoveFlag == true)
        {
            Vector3 UpMovement = Vector3.right * Speed * Time.deltaTime * joystick.Horizontal;
            Vector3 rightMovement = Vector3.forward * Speed * Time.deltaTime * joystick.Vertical;

            transform.position += UpMovement;
            transform.position -= rightMovement;
            if (GameSetting.Effect == 0)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(3).GetComponent<AudioSource>().Stop();
            }
            else
            {
                if (!SoundManager.instance.EffectAudio.transform.GetChild(3).GetComponent<AudioSource>().isPlaying)
                {
                    SoundManager.instance.EffectAudio.transform.GetChild(3).GetComponent<AudioSource>().Play();
                }
            }
        }
#endif
    }
}