using UnityEngine;
using Scene = UnityEngine.SceneManagement.Scene;

[CreateAssetMenu(fileName = "ScoreHolder", menuName = "ScriptableObjects/ScoreHolder")]
public class ScoreHolder : ScriptableObject
{
    public int correctAnswers;
    public int totalQuestions;
    public float historicHighScore;
    public Scene currentGame;
}
