using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InGameUI : MonoBehaviour
{
    private void Start()
    {
        killButton.interactable = true;
        killCoolText.text = string.Empty;
    }

    public void Leave() 
    {
        MenuManager.me.LeaveInGame();
    }

    public int killCooldown;

    public TMP_Text killCoolText;

    public Button killButton;

    public void Kill()
    {
        MenuManager.me.kill = true;
        killCooldown = 30;
        killCoolText.text = killCooldown.ToString();
        killButton.interactable = false;
        Invoke("ReduceCooldown", 1);
    }

    void ReduceCooldown() 
    {
        if (killCooldown >= 1)
        {
            killCooldown--;
            Invoke("ReduceCooldown", 1);
            killCoolText.text = killCooldown.ToString();
        }
        else
        {
            killButton.interactable = true;
            killCoolText.text = string.Empty;
        }
    }

}
