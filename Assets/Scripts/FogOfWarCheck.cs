using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarCheck : MonoBehaviour
{
    public bool isVisible;
    public enum ObjectType
    {
        Enemy,
        Construction
    }
    public ObjectType objectType;
    [HideInInspector]
    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    private void Awake()
    {
        foreach (var item in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderers.Add(item);
        }
        foreach (var item in meshRenderers)
        {
            item.enabled = false;
        }
    }
    public void DoVisible()
    {
        isVisible = true;
        foreach (var item in meshRenderers)
        {
            item.enabled = true;
        }
    }
    public void DoInvisible()
    {
        isVisible = false;
        foreach (var item in meshRenderers)
        {
            item.enabled = false;
        }
    }

}
