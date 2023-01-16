Shader "Custom/Curved Text Shader" {
    Properties {
        _MainTex ("Font Texture", 2D) = "white" {}
        _Color ("Text Color", Color) = (1,1,1,1)
    }

    SubShader {

        Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}
        LOD 200
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
				
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
				float4 color : TEXCOORD2;
            };
			
			

            sampler2D _MainTex;
            uniform fixed4 _Color;
			
			float4 _MainTex_ST;
			float _CurveStrengthx;
			float _CurveStrengthy;

            v2f vert (appdata_t v)
            {
                v2f o;
				
				
			
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
               
			  o.vertex = UnityObjectToClipPos(v.vertex);


	float dist = UNITY_Z_0_FAR_FROM_CLIPSPACE(o.vertex.z);

	o.vertex.y -= _CurveStrengthx * dist * dist * _ProjectionParams.y;
	o.vertex.x -= _CurveStrengthy * dist * dist * _ProjectionParams.x;

	o.uv = TRANSFORM_TEX(v.uv, _MainTex);

	o.color = v.color;


	return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col = tex2D(_MainTex, i.uv)*5 ;
                col *= tex2D(_MainTex,i.uv);
				
                return col;
            }
            ENDCG
        }
    }
}