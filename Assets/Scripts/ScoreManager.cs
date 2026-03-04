using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int currentScore;

    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    public void ChangeScore(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();

    }

    public void HighScoreUpdate()
    {
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            if (currentScore > PlayerPrefs.GetInt("SavedHighScore"))
            {
                // Mettre à jour le highscore
                PlayerPrefs.SetInt("SavedHighScore", currentScore);
            }
        }
        else
        {
            // Si il n'y a pas de highscore
            PlayerPrefs.SetInt("SavedHighScore", currentScore);
        }
        
        // Modifier le TMP
        finalScoreText.text = currentScore.ToString();
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
        
    }
}
