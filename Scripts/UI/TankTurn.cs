using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TankTurn : CCompoent,IPointerDownHandler,IPointerUpHandler
{
    public Turn turn;

    public void OnPointerDown(PointerEventData eventData)
    {
        TankManger.instance.IsTurn = true;

        if(turn == Turn.Left)
        {
            TankManger.instance.TurnTank = Turn.Left;
        }
        else if(turn == Turn.Right)
        {
            TankManger.instance.TurnTank = Turn.Right;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TankManger.instance.IsTurn = false;
    }
}
