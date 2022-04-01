using UnityEngine;

namespace VRTD.Input
{
    public interface IController
    {
        Vector3 Position { get; }
        bool IsItemSelected { get; }
        Quaternion Rotation { get; }
        IHandAnimatorController HandAnimatorController { get; }
        public Vector3 GetVelocity();
        void Attach(Transform transform);
        void Detach(Transform transform);
        void SetVibration(AudioClip audioClip);
        void SetVibration(float frequency, float amplitude, float time);
        void StopVibration();
        void SetHandType(HandType handType);
        Vector3 VectorForward { get; }
        Vector3 SnapPoint { get; }
        void Release();
        OVRInput.Controller CurrentController { get; }
    }
}