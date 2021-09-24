using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundImage : CCompoent
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameSetting.BGM == 0 && GameSetting.Effect == 0)
        {
            UIManager.instance.IsSoundOn = false;

            UIManager.instance.SoundImage.GetComponent<Image>().sprite = UIManager.instance.SoundOff;
        }
        else
        {
            UIManager.instance.IsSoundOn = true;

            UIManager.instance.SoundImage.GetComponent<Image>().sprite = UIManager.instance.SoundOn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
