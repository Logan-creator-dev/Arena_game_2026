using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
   [SerializeField] private Asteroid _asteroidPrefab;
  
   private int level = 0;
   
   public int _asteroidCount = 0;
   public ScoreManager _scoreManager;
   public GameObject gameOverPanel;
   

   private void Update()
   {
      if (_asteroidCount == 0)
      {
         level++;

         int numAsteroids = 2 + (2 * level);
         for (int i = 0; i < numAsteroids; i++)
         {
            SpawnAsteroid();
         } 
         
      }
   }

   private void SpawnAsteroid()
   {
      float offset = Random.Range(0f, 1f);
      Vector2 viewportSpawnPosition = Vector2.zero;
      
      int edge = Random.Range(0, 4);
      if (edge == 0)
      {
         viewportSpawnPosition = new Vector2(offset, 0);
      }
      else if (edge == 1)
      {
         viewportSpawnPosition = new Vector2(offset, 1);
      }
      else if (edge == 2)
      {
         viewportSpawnPosition = new Vector2(0, offset);
      }
      else if (edge == 3)
      {
         viewportSpawnPosition = new Vector2(1, offset);
      }
      
      
      Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
      Asteroid asteroid = Instantiate(_asteroidPrefab, worldSpawnPosition, Quaternion.identity);
      asteroid.gameManager = this;
   }

   public void GameOver()
   {
      gameOverPanel.SetActive(true);
      _scoreManager.HighScoreUpdate();
      StartCoroutine(Restart());
   }

   private IEnumerator Restart()
   {
      Debug.Log("Game Over");
      
      yield return new WaitForSeconds(3f);
      
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

      yield return null;
   }
}
