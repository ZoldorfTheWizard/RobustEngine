using System;
using OpenTK;

namespace RobustEngine.Graphics
{
    public interface ITransformable2D
    {
        void SetOrigin(Vector2 origin);
        void SetScale(Vector2 scale);
        void SetRotation(float rotate, Axis axis);
        void SetPosition(Vector2 newpos);
    }
}
