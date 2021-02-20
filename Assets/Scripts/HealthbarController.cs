using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    [SerializeField]
    private Image healhtbarImage;

    public void SetHealthbarHealth(float health, float maxHealth)
    {
        healhtbarImage.fillAmount = health / maxHealth;
    }
}
