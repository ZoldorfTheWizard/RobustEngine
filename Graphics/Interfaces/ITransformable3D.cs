
using OpenTK;

namespace RobustEngine.Graphics
{
    internal interface ITransformable3D
    {   
        Matrix4 ModelMatrix {get;}  
        Vector3 Origin   {get; set;}
        Vector3 Scale    {get; set;}
        Vector3 Rotation {get; set;}               
        Vector3 Position {get; set;}
        Vector3 Size     {get; set;}

        /// <summary>
        /// Sets the Origin of the object in Model Space
        /// </summary>
        /// <param name="newScale"></param>
        void SetOrigin(Vector3 newOrigin);

        /// <summary>
        /// Sets the Rotation In Model Space
        /// </summary>
        /// <param name="newScale"></param>
        void SetScale(Vector3 newScale);

        /// <summary>
        /// Sets the Rotation In Model Space
        /// </summary>
        /// <param name="newRotation"> Degrees to rotate by per axis</param>
        void SetRotation(Vector3 rotation);
 
        /// <summary>
        /// Sets the Position In Model Space
        /// </summary>
        /// <param name="newPosition">New position.</param>
        void SetPosition(Vector3 newPosition);

        /// <summary>
        /// Resizes the Vertex Data In Local Space
        /// </summary>
        /// <param name="size"></param>
        void SetSize(Vector3 size);
        
        /// <summary>
        /// Push a Matrix onto the Model Matrix Stack
        /// </summary>
        /// <param name="mat"></param>
        void PushMatrix(Matrix4 mat4);

        /// <summary>
        /// Reset the Model Matrix Stack to Identity
        /// </summary>
        void PopMatrix();
    }
}