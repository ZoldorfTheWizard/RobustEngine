using System;
using OpenTK;

namespace RobustEngine.Graphics
{
    public interface IRenderable2D
    {
        float PointSize{get;set;}
        float LineWidth{get;set;}

        int[] Indicies {get;}
        Vertex[] VertexData {get;}   
        
        Matrix4 ModelMatrix {get;}  
        
        Debug DebugMode {get;set;}        
        
        Color FillColor {get;set;}       
        void SetFillColor(Color color);  
    }
}
