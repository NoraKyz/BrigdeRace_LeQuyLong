using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    
    [Header("Properties")]
    [SerializeField] protected float speed;
    [SerializeField] List<CharacterBrick> bricks = new List<CharacterBrick>();
    
    protected string currentAnimName;
    protected virtual void Start()
    {
        OnInit();
    }

    protected void OnInit()
    {
        
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
}
