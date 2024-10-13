using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RopeAction : MonoBehaviour
{
    [SerializeField]
    Transform ropeSprite;

    UnityAction onFinished;

    public void Init(UnityAction onFInished)
    {
        this.onFinished = onFInished;
    }

    public void SetAngleByObject(Vector2 pos)
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);

        // 접촉 지점과 캐릭터 위치 사이의 벡터
        Vector2 direction = (Vector2)transform.position - pos;

        // 접촉 지점과 수직으로 뻗은 선의 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Debug.Log("계산된 각도: " + angle);
        Debug.Log("좌표 : " + pos);

        StartCoroutine(RotateAround(pos, angle));
    }

    IEnumerator RotateAround(Vector2 pivot, float initialAngle)
    {
        // 회전 속도 설정
        float rotationSpeed = 200f; // 초당 회전 속도 (degree)

        // 현재 각도
        float currentAngle = initialAngle;
        float targetAngle = initialAngle + 170f;

        // 원형 운동 반지름 (캐릭터와 접촉 지점 사이 거리)
        float radius = Vector2.Distance(transform.position, pivot);

        // 중심축에 다가가는 속도 (반지름 감소율)
        float radiusReductionSpeed = 1f;

        // 초기 길이
        float initialLength = radius; // 초기 길이

        Vector3 initialRopeScale = ropeSprite.localScale;

        while (currentAngle < targetAngle) // 지정한 각도만큼 회전
        {
            // 일정 각도만큼 회전
            currentAngle += rotationSpeed * Time.deltaTime;

            // 반지름을 점점 줄여가며 중심축에 가까워짐
            radius -= radiusReductionSpeed * Time.deltaTime;

            // 반지름이 0 이하로 내려가지 않도록 설정
            radius = Mathf.Max(radius, 0);

            // 회전 각도에 따라 캐릭터의 새로운 위치 계산
            float rad = currentAngle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            // 캐릭터의 위치 업데이트
            transform.position = pivot + offset;

            // 캐릭터의 방향을 중심축을 향하도록 회전
            Vector2 direction = pivot - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90); // 캐릭터가 수직을 바라보도록 보정

            
            // 선 스프라이트의 스케일 조정 (초기 스케일 반영)
            float scale = radius / initialLength; // 현재 길이에 대한 비율
            ropeSprite.localScale = Vector3.Lerp(new Vector3(1, 0, 1), initialRopeScale, scale * scale);

            // 선 스프라이트의 회전값 적용 (초기 회전값 반영)
            ropeSprite.rotation = Quaternion.Euler(0, 0, angle + 90); // Z축 회전 적용

            // 선 스프라이트의 위치 업데이트
            Vector2 ropePosition = pivot + (Vector2)(Quaternion.Euler(0, 0, angle + 90) * Vector2.up);
            ropeSprite.position = ropePosition; // 선의 끝이 중심축에 붙도록 설정
            
            yield return null; // 한 프레임 대기
        }
        onFinished?.Invoke();
    }
}
