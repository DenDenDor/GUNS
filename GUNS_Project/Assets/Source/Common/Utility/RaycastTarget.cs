using UnityEngine;
using UnityEngine.UI;

namespace Source.Utility
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class RaycastTarget : Graphic
    {
        public override void SetMaterialDirty()
        {
            return;
        }

        public override void SetVerticesDirty()
        {
            return;
        }
    }
}