using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int index;
    public string NextLevel;
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    public BoxCollider2D TapCollider;
    [SerializeField] private GameObject _panel;

    [SerializeField] private Text _statusInfo;

    private bool _isGameEnded = false;

    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
        else
        {
            SetGameOver(false);
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
            SetGameOver(true);

        }
        
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    public void SetGameOver(bool isWin)

    {
        if (isWin)
        {
            if (NextLevel == "Level2")
            {
                SceneManager.LoadScene(NextLevel);
            }
            else
            {
                _statusInfo.text = "You Win!\n Tap R to Play again";

                _panel.gameObject.SetActive(true);
            }
        }
        else
        {
            _statusInfo.text = "You Lose!\n Tap R to restart";

            _panel.gameObject.SetActive(true);
        }
        

    }

    private void Update()

    {
        // Jika menekan tombol R, fungsi restart akan terpanggil

        if (Input.GetKeyDown(KeyCode.R))

        {

            SceneManager.LoadScene("Level1");

        }
    }

}

