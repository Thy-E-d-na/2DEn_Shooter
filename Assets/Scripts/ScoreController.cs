using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Text textScore;
    private int totalScore = 0;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI heartText;
    public int heartCount = 3;
    private int heartScore = 0;

    public static ScoreController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void GetScore(int score)
    {
        this.totalScore += score;
        textScore.text = "Score: " + this.totalScore.ToString();
        heartScore += score;
        if (heartScore >= 100000)
        {
            AudioController.Instance.PlayButtonClip();
            heartScore -= 100000;
            heartCount++;
            heartText.text = "x " + heartCount.ToString();
        }
    }

    public void HeartCountDown()
    {
        heartCount--;
        heartText.text = "x " + heartCount.ToString();
        if (heartCount < 0)
        {
            AudioController.Instance.PlayGameOverMusic();
            gameOverPanel.SetActive(true);
        }
    }

}
