using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeImage : CCompoent,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
{
    public GameObject UpgradeSlider;

    public Upgrades.Upgrade upgrades;

    public void OnPointerDown(PointerEventData eventData)
    {
        UpgradeSlider.SetActive(true);

        if (upgrades == Upgrades.Upgrade.Track)
        {
            if(GameSetting.TrackUpgrade == false)
            {
                UpgradeUI.instance.TankUpgradeStat("Track");
                GameManager.UpgradeTracks = UpgradeUI.instance.Up.AddSpeed;
                
                if (GameSetting.Effect == 0)
                {
                    SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Stop();
                }
                else
                {
                    if (!SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().isPlaying)
                    {
                        SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Play();
                    }
                }
            }
            else
            {
                return;
            }
            
        }
        else if (upgrades == Upgrades.Upgrade.Turret)
        {
            if(GameSetting.TurretUpgrade == false)
            {
                UpgradeUI.instance.TankUpgradeStat("Turret");
                GameManager.UpgradeTurrets = UpgradeUI.instance.Up.AddAttackRate;
                if (GameSetting.Effect == 0)
                {
                    SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Stop();
                }
                else
                {
                    if (!SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().isPlaying)
                    {
                        SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Play();
                    }
                }
            }
            else
            {
                return;
            }
           
        }
        else if(upgrades == Upgrades.Upgrade.TankArmor)
        {
            if(GameSetting.TankArmorUpgrade == false)
            {
                UpgradeUI.instance.TankUpgradeStat("TankArmor");
                GameManager.UpgradeTankArmors = UpgradeUI.instance.Up.AddDefnece;
                if (GameSetting.Effect == 0)
                {
                    SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Stop();
                }
                else
                {
                    if (!SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().isPlaying)
                    {
                        SoundManager.instance.EffectAudio.transform.GetChild(8).GetComponent<AudioSource>().Play();
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
        {
            if(GameSetting.TrackUpgrade == true)
            {
                UIManager.instance.UpgradeSliders[0].gameObject.SetActive(true);
            }
            if(GameSetting.TurretUpgrade == true)
            {
                UIManager.instance.UpgradeSliders[1].gameObject.SetActive(true);
            }
            if(GameSetting.TankArmorUpgrade == true)
            {
                UIManager.instance.UpgradeSliders[2].gameObject.SetActive(true);
            }
        }
        UpgradeSlider.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
        {
            if (upgrades == Upgrades.Upgrade.Track)
            {
                if(GameSetting.TrackUpgrade == false)
                {
                    if (GameSetting.ScrewValue >= UpgradeUI.instance.Up.NeedScrewValue)
                    {
                        GameSetting.ScrewValue -= UpgradeUI.instance.Up.NeedScrewValue;
                        GameSetting.TrackUpgrade = true;
                        PlayerPrefs.SetString("TrackUpgrade", GameSetting.TrackUpgrade.ToString());
                        PlayerPrefs.SetInt("ScrewValue", GameSetting.ScrewValue);
                        PlayerPrefs.SetFloat("UpgradeTracks", GameManager.UpgradeTracks);
                        UpgradeUI.instance.YesObject[0].SetActive(true);

                        if (GameSetting.Effect == 0)
                        {
                            SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Stop();
                        }
                        else
                        {
                            if (!SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().isPlaying)
                            {
                                SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Play();
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            else if(upgrades == Upgrades.Upgrade.Turret)
            {
                if(GameSetting.TurretUpgrade == false)
                {
                    if (GameSetting.ScrewValue >= UpgradeUI.instance.Up.NeedScrewValue)
                    {
                        GameSetting.ScrewValue -= UpgradeUI.instance.Up.NeedScrewValue;
                        GameSetting.TurretUpgrade = true;
                        PlayerPrefs.SetString("TurretUpgrade", GameSetting.TurretUpgrade.ToString());
                        PlayerPrefs.SetInt("ScrewValue", GameSetting.ScrewValue);
                        PlayerPrefs.SetFloat("UpgradeTurrets", GameManager.UpgradeTurrets);
                        UpgradeUI.instance.YesObject[1].SetActive(true);

                        if (GameSetting.Effect == 0)
                        {
                            SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Stop();
                        }
                        else
                        {
                            if (!SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().isPlaying)
                            {
                                SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Play();
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if(GameSetting.TankArmorUpgrade == false)
                {
                    if(GameSetting.ScrewValue >= UpgradeUI.instance.Up.NeedScrewValue)
                    {
                        GameSetting.ScrewValue -= UpgradeUI.instance.Up.NeedScrewValue;
                        GameSetting.TankArmorUpgrade = true;
                        PlayerPrefs.SetString("TankArmorUpgrade", GameSetting.TankArmorUpgrade.ToString());
                        PlayerPrefs.SetInt("ScrewValue", GameSetting.ScrewValue);
                        PlayerPrefs.SetInt("UpgradeTankArmors", GameManager.UpgradeTankArmors);
                        UpgradeUI.instance.YesObject[2].SetActive(true);

                        if (GameSetting.Effect == 0)
                        {
                            SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Stop();
                        }
                        else
                        {
                            if (!SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().isPlaying)
                            {
                                SoundManager.instance.EffectAudio.transform.GetChild(7).GetComponent<AudioSource>().Play();
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
