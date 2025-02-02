using UnityEngine;

public class sly114_CameraComtroller : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public float smoothSpeed = 5f; // �ε巯�� �̵� �ӵ�
    public Vector3 offset = new Vector3(0f, 0f, -10f); // ī�޶� ��ġ ����

    void LateUpdate()
    {
        if (player != null)
        {
            // �ε巴�� ī�޶� �̵�
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
