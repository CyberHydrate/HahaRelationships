Shader "Custom/PlaneBorder"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,0,0,1)
        _BorderColor ("Border Gold", Color) = (1,0.84,0,1)
        _BorderWidth ("Border Width", Range(0.001, 0.08)) = 0.03
        _Metallic ("Metallic", Range(0,1)) = 0.7
        _Smoothness ("Smoothness", Range(0,1)) = 0.9
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" "LightMode"="ForwardBase" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
            };

            float4 _BaseColor;
            float4 _BorderColor;
            float _BorderWidth;
            float _Metallic;
            float _Smoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, float3(0,1,0));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 计算描边区域
                float edgeDist = min( min(i.uv.x, 1 - i.uv.x), min(i.uv.y, 1 - i.uv.y) );
                fixed3 albedo = edgeDist < _BorderWidth ? _BorderColor.rgb : _BaseColor.rgb;

                // 基础光照
                float3 normal = normalize(i.worldNormal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float NdotL = saturate(dot(normal, lightDir));
                float3 diffuse = albedo * _LightColor0.rgb * NdotL;

                // 简易金属高光，匹配Metallic/Smoothness参数
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfDir = normalize(lightDir + viewDir);
                float specPower = lerp(2, 128, _Smoothness);
                float specular = pow(saturate(dot(normal, halfDir)), specPower) * _Metallic;
                float3 specCol = albedo * specular;

                float3 finalRGB = diffuse + specCol;
                return fixed4(finalRGB, 1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}