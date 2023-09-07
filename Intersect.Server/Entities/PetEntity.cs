
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Logging;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.Entities.Pathfinding;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Stat = Intersect.Enums.Stat;

namespace Intersect.Server.Entities
{
    public class PetEntity:Entity
    {
        // Basic properties for a Pet
        public string Name { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }
        public PetBase Base { get; private set; }
      //  private bool IsStunnedOrSleeping => CachedStatuses.Any(PredicateStunnedOrSleeping);
        //private bool IsUnableToCastSpells => CachedStatuses.Any(PredicateUnableToCastSpells);

        //Pathfinding
        private Pathfinder mPathFinder;
        // Owner of the pet
        public Player Owner { get; set; }

        // State of the pet (Following, Attacking, Idle, etc.)
        public PetState State { get; set; }

        public MapController AggroCenterMap;

        /// <summary>
        /// The X value on which this NPC was "aggro'd" and started chasing a target.
        /// </summary>
        public int AggroCenterX;

        /// <summary>
        /// The Y value on which this NPC was "aggro'd" and started chasing a target.
        /// </summary>
        public int AggroCenterY;

        /// <summary>
        /// The Z value on which this NPC was "aggro'd" and started chasing a target.
        /// </summary>
        public int AggroCenterZ;
        // Initialize a new PetEntity with default values

        public PetEntity(PetBase myBase, Player owner = null) : base()  // Añadido un parámetro para el dueño, inicializado a null por defecto
        {
            Name = myBase.Name;
            Sprite = myBase.Sprite;
            Level = myBase.Level;
            Immunities = myBase.Immunities;
            Base = myBase;
            Owner = owner;  // Establecer el dueño de la mascota
            State = PetState.Following;
            Experience = 0;

            for (var i = 0; i < (int)Enums.Stat.StatCount; i++)
            {
                BaseStats[i] = myBase.Stats[i];
                Stat[i] = new Combat.Stat((Stat)i, this);
            }

            var spellSlot = 0;
            for (var i = 0; i < Base.Spells.Count; i++)
            {
                var slot = new SpellSlot(spellSlot);
                slot.Set(new Spell(Base.Spells[i]));
                Spells.Add(slot);
                spellSlot++;
            }

            for (var i = 0; i < (int)Vital.VitalCount; i++)
            {
                SetMaxVital(i, myBase.MaxVital[i]);
                SetVital(i, myBase.MaxVital[i]);
            }

            mPathFinder = new Pathfinder(this);

            if (myBase.DeathAnimation != null)
            {
                DeathAnimation = myBase.DeathAnimation.Id;
            }

            // Aquí puedes añadir más campos si los necesitas
        }
        public override EntityType GetEntityType()
        {
            return EntityType.Pet;
        }
        public override void Die(bool generateLoot = true, Entity killer = null)
        {
            lock (EntityLock)
            {
                base.Die(generateLoot, killer);

                AggroCenterMap = null;
                AggroCenterX = 0;
                AggroCenterY = 0;
                AggroCenterZ = 0;

                if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
                {
                    instance.RemoveEntity(this);
                }
                PacketSender.SendEntityDie(this);
                PacketSender.SendEntityLeave(this);
            }
        }
        public void Adopt(Player newOwner)
        {
            Owner = newOwner;
            // ... Cualquier otra lógica que deba ejecutarse cuando la mascota es adoptada
        }

