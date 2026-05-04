////////////////////////////////////////////////////////////
Shader "Real Ivy/Flat leaves Lit"
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

        _Smoothness("Smoothness",Range(0,1))=0.5
        _LightStrength("Light Strength",Range(0,5))=1.5
        _BackLight("Back Light",Range(0,5))=1.2

        _EmissionColor("Emission Color",Color)=(0,0,0,0)
        _EmissionStrength("Emission Strength",Range(0,5))=0
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }

        Pass
        {
            Name "ForwardLit"
            Tags{"LightMode"="UniversalForward"}

            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float4 color      : COLOR;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv         : TEXCOORD0;
                float3 normalWS   : TEXCOORD1;
                float3 posWS      : TEXCOORD2;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            TEXTURE2D(_WindPattern);
            SAMPLER(sampler_WindPattern);

            CBUFFER_START(UnityPerMaterial)

            float4 _BaseMap_ST;
            float4 _Color;

            float _Cutoff;
            float _Frequency;
            float _Amplitude;
            float _Radius;

            float _Smoothness;
            float _LightStrength;
            float _BackLight;

            float4 _EmissionColor;
            float _EmissionStrength;

            CBUFFER_END

            Varyings vert(Attributes v)
            {
                Varyings o;

                float3 pos = v.positionOS.xyz;

                float t = _Time.y * _Frequency;

                float wave =
                SAMPLE_TEXTURE2D_LOD(
                    _WindPattern,
                    sampler_WindPattern,
                    pos.xy + t,
                    0).r;

                pos += v.normalOS *
                       sin(t + wave) *
                       _Amplitude *
                       _Radius *
                       v.color.a;

                VertexPositionInputs posInputs =
                    GetVertexPositionInputs(pos);

                VertexNormalInputs normalInputs =
                    GetVertexNormalInputs(v.normalOS);

                o.positionCS = posInputs.positionCS;
                o.posWS      = posInputs.positionWS;
                o.normalWS   = normalize(normalInputs.normalWS);
                o.uv         = TRANSFORM_TEX(v.uv,_BaseMap);

                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                half4 tex = SAMPLE_TEXTURE2D(
                    _BaseMap,
                    sampler_BaseMap,
                    i.uv);

                half4 col = tex * _Color;

                clip(col.a - _Cutoff);

                Light light = GetMainLight();

                float3 N = normalize(i.normalWS);
                float3 L = normalize(light.direction);

                // ánh sáng chiếu vào mặt lá
                float NdotL = saturate(dot(N, L));

                // ánh sáng xuyên qua mặt sau lá
                float back = saturate(dot(-N, L));

                // diffuse
                float3 diffuse =
                    col.rgb *
                    light.color *
                    NdotL *
                    _LightStrength;

                // back light xuyên lá
                float3 backLight =
                    col.rgb *
                    light.color *
                    back *
                    _BackLight;

                // specular nhẹ
                float3 V = normalize(GetWorldSpaceViewDir(i.posWS));
                float3 H = normalize(L + V);

                float spec =
                    pow(
                        saturate(dot(N,H)),
                        lerp(8,64,_Smoothness)
                    ) * _Smoothness;

                float3 specular =
                    spec * light.color;

                // emission
                float3 emission =
                    _EmissionColor.rgb *
                    _EmissionStrength;

                float3 finalCol =
                    diffuse +
                    backLight +
                    specular +
                    emission;

                // giữ màu gốc trong bóng tối
                finalCol += col.rgb * 0.2;

                return half4(finalCol, col.a);
            }

            ENDHLSL
        }
    }
}