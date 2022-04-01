using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTD.Input
{
    public interface IInputController
    {
        void SetRestrictiveHeadCylinder(float height, float radius);
        void SetRestrictiveHandsCylinder(float height, float radius);
        bool StickMovingEnabledVertical { get; set; }
        bool StickMovingEnabledHorizontalPlane { get; set; }
        HandType HandType { get; }
        IList<IController> HandControllers { get; }
        event Action HandChanged;
        IHeadController HeadController { get; }
        void SetOrigin(Vector3 origin);
        Vector3 OriginPosition { get; }


        void SetNewHandController(HandType handType);
        /// <summary>
        /// <see cref="IStickControllable.OnStickInput(Vector3)"/> will be called every FixedUpdate
        /// </summary>
        /// <param name="stickControllable"></param>
        void AddStickControllable(IStickControllable stickControllable);

        void RemoveStickControllable(IStickControllable stickControllable);
        bool StickControllEnabled { get; set; }
    }
}