        public void Unadopt()
        {
            Owner = null;
            // ... Cualquier otra lógica que deba ejecutarse cuando la mascota es liberada o desinvocada
        }
        // Method to follow the owner
        public void FollowOwner(long timeMs)
        {
            var targetMap = Owner.MapId;
            var targetX = Owner.X;
            var targetY = Owner.Y;
            var targetZ = Owner.Z;

            // Check if the pet is already close to the owner
            if (Math.Abs(targetX - this.X) <= 1 && Math.Abs(targetY - this.Y) <= 1)
            {
                return;  // Close enough, no need to move
            }

            if (mPathFinder.GetTarget() != null)
            {
                if (targetMap != mPathFinder.GetTarget().TargetMapId ||
                    targetX != mPathFinder.GetTarget().TargetX ||
                    targetY != mPathFinder.GetTarget().TargetY)
                {
                    mPathFinder.SetTarget(null);
                }
            }

            if (mPathFinder.GetTarget() == null)
            {
                mPathFinder.SetTarget(new PathfinderTarget(targetMap, targetX, targetY, targetZ));
            }

            if (mPathFinder.GetTarget() != null)
            {
                switch (mPathFinder.Update(timeMs))
                {
                    case PathfinderResult.Success:
                        var dir = mPathFinder.GetMove();
                        if (dir > Direction.None)
                        {
                            if (CanMoveInDirection(dir, out var blockerType, out _))
                            {
                                Move(dir, null);
                            }
                            else
                            {
                                mPathFinder.PathFailed(timeMs);
                            }
                        }
                        break;
                    case PathfinderResult.OutOfRange:
                        PacketSender.SendChatMsg(Owner, "Tu invocación está fuera de rango.", ChatMessageType.Error);
                        break;
                    case PathfinderResult.NoPathToTarget:
                    case PathfinderResult.Failure:
                       
                        // Ejemplo: Enviar un mensaje al jugador de advertencia
                        PacketSender.SendChatMsg(Owner, "Tu invocación no puede encontrarte.", ChatMessageType.Error);

                        break;
                    case PathfinderResult.Wait:
                        // Wait for the next cycle to move
                        break;
                }
            }
        }
        /*
                public  void HandleAttack()
                {
                    if (State != PetState.Attacking)  // Asegurarse de que la mascota esté en el estado de ataque
                    {
                        return;
                    }

                    // Obtener el objetivo del dueño del jugador
                    Entity target = Owner.Target;  // Asumiendo que 'Owner' es una referencia al jugador dueño de la mascota
                                                   // Selecciona la mejor habilidad para usar
                    Spell bestAbility = SelectBestAbility(target);
                    // Usa la habilidad seleccionada en el objetivo
                    UseAbility();
                    // Si el dueño no tiene un objetivo, entonces la mascota tampoco debería tener uno
                    if (target == null)
                    {
                        return;
                    }

                    // Utilizar la lógica de ataque ya implementada en la clase Entity
                    if (!CanAttack(target, null))
                    {
                        return;
                    }

                    if (!IsOneBlockAway(target))
                    {
                        return;
                    }

                    if (!IsFacingTarget(target))
                    {
                        return;
                    }

                    var deadAnimations = new List<KeyValuePair<Guid, Direction>>();
                    var aliveAnimations = new List<KeyValuePair<Guid, Direction>>();

                    // Aquí puedes añadir la animación específica para cuando la mascota ataca, si tienes una
                    if (Base.AttackAnimation != null)
                    {
                        PacketSender.SendAnimationToProximity(
                            Base.AttackAnimationId, -1, Guid.Empty, target.MapId, (byte)target.X, (byte)target.Y,
                            Dir, target.MapInstanceId
                        );
                    }

                    // Llamar al método TryAttack de la clase base (Entity)
                    base.TryAttack(
                        target, Base.Damage, (DamageType)Base.DamageType, (Stat)Base.ScalingStat, Base.Scaling,
                        Base.CritChance, Base.CritMultiplier, deadAnimations, aliveAnimations
                    );

                    PacketSender.SendEntityAttack(this, CalculateAttackTime());
                }
                public List<Spell> Abilities { get; set; }  // Lista de habilidades que la mascota puede usar

                public Spell SelectBestAbility(Entity target)
                {
                    // Simulando una selección de habilidades más avanzada, como en Npc.cs
                    Spell bestAbility = null;
                    int highestPriority = -1;
                    foreach (var spell in Abilities)
                    {
                        int priority = CalculateSpellPriority(spell, target);  // Un método hipotético que calcula la prioridad de un hechizo
                        if (priority > highestPriority)
                        {
                            bestAbility = spell;
                            highestPriority = priority;
                        }
                    }

                    return bestAbility;
                }

                public void UseAbility()
                {
                    // La lógica de uso de la habilidad sería similar a la implementada en Npc.cs,
                    // incluida la verificación de la distancia, la orientación, etc.
                    // ...
                    TryCastSpell();  // Un método hipotético que intenta lanzar un hechizo
                }

                private void TryCastSpell()
                {
                    var target = Owner?.Target;  // Usamos el objetivo del dueño

                    if (target == null || State != PetState.Attacking)  // La mascota debe estar en estado de ataque
                    {
                        return;
                    }

                    // Comprobar si la mascota está aturdida/dormida
                    if (IsStunnedOrSleeping)
                    {
                        return;
                    }

                    // Comprobar si la mascota ya está lanzando un hechizo
                    if (IsCasting)
                    {
                        return;  // No puede moverse mientras lanza un hechizo
                    }

                    if (CastFreq >= Timing.Global.Milliseconds)
                    {
                        return;
                    }

                    // Comprobar si la mascota puede lanzar hechizos
                    if (IsUnableToCastSpells)
                    {
                        return;
                    }

                    if (Abilities == null || Abilities.Count <= 0)
                    {
                        return;
                    }

                    // Elegir la mejor habilidad para usar
                    var bestAbility = SelectBestAbility(target);
                    if (bestAbility == null)
                    {
                        return;
                    }

                    // Verificar si incluso se nos permite lanzar este hechizo.
                    if (!CanCastSpell(bestAbility, target, true, out var _))
                    {
                        return;
                    }

                    // El resto del código sería similar, adaptado para la mascota...
                    // ...

                    UseAbility();  // Usar la habilidad seleccionada
                }

                public int CalculateSpellPriority(Spell spell, Entity target)
                {
                    // Aquí se implementaría la lógica para calcular la prioridad de un hechizo,
                    // similar a cómo lo hacen los NPCs.
                    // ...
                    return 0;  // Valor de ejemplo
                }
                private static bool PredicateStunnedOrSleeping(Status status)
                {
                    switch (status?.Type)
                    {
                        case SpellEffect.Sleep:
                        case SpellEffect.Stun:
                            return true;

                        case SpellEffect.Silence:
                        case SpellEffect.None:
                        case SpellEffect.Snare:
                        case SpellEffect.Blind:
                        case SpellEffect.Stealth:
                        case SpellEffect.Transform:
                        case SpellEffect.Cleanse:
                        case SpellEffect.Invulnerable:
                        case SpellEffect.Shield:
                        case SpellEffect.OnHit:
                        case SpellEffect.Taunt:
                        case null:
                            return false;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                private static bool PredicateUnableToCastSpells(Status status)
                {
                    switch (status?.Type)
                    {
                        case SpellEffect.Silence:
                        case SpellEffect.Sleep:
                        case SpellEffect.Stun:
                            return true;

                        case SpellEffect.None:
                        case SpellEffect.Snare:
                        case SpellEffect.Blind:
                        case SpellEffect.Stealth:
                        case SpellEffect.Transform:
                        case SpellEffect.Cleanse:
                        case SpellEffect.Invulnerable:
                        case SpellEffect.Shield:
                        case SpellEffect.OnHit:
                        case SpellEffect.Taunt:
                        case null:
                            return false;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                // Método para ganar experiencia
                public void GainExperience(long exp)
                {
                    Experience += exp;

                    // Verificar si la mascota sube de nivel
                    while (Experience >= GetExperienceForNextLevel())
                    {
                        LevelUp();
                    }
                }

                // Método para determinar la experiencia necesaria para el siguiente nivel
                private long GetExperienceForNextLevel()
                {
                    // Implementa tu fórmula para calcular la experiencia necesaria para el siguiente nivel
                    return Level * 100;
                }

                // Método para subir de nivel
                private void LevelUp()
                {
                    Level++;
                    Experience = 0;

                    // Otros efectos de subir de nivel (por ejemplo, aumentar la salud)
                }

                public void TryFindNewTarget(long timeMs)
                {
                    Entity newTarget = Owner.Target; // Asumiendo que 'Owner' es una referencia al jugador dueño de la mascota

                    if (newTarget != null && !newTarget.IsDead())
                    {
                        State = PetState.Attacking;
                        // Aquí podrías añadir lógica para asignar el nuevo objetivo a la mascota
                         this.Target = newTarget;
                    }
                    else
                    {
                        State = PetState.Following;
                    }
                }

                public void ProcessRegen()
                {
                    if (Base == null)
                    {
                        return;
                    }

                    foreach (Vital vital in Enum.GetValues(typeof(Vital)))
                    {
                        if (vital >= Vital.VitalCount)
                        {
                            continue;
                        }

                        var vitalId = (int)vital;
                        var vitalValue = GetVital(vital);
                        var maxVitalValue = GetMaxVital(vital);
                        if (vitalValue >= maxVitalValue)
                        {
                            continue;
                        }

                        var vitalRegenRate = Base.VitalRegen[vitalId] / 100f; // Asumiendo que PetBase tiene un campo VitalRegen
                        var regenValue = (int)Math.Max(1, maxVitalValue * vitalRegenRate) *
                                         Math.Abs(Math.Sign(vitalRegenRate));

                        AddVital(vital, regenValue);  // Asumiendo que tienes un método AddVital
                    }
                }
                public void SetState(PetState newState, long timeMs)
                {
                    if (IsValidStateChange(newState))
                    {
                        State = newState;
                        HandleStateChange(timeMs);
                    }
                    else
                    {
                        // Podrías registrar un error o enviar una notificación al cliente.
                        // Log.Error($"Invalid state change attempted from {State} to {newState}.");
                    }
                }

                private bool IsValidStateChange(PetState newState)
                {
                    switch (newState)
                    {
                        case PetState.Idle:
                            // Supongamos que siempre es válido volver al estado Idle
                            return true;

                        case PetState.Following:
                            // Añade condiciones para determinar si es válido entrar en el estado Following.
                            // Por ejemplo, asegurarse de que el dueño está en el mismo mapa.
                            return Owner != null && Owner.MapId == this.MapId;

                        case PetState.Attacking:
                            // Aquí puedes añadir lógica similar a la de la agresión de los NPC.
                            // Asegúrate de que hay un objetivo válido para atacar.
                            return Owner != null && Owner.Target != null && CanAttack(Owner.Target, null);

                        default:
                            return false;
                    }
                }

                private void HandleStateChange(long timeMs)
                {
                    switch (State)
                    {
                        case PetState.Idle:
                            AutonomousMovement(timeMs);
                            break;
                        case PetState.Following:
                            // Lógica para estado Following
                            FollowOwner(timeMs);
                            break;
                        case PetState.Attacking:
                            // Lógica para estado Attacking
                            HandleAttack();
                            break;
                    }
                }
                private Direction mCurrentDirection;
                private long mMoveEndTime;
                private long mPauseEndTime;

                // Enum para definir sub-estados dentro del estado Idle.
                public enum PetIdleState
                {
                    RandomWalking,
                    MovingToInterestPoint,
                    Resting
                }

                public PetIdleState CurrentIdleState { get; set; }
                public long LastIdleStateChange { get; set; }
                public int InterestPointX { get; set; }
                public int InterestPointY { get; set; }
                //Spell casting
                public long CastFreq;

                public void AutonomousMovement(long timeMs)
                {
                    if (State != PetState.Idle)
                    {
                        return;
                    }

                    // Cambio de sub-estado si ha pasado suficiente tiempo (por ejemplo, 5 segundos)
                    if (timeMs - LastIdleStateChange > 5000)
                    {
                        // Seleccionar un nuevo sub-estado al azar
                        Array values = Enum.GetValues(typeof(PetIdleState));
                        CurrentIdleState = (PetIdleState)values.GetValue(new Random().Next(values.Length));
                        LastIdleStateChange = timeMs;
                    }

                    switch (CurrentIdleState)
                    {
                        case PetIdleState.RandomWalking:
                            // Mover en una dirección aleatoria
                            Direction randomDir = (Direction)Randomization.Next(0, 4);  // Asumiendo que Direction es un enum con valores 0-3
                            Move(randomDir, null);
                            break;

                        case PetIdleState.MovingToInterestPoint:
                            // Mover hacia un punto de interés (podría definirse previamente o generarse al azar)
                            mPathFinder.SetTarget(new PathfinderTarget(MapId, InterestPointX, InterestPointY, Z));
                            // Actualizar el pathfinder y moverse acorde a ello
                            // ...
                            break;

                        case PetIdleState.Resting:
                            // La mascota se detiene y "descansa"
                            // Podrías poner aquí algún efecto visual o de sonido para indicar que la mascota está descansando
                            break;
                    }
                }

               */
        public override bool CanMoveInDirection(
    Direction direction,
    out MovementBlockerType blockerType,
    out EntityType entityType
)
        {
            entityType = default;

            // Si la mascota no tiene dueño, no debería poder moverse.
            if (Owner == null)
            {
                blockerType = MovementBlockerType.MapAttribute;
                return false;
            }

            // Delegar la comprobación de movimiento al método base
            if (!base.CanMoveInDirection(direction, out blockerType, out entityType))
            {
                return false;
            }

            // Aquí podrías agregar lógica adicional si es necesario, por ejemplo,
            // evitar que la mascota se mueva a ciertas áreas o interactúe con ciertos objetos.

            return blockerType == MovementBlockerType.NotBlocked;
        }

