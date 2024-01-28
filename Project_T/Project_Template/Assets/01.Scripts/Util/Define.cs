using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum Direction
    {
        Left = -1,
        Right = 1
    }

    public enum Enemy
    {

    }

    public enum EnemyState
    {
        Stay, Idle, Move, Follow, Attack, SkillCast, Die, EndBattle
    }


    public enum VoidEventType
    {

    }

    public enum IntEventType
    {

    }

    public enum UIEventType
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
        Drop
    }

    public enum UIType
    {

    }

    public enum Scene
    {
        Login, Main, Stage, Test
    }

    public enum SoundType
    {

    }
}
