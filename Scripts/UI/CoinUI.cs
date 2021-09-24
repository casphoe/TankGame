using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinUI : CCompoent
{

    public Text CoinText;
    public Text ScrewText;

    void Update()
    {
        CoinText.text = "Coin : " + string.Format("{0:#,0}", GameSetting.CoinCount);
        ScrewText.text = ": " + string.Format("{0:#,0}", GameSetting.ScrewValue);
    }
}
