using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShell : CCompoent
{
    private Rigidbody rigid;

    public int Damage;
    public LayerMask Enemy;
    public ParticleSystem ExplosinParticles;
    public float ExplosionRadius = 5f;
    public float ExplosionForce = 1000f;

    // Start is called before the first frame update
    void Start()
    { 
        Damage = TankManger.instance.Attack;
        rigid = GetComponent<Rigidbody>();

       /* if (GameSetting.Effect == 0)
        {
            SoundManager.instance.EffectAudio.transform.GetChild(0).GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (!SoundManager.instance.EffectAudio.transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(0).GetComponent<AudioSource>().Play();
            }
        }*/
    }

    private void Update()
    {
        ParabolaMove();
    }


    void ParabolaMove()
    {
        
        rigid.velocity = TankManger.instance.CreateShellPoistion.forward * TankManger.instance.ChargeCount * 65;
        //총알의 진행 방향을 Move라는 백터로 저장
        Vector3 Move = rigid.velocity;
        //move라는 타겟으로 회전한다.
        var Rotation = Quaternion.LookRotation(Move);
        transform.rotation = Rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] Colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, Enemy);

        for (int i = 0; i < Colliders.Length; i++)
        {
            Rigidbody TargetRigidBody = Colliders[i].GetComponent<Rigidbody>();
            //충돌한 객체에 rigidbody없으면 다음 콜라이더 넘어갑니다
            if (!TargetRigidBody)
            {
                continue;
            }

            TargetRigidBody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);

            Enemy enemys = TargetRigidBody.GetComponent<Enemy>();

            if(!enemys)
            {
                continue;
            }
            Damage = (int)ExpersionDamage(TargetRigidBody.position);

            enemys.EnemyDamage(Damage);
        }


        //파티클을 실행
        ExplosinParticles.Play();

        //오디오 실행

        //제어 하고자 하는 파티클에 접근해 속성을 변화시키는 방식
        ParticleSystem.MainModule mainModule = ExplosinParticles.main;

        //충돌이 일어나고 0.2초 후 오브젝트를 끔
        Function.LateCallFunc(this, 0.3f, (CCompoent) =>
        {
            gameObject.SetActive(false);
        });
    }

    private float ExpersionDamage(Vector3 targetPosition)
    {
        Vector3 explosionTarget = targetPosition - transform.position;

        float explosionDistane = explosionTarget.magnitude;

        float relaviteDistance = (ExplosionRadius - explosionDistane) / ExplosionRadius;

        float damage = relaviteDistance + Damage;

        damage = Mathf.Max(Damage, damage);

        return damage;
    }
}