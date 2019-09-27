using System;
using RobustEngine.Graphics.Interfaces;

using OpenTK;

namespace RobustEngine.Graphics
{
    
    public abstract class Transformable2D : ITransformable2D
    {
                
        private Matrix4 modelmatrix;       
        private Vector2 origin;
        private Vector2 position;
        private Vector2 scale;
        private Vector2 size; //TODO CALC SIZE WHEN SCALE IS SET or maybe not?
        private Vector3 rotation;

        #region Accessors     

        public Matrix4 ModelMatrix
        { 
            get { return modelmatrix; }
            set { modelmatrix = value; }
        }  

        public Vector2 Origin 
        {
            get { return origin; }
            set { SetOrigin(value); origin=value; }
        }

        public Vector2 Scale 
        {
            get { return scale; }
            set { SetScale(value); scale=value; }
        }
        
        public Vector3 Rotation
        {
            get { return rotation; }
            set { SetRotation(value); rotation=value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { SetPosition(value); position=value; }
        } 

        public Vector2 Size 
        {
            get { return size; }
            set { SetSize(value); size=value; }
        }     

        #endregion

        public Transformable2D()
        {
            ModelMatrix = Matrix4.Identity;
        }
  
        #region ITransformable
                
        ///<inherit-doc />
		public void SetOrigin(Vector2 newOrigin)
        {
            modelmatrix *= Matrix4.CreateTranslation(-newOrigin.X, -newOrigin.Y, 0);
        }

       
        ///<inherit-doc />
		public void SetScale(Vector2 newScale)
        {
            modelmatrix *= Matrix4.CreateScale(newScale.X, newScale.Y, 1);
        }  
        
        ///<inherit-doc />
		public void SetRotation(Vector3 newRotation)
        {          
            rotation.X = MathHelper.DegreesToRadians(newRotation.X);
            rotation.Y = MathHelper.DegreesToRadians(newRotation.Y);
            rotation.Z = MathHelper.DegreesToRadians(newRotation.Z);
            
            modelmatrix *= Matrix4.CreateRotationX(rotation.X);
            modelmatrix *= Matrix4.CreateRotationY(rotation.Y);             
            modelmatrix *= Matrix4.CreateRotationZ(rotation.Z);             
        }

        ///<inherit-doc />
		public void SetPosition(Vector2 newPosition)
        {
            modelmatrix *= Matrix4.CreateTranslation(newPosition.X,newPosition.Y, 0);
        }

        ///<inherit-doc />
        public virtual void SetSize(Vector2 newSize)
        {
         
        }

        ///<inherit-doc />
		public void PushMatrix(Matrix4 mat)
        {
            modelmatrix *= mat;
        }

        ///<inherit-doc />
		public void PopMatrix()
        {
            modelmatrix = Matrix4.Identity;
        }
     
        #endregion
    }
}