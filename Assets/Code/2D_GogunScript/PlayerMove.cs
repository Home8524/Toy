using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float force = 1f;

    [SerializeField]
    ShootingRope shootRope;

    [SerializeField]
    RopeAction ropeAction;

    private void Start()
    {
        shootRope.Init(RopeStrechFinished);
        ropeAction.Init(RopeActionFinished);
    }

    void RopeStrechFinished(Vector2 pos)
    {
        if (pos == Vector2.zero)
        {
            Singleton.GetInstance.cState = eCharacterState.jump;
            return;
        }

        Singleton.GetInstance.cState = eCharacterState.ropeAction;
        ropeAction.SetAngleByObject(pos);
    }

    void RopeActionFinished()
    {
        Singleton.GetInstance.cState = eCharacterState.jump;
    }

    void Update()
    {
        if (Singleton.GetInstance.cState == eCharacterState.ropeAction) return; //로프액션중 추가적인 입력 받지않음

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 nextPos = transform.localPosition + Vector3.left * force;
            transform.localPosition = nextPos;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 nextPos = transform.localPosition + Vector3.right * force;
            transform.localPosition = nextPos;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Singleton.GetInstance.cState = eCharacterState.ropeStrech;
            shootRope.ActiveRope();
        }
    }
}
