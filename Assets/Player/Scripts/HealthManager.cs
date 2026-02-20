using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    float hitPoints;

    public Slider healthSlider;
    [SerializeField] GameObject DeathPanel;

    void Start()
    {
        hitPoints = maxHitPoints;
        SetHealthSlider();
    }
    void Hit(float rawDamage)
    {
        hitPoints -= rawDamage;
        SetHealthSlider();

        Debug.Log("OUCH: " + hitPoints.ToString());

        if (hitPoints <= 0)
        {
            Cursor.visible = true;
            Time.timeScale = 0f;
            DeathPanel.SetActive(true);
            GetComponent<PlayerAttack>().isAlive = false;
            GetComponent<MenuControls>().isAlive = false;
        }
    }

    void SetHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = NormalisedHitPoints();
        }
    }

    float NormalisedHitPoints()
    {
        return hitPoints / maxHitPoints;
    }
}
