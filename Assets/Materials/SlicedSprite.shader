﻿Shader "Unlit/SlicedSprite"
{
    Properties
    {
		[PerRenderData]
		[HDR]
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		[PerRenderData]
        _MainTex ("Texture", 2D) = "white" {}
		[PerRenderData]
		_Progress ("Progress", Float) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
			ZTest Always
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float4 color : COLOR;
            };

            struct v2f
            {
				float4 color : COLOR;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _Color;
			float _Progress;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float angle = atan2(i.uv.y - 0.5, i.uv.x - 0.5);
				if (angle > _Progress)
					clip(-1);

                fixed4 col = tex2D(_MainTex, i.uv) * i.color * _Color;
                return col;
            }
            ENDCG
        }
    }
}
