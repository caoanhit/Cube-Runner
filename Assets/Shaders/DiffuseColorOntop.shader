Shader "Custom/Diffuse Color Ontop"
{
    Properties
    {
		_Color("Color",color) = (1,1,1,1)
        _Alpha("Alpha", Range(0,1)) =1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
		"LightMode" = "ForwardBase" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest Always
        LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
					// make fog work
			#pragma multi_compile_fog
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				half3 normal : NORMAL;
			};

			struct v2f
			{
				SHADOW_COORDS(0)
				UNITY_FOG_COORDS(1)
				fixed4 diff : COLOR1;
				fixed3 ambient : COLOR2;
				float4 pos : SV_POSITION;
			};
			fixed4 _Color;
            float _Alpha;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				o.diff = nl * _LightColor0;
				o.ambient = ShadeSH9(half4(worldNormal,1));
				TRANSFER_SHADOW(o);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _Color;
				fixed shadow = SHADOW_ATTENUATION(i);
				fixed3 lighting = i.diff*shadow + i.ambient;
				col.rgb *= lighting;
                col.a=_Alpha;
				UNITY_APPLY_FOG(i.fogCoord, col);
                
				return col;
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}