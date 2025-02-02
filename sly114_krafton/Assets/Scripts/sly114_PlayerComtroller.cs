using UnityEngine;
using System.Collections;

public class sly114_PlayerComtroller : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.up; // 마지막 이동 방향 (기본값: 위쪽)

    public GameObject bulletPrefab; // 총알 프리팹
    public GameObject specialAttackPrefab; // 특수 공격 프리팹
    public Transform firePoint; // 총알 발사 위치
    public float bulletSpeed = 10f;
    public float specialAttackDuration = 0.3f; // 특수 공격 지속 시간

    private int playerHealth = 10; // 체력 10

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 🔹 마지막으로 이동한 방향 저장 (0이 아닐 때만)
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(SpecialAttack());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity = Vector2.zero; // 벽에 부딪히면 멈춤
        }
    }


    void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * speed;
    }

    // 🟢 총알 발사 함수 (Space 키)
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = lastMoveDirection * bulletSpeed; // 마지막 방향으로 발사

        Destroy(bullet, 2f); // 총알 2초 후 자동 제거
    }

    // 🔴 특수 공격 (Q 키) → 큰 사각형이 깜빡이는 연출
    IEnumerator SpecialAttack()
    {
        GameObject specialAttack = Instantiate(specialAttackPrefab, transform.position, Quaternion.identity);
        specialAttack.transform.SetParent(transform); // 플레이어를 따라가게 설정

        yield return new WaitForSeconds(specialAttackDuration);

        Destroy(specialAttack); // 특수 공격 사각형 제거
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exit"))
        {
            sly114_GameManager.Instance.GameOver(true); // 탈출 성공
        }
        else if (collision.CompareTag("Enemy"))
        {
            sly114_GameManager.Instance.DamagePlayer(1); // 체력 1 감소
        }
    }
}
