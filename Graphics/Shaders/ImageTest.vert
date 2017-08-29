#version 330
layout(location = 0) in vec3 PositionIn;
layout(location = 1) in vec4 ColorIn;
layout(location = 3) in vec2 UVCoordIn;

uniform mat4 ModelMatrix;

out vec2 TexCoord; 
out vec4 Color;

void main()
{
   gl_Position = ModelMatrix * vec4(PositionIn, 1) ;  
   Color = ColorIn;
   TexCoord = UVCoordIn;  
}
