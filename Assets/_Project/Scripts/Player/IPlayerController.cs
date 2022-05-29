using System;

namespace pepipe.DeathRun.Player 
{
    public interface IController {
        public Action Jumping { get; set; }
        public Action StopJumping { get; set; }
        public Action Dying { get; set; }
        public Action<int> Score { get; set; } 
    }
}