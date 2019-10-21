Shader "Custom/Vertex Unlit"
{
	Properties
	{
		_Color("Color",color) = (1,1,1,1)
		_Alpha("Alpha",Range(0,1))=1
	}
	SubShader
	{
		Cull Off
		Tags { "RenderType"="Transparent"
		}
		Blend SrcAlpha OneMinusSrcAlpha
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
				fixed4 color : COLOR;
			};

			struct v2f
			{
				SHADOW_COORDS(1)
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
				fixed4 color : COLOR0;
			};
			fixed4 _Color;
			fixed _Alpha;
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = v.color;;
				TRANSFER_SHADOW(o);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed shadow = SHADOW_ATTENUATION(i);
				fixed4 col = i.color*_Color ;
				col.rgb *=shadow;
				col.a=_Alpha;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}
