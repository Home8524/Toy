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
    //������, ���俩�ε��� ����
    public void Init()
    {
        leftBox = Utils.FindComp<BoxCollider>(transform, "leftCol");
        rightBox = Utils.FindComp<BoxCollider>(transform, "rightCol");
    }

    public void SetData(bool isLeftAnswer, UnityAction release)
    {
        //������ ��� ��Ȱ��ȭ�ؼ� �浹 ����
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

        //��ǥ�� ������ Ǯ�� ��ȯ
        if (updateTime < 1f) return;

        onRelease?.Invoke();
    }
}
