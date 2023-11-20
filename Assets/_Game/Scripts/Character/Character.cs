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
    [SerializeField] protected LayerMask stairLayer;
    
    [Header("Properties")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected ColorType colorType;
    [SerializeField] protected Transform brickHolder;
    
    protected Stack<CharacterBrick> bricks = new Stack<CharacterBrick>();
    
    protected string currentAnimName;
    public bool HasBrick => bricks.Count > 0;
    public ColorType ColorType => colorType;

    protected void Start()
    {
        OnInit();
    }
    protected void LateUpdate()
    {
        RotateTowardMoveDirection();
    }
    protected void OnTriggerEnter(Collider other)
    {
        
    }

    protected virtual void OnInit()
    {
        ChangeColor(colorType);
    }
    public void AddBrick()
    {
        
    }

    public void RemoveBrick()
    {
        
    }

    public void DropBrick()
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
        transform.rotation = Quaternion.LookRotation(targetRotation);
    }
    protected void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        modelCharacter.material = colorData.GetMaterial(colorType);
    }

}
