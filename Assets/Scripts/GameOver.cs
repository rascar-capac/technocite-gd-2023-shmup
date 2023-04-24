using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _scoreText;

    // -- METHODS

    public void Show(int score)
    {
        gameObject.SetActive(true);
        _scoreText.text = score.ToString();
    }

    // -- UNITY

    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
