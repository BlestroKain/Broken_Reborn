using Intersect.Server.Entities;
using Intersect.Server.General;
using Intersect.Utilities;
using Intersect;
using System;
using Intersect.Server.Localization;
using Intersect.Server.Networking;

namespace Intersect.Server.Entities
{
    public abstract partial class Entity : IDisposable {

        public bool isVictimEvaded(Entity target)
        {

            var evasionRate = Formulas.CalculateEvasion(this, target);
            if (Randomization.Next(0, 501) < evasionRate)
            {
                PacketSender.SendActionMsg(this, Strings.Combat.miss, CustomColors.Combat.Missed);

                if (this is Npc npc)
                {
                    npc.MoveTimer = Timing.Global.Milliseconds + (long)GetMovementTime();
                }

                PacketSender.SendEntityAttack(this, CalculateAttackTime());

                return true;
            }

            return false;
        }
    }
}
