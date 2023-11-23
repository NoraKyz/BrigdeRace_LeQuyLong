using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public abstract class Character : ObjectColor
{
    [Header("Components")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform model;
    
    [Header("Properties")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Transform brickHolder;

    protected bool isFalling = false;
    protected Stack<CharacterBrick> bricks = new Stack<CharacterBrick>();
    protected string currentAnimName;
    public int BrickAmount => bricks.Count;

    [HideInInspector] public int currentStageId;

    protected void Start()
    {
        OnInit();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            GateOut gateOut = other.GetComponent<GateOut>();
            
            if (gateOut.StageId == currentStageId)
            {
                TF.position = TF.position;
            }
            else
            {
                
            }
        }
    }

    protected override void OnInit()
    {
        base.OnInit();
        ChangeAnim(CharacterAnimName.Idle);
    }
    protected virtual void Move() { }
    protected void RotateTowardMoveDirection(Vector3 nextPoint)
    {
        Vector3 direction = nextPoint - transform.position;
        direction.y = 0;
        model.forward = direction;
    }
    private Vector3 GetNextBrickPos()
    {
        if(BrickAmount == 0)
        {
            return Vector3.zero;
        }
        
        return bricks.Peek().transform.localPosition + Vector3.up * Constants.CharacterBrickSize.y * 1.2f;
    }
    private void OvercomeGate()
    {
        
    }
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
    public void AddBrick()
    {
        CharacterBrick brick = SimplePool.Spawn<CharacterBrick>(
            PoolType.CharacterBrick, 
            GetNextBrickPos(),
            Quaternion.identity, 
            brickHolder
        );
        brick.ChangeColor(colorType);
        bricks.Push(brick);
    }
    public void RemoveBrick()
    {
        SimplePool.Despawn(bricks.Pop());
    }
    public void DropBrick()
    {
        while (BrickAmount > 0)
        {
            // TODO: complete this
        }
    }
    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up;
        }

        return transform.position;
    }
    public bool CanMove(Vector3 nextPoint)
    {
        bool isCanMove = true;
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stairLayer))
        {
            BrigdeBrick brigdeBrick = hit.collider.GetComponent<BrigdeBrick>();

            if (brigdeBrick.colorType != colorType && BrickAmount > 0)
            {
                brigdeBrick.ChangeColor(colorType);
                RemoveBrick();
            }

            if (brigdeBrick.colorType != colorType && BrickAmount == 0 && model.forward.z > 0)
            {
                isCanMove = false;
            }
        }

        return isCanMove;
    }
}
