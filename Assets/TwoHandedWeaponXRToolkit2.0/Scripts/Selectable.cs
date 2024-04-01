using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    public List<MeshRenderer> renderers = new List<MeshRenderer>();
    public GameObject canvas;
    
    private void Start()
    {
        if (canvas != null) canvas.SetActive(false);
    }

    public void SpatialEnter()
    {
        if (Manager.instance.hudSelection.HUDType == HUD_Selection.TYPE.Spatial_One || Manager.instance.hudSelection.HUDType == HUD_Selection.TYPE.Spatial_Two)
        {
            canvas.SetActive(true);
        }
    }
    
    public void SpatialExit()
    {
        if (Manager.instance.hudSelection.HUDType == HUD_Selection.TYPE.Spatial_One || Manager.instance.hudSelection.HUDType == HUD_Selection.TYPE.Spatial_Two)
        {
            canvas.SetActive(false);
        }
    }
    
    public void ShaderEnter()
    {
        if (Manager.instance.hudSelection.HUDType == HUD_Selection.TYPE.Shader_One || Manager.instance.hudSelection.HUDType == HUD_Selection.TYPE.Shader_Two)
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].material.EnableKeyword("_EMISSION");
            }
        }
    }

    public void ShaderExit()
    {
        if (Manager.instance.hudSelection.HUDType == HUD_Selection.TYPE.Shader_One || Manager.instance.hudSelection.HUDType == HUD_Selection.TYPE.Shader_Two)
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].material.DisableKeyword("_EMISSION");
            }
        }
    }
}
