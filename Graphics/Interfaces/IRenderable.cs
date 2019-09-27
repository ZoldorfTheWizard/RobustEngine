using System;
using OpenTK;

namespace RobustEngine.Graphics.Interfaces
{
    internal interface IRenderable
    {        
        int[] Indicies {get;}
        Vertex[] VertexData {get;}   
        Matrix4 ModelMatrix {get;}          
        Debug DebugMode {get;set;}        
        //float PointSize{get;set;}
        //float LineWidth{get;set;}
    }
}
