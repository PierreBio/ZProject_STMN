using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int maxScore;
    public Text currentHealthTracker;
    public HealthBarHandler healthBarHandler;
    public Text currentScoreTracker;
    private int currentHealth;
    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarHandler.setHealthValue(currentHealth, maxHealth);
        currentScore = 0;
        UpdateGUI();
    }

    // Update is called once per frame
    void UpdateGUI()
    {
        currentHealthTracker.text = currentHealth.ToString();
        currentScoreTracker.text = currentScore.ToString();
    }

    public void AlterHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBarHandler.setHealthValue(currentHealth, maxHealth);
        UpdateGUI();
    }

    public void AlterScore(int amount)
    {
        currentScore += amount;
        currentScore = Mathf.Clamp(currentScore, 0, maxScore);
        UpdateGUI();
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy_Small":
                AlterHealth(-1);
                break;
            case "Enemy_Medium":
                AlterHealth(-5);
                break;
            case "Enemy_Large":
                AlterHealth(-10);
                break;
            case "Health_Item":
                AlterHealth(5);
                break;
            default:
                break;
        }
    }
}
