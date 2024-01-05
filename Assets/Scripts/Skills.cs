using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    public float slowDuration = 5f;
    public float slowFactor = 0.5f;
    public int slowCost = 200;

    private bool isSkillActive = false;

    public Button slowButton;

    private void Start()
    {
        // Subscribe to the button click event
        slowButton.onClick.AddListener(ActivateSlowSkill);
    }

    public void ActivateSlowSkill()
    {
        if (!isSkillActive && LevelManager.money >= slowCost)
        {
            isSkillActive = true;
            LevelManager.money -= slowCost;

            // Find all enemy objects in the scene
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            // Apply the slow effect to each enemy
            foreach (Enemy enemy in enemies)
            {
                enemy.ApplySlowEffect(slowDuration, slowFactor);
            }

            // Deactivate the skill after the specified duration
            Invoke("DeactivateSkill", slowDuration);
        }
    }

    private void DeactivateSkill()
    {
        isSkillActive = false;
    }
}