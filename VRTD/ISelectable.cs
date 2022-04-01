using System;
using UnityEngine;

namespace VRTD.Input
{
    public interface ISelectable : IIndicatedObject
    {
        public void Select(IController controller);
        public HandAnimationType HandAnimationType { get; }
        public void Release(IController controller, Vector3 velocity);
        event Action<IController,bool> OnSelected;
    }
}