using System;
using RobustEngine.Graphics.Interfaces;

using OpenTK;

namespace RobustEngine.Graphics
{
    
    public class Transformable3D : ITransformable3D
    {
                
        private Matrix4 modelmatrix;       
        private Vector3 origin;
        private Vector3 position;
        private Vector3 scale;
        private Vector3 size; //TODO CALC SIZE WHEN SCALE IS SET or maybe not?
        private Vector3 rotation;

        #region Accessors     

        public Matrix4 ModelMatrix
        { 
            get { return modelmatrix; }
            set { modelmatrix = value; }
        }  

        public Vector3 Origin 
        {
            get { return origin; }
            set { SetOrigin(value); origin=value; }
        }

        public Vector3 Scale 
        {
            get { return scale; }
            set { SetScale(value); scale=value; }
        }
        
        public Vector3 Rotation
        {
            get { return rotation; }
            set { SetRotation(value); rotation=value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { SetPosition(value); position=value; }
        } 

        public Vector3 Size 
        {
            get { return size; }
            set { SetSize(value); size=value; }
        }     

        #endregion

        public Transformable3D()
        {
            ModelMatrix = Matrix4.Identity;
        }
  
        #region ITransformable
                
        public void SetOrigin(Vector3 newOrigin)
        {
            modelmatrix *= Matrix4.CreateTranslation(-newOrigin.X, -newOrigin.Y, 0);
        }

       
        public void SetScale(Vector3 newScale)
        {
            modelmatrix *= Matrix4.CreateScale(newScale.X, newScale.Y, 1);
        }  
        
        public void SetRotation(Vector3 newRotation)
        {          
            rotation.X = MathHelper.DegreesToRadians(newRotation.X);
            rotation.Y = MathHelper.DegreesToRadians(newRotation.Y);
            rotation.Z = MathHelper.DegreesToRadians(newRotation.Z);
            
            modelmatrix *= Matrix4.CreateRotationX(rotation.X);
            modelmatrix *= Matrix4.CreateRotationY(rotation.Y);             
            modelmatrix *= Matrix4.CreateRotationZ(rotation.Z);             
        }

        public void SetPosition(Vector3 newPosition)
        {
            modelmatrix *= Matrix4.CreateTranslation(newPosition.X,newPosition.Y, 0);
        }

        public virtual void SetSize(Vector3 newSize)
        {
         
        }

        public void PushMatrix(Matrix4 mat)
        {
            modelmatrix *= mat;
        }

        public void PopMatrix()
        {
            modelmatrix = Matrix4.Identity;
        }
     
        #endregion
    }
}