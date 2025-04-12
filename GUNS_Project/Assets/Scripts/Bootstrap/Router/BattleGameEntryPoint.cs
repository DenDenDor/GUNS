using System.Collections.Generic;
using UnityEngine;

public class BattleGameEntryPoint : AbstractGameEntryPoint
{
    protected override List<IRouter> Routers => new List<IRouter>()
    {
        new UpdateRouter(),
        new WaveRouter(),
        new AttackRouter(),
        new HealthRouter(),
        new PressurePlateRouter(),
        new BuildingRouter(),
        new BattleRouter(),
        new LevelRouter(),
        new BarrackRouter(),
        new WatchableNotificationRouter(),
        new SilverRouter(),
        new BulletRouter(),
        new GoldRouter(),
        new PlayerRouter(),
        new RotateRouter(),
        new CameraRouter(),
        new InventoryRouter(),
        new MovementRouter(),
        new EnemyRouter(),
        new ProgressBarRouter(),
        new SoldierRouter(),
    };
}