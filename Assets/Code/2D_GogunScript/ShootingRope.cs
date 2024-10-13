using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingRope : MonoBehaviour
{
    float speed = 2f;
    float updateTime = 0f;
    Vector3 startScale = new Vector3(1, 0, 1);
    Vector3 maximumScale = new Vector3(1, 5, 1);

    bool active = false;
    UnityAction<Vector2> onFinished;

    private void FixedUpdate()
    {
        if (active == false) return;

        float dt = Time.deltaTime * speed;
        updateTime += dt;

        transform.localScale = Vector3.Lerp(startScale, maximumScale, updateTime);

        if (updateTime < 1f) return;

        FinishAction();

        //���� ����
        onFinished?.Invoke(Vector2.zero);
    }

    void FinishAction()
    {
        updateTime = 0f;
        active = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Singleton.GetInstance.cState != eCharacterState.ropeStrech) return;

        if (collision.contactCount == 0) return;

        // ù ��° ���� ����
        ContactPoint2D firstContact = collision.contacts[0];

        // ù ��° ���� ������ ��ġ ���

        Debug.Log("First Contact Point: " + firstContact.point);

        FinishAction();

        //���� ����
        onFinished?.Invoke(firstContact.point);
    }

    public void Init(UnityAction<Vector2> onFinished)
    {
        this.onFinished = onFinished;
    }

    public void ActiveRope()
    {
        active = true;
    }
}
