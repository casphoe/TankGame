using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : CCompoent
{
    public static PoolingManager instance;

    public List<GameObject> PlayerShells = new List<GameObject>(); //플레이어 총알을 담아둘 리스트를 생성
    public List<GameObject> EnemyShells = new List<GameObject>(); //적들의 총알을 담아둘 리스트를 생성

    public GameObject PlayerShell;
    public GameObject EnemyShell;

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

    public void PlayerBulletManager(int Count)
    {
        for(int i = 0; i < Count; i++)
        {
            GameObject PS = Instantiate(PlayerShell) as GameObject;

            PS.transform.parent = gameObject.transform.GetChild(0).transform;

            PS.SetActive(false);

            PlayerShells.Add(PS);
        }
    }

    public void EnemyBulletManager(int Count)
    {
        for(int i = 0; i < Count; i++)
        {
            GameObject ES = Instantiate(EnemyShell) as GameObject;

            ES.transform.parent = gameObject.transform.GetChild(1).transform;

            ES.SetActive(false);

            EnemyShells.Add(ES);
        }
    }

    public GameObject PlayerBulletCreate(Vector3 pos)
    {
        GameObject Pshell = null;

        for(int i = 0; i < PlayerShells.Count; i++)
        {
            if(PlayerShells[i].activeSelf == false)
            {
                Pshell = PlayerShells[i];

                break;
            }
        }

        if(Pshell == null)
        {
            GameObject newPlayerShell = Instantiate(PlayerShell, pos, TankManger.instance.CreateShellPoistion.rotation);

            newPlayerShell.transform.parent = gameObject.transform.GetChild(0).transform;

            PlayerShells.Add(newPlayerShell);

            Pshell = newPlayerShell;
        }

        Pshell.SetActive(true);
        Pshell.transform.position = pos;
        return Pshell;
    }
}