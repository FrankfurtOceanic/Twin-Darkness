// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/ScreenShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

           struct v2f {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
            };
            
            v2f vert (appdata_base v)
            {
                 v2f o;
                 o.pos = UnityObjectToClipPos(v.vertex);

                 float3 origin = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0)).xyz;
                 float3 area = float3(100,100,100);
 
                 float3 cameraDir = mul((float3x3)UNITY_MATRIX_V,float3(0,0,1));
                 float3 norm = v.normal;//mul(unity_ObjectToWorld, float4(v.normal, 0.0));
                 //norm *= v.color.x;
                 float light = saturate((dot(norm, cameraDir)+1.0)*0.5);

                 o.color.xyz = ((origin + area) * 0.5) / area;
                 o.color.x *= light;
                 o.color.y /= light;
                 //o.color *= v.color.y;
                 o.color = frac(o.color * 100);
                 
                 
                 
                 o.color.xyz = v.normal;
                 o.color.w = 1;
                 
                 
                 
                 return o;
            }


            sampler2D _MainTex;
            sampler2D _CameraNormalsTexture;

            fixed4 frag(v2f i) : SV_TARGET
            {
                return i.color;
            }
            ENDCG
        }
    }
}
