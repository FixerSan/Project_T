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

    public enum PlayerState
    {
        Idle, Move, Follow, Attack, SkillCast, Die
    }

    public enum Monster
    {

    }


    public enum MonsterState
    {
        Create, Stay, Idle, Move, Follow, Attack, SkillCast, Die, EndBattle
    }

    public enum Attacks
    {
        Hammer, Defender
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
        UIScene_Test
    }

    public enum Scene
    {
        Login, Main, Stage, Test
    }

    public enum SoundType
    {

    }
}
