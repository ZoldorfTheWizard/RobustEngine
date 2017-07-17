#version 330
layout(location = 0) in vec4 PositionIn;
layout(location = 1) in vec4 ColorIn;
layout(location = 3) in vec2 UVCoordIn;

out vec2 TexCoord; 
out vec4 Color;

void main()
{
   gl_Position = PositionIn;
   Color = ColorIn;
   TexCoord = UVCoordIn;
  
}
