// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "LCH/BuidingProgressShader" 
{
	Properties 
	{
		//_Color ("Color", Color) = (1,1,1,1)
		_BaseTex ("Base (RGB)", 2D) = "white" {}
		//渐变贴图
		//_GradualChangeTex("GradualChange (R)",2D)="white"{}
		//建造进度
		_Progress ("Progress", Range(0,1)) = 1
		//C#中通过遍历Mesh.vertex，选出下面两个Y的坐标值
		//模型最高顶点的Y坐标
		_MeshVertexYMax("MeshVertexYMax",Float)=1
		//模型最低顶点的Y坐标
		_MeshVertexYMin("MeshVertexYMin",Float)=0

		_Range("Range",Vector) = (0,0,0,0)

	}
	SubShader 
	{
		Pass
		{
		cull off
		//Blend SrcColor OneMinusDstColor
		//AlphaTest Less 0.5
		CGPROGRAM
		
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		struct m2v
		{
			float4 pos:POSITION;
			float2 uv:TEXCOORD0;
		};

		struct v2f
		{
			float4 pos:SV_POSITION;
			float2 uv:TEXCOORD0;
			//bool isShow:TEXCOORD1;
			//bool isMin:TEXCOORD2;
			float4 localpos:TEXCOORD3;

		};

		sampler2D _BaseTex;
		//sampler2D _GradualChangeTex;
		float4 _BaseTex_ST;
		float _Progress;
		float _MeshVertexYMax;
		float _MeshVertexYMin;
		float4 _Range;
		v2f vert(m2v v)
		{
			v2f f;
			//if(v.pos.y>=(_MeshVertexYMin+_Progress*(_MeshVertexYMax-_MeshVertexYMin)))
			//{
			//	//消除progress=1时，最高顶点判断为false的情况
			//	f.isShow=(_Progress==1);
				
			//	//f.isShow=false;
			//}
			//else
			//{
			//	f.isShow=true;
			//	//f.uv = v.uv.xy * _BaseTex_ST.xy + _BaseTex_ST.zw;
			//}
			//f.isMin=(_Progress==0);
			f.uv = v.uv.xy;
			f.localpos = v.pos;//mul(unity_ObjectToWorld,v.pos);
			//f.isShow = mul(unity_ObjectToWorld,v.pos).y<_Range.y;
			f.pos=UnityObjectToClipPos(v.pos);
			return f;
		}

		float4 frag(v2f f):SV_Target
		{
			//float4 gradualmap=fixed4(tex2D(_GradualChangeTex, f.uv).rgb,1);
			//
			//float comparePrgress=gradualmap.r/1;
			bool isShow;
			if(f.localpos.y<=(_MeshVertexYMin+_Progress*(_MeshVertexYMax-_MeshVertexYMin)))
			{
				//消除progress=1时，最高顶点判断为false的情况
				isShow=(_Progress==0);
				
				//f.isShow=false;
			}
			else
			{
				isShow=true;
				//f.uv = v.uv.xy * _BaseTex_ST.xy + _BaseTex_ST.zw;
			}

			
			float4 col= fixed4(tex2D(_BaseTex, f.uv).rgb,1);
			if(isShow){
				discard;
			}
			return col;
		}

		ENDCG
		}
		
	}
	FallBack "Diffuse"
}
