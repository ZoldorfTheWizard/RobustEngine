#version 330


layout(location = 0) in vec4 position;
void main()
{
   // transform the vertex position
    gl_Position = position; //gl_ModelViewProjectionMatrix * gl_Vertex;

    // transform the texture coordinates
    //gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;

    // forward the vertex color
   //gl_FrontColor = gl_Color;
}
