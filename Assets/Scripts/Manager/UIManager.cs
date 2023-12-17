using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI money;
    public TMPro.TextMeshProUGUI lives;
    public TMPro.TextMeshProUGUI countdown;

    private void Update()
    {
        money.text = "$" + LevelManager.money.ToString();
        lives.text = GameState.lives.ToString() + " Lives";
        countdown.text = string.Format("{0:00.00}", LevelManager.GetCountdown());
    }
}
