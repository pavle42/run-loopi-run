using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hoverable : MonoBehaviour
{
    public UnityEvent onHoverEnter, onHoverExit;
    public bool shouldBeHighlighted;

    public Material hoverMaterial; 
    [HideInInspector] public bool isHovered;

    // Outline shader property IDs
    private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");
    private static readonly int OutlineWidthID = Shader.PropertyToID("_OutlineWidth");

    private Color hoverColor = Color.white;
    private float hoverWidth = 0.02f;

    // Cached renderer data
    private Renderer[] renderers;                   // MeshRenderer *and* SkinnedMeshRenderer
    private Color[][] originalColors;
    private float[][] originalWidths;

    void Awake()
    {
        if (hoverMaterial)
        {
            if (hoverMaterial.HasProperty(OutlineColorID))
                hoverColor = hoverMaterial.GetColor(OutlineColorID);

            if (hoverMaterial.HasProperty(OutlineWidthID))
                hoverWidth = hoverMaterial.GetFloat(OutlineWidthID);
        }

        if (shouldBeHighlighted)
        {
            onHoverEnter.AddListener(ApplyHoverOutline);
            onHoverExit.AddListener(RestoreOriginalOutline);
        }
    }

    // Called by your ray-caster
    public void OnRayEnter() { isHovered = true; onHoverEnter.Invoke(); }
    public void OnRayExit() { isHovered = false; onHoverExit.Invoke(); }

    private void ApplyHoverOutline()
    {
        // Grab all renderers (static + skinned) under this object
        renderers = GetComponentsInChildren<Renderer>(true);

        originalColors = new Color[renderers.Length][];
        originalWidths = new float[renderers.Length][];

        for (int i = 0; i < renderers.Length; i++)
        {
            var mats = renderers[i].materials;          // duplicates sharedMaterial to avoid global side-effects
            originalColors[i] = new Color[mats.Length];
            originalWidths[i] = new float[mats.Length];

            for (int j = 0; j < mats.Length; j++)
            {
                var m = mats[j];

                if (m.HasProperty(OutlineColorID))
                {
                    originalColors[i][j] = m.GetColor(OutlineColorID);
                    m.SetColor(OutlineColorID, hoverColor);
                }

                if (m.HasProperty(OutlineWidthID))
                {
                    originalWidths[i][j] = m.GetFloat(OutlineWidthID);
                    m.SetFloat(OutlineWidthID, hoverWidth);
                }
            }
        }
    }

    private void RestoreOriginalOutline()
    {
        if (renderers == null) return;

        for (int i = 0; i < renderers.Length; i++)
        {
            var mats = renderers[i].materials;
            for (int j = 0; j < mats.Length; j++)
            {
                var m = mats[j];

                if (m.HasProperty(OutlineColorID))
                    m.SetColor(OutlineColorID, originalColors[i][j]);

                if (m.HasProperty(OutlineWidthID))
                    m.SetFloat(OutlineWidthID, originalWidths[i][j]);
            }
        }
    }
}
