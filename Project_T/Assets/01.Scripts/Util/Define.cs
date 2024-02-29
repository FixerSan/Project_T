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
        Hammer, Defender = 10, TestAttack = 20 , Shuriken = 30
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
        UIScene_Test, UIPopup_SelectLevelUpReward, UIPopup_SelectStage
    }

    public enum UISlot
    {
        UISlot_LevelUpReward
    }

    public enum Scene
    {
        Login, Main, Test, Stage_One
    }



    public enum SoundType
    {

    }

    public enum LobbyCharacterState
    {
        Idle, MoveEffect, MoveToStage
    }
}
