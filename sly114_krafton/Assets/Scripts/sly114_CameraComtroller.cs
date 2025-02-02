using UnityEngine;

public class sly114_CameraComtroller : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public float smoothSpeed = 5f; // 부드러운 이동 속도
    public Vector3 offset = new Vector3(0f, 0f, -10f); // 카메라 위치 조정

    void LateUpdate()
    {
        if (player != null)
        {
            // 부드럽게 카메라 이동
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
