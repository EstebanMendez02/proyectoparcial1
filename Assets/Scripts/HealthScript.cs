using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthScript : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    public int healthUI = 3;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void RemoveHealth(int damage)
    {
        healthUI -= damage;
        textMesh.text = $"{healthUI} / 3";
    }
    
}
