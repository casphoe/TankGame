using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip BGM;

    public AudioClip TankShoot;
    public AudioClip Hit;
    public AudioClip GameOver;
    public AudioClip Engine;
    public AudioClip TankIdle;
    public AudioClip Experision;
    public AudioClip StageClear;
    public AudioClip Buy;
    public AudioClip Click;
    public AudioClip ShotCharge;
    public AudioClip LevelUp;

    public List<AudioClip> EffectAudioList;

    public AudioSource BackGroundAudio;
    public AudioSource EffectAudio;

    public override void Awake()
    {
        base.Awake();

        EffectAudioList = new List<AudioClip>();

        EffectAudioList.Add(TankShoot);
        EffectAudioList.Add(Hit);
        EffectAudioList.Add(GameOver);
        EffectAudioList.Add(Engine);
        EffectAudioList.Add(TankIdle);
        EffectAudioList.Add(Experision);
        EffectAudioList.Add(StageClear);
        EffectAudioList.Add(Buy);
        EffectAudioList.Add(Click);
        EffectAudioList.Add(ShotCharge);
        EffectAudioList.Add(LevelUp);

        for(int i = 0; i < EffectAudioList.Count; i++)
        {
            EffectAudio.gameObject.transform.GetChild(i).GetComponent<AudioSource>().clip = EffectAudioList[i];
        }

        if(BackGroundAudio.clip != null)
        {
            BGM = BackGroundAudio.clip;
        }
        else
        {
            BackGroundAudio.clip = BGM;
        }

        if(GameSetting.BGM == 0)
        {
            BackGroundAudio.Stop();
        }
        else
        {
            if(!BackGroundAudio.isPlaying)
            {
                BackGroundAudio.Play();
            }
        }

        if(GameSetting.Effect == 0)
        {
            for(int i = 0; i < EffectAudioList.Count; i++)
            {
                EffectAudio.gameObject.transform.GetChild(i).GetComponent<AudioSource>().Stop();
            }
        }
    }
}
