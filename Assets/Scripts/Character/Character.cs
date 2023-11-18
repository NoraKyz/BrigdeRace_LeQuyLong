using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Character : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected SkinnedMeshRenderer modelCharacter;
    
    [Header("Properties")]
    [SerializeField] protected float speed;
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected ColorType colorType;
    [SerializeField] List<CharacterBrick> bricks = new List<CharacterBrick>();
    
    protected string currentAnimName;

    protected void Start()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        modelCharacter.material = colorData.GetMaterial(colorType);
    }

    protected void LateUpdate()
    {
        RotateTowardMoveDirection();
    }

    protected void AddBrick()
    {
        
    }

    protected void RemoveBrick()
    {
        
    }

    protected void DropBrick()
    {
        
    }

    protected abstract void Move();

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName == animName)
        {
            return;
        }
        
        anim.ResetTrigger(animName);
        currentAnimName = animName;
        anim.SetTrigger(animName);
    }
    
    protected void RotateTowardMoveDirection()
    {
        if (rb.velocity == Vector3.zero)
        {
            return;
        }
        
        Vector3 targetRotation = rb.velocity;
        targetRotation.y = 0;
        transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
    }
}
