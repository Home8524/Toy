using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float force = 0.1f;

    Rigidbody p_rigid;
    float width = 7;

    // test Move
    void Start()
    {
        p_rigid = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //이하 touch move로 변경
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 nextPos = transform.localPosition + Vector3.left * force;
            nextPos.x = Mathf.Clamp(nextPos.x, -width, width);
            transform.localPosition = nextPos;
            //p_rigid.AddForce(Vector3.left * force);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 nextPos = transform.localPosition + Vector3.right * force;
            nextPos.x = Mathf.Clamp(nextPos.x, -width, width);
            transform.localPosition = nextPos;
            //p_rigid.AddForce(Vector3.right * force);
        }
    }
}
