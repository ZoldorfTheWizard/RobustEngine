using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobustEngine.Graphics
{
    public interface IRenderable2D
    {
        void SetScale(Vector2 Scale);
        void SetRotation(float Rotation);
        void SetPosition(Vector2 Position);
        void Draw();
    }
}
