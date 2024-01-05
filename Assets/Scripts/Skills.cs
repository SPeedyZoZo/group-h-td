using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [Header("Slow Effect")]
    public float slowDuration = 5f;
    public float slowFactor = 0.5f;
    public int slowCost = 200;
    public Button slowButton;

    [Header("Burning Effect")]
    public float BurningDuration = 5f;
    public int BurningAmount = 10;
    public int BurningCost = 150;
    public Button BurningButton;


    private bool isSkillActive = false;
    private bool isBurningSkillActive = false;



    private void Start()
    {
        slowButton.onClick.AddListener(ActivateSlowSkill);
        BurningButton.onClick.AddListener(ActivateBurningSkill);
    }

    public void ActivateSlowSkill()
    {
        if (!isSkillActive && LevelManager.money >= slowCost)
        {
            isSkillActive = true;
            LevelManager.money -= slowCost;

            Enemy[] enemies = FindObjectsOfType<Enemy>();

            foreach (Enemy enemy in enemies)
            {
                enemy.ApplySlowEffect(slowDuration, slowFactor);
            }
            Invoke("DeactivateSkill", slowDuration);
        }
    }

    private void DeactivateSkill()
    {
        isSkillActive = false;
    }

    public void ActivateBurningSkill()
    {
        if (!isBurningSkillActive && LevelManager.money >= BurningCost)
        {
            isBurningSkillActive = true;
            LevelManager.money -= BurningCost;

            Enemy[] enemies = FindObjectsOfType<Enemy>();

            foreach (Enemy enemy in enemies)
            {
                enemy.ApplyBurningEffect(BurningDuration, BurningAmount);
            }

            Invoke("DeactivateBurningSkill", BurningDuration);
        }
    }

    private void DeactivateBurningSkill()
    {
        isBurningSkillActive = false;
    }
}
