using OpenTK;

namespace RobustEngine.Graphics
{
    interface ITransformable2D
    {
        void SetScale(Vector2 Scale);
        void SetRotation(float Rotation);
        void SetPosition(Vector2 Position);

        void Scale(Vector2 Scale);
        void Rotate(float Rotation);
        void Move(Vector2 Position);
    }
}
