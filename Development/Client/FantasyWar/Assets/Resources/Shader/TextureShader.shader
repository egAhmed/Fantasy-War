Shader "LCH/TextureShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OffsetSpeed("Offset", Range(0, 1)) = 0
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _OffsetSpeed;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv = v.uv.xy * _MainTex_ST.xy*float2(0,0) + _MainTex_ST.zw;
				//o.uv.y += _Time.x;     //_Time是一个float4类型的，xyzw t是自场景加载开始所经过的时间，相当于Time.time，t/20,t,2t,3t, _Timey以外，还有_SinTime, 
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				return fixed4(color.rgb, 1.0);
			}
			ENDCG
		}
	}
}