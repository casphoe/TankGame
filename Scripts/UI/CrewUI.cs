using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrewUI : CCompoent,IPointerDownHandler, IPointerClickHandler
{
    public CrewSetting.Crew crew;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(crew == CrewSetting.Crew.Pilot)
        {
            UIManager.instance.CrewStat("Pilot");
            GameManager.instance.CrewBase = CrewSetting.Crew.Pilot;
        }
        else if(crew == CrewSetting.Crew.Profiient)
        {
            UIManager.instance.CrewStat("Profiient");
            GameManager.instance.CrewBase = CrewSetting.Crew.Profiient;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
        {
            if(crew == CrewSetting.Crew.Pilot)
            {
                if(GameSetting.NormalCrew == 0)
                {
                    UIManager.instance.CrewPurchase();
                }
                else
                {
                    return;
                }
            }            
            if(crew == CrewSetting.Crew.Profiient)
            {
                if(GameSetting.SkilledCrew == 0)
                {
                    UIManager.instance.CrewPurchase();
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void Update()
    {
        if(GameSetting.SkilledCrew == 0)
        {
            if (GameSetting.CoinCount >= UIManager.instance.crew.Purchase)
            {
                UIManager.instance.CrewPurchasePanel[1].GetComponent<Image>().sprite = UIManager.instance.PurchaseBool[1];
            }
            else
            {
                UIManager.instance.CrewPurchasePanel[1].GetComponent<Image>().sprite = UIManager.instance.PurchaseBool[0];
            }
        }
        if(GameSetting.NormalCrew == 0)
        {
            if (GameSetting.CoinCount >= UIManager.instance.crew.Purchase)
            {
                UIManager.instance.CrewPurchasePanel[0].GetComponent<Image>().sprite = UIManager.instance.PurchaseBool[1];
            }
            else
            {
                UIManager.instance.CrewPurchasePanel[0].GetComponent<Image>().sprite = UIManager.instance.PurchaseBool[0];
            }
        }
    }
}