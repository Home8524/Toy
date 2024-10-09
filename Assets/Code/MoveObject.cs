using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveObject : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    float updateTime;
    float duration = 4f;

    BoxCollider leftBox;
    BoxCollider rightBox;

    UnityAction onRelease;
    //시작점, 정답여부등을 세팅
    public void Init()
    {
        leftBox = Utils.FindComp<BoxCollider>(transform, "leftCol");
        rightBox = Utils.FindComp<BoxCollider>(transform, "rightCol");
    }

    public void SetData(bool isLeftAnswer, UnityAction release)
    {
        //정답일 경우 비활성화해서 충돌 방지
        leftBox.enabled = !isLeftAnswer;
        rightBox.enabled = isLeftAnswer;

        updateTime = 0f;
        startPos = Vector3.zero;
        endPos = Vector3.zero;
        endPos.z = -600;
        onRelease = release;
        transform.localPosition = startPos;
    }

    public void MoveUpdate()
    {
        updateTime += Time.deltaTime / duration;

        //Debug.Log("updateTime : " + updateTime);

        Vector3 nextPos = Vector3.Lerp(startPos, endPos, updateTime);

        transform.localPosition = nextPos;

        //목표점 도착시 풀에 반환
        if (updateTime < 1f) return;

        onRelease?.Invoke();
    }
}
