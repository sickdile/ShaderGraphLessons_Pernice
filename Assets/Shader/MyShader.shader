Shader "RUFA/PrimoShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Vel("Velocita'", Float) = 2.0
        _WaveAmplitude("Ampiezza dell'onda", Range(0,0.5)) = 0.2
        _MioColore("Colore da aggiungere", Color) = (0,0,0)
        _Opacity("Opacita fusion", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Vel;
            float _WaveAmplitude;

            fixed4 _MioColore;

            float _Opacity;

            v2f vert (appdata v)
            {

                float onda = sin(_Time.y * _Vel) * 0.5 + 0.5;
                
                v.vertex.xyz += v.normal * onda * _WaveAmplitude;


                v2f o;
                //trasforma la pos del vertice in clip space.
                //clip space = una via di mezzo tra lo screen space e il world space; deve ancora
                //passare per il rasterizzatore.
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture

                //fixed4 col = fixed4 (1,0,0,1);
               
                fixed4 col = tex2D(_MainTex, i.uv);

                float oscillazione = sin(_Time.y * _Vel);

               // float redValue = oscillazione * 0.5 + 0.5;

                col = 1-(1-col)*(1- _MioColore * _Opacity) ;

                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
