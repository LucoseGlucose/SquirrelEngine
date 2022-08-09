#version 430

in vec3 _vertexPos;

out vec4 _color;

uniform samplerCube _cubeMap;

void main() 
{
	_color = texture(_cubeMap, _vertexPos);
}