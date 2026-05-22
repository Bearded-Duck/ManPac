using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;

    [Header("UI Text Setup")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int points)
    {
        score += points;
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    public void UpdateHPDisplay(int currentHP)
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP;
        }
        else
        {
            // This will tell us if you forgot to drag the text object into the slot!
            Debug.LogError("GameManager is trying to update HP, but the 'Hp Text' slot is EMPTY in the Inspector!");
        }
    }
}