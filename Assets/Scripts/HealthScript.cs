using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthScript : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    public int healthUI = 5;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void RemoveHealth(int damage)
    {
        healthUI -= damage;
        textMesh.text = $"{healthUI} / 5";
    }
    
}
