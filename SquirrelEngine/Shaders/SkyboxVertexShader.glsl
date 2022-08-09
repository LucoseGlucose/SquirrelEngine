#version 430

layout(location = 0) in vec3 _inVertexPos;

out vec3 _vertexPos;

uniform mat4 _projMat;
uniform mat4 _viewMat;
uniform mat4 _modelMat;

void main() 
{
	_vertexPos = _inVertexPos;
	gl_Position = _projMat * _viewMat * _modelMat * vec4(_inVertexPos, 1);
}