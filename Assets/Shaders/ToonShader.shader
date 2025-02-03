Shader "Guidev/ToonShader"
{
    Properties
    {
        _Albedo("Albedo",Color) = (1,1,1,1)
        _Shades("Shades",Range(1,20)) = 3

        _InkColor("InkColor", Color) = (0,0,0,0)
        _InkSize("InkSize", float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

//          Pass
//         {
//             Cull Front

//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
            

//             #include "UnityCG.cginc"

//             struct appdata
//             {
//                 float4 vertex : POSITION;
//                 float3 normal : NORMAL;
//             };

//             struct v2f
//             {
               
                
//                 float4 vertex : SV_POSITION;
                
//             };

//             float4 _InkColor;
//             float _InkSize;

           

//             v2f vert (appdata v)
//             {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex + _InkSize * v.normal);
                
//                 return o;
//             }

//             fixed4 frag (v2f i) : SV_Target
//             {
               
//                 return _InkColor;
//             }
//             ENDCG
//         }


         Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
               
                
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            float4 _Albedo;

            float _Shades;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //Calculate the cosine of the angle between the normal vector and the light direction
                //The world space light direction is stored in _WorldSpaceLightPos0
                //the world space normal is stored in i.worldNormal
                //all what we have to do now is to normalize both vectors and calculate the dot product
                float cosineAngle = dot(normalize(i.worldNormal), normalize(_WorldSpaceLightPos0.xyz));

                //Set the min to zero as the result can be negative in cases where the light is behind the shaded point
                cosineAngle = max(cosineAngle, 0.0);

                cosineAngle = floor(cosineAngle * _Shades) / _Shades;
                return _Albedo * cosineAngle;
            }
            ENDCG
        }
    }
    Fallback"VertexLit"
}
