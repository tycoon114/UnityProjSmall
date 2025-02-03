using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sly114_GameManager : MonoBehaviour
{
    public static sly114_GameManager Instance; // �̱��� ����
    public Text timerText; // ���� �ð� UI
    public Text healthText; // ü�� UI
    public GameObject exitPrefab; // Ż�ⱸ ������
    public GameObject player; // �÷��̾�
    public Transform exitSpawnArea; // Ż�ⱸ�� ������ ����

    private float gameTime = 300f; // 5�� (�� ����)
    private float spawnExitTime = 240f; // 4��° Ż�ⱸ ����
    private bool exitSpawned = false; // Ż�ⱸ ���� ����
    private int playerHealth = 100; // �÷��̾� ü��

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        while (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            UpdateUI();

            if (!exitSpawned && gameTime <= 300f - spawnExitTime)
            {
                SpawnExit();
                exitSpawned = true;
            }

            yield return null;
        }

        GameOver(false); // 5�� ������ ����
    }

    void UpdateUI()
    {
        timerText.text = $"Time: {Mathf.Ceil(gameTime)}s";
        healthText.text = $"HP: {playerHealth}";
    }

    void SpawnExit()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(exitSpawnArea.position.x - 3, exitSpawnArea.position.x + 3),
            Random.Range(exitSpawnArea.position.y - 3, exitSpawnArea.position.y + 3)
        );

        Instantiate(exitPrefab, randomPos, Quaternion.identity);
    }

    public void DamagePlayer(int damage)
    {
        playerHealth -= damage;
        UpdateUI();

        if (playerHealth <= 0)
            GameOver(false); // ü���� 0�� �Ǹ� ����
    }

    public void GameOver(bool success)
    {
        StopAllCoroutines();
        string resultMessage = success ? "YOU ESCAPED!" : "GAME OVER";
        timerText.text = resultMessage;
        Invoke("RestartGame", 3f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ScoreManager() { 
    
    }


}
