#include "../common/postFx/postFx.hlsl"
#include "../common/torque.hlsl"

uniform float amount;		//The amount of scaling of each sample, will be defined in script
uniform float redMul;		//The number of samples, will also be defined in script
uniform float greenMul;		//The number of samples, will also be defined in script
uniform float blueMul;		//The number of samples, will also be defined in script
uniform sampler2D backBuffer : register(S0);

float4 main( PFXVertToPix IN ) : COLOR
{   
	float2 uv = IN.uv0;
	float4 c0 = tex2D(backBuffer, IN.uv0);
	c0.b = c0.b*blueMul;
	c0.r = c0.r*redMul;
	c0.g = c0.g*greenMul;
	return c0;
}