        public bool CanPetCombat(Entity enemy)
        {
            // Si la mascota no tiene dueño, no puede entrar en combate.
            if (Owner == null)
            {
                return false;
            }

            // Si el enemigo es un NPC, verifica si la mascota puede atacar a NPCs.
            if (enemy is Npc enemyNpc)
            {
                // Aquí podrías agregar lógica para determinar si la mascota puede atacar a este tipo de NPC.
                // Por ejemplo, podrías tener una lista blanca o negra de tipos de NPC que la mascota puede o no atacar.
                return true;
            }

            // Si el enemigo es un jugador, verifica si la mascota puede atacar a jugadores.
            if (enemy is Player)
            {
                // Aquí podrías agregar lógica para determinar si la mascota puede atacar a jugadores.
                // Por ejemplo, podrías hacer que solo ataque si el dueño también está en combate con ese jugador.
                return Owner.Target == enemy;
            }

            return false;
        }
        public override void Update(long timeMs)
        {
            var lockObtained = false;
            try
            {
                Monitor.TryEnter(EntityLock, ref lockObtained);
                if (lockObtained)
                {
                    // Llamada al método Update de la clase base
                    base.Update(timeMs);

                    // Si la mascota no tiene dueño, tal vez deberíamos eliminarla o enviarla de vuelta a su hogar
                    if (Owner == null)
                    {
                        return;
                    }

                    // Siempre seguir al dueño, independientemente de si está en combate o no
                    FollowOwner(timeMs);

                    // Si el dueño está en combate, hacer que la mascota también entre en combate
                    if (Owner.IsAttacking)
                    {
                        var playerTarget = Owner.Target;
                        if (playerTarget != null && !playerTarget.IsDead() && CanAttack(playerTarget, null))
                        {
                            // Cambia el objetivo de la mascota si es necesario
                            if (playerTarget != Target)
                            {
                                Target = playerTarget;
                                CombatTimer = Timing.Global.Milliseconds + Options.CombatTime;
                            }

                            // Intenta atacar al objetivo
                            TryAttack(playerTarget);
                        }
                    }
                }
            }
            finally
            {
                if (lockObtained)
                {
                    Monitor.Exit(EntityLock);
                }
            }
        }

