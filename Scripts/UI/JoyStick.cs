using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : CCompoent
{

    public Transform Stick; //조이스틱

    private Vector3 StickFirstPos; //조이스틱 처음 위치
    private Vector3 JoyStickVec; //조이스틱 방향
    private float Radius; //조이스틱 배경의 반 지름

    public float Horizontal
    {
        get
        {
            return JoyStickVec.y;
        }
    }

    public float Vertical
    {
        get
        {
            return JoyStickVec.x;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;

        StickFirstPos = Stick.position;

        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;

        Radius *= Can;

        TankManger.instance.MoveFlag = false;

       
    }


    public void OnDrag(BaseEventData _data)
    {
        TankManger.instance.MoveFlag = true;
        PointerEventData Data = _data as PointerEventData;

        Vector3 Pos = Data.position;

        //조이스틱을 이동시킬 방향을 구함(오른쪽,왼쪽,위,아래)
        JoyStickVec = (Pos - StickFirstPos).normalized;

        //조이스틱의 처음 위치와 현재 내가 터치하고 있는 위치의 거리를 구함
        float Distance = Vector3.Distance(Pos, StickFirstPos);

        //거리가 반지름 보다 작으면 조이스틱을 현재 터치하고 있는곳으로 이동
        if (Distance < Radius)
        {
            Stick.position = StickFirstPos + JoyStickVec * Distance;
        }
        //거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동
        else
        {
            Stick.position = StickFirstPos + JoyStickVec * Radius;
        }

    }
   
    public void DragEnd()
    {
        //스틱을 원래 위치로
        Stick.position = StickFirstPos;

        //방향 0
        JoyStickVec = Vector3.zero;

        TankManger.instance.MoveFlag = false;
    }
}
