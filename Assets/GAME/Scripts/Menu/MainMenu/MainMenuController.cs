using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Slider numberOfPlayersSlider;
    public TextMeshProUGUI numberOfPlayersText;

    private void Awake()
    {
        numberOfPlayersSlider.onValueChanged.AddListener((float v) =>
        {
            numberOfPlayersText.text = $"Number of Players: {(int)v}";
        });

        playButton.onClick.AddListener(() => 
        { 
            GameManager.instance.SetNumberOfPlayers((int)numberOfPlayersSlider.value);
            GameLevelManager.instance.LoadGameLevelSelectorScene();
        });
    }
}
