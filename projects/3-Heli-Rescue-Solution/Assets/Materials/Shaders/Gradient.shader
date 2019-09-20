Shader "Custom/Gradient"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _ColorTop ("Top Color", Color) = (1, 1, 1, 1)
        _ColorBottom ("Bottom Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags {"Queue" = "Background" "IgnoreProjector" = "True"}
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            fixed4 _ColorTop;
            fixed4 _ColorBottom;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                fixed4 color = lerp(_ColorTop, _ColorBottom, i.uv.y );
                
                return color;
            }
            ENDCG
        }
    }
}
