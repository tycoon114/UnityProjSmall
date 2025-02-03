using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sly114_GameManager : MonoBehaviour
{
    public static sly114_GameManager Instance; // 싱글턴 패턴
    public Text timerText; // 남은 시간 UI
    public Text healthText; // 체력 UI
    public GameObject exitPrefab; // 탈출구 프리팹
    public GameObject player; // 플레이어
    public Transform exitSpawnArea; // 탈출구가 생성될 영역

    private float gameTime = 300f; // 5분 (초 단위)
    private float spawnExitTime = 240f; // 4분째 탈출구 생성
    private bool exitSpawned = false; // 탈출구 생성 여부
    private int playerHealth = 100; // 플레이어 체력

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

        GameOver(false); // 5분 지나면 실패
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
            GameOver(false); // 체력이 0이 되면 실패
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
