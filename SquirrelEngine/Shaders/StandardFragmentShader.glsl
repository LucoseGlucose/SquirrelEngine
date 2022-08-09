#version 430

in vec3 _vertexPos;
in vec3 _normal;
in vec2 _UV;
in vec3 _worldPos;

out vec4 _color;

uniform vec4 _mainColor = vec4(.25);
uniform float _ambientLight;
uniform vec3 _lightPos;
uniform vec3 _lightColor;
uniform vec3 _camPos;
uniform float _metallic = .5;
uniform float _smoothness = .5;
uniform float _specFalloff = 10;
uniform sampler2D _mainTex;
uniform int _lightType = 0;
uniform samplerCube _skybox;

void main() 
{
	vec3 norm = normalize(_normal);
	
	vec3 lightDir = normalize(-_lightPos);
	if (_lightType == 0) { lightDir = normalize(_lightPos - _worldPos); }

	float diffuse = max(dot(_normal, lightDir), 0) + _ambientLight;

	vec3 viewDir = normalize(_camPos - _worldPos);
	vec3 reflectDir = reflect(-lightDir, _normal);

	float specAmount = pow(max(dot(viewDir, reflectDir), 0), _specFalloff);
	float specular = specAmount * _smoothness;

	vec3 skyboxDir = reflect(-viewDir, _normal);
	vec4 skyCol = texture(_skybox, skyboxDir);

	vec4 texCol = texture(_mainTex, _UV) + _mainColor;
	vec3 color = texCol.xyz * diffuse + _lightColor * diffuse + specular * _lightColor + skyCol.xyz * _metallic;
	
	float dist = 1;
	if (_lightType == 0) { dist = distance(_worldPos, _lightPos); }

	_color = vec4(color / dist, 1);
}