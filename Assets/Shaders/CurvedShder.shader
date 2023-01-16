Shader "Curved/CurvedShder"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Transparency("Transparency",Range(0.0,0.5)) = 0.25
    }
    SubShader
    {
        //Tags { "RenderType"="Transparent" }
        Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}
        LOD 200
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert addShadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float3 _Curvature;

        float4 _TintColor; 
        float _Transparency; 
        void vert(inout appdata_full v)
        {
            float4 vPos = mul(unity_ObjectToWorld,v.vertex);

            float dist = vPos.z - _WorldSpaceCameraPos.z;

            float addHeight = dist * dist;

            vPos.y -= addHeight * _Curvature.y;

            dist = vPos.x - _WorldSpaceCameraPos.x;
            float addSideHeight = dist * dist;
            vPos.y -= addSideHeight * _Curvature.x;
            
            //For Corner
            vPos.x += addHeight * _Curvature.z;

            vPos = mul(unity_WorldToObject,vPos);
            v.vertex = vPos;

        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {   
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            c.a = _Transparency;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
