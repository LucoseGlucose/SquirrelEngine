#version 430

in vec3 _vertexPos;
in vec3 _normal;
in vec2 _UV;
in vec3 _worldPos;

out vec4 _color;

uniform vec4 _mainColor = vec4(.25);
uniform vec3 _ambientLight;
uniform vec3 _lightPos;
uniform vec3 _lightColor;
uniform vec3 _camPos;
uniform float _specStrength = 1;
uniform float _specFalloff = 10;

void main() 
{
	vec3 norm = normalize(_normal);
	vec3 lightDir = normalize(_lightPos - _worldPos);
	float diffuse = max(dot(_normal, lightDir), 0);

	vec3 viewDir = normalize(_camPos - _worldPos);
	vec3 reflectDir = reflect(-lightDir, _normal);
	float specAmount = pow(max(dot(viewDir, reflectDir), 0), _specFalloff);
	float specular = specAmount * _specStrength;

	_color = vec4((_mainColor.xyz + _ambientLight * _mainColor.xyz) * (_lightColor * diffuse + specular * _mainColor.xyz) / distance(_worldPos, _lightPos), 1);
}