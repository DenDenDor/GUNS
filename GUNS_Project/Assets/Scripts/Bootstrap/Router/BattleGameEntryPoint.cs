using System.Collections.Generic;
using UnityEngine;

public class BattleGameEntryPoint : AbstractGameEntryPoint
{
    protected override List<IRouter> Routers => new List<IRouter>()
    {
        new UpdateRouter(),
        new AttackRouter(),
        new HealthRouter(),
        new PlayerRouter(),
        new PressurePlateRouter(),
        new MovementRouter(),
        new EnemyRouter(),
        new SoldierRouter(),
    };
}