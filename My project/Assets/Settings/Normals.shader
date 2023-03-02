// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Normals"
{
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
         
            struct v2f {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
            };
            
            v2f vert (appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                //o.color.xyz = v.normal; //* 0.5 + 0.5;
                o.color.w = 1.0;


                float3 origin = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0)).xyz;
                float3 area = float3(100,100,100);
 
                float3 cameraDir = mul((float3x3)UNITY_MATRIX_V,float3(0,0,1));
                float3 norm = mul(unity_ObjectToWorld, float4(v.normal, 0.0));
                norm *= 0.1;
                float light = saturate((dot(norm, cameraDir)+1.0)*0.5);


                 o.color.xyz = ((origin + area) * 0.5) / area;
                 o.color.x *= light;
                 o.color.y /= light;
                 o.color *= v.color.g;
                 o.color = frac(o.color * 100);
 
                 return o;




                return o;
            }

            fixed4 frag (v2f i) : SV_Target { return i.color; }
            ENDCG
        }
    } 
}

