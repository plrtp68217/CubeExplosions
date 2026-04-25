using System;
using UnityEngine;

namespace Assets.Scripts.Types.Interfaces
{
    public interface ISeparable
    {
        event Action<ISeparable> Separating;
        int SeparationChance { get; set; }
        ISeparable Clone();
        void TrySeparate();
    }
}
