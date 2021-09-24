using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShotCharge : CCompoent,IPointerDownHandler,IPointerUpHandler
{
    private bool IsCharge = false;

    void Update()
    {
        if(IsCharge == true)
        {
            TankManger.instance.ChargeSlider.value += TankManger.instance.ChargeValue;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsCharge = true;
        
        if(GameSetting.Effect == 0)
        {
            SoundManager.instance.EffectAudio.transform.GetChild(9).GetComponent<AudioSource>().Stop();
        }
        else
        {
            if(!SoundManager.instance.EffectAudio.transform.GetChild(9).GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.instance.EffectAudio.transform.GetChild(9).GetComponent<AudioSource>().Play();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsCharge = false;
        TankManger.instance.ChargeCount = TankManger.instance.ChargeSlider.value;
        TankManger.instance.ChargeSlider.value = 0;
        TankManger.instance.ShellShot();
    }
}
