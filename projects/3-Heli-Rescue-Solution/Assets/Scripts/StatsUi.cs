using TMPro;
using UnityEngine;

public class StatsUi : MonoBehaviour
{
    public GameController GameController;
    public TextMeshProUGUI TextField;

    void Update()
    {
        var timeLeft = GameController.PrettyTimeLeft;
        TextField.text = $"People saved: {GameController.PeopleSaved} / {GameController.PeopleToSave} \nTime left: {timeLeft}";
    }
}