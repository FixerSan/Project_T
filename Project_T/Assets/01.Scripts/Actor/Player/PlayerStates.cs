using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

namespace PlayerStates
{
    public class Idle : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {

        }
        public override void Update(PlayerController _entity)
        {
            if (_entity.player.CheckMove()) return;
            if (_entity.player.CheckFollow()) return;
            if (_entity.player.CheckAttack()) return;
        }
        public override void Exit(PlayerController _entity)
        {

        }
    }

    public class Follow : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {

        }
        public override void Update(PlayerController _entity)
        {
            if (_entity.player.CheckAttack()) return;
            _entity.player.Follow();
        }
        public override void Exit(PlayerController _entity)
        {
            _entity.player.Stop();
        }
    }

    public class Move : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {

        }
        public override void Update(PlayerController _entity)
        {
            if (_entity.player.CheckStop()) return;
            _entity.player.Move(Managers.Object.PlayerMovePos - _entity.transform.position);
        }
        public override void Exit(PlayerController _entity)
        {
            _entity.player.Stop();
        }
    }
    public class Attack : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.player.Attack();
        }
        public override void Update(PlayerController _entity)
        {
            if (_entity.player.CheckMove_Attack()) return;
        }
        public override void Exit(PlayerController _entity)
        {

        }
    }
    public class SkillCast : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {

        }
        public override void Update(PlayerController _entity)
        {

        }
        public override void Exit(PlayerController _entity)
        {

        }
    }
    public class Die : State<PlayerController>
    {
        public override void Enter(PlayerController _entity)
        {
            _entity.player.Die();
        }
        public override void Update(PlayerController _entity)
        {

        }
        public override void Exit(PlayerController _entity)
        {

        }
    }
}
