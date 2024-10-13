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

        // ���� ������ ĳ���� ��ġ ������ ����
        Vector2 direction = (Vector2)transform.position - pos;

        // ���� ������ �������� ���� ���� ���� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Debug.Log("���� ����: " + angle);
        Debug.Log("��ǥ : " + pos);

        StartCoroutine(RotateAround(pos, angle));
    }

    IEnumerator RotateAround(Vector2 pivot, float initialAngle)
    {
        // ȸ�� �ӵ� ����
        float rotationSpeed = 200f; // �ʴ� ȸ�� �ӵ� (degree)

        // ���� ����
        float currentAngle = initialAngle;
        float targetAngle = initialAngle + 170f;

        // ���� � ������ (ĳ���Ϳ� ���� ���� ���� �Ÿ�)
        float radius = Vector2.Distance(transform.position, pivot);

        // �߽��࿡ �ٰ����� �ӵ� (������ ������)
        float radiusReductionSpeed = 1f;

        // �ʱ� ����
        float initialLength = radius; // �ʱ� ����

        Vector3 initialRopeScale = ropeSprite.localScale;

        while (currentAngle < targetAngle) // ������ ������ŭ ȸ��
        {
            // ���� ������ŭ ȸ��
            currentAngle += rotationSpeed * Time.deltaTime;

            // �������� ���� �ٿ����� �߽��࿡ �������
            radius -= radiusReductionSpeed * Time.deltaTime;

            // �������� 0 ���Ϸ� �������� �ʵ��� ����
            radius = Mathf.Max(radius, 0);

            // ȸ�� ������ ���� ĳ������ ���ο� ��ġ ���
            float rad = currentAngle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            // ĳ������ ��ġ ������Ʈ
            transform.position = pivot + offset;

            // ĳ������ ������ �߽����� ���ϵ��� ȸ��
            Vector2 direction = pivot - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90); // ĳ���Ͱ� ������ �ٶ󺸵��� ����

            
            // �� ��������Ʈ�� ������ ���� (�ʱ� ������ �ݿ�)
            float scale = radius / initialLength; // ���� ���̿� ���� ����
            ropeSprite.localScale = Vector3.Lerp(new Vector3(1, 0, 1), initialRopeScale, scale * scale);

            // �� ��������Ʈ�� ȸ���� ���� (�ʱ� ȸ���� �ݿ�)
            ropeSprite.rotation = Quaternion.Euler(0, 0, angle + 90); // Z�� ȸ�� ����

            // �� ��������Ʈ�� ��ġ ������Ʈ
            Vector2 ropePosition = pivot + (Vector2)(Quaternion.Euler(0, 0, angle + 90) * Vector2.up);
            ropeSprite.position = ropePosition; // ���� ���� �߽��࿡ �ٵ��� ����
            
            yield return null; // �� ������ ���
        }
        onFinished?.Invoke();
    }
}
