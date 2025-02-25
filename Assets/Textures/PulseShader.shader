Shader "Custom/ObjectPulseShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PulseColor ("Pulse Color", Color) = (1,1,1,1)
        _PulseSpeed ("Pulse Speed", Float) = 2.0
        _PulseTime ("Pulse Time", Float) = -1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _PulseColor;
            float _PulseSpeed;
            float _PulseTime;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                if (col.a < 0.1) discard;

                float time = _Time.y - _PulseTime;
                float pulseEffect = 0.0;

                if (_PulseTime > 0.0)
                {
                    float dist = length(i.uv - 0.5);
                    float t = time * _PulseSpeed;
                    pulseEffect = exp(-10.0 * abs(dist - t));
                }

                return col + _PulseColor * pulseEffect;
            }
            ENDCG
        }
    }
}
