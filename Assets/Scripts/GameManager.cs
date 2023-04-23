using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // -- FIELDS

    [HideInInspector] public int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    // -- METHODS

    public void IncreaseScore(int pointsToAdd)
    {
        if(pointsToAdd <= 0)
        {
            return;
        }

        _score += pointsToAdd;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }

    // -- UNITY

    private void Awake()
    {
        UpdateScoreText();
    }
}
