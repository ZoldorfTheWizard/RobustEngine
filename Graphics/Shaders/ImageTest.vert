#version 330
layout(location = 0) in vec3 PositionIn;
layout(location = 1) in vec4 ColorIn;
layout(location = 2) in vec3 NormalIn;
layout(location = 3) in vec2 UVCoordIn;

uniform mat4 ModelMatrix;
uniform mat4 ViewMatrix;

out vec2 TexCoord; 
out vec4 Color;

void main()
{
   gl_Position = ViewMatrix * (ModelMatrix * vec4(PositionIn, 1));  
   Color = ColorIn;
   TexCoord = UVCoordIn;  
}
