using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // -- FIELDS

    private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameOver _gameOverPanel;
    [SerializeField] private PlayerInput _playerInput;

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

    public int GetScore()
    {
        return _score;
    }

    public void EndGame()
    {
        _gameOverPanel.Show(_score);
        Time.timeScale = 0f;
        _playerInput.DeactivateInput();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }

    // -- UNITY

    private void Awake()
    {
        Time.timeScale = 1f;
        UpdateScoreText();
    }
}
