using UnityEngine;
using UnityEngine.Events;

public class ScoreSysteme : MonoBehaviour
{
    public UnityEvent OnScoreChanged;
    public int _score { get; private set; }

    public void AddScore(int score)
    {
        _score += score;
        OnScoreChanged.Invoke();
    }
}
