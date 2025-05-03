using System;
using UnityEngine;

namespace Localization
{

    public abstract class DisplayView : MonoBehaviour //, IDisplayView
    {
        public abstract string Key { get; }
        public abstract void Accept(ILocalizationVisitor visitor);

        private void Start()
        {
            LocalizedInstaller.Instance.AddView(this);
        }

        private void OnDestroy()
        {
            LocalizedInstaller.Instance.RemoveView(this);
        }
    }

}