using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : CCompoent
{

    //지정한 위치로 카메라의 위치를 변경하고 싶다.

    //카메라가 쫓아다닐 위치 변수
    public Vector3 FollowPoistion;

    public Vector3 Target;
    public float Smoothing = 5f;

    private void Start()
    {
        //카메라가 쫓아다닐 위치 변수의 위치값을 게산
        FollowPoistion = transform.position - TankManger.instance.TankPosition;
    }
    // Update is called once per frame
    void Update()
    {
        
        Target = TankManger.instance.TankPosition + FollowPoistion;
        /*
         * 선형 보간법
         * 어떤 수치에서 어떤 수치로 값이 변경 되는데 한번에 변경되지 않고 부드럽게 변경 시켜주는 함수
         * 
         * 숫자간의 선형 보간 : mathf.lerp
         * 백터간의 선형 보간 : vector.lerp
         * Quaternion간의 선형 보간 : quaternion.lerp
         */
        transform.position = Vector3.Lerp(transform.position, Target, Smoothing * Time.deltaTime);
    }
}
