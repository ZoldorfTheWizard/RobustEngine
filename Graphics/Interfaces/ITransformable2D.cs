using OpenTK;

namespace RobustEngine.Graphics
{
    public interface ITransformable2D
    {   
        Matrix4 ModelMatrix {get;}  
        Vector2 Origin   {get; set;}
        Vector2 Position {get; set;}
        Vector2 Scale    {get; set;}
        Vector2 Size     {get; set;}
        Vector2 Rotation {get; set;}

               
        /// <summary>
        /// Sets the origin of the model
        /// </summary>
        /// <param name="newScale"></param>
         void SetOrigin(Vector2 newOrigin);

        /// <summary>
        /// Sets the world Position of the 2D model
        /// </summary>
        /// <param name="newPosition">New position.</param>
        void SetPosition(Vector2 newPosition);

        /// <summary>
        /// Sets the rotation of the model. 
        /// </summary>
        /// <param name="newRotation"> Degrees to rotate by per axis</param>
        void SetRotation(Vector2 rotation);
 
        /// <summary>
        /// Sets the scale of the model
        /// </summary>
        /// <param name="newScale"></param>
        void SetScale(Vector2 newScale);

        /// <summary>
        /// Sets the Size of the model
        /// </summary>
        /// <param name="size"></param>
        void SetSize(Vector2 size);
        

        void PushMatrix(Matrix4 mat4);
        void PopMatrix();
    }
}
