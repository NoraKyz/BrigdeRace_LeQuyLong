using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Brick : GameUnit
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
    
    protected virtual void OnInit()
    {
        ChangeColor(colorType);
    }
    
    protected virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        meshRenderer.material = colorData.GetMaterial(colorType);
    }
    
    protected virtual IEnumerator FlyToCharacter(Vector3 targetPosition)
    {
        // TODO: effect fly to player
        yield return new WaitForSeconds(1f);
        OnDespawn();
    }
}
