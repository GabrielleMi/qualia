using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private List<Renderer> _renderers;

    public void ChangeColour(Color color)
    {
        foreach (Renderer renderer in _renderers)
        {
            renderer.material.color = color;
            renderer.material.SetColor("_EmissionColor", color);
        }
    }
}
