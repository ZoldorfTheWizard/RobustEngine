#version 330 core
layout (location = 0 )out vec4 FragColor;
void main()
{
	FragColor = vec4(0,1,1,1); // texture2D(TextureUnit0,gl_TexCoord[0].xy);
}

