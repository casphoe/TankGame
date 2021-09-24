using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : CCompoent
{
    public Dictionary<string, PlayerSetting> Tanks;

    public Dictionary<string, Upgrades> TankUpgrades;
    public Upgrades Up;
    public PlayerSetting T;

    public static UpgradeUI instance;

    public GameObject[] YesObject = new GameObject[3];

    public Button[] Upgrade = new Button[3];

    public override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            return;
        }
    }

    private void Start()
    {
        TankUpgrades = GameManager.instance.UpgradeMGR();
        Tanks = GameManager.instance.PlayerMGR();

        if (GameSetting.TrackUpgrade == true || GameSetting.TurretUpgrade == true || GameSetting.TankArmorUpgrade == true)
        {
            if (GameSetting.TrackUpgrade == true)
            {
                UIManager.instance.UpgradeSliders[0].gameObject.SetActive(true);

                YesObject[0].SetActive(true);
                Upgrade[0].interactable = false;
            }
            if (GameSetting.TurretUpgrade == true)
            {
                UIManager.instance.UpgradeSliders[1].gameObject.SetActive(true);

                YesObject[1].SetActive(true);
                Upgrade[1].interactable = false;

            }
            if (GameSetting.TankArmorUpgrade == true)
            {
                UIManager.instance.UpgradeSliders[2].gameObject.SetActive(true);

                YesObject[2].SetActive(true);
                Upgrade[2].interactable = false;

            }
        }
        else
        {
            for (int i = 0; i < YesObject.Length; i++)
            {
                YesObject[i].SetActive(false);
            }
        }

        if (GameSetting.SkilledCrew == 1 || GameSetting.NormalCrew == 1)
        {
            if (GameSetting.SkilledCrew == 1)
            {
                UIManager.instance.PurchaseYesImage[1].gameObject.SetActive(true);
            }
            if (GameSetting.NormalCrew == 1)
            {
                UIManager.instance.PurchaseYesImage[0].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < UIManager.instance.PurchaseYesImage.Length; i++)
            {
                UIManager.instance.PurchaseYesImage[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (GameManager.instance.TankBase == PlayerSetting.TankDB.LightTank)
        {
            //UpgradeUI.instance.TankStat("LightTank");
            TankStat("LightTank");
        }
        else if (GameManager.instance.TankBase == PlayerSetting.TankDB.MediumTank)
        {
            //UpgradeUI.instance.TankStat("MediumTank");
            TankStat("MediumTank");
        }
        else if (GameManager.instance.TankBase == PlayerSetting.TankDB.HeavyTank)
        {
            //UpgradeUI.instance.TankStat("HeavyTank");
            TankStat("HeavyTank");
        }
    }

    public void TankStat(string Name)
    {
        Tanks.TryGetValue(string.Format("{0}", Name), out T);

        UIManager.instance.UpgradeSliders[0].value = T.Speed + GameManager.UpgradeTracks;
        UIManager.instance.UpgradeSliders[1].value = T.AttackRate - GameManager.UpgradeTurrets;
        UIManager.instance.UpgradeSliders[2].value = T.Defense + GameManager.UpgradeTankArmors;

        UIManager.instance.UpgradeText[0].text = string.Format("{0} + {1} = {2}", T.Speed, GameManager.UpgradeTracks, T.Speed + GameManager.UpgradeTracks);
        UIManager.instance.UpgradeText[1].text = string.Format("{0} - {1} = {2}", T.AttackRate, GameManager.UpgradeTurrets, T.AttackRate - GameManager.UpgradeTurrets);
        UIManager.instance.UpgradeText[2].text = string.Format("{0} + {1} = {2}", T.Defense, GameManager.UpgradeTankArmors, T.Defense + GameManager.UpgradeTankArmors);
    }

    public void TankUpgradeStat(string Name)
    {
        TankUpgrades.TryGetValue(string.Format("{0}", Name), out Up);
    }
}