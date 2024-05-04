using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public TMP_Text CoinCounter;
    public TMP_Text HealthCounter;


    public GameObject CompletedPanel;
    public GameObject GameOverPanel;
    public GameObject[] Starts;
    private void OnEnable()
    {
        GameManager.CompletedLevelEvent += CompletedLevelEventHandle;
        GameManager.GameOverEvent += GameOverEventHandle;
        GameManager.CoinCollectedEvent += CoinCollectedEventHandle;
        GameManager.HealthChangedEvent += HealthChangedEventHandle;
    }

    private void OnDisable()
    {
        GameManager.CompletedLevelEvent -= CompletedLevelEventHandle;
        GameManager.GameOverEvent -= GameOverEventHandle;
        GameManager.CoinCollectedEvent -= CoinCollectedEventHandle;
        GameManager.HealthChangedEvent -= HealthChangedEventHandle;
    }
    
    void Start()
    {
        HealthCounter.text = "Health: " + GameManager.Instance.Health.ToString();
        CoinCounter.text = "Coin: " + GameManager.Instance.Coin.ToString();
    }


    private void CompletedLevelEventHandle()
    {
        CompletedPanel.SetActive(true);
        OpenStars();
    }

    private void GameOverEventHandle()
    {
        GameOverPanel.SetActive(true);
    }

    private void CoinCollectedEventHandle(int coin)
    {
        CoinCounter.text = "Coin: " + coin.ToString();
    }

    private void HealthChangedEventHandle(int health)
    {
        HealthCounter.text = "Health: " + health.ToString();
    }

    private void OpenStars()
    {
        int _starCount = GameObject.FindWithTag("Player").GetComponent<PlayerManager>().starCount;
        for (int i = 0; i < _starCount; i++)
        {
            Starts[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    private void CloseStars()
    {
        foreach (var star in Starts)
        {
            star.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    public void NextLevelButton()
    {
        CloseStars();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TryAgainButton()
    {
        CloseStars();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
