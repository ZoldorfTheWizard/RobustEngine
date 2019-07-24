using System;
using OpenTK;

namespace RobustEngine.Graphics
{
    public interface IRenderable2D
    {
        
        Vertex[] VertexData {get;}   
        int[] Indicies {get;}
        Matrix4 ModelMatrix {get;}          
        Debug DebugMode {get;set;}        
        float PointSize{get;set;}
        float LineWidth{get;set;}
    }
}
