using System;
using System.Collections.Generic;
using RobustEngine.Graphics.Shapes2D;

namespace RobustEngine.Graphics.Batches
{
    public class Shape2DBatch : Batch
    {

        public List<Shape2D> ShapeQueue;
        public List<Vertex> VertexQueue;
        private int offset;
        
        public Shape2DBatch(string name, float w, float h) : base(name,w,h)
        {
            ShapeQueue = new List<Shape2D>();
            VertexQueue = new List<Vertex>();
            offset=0;
        }

        public void Queue(Shape2D shape)
        {
            ShapeQueue.Add(shape);
            BatchSize++;
        }

        public void Process()
        {
            VertexQueue.Clear();
            ProcessRect2D();
            VertexData = VertexQueue.ToArray();
            VBO.Update(VertexData);
        }

        public void Draws()
        {        
            Draw();
        }

        private void ProcessRect2D()
        {
            var Rect2DQueue = ShapeQueue.FindAll(delegate(Shape2D shape)
            {
                return shape.GetType() == typeof(Rect2D);
            });

        
            foreach (Rect2D i in Rect2DQueue)
            {
                for (int x = 1; x <= i.VertexData.Length; x++)
                {
                    VertexQueue.Add(Vertex.TransformVertex(ref i.VertexData[x-1], i.ModelMatrix));
                }
            }
           
        }


    }

}