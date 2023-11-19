using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Brick : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected MeshRenderer meshRenderer;
    
    [Header("Properties")]
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected ColorType colorType;
    

    protected void Start()
    {
        OnInit();
    }
    
    protected void OnInit()
    {
        ChangeColor(colorType);
    }
    
    protected void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        meshRenderer.material = colorData.GetMaterial(colorType);
    }
}
