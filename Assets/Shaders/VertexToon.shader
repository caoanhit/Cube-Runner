Shader "Custom/VertexToon"
{
	Properties
	{
		_Color("Color",color) = (1,1,1,1)
		[NoScaleOffset] _Gradient("Gradient", 2D) = "white" {}
	}
	SubShader
	{
		Cull Off
		Tags { "RenderType"="Opaque"
		"LightMode" = "ForwardBase"
		}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "UnityLightingCommon.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				SHADOW_COORDS(1)
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
				fixed4 color : COLOR0;
				fixed3 diff : COLOR1;
				fixed3 ambient : COLOR2;

			};
			fixed4 _Color;
			sampler2D _Gradient;
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.diff = dot(worldNormal, _WorldSpaceLightPos0.xyz)*0.5+0.5;
				TRANSFER_SHADOW(o);
				o.ambient= ShadeSH9(half4(worldNormal,1));
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed shadow = SHADOW_ATTENUATION(i);
				fixed4 col = i.color*_Color ;
				fixed3 lighting =tex2D(_Gradient,i.diff*shadow)*_LightColor0 +i.ambient;
				col.rgb *=lighting;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}
