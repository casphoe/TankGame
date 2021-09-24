using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShell : CCompoent
{

    public int Damage;
    public LayerMask PlayerTanks;
    public ParticleSystem ExplosinParticles;
    public float ExplosionRadius = 5f;
    public float ExplosionForce = 1000f;

    private void Start()
    {
        Damage = EnemyManager.instance.Power;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] Colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, PlayerTanks);

        for(int i = 0; i < Colliders.Length; i++)
        {
            Rigidbody TargetRigidBody = Colliders[i].GetComponent<Rigidbody>();

            if(!TargetRigidBody)
            {
                continue;
            }

            TargetRigidBody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);

            Player player = TargetRigidBody.GetComponent<Player>();

            if(!player)
            {
                continue;
            }

            Damage = (int)ExpersionDamage(TargetRigidBody.position);

            player.PlayerDamage(Damage);
        }

        ExplosinParticles.Play();

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

        float explosionDistance = explosionTarget.magnitude;

        float relaviteDistance = (ExplosionRadius - explosionDistance) / ExplosionRadius;

        float damage = relaviteDistance + Damage;

        damage = Mathf.Max(Damage, damage);

        return damage;
    }
}
