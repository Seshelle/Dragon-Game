// Upgrade NOTE: replaced 'PositionFog()' with multiply of UNITY_MATRIX_MVP by position
// Upgrade NOTE: replaced 'V2F_POS_FOG' with 'float4 pos : SV_POSITION'

Shader "FX/Force Field Hex"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,0.5)
		_MainTex("Texture", 2D) = "white" {}
	_UVScale("UV Scale", Range(0.05, 4)) = 1
		
	}
		SubShader{

		ZWrite Off
		Tags{ "Queue" = "Transparent" }
		Blend One One


		Pass{

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_fog_exp2
#include "UnityCG.cginc"

		float4 _Color;
	sampler2D _MainTex;
	float _Rate;
	float _Rate2;
	float _Scale;
	float _Distortion;
	float _ZPhase;
	float _UVScale;
	float _UVDistortion;

		// vertex shader inputs
		struct appdata
	{
		float4 vertex : POSITION; // vertex position
		float2 uv : TEXCOORD0; // texture coordinate
	};

	// vertex shader outputs ("vertex to fragment")
	struct v2f
	{
		float2 uv : TEXCOORD0; // texture coordinate
		float4 vertex : SV_POSITION; // clip space position
	};

	// vertex shader
	v2f vert(appdata v)
	{
		v2f o;
		// transform position to clip space
		// (multiply with model*view*projection matrix)
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

		// just pass the texture coordinate
		o.uv = v.uv;
		return o;
	}

	// pixel shader; returns low precision ("fixed4" type)
	// color ("SV_Target" semantic)
	fixed4 frag(v2f i) : SV_Target
	{

		return half4(tex2D(_MainTex, (i.uv) * _UVScale) * 1.5 * _Color);
	}
		ENDCG
	}
	}
}

