using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float TimeLeft;
    public bool Crashed;
    public static GameController Instance;

    public int PeopleSaved = 0;
    public int PeopleToSave = 10;

    public bool GameLost => TimeLeft <= 0 || Crashed;
    public bool GameWon => !GameLost && PeopleSaved >= PeopleToSave;

    public string PrettyTimeLeft
    {
        get
        {
            var minutes = (int) (TimeLeft / 60);
            var seconds = (int) (TimeLeft - minutes * 60);
            return $"{minutes:D2}:{seconds:D2}";
        }
    }

    public int Score => (int) (TimeLeft * 10 + PeopleSaved * 1000);

    private bool _gameEnded = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TimeLeft = 60;
        Crashed = false;

        var peopleToSave = 0;
        foreach (var spawner in FindObjectsOfType<Spawner>())
        {
            peopleToSave += spawner.SpawnTotal;
        }

        PeopleToSave = peopleToSave;
    }

    void Update()
    {
        // Update Progress
        if (_gameEnded) return;

        // Check Lose Conditions
        TimeLeft -= Time.unscaledDeltaTime;
        if (TimeLeft <= 0)
        {
            TimeLeft = 0;
            EndGame();
        }
        else if (Crashed)
        {
            EndGame();
        }

        // Check Win Conditions
        if (PeopleSaved >= PeopleToSave)
        {
            EndGame();
        }
    }

    public void PersonSaved()
    {
        PeopleSaved = Math.Min(PeopleSaved + 1, PeopleToSave);
    }

    public void Crash()
    {
        Crashed = true;
    }

    void EndGame()
    {
        _gameEnded = true;
        SceneManager.LoadScene("3_GameOver");
    }
}