using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController trailController;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    public BoxCollider2D TapCollider;
    public GameOverPanel gameOverPanel;

    private bool _isGameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Birds.Count; i++) {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++) {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBird() {
        trailController.ClearTrail();

        TapCollider.enabled = false;

        Birds.RemoveAt(0);

        if (Birds.Count > 0) {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }

        if (Birds.Count <= 0)
            _isGameEnded = true;

        if (_isGameEnded) {
            gameOverPanel.gameObject.SetActive(true);
            if (Enemies.Count > 0)
                gameOverPanel.SetTextLose();
            else
                gameOverPanel.SetTextWin();
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy) {
        for (int i = 0; i < Enemies.Count; i++) {
            if (Enemies[i].gameObject == destroyedEnemy) {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0) {
            _isGameEnded = true;
        }
    }

    public void AssignTrail(Bird bird) {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp() {
        if (_shotBird != null) {
            _shotBird.OnTap();
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
