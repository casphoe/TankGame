using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrewSelect : CCompoent,IPointerDownHandler
{
    public CrewSetting.Crew crews;

    public static CrewSelect instacne;

    public override void Awake()
    {
        base.Awake();

        if (instacne == null)
        {
            instacne = this;
        }
        else if (instacne != null)
        {
            return;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(crews == CrewSetting.Crew.Beginner)
        {
            GameManager.instance.CrewBase = CrewSetting.Crew.Beginner;
        }
        else if(crews == CrewSetting.Crew.Pilot)
        {
            GameManager.instance.CrewBase = CrewSetting.Crew.Pilot;
        }
        else if(crews == CrewSetting.Crew.Profiient)
        {
            GameManager.instance.CrewBase = CrewSetting.Crew.Profiient;
        }
    }
}