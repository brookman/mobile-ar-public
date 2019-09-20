using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private GameController _gameController;

    public TMP_Text TitleText;
    public TMP_Text ResultText;

    void Start()
    {
        var _gameController = GameController.Instance;

        if (_gameController == null)
        {
            Debug.LogError("No GameController Instance available");
            return;
        }

        if (_gameController.GameWon)
        {
            TitleText.text = "Congratulations, you won!";
        }
        else
        {
            TitleText.text = "Oh No!";
            if (_gameController.Crashed)
            {
                TitleText.text += " You crashed!";
            }
            else
            {
                TitleText.text += " Out of time!";
            }
        }

        var text = "";
        text += $"You saved {_gameController.PeopleSaved} of {_gameController.PeopleToSave} people\n";
        text += $"Remaining Time: {_gameController.PrettyTimeLeft}\n";
        text += $"Score: {_gameController.Score}\n";
        ResultText.text = text;
    }
}