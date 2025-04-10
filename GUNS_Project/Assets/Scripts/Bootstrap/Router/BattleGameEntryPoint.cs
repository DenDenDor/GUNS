using System.Collections.Generic;
using UnityEngine;

public class BattleGameEntryPoint : AbstractGameEntryPoint
{
    protected override List<IRouter> Routers => new List<IRouter>()
    {
        new UpdateRouter(),
        new AttackRouter(),
        new HealthRouter(),
        new PressurePlateRouter(),
        new BuildingRouter(),
        new BattleRouter(),
        new LevelRouter(),
        new BarrackRouter(),
        new SilverRouter(),
        new BulletRouter(),
        new GoldRouter(),
        new PlayerRouter(),
        new InventoryRouter(),
        new MovementRouter(),
        new EnemyRouter(),
        new SoldierRouter(),
    };
}