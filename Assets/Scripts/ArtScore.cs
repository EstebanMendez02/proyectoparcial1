using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArtScore : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    int score = 0;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void AddPoints(int points)
    {
        Debug.Log("arte");
        score += points;
        textMesh.text = $"Arte: {score} / 5";
    }
}