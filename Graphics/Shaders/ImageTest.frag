#version 330 core
 
in vec2 TexCoord;
in vec4 Color;

uniform sampler2D BoundTexture;
uniform bool UsingTexture;

out vec4 FragColor;

void main()
{
    if(UsingTexture)
    {
        FragColor = texture(BoundTexture,TexCoord);
    }
    else
    { 
        FragColor = Color; 
    }  
   
}



