using System;
using System.Collections.Generic;
using RobustEngine.Graphics.Shapes2D;
using OpenTK;

namespace RobustEngine.Graphics.Batching2D
{
    //TODO make batch2d protected
    //TODO virtualize process
    //TODO make shape2dbatch use Irenderable properly
    public sealed class Shape2DBatch : Batch2D
    {

        private List<Shape2D> ShapeQueue;
        private List<Vertex> VertexQueue;
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

        public void Clear()
        {
            ShapeQueue.Clear();
            BatchSize=0;
        }

        public override void Process()
        {
            VertexQueue.Clear();

            var Rect2DQueue = ShapeQueue.FindAll(delegate(Shape2D shape)
            {
                return shape.GetType() == typeof(Rect2D);
            });
        
            for (int i =0; i < Rect2DQueue.Count; i++)
            {
                var rect = Rect2DQueue[i];
               
                VertexQueue.Add(Vertex.TransformVertex(ref rect.VertexData[0], rect.ModelMatrix));
                VertexQueue.Add(Vertex.TransformVertex(ref rect.VertexData[1], rect.ModelMatrix));
                VertexQueue.Add(Vertex.TransformVertex(ref rect.VertexData[2], rect.ModelMatrix));
                VertexQueue.Add(Vertex.TransformVertex(ref rect.VertexData[3], rect.ModelMatrix));
                rect.SetOrigin(new Vector2(rect.Position.X,rect.Position.Y ));
            }

            VertexData = VertexQueue.ToArray();
            VBO.Update(VertexData);
        }

        public void Draws()
        {        
            Draw();
        } 
    }

}