﻿Shader "Custom/Gradient Sprite"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _BottomColor ("Bottom Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags {
            "Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
        }
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 color    : COLOR;
                half screenpos : TEXCOORD0;
            };
            fixed4 _TopColor,_BottomColor;
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float4 screenpos= ComputeScreenPos(o.pos);
                o.screenpos=screenpos.y/screenpos.w;
                o.color= v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col=i.color;
                col *= lerp(_BottomColor,_TopColor,i.screenpos);
                return col;
            }
            ENDCG
        }
    }
}
