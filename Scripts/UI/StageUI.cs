using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageUI : CCompoent,IPointerDownHandler
{
    public GameSetting.Stage GetStage;

    public static StageUI instacne;

    public override void Awake()
    {
        base.Awake();

        if(instacne == null)
        {
            instacne = this;
        }
        else if(instacne != null)
        {
            return;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GetStage == GameSetting.Stage.Stage1)
        {
            GameManager.instance.StageSelect = GameSetting.Stage.Stage1;
        }
        else if(GetStage == GameSetting.Stage.Stage2)
        {
            GameManager.instance.StageSelect = GameSetting.Stage.Stage2;
        }
        else if(GetStage == GameSetting.Stage.Stage3)
        {
            GameManager.instance.StageSelect = GameSetting.Stage.Stage3;
        }
    }
}