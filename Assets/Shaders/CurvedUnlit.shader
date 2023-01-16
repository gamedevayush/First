Shader "Unlit/CurvedUnlit"
{ 
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Transparency("Transparency",Range(0.0,0.5)) = 0.25
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work 
			#pragma multi_compile_fog
				
			// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


#include "UnityCG.cginc"

struct appdata
{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
	float4 color : COLOR;
};

struct v2f
{
	float2 uv : TEXCOORD0;
	UNITY_FOG_COORDS(1)
	float4 color : TEXCOORD2;
	float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
float4 _MainTex_ST;
float _CurveStrengthx;
float _CurveStrengthy;


v2f vert(appdata v)
{
	v2f o;

	float _Horizon = 100.0f;
	float _FadeDist = 50.0f;

	o.vertex = UnityObjectToClipPos(v.vertex);


	float dist = UNITY_Z_0_FAR_FROM_CLIPSPACE(o.vertex.z);

	o.vertex.y -= _CurveStrengthx * dist * dist * _ProjectionParams.y;
	o.vertex.x -= _CurveStrengthy * dist * dist * _ProjectionParams.x;

	o.uv = TRANSFORM_TEX(v.uv, _MainTex);

	o.color = v.color;

	return o;
}

fixed4 frag(v2f i) : SV_Target
{
	// sample the texture
	fixed4 col = tex2D(_MainTex, i.uv) * i.color;
return col;
}

			ENDCG
		}
	}
}