        //Targeting
        public void AssignTarget(Entity en)
        {
            var oldTarget = Target;

            // Are we resetting? If so, do not allow for a new target.
            var pathTarget = mPathFinder?.GetTarget();
            if (AggroCenterMap != null && pathTarget != null)
            {
                if (en == null)
                {
                    if (pathTarget.TargetMapId == AggroCenterMap.Id && pathTarget.TargetX == AggroCenterX && pathTarget.TargetY == AggroCenterY)
                    {
                        return;
                    }
                }
            }

            //Why are we doing all of this logic if we are assigning a target that we already have?
            if (en != null && en != Target)
            {
                // Can't assign a new target if taunted, unless we're resetting their target somehow.
                // Also make sure the taunter is in range.. If they're dead or gone, we go for someone else!
                if ((pathTarget != null && AggroCenterMap != null && (pathTarget.TargetMapId != AggroCenterMap.Id || pathTarget.TargetX != AggroCenterX || pathTarget.TargetY != AggroCenterY)))
                {
                    foreach (var status in CachedStatuses)
                    {
                        if (status.Type == SpellEffect.Taunt && en != status.Attacker && GetDistanceTo(status.Attacker) != 9999)
                        {
                            return;
                        }
                    }
                }

                if (en is Projectile projectile)
                {
                    if (projectile.Owner != this && !TargetHasStealth(projectile))
                    {
                        Target = projectile.Owner;
                    }
                }
                else
                {
                    if (en is PetEntity npc)
                    {
                        if (npc.Base == Base)
                        {
                           
                        }
                    }
                    if (en is Npc)
                    {
                        //TODO Make sure that the npc can target the player
                        if (this != en && !TargetHasStealth(en))
                        {
                            Target = en;
                        }
                    }
                    if (Owner == null)
                    {
                        return;
                    }

                    // Asegurarse de que solo el dueño puede asignar un objetivo
                    if (Owner.Id != this.Id)
                    {
                        return;
                    }

                    if (en is Player)
                    {
                        // Aseguramos que la entidad 'en' no es la misma que está ejecutando este código
                        // y que 'en' no tiene habilidades de sigilo activas.
                        if (this != en && !TargetHasStealth(en)) // Función hipotética TargetHasStealth
                        {
                            // Como la mascota siempre tiene un dueño, atacamos cualquier jugador que no sea el dueño.
                            if (Owner != null && en.Id != Owner.Id)
                            {
                                Target = en;
                            }
                        }
                    }
                    
                    else
                    {
                        if (this != en && !TargetHasStealth(en))
                        {
                            Target = en;
                        }
                    }
                }

              
            }
            else
            {
                Target = en;
            }

            if (Target != oldTarget)
            {
                CombatTimer = Timing.Global.Milliseconds + Options.CombatTime;
               // PacketSender.SendNpcAggressionToProximity(this);
            }
          //  mTargetFailCounter = 0;
        }
        public bool TargetHasStealth(Entity target)
        {
            return target == null || target.CachedStatuses.Any(s => s.Type == SpellEffect.Stealth);
        }
        public override EntityPacket EntityPacket(EntityPacket packet = null, Player forPlayer = null)
        {
            if (packet == null)
            {
                packet = new PetPacket();
            }

            packet = base.EntityPacket(packet, forPlayer);

            var pkt = (PetPacket)packet;
            pkt.CurrentState = State;  // Aquí asignamos el estado actual de la mascota al paquete

            return pkt;
        }
    }
    
   
}
