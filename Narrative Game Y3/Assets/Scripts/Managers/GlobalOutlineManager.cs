using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalOutlineManager : MonoBehaviour
{
    public static GlobalOutlineManager instance;

    [SerializeField, Range(0f, 10f)] private float outlineWidth = 2f;
    [SerializeField] private Color outlineColor;
    [SerializeField] private Outline.Mode outlineMode;

    [SerializeField] bool resetOutlineAtStart;

    Outline[] outlines;

    public float GetOutlineWIdth() { return outlineWidth; }

    private void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (GlobalOutlineManager)");
        instance = this;
    }

    void Start()
    {
        outlines = FindObjectsOfType<Outline>();

        UpdateOutlienIfChanges();

        if (resetOutlineAtStart) SetAllOutlineWidth(0);
    }

    void OnValidate()
    {
        if (outlines == null) return;

        UpdateOutlienIfChanges();
    }

    void UpdateOutlienIfChanges()
    {
        SetAllOutlineWidth(outlineWidth);
        SetAllOutlineColor(outlineColor);
        SetAllOutlineMode(outlineMode);
    }

    void SetAllOutlineWidth(float _width)
    {
        foreach (var item in outlines)
        {
            item.SetOutlineWidth(_width);
        }
    }

    void SetAllOutlineColor(Color _color)
    {
        foreach (var item in outlines)
        {
            item.SetOutlineColor(_color);
        }
    }

    void SetAllOutlineMode(Outline.Mode _mode)
    {
        foreach (var item in outlines)
        {
            item.SetOutlineMode(_mode);
        }
    }
}
