#version 430

layout(location = 0) in vec3 _inVertexPos;
layout(location = 1) in vec3 _inNormal;
layout(location = 2) in vec2 _inUV;

out vec3 _vertexPos;
out vec3 _normal;
out vec2 _UV;
out vec3 _worldPos;

uniform mat4 _projMat;
uniform mat4 _viewMat;
uniform mat4 _modelMat;

void main() 
{
	_vertexPos = _inVertexPos;
	_normal = (_modelMat * vec4(_inNormal, 1)).xyz;
	_UV = _inUV;
	_worldPos = vec3(_modelMat * vec4(_inVertexPos, 1));
	gl_Position = _projMat * _viewMat * _modelMat * vec4(_inVertexPos, 1.0);
}