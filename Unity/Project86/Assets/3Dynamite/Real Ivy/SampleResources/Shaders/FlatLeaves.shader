////////////////////////////////////////////////////////////
Shader "Real Ivy/Flat leaves"
{
    Properties
    {
        _BaseMap("Albedo",2D)="white"{}
        _Color("Color",Color)=(1,1,1,1)
        _Cutoff("Cutoff",Range(0,1))=0.5
        _Frequency("Wind Speed",Float)=1
        _Amplitude("Wind Strength",Float)=1
        _Radius("Radius",Float)=0.2
        _WindPattern("Wind Pattern",2D)="white"{}
    }

    SubShader
    {
        Tags{"RenderPipeline"="UniversalPipeline" "Queue"="AlphaTest" "RenderType"="TransparentCutout"}

        Pass
        {
            Name "ForwardLit"
            Tags{"LightMode"="UniversalForward"}
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS:POSITION;
                float3 normalOS:NORMAL;
                float4 color:COLOR;
                float2 uv:TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS:SV_POSITION;
                float2 uv:TEXCOORD0;
            };

            TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);
            TEXTURE2D(_WindPattern); SAMPLER(sampler_WindPattern);

            CBUFFER_START(UnityPerMaterial)
            float4 _BaseMap_ST;
            float4 _Color;
            float _Cutoff;
            float _Frequency;
            float _Amplitude;
            float _Radius;
            CBUFFER_END

            Varyings vert(Attributes v)
            {
                Varyings o;

                float3 pos=v.positionOS.xyz;

                float t=_Time.y*_Frequency;
                float wave=SAMPLE_TEXTURE2D_LOD(_WindPattern,sampler_WindPattern,pos.xy+t,0).r;
                pos += v.normalOS * sin(t+wave) * _Amplitude * _Radius * v.color.a;

                o.positionCS=TransformObjectToHClip(pos);
                o.uv=TRANSFORM_TEX(v.uv,_BaseMap);
                return o;
            }

            half4 frag(Varyings i):SV_Target
            {
                half4 col=SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap,i.uv)*_Color;
                clip(col.a-_Cutoff);
                return col;
            }
            ENDHLSL
        }
    }
}