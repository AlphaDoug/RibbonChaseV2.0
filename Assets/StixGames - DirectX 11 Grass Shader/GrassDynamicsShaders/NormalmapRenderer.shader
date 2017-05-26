﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Stix Games/Grass Dynamics/Normalmap Renderer"
{
	Properties
	{
		_Normal ("NormalMap", 2D) = "bump" {}
		_Alpha ("Alpha Map", 2D) = "white"  {}
		_Cutout("Cutout", Range(0, 1)) = 0.2
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
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 pos : TEXCOORD2;
				float3 normal : NORMAL;
				float3 tangent : TANGENT;
				float3 bitangent : TEXCOORD1;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			sampler2D _Normal;
			float4 _Normal_ST;
			
			sampler2D _Alpha;
			float _Cutout;

			float _GrassDisplacementBorderArea;
			float4 _GrassRenderTextureArea;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.pos = mul(unity_ObjectToWorld, v.vertex);

				//Multiply with inverse transposed matrix
				o.normal =  mul(float4(v.normal,0.0f), unity_WorldToObject).xyz;
				o.tangent = mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0.0));
				o.bitangent = cross(o.normal, o.tangent) * v.tangent.w;

				o.uv = TRANSFORM_TEX(v.uv, _Normal);

				o.color = v.color;
				return o;
			}
			
			float3 frag (v2f i) : SV_Target
			{
				clip(tex2D(_Alpha, i.uv).r - _Cutout);

				float3 tangentSpaceNormal = UnpackNormal(tex2D(_Normal, i.uv));
				float3 normal = normalize(i.normal);
				float3 tangent = normalize(i.tangent);
				float3 bitangent = normalize(i.bitangent);

				float3 worldNormal = tangentSpaceNormal.x * tangent + tangentSpaceNormal.z * normal + tangentSpaceNormal.y * bitangent;

				//Apply normal strength
				worldNormal = normalize(worldNormal * i.color.a + float3(0, 1, 0) * (1 - i.color.a));

				//Smooth out the border of the displacement. 
				//If the displacement area too big to see the border, you should probably remove that for performance.
				float borderSmoothing = smoothstep(_GrassRenderTextureArea.x, _GrassRenderTextureArea.x + _GrassDisplacementBorderArea, i.pos.x);
				borderSmoothing *= smoothstep(_GrassRenderTextureArea.y, _GrassRenderTextureArea.y + _GrassDisplacementBorderArea, i.pos.z);

				float xBorder = _GrassRenderTextureArea.x + _GrassRenderTextureArea.z;
				borderSmoothing *= smoothstep(xBorder, xBorder - _GrassDisplacementBorderArea, i.pos.x);

				float yBorder = _GrassRenderTextureArea.y + _GrassRenderTextureArea.w;
				borderSmoothing *= smoothstep(yBorder, yBorder - _GrassDisplacementBorderArea, i.pos.z);

				return normalize(worldNormal * borderSmoothing + float3(0, 1, 0) * (1 - borderSmoothing)).xzy;
			}
			ENDCG
		}
	}
}
