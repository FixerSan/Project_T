public class Define
{
    public static int minStageIndex = 1;
    public static int maxStageIndex = 5;
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
        UIScene_Test, UIPopup_SelectLevelUpReward, UIPopup_SelectStage, UIPopup_Heroes
    }

    public enum UISlot
    {
        UISlot_LevelUpReward
    }

    public enum Scene
    {
        Login, Main, Test, Stage_One
    }

    public enum Hero
    {
        Eric, Shinobi
    }

    public enum SoundType
    {

    }

    public enum LobbyCharacterState
    {
        Idle, MoveEffect, Walk
    }
}
