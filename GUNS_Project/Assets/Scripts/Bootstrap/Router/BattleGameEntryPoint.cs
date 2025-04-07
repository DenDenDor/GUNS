using System.Collections.Generic;
using UnityEngine;

public class BattleGameEntryPoint : AbstractGameEntryPoint
{
    protected override List<IRouter> Routers => new List<IRouter>()
    {
        new UpdateRouter(),
        new AttackRouter(),
        new HealthRouter(),
        new SilverRouter(),
        new PlayerRouter(),
        new PressurePlateRouter(),
        new InventoryRouter(),
        new MovementRouter(),
        new EnemyRouter(),
        new SoldierRouter(),
    };
}