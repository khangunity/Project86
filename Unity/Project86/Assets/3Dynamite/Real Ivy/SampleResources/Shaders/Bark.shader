////////////////////////////////////////////////////////////
Shader "Real Ivy/Bark"
{
    Properties
    {
        _BaseMap("Albedo",2D)="white"{}
        _Color("Color",Color)=(1,1,1,1)
        _NormalMap("Normal",2D)="bump"{}
        _Smoothness("Smoothness",Range(0,1))=0.4
    }

    SubShader
    {
        Tags{"RenderPipeline"="UniversalPipeline" "RenderType"="Opaque"}
        Pass
        {
            Name "ForwardLit"
            Tags{"LightMode"="UniversalForward"}

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS:POSITION;
                float2 uv:TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS:SV_POSITION;
                float2 uv:TEXCOORD0;
            };

            TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
            float4 _BaseMap_ST;
            float4 _Color;
            float _Smoothness;
            CBUFFER_END

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionCS=TransformObjectToHClip(v.positionOS.xyz);
                o.uv=TRANSFORM_TEX(v.uv,_BaseMap);
                return o;
            }

            half4 frag(Varyings i):SV_Target
            {
                half4 col=SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap,i.uv)*_Color;
                return col;
            }
            ENDHLSL
        }
    }
}