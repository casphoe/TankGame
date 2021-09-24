using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function 
{
    public static IEnumerator CreateWaitSecond(float fDelay, bool Realtime = false)
    {
        /*
         * Realtime 이란
         * :
         * 유니티 엔진에서 Realtime 은 게임상에 시간이 아니라 현실 세계 상에 시간을 의미
         * (즉, Realtime 은 Time.tiescale에 영향을 받지 않은 순수한 현실 세계 상세 시간을 뜻함)
         * 
         */

        if (Realtime)
        {
            yield return new WaitForSecondsRealtime(fDelay);
        }
        else
        {
            yield return new WaitForSeconds(fDelay); //정해진 시간동안 대기
        }
    }

    //! 함수를 지연 호출한다.
    public static void LateCallFunc(CCompoent a_oComponet, float a_fDelay, System.Action<CCompoent> a_oCallback, bool a_bIsRealTime = false)
    {
        var oEnumerator = Function.DoLateCallFunc(a_oComponet, a_fDelay, a_oCallback, a_bIsRealTime);

        /*
         *   코루틴이란?
         *   :
         *   - 일반적인 함수 (서브 루틴)와 달리 함수의 특정 지점부터 실행 가능한 함수를 의미 => 원래 기본함수는 실행하면 맨처음부터 실행(쓰레드 : 프로그램의 흐름 => 문맥)
         *   (즉, 코루틴 함수 내부의 특정 위치에서 반환되었을 경우 다시 해당 지점부터 이어서 실행 가능한 함수를 의미한다.)
         */
        a_oComponet?.StartCoroutine(oEnumerator);
    }

    public static IEnumerator DoLateCallFunc(CCompoent a_oComponet, float a_fDelay, System.Action<CCompoent> a_oCallback, bool a_bIsRealTime)
    {
        //Debug.LogFormat("BeFore Time : {0}", System.DateTime.Now); //System.DateTime.Now : 현재시간을 알 수 있음
        yield return Function.CreateWaitSecond(a_fDelay, a_bIsRealTime);

        //Debug.LogFormat("After Time : {0}", System.DateTime.Now);

        a_oCallback?.Invoke(a_oComponet);
    }
}
