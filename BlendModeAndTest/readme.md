# Blend and Clip Mode 

项目地址：here

## Blend

### Blend Operation

首先来看一下场景中两个纹理面片的位置（尼尔距离相机最远，城堡离相机最近）

![scene](./img/scene.png)

渲染流程是先渲染的尼尔，之后渲染城堡，混合操作发生在渲染城堡时，Source Color是城堡纹理，Destination Color是尼尔纹理

![nier1](./img/nier1.png)

![castle1](./img/castle1.png)

**Add [One] [One]**

![xin1](./img/nier4.png)

**Add [Source Alpha] [1-Source Alpha]**

![xin1](./img/nier3.png)

**Subtract [One] [One]**

![xin1](./img/nier5.png)

**Reverse Subtract [One] [One]**

![image-20211118150232828](./img/nier6.png)

**Min [One] [One]**

![image-20211118150352479](./img/nier7.png)

**Max [One] [One]**

![nier8](./img/nier8.png)



### More details in Alpha Blending

小新纹理（source color）向城堡纹理（destination color）混合

![scene](./img/scenea.png)

![xin1](./img/xin2.png)

**SourceColor[小新] * SourceAlpha+DestinationColor[城堡] * SourceAlpha：**

![xin1](./img/xin1.png)

**SourceColor * 0+DestinationColor *SourceAlpha:**

![xin1](./img/xin3.png)

**SourceColor * 0+DestinationColor *（1-SourceColor）:**

![xin1](./img/xin4.png)

需要注意的一点：alpha混合的值不仅仅是纹理的alpha值，而是最终输出颜色的alpha值，注意看下面的实例

这是我们的片元着色器：

除了采样纹理颜色，我们也会乘以一个颜色分量**_MainColor**

```glsl
fixed4 frag(v2f i) : SV_Target
{
    fixed4 col = tex2D(_MainTex, i.uv) * _MainColor;
    return col;
}
```

![xin1](./img/xin5.png)

![xin1](./img/xin6.png)

![xin1](./img/xin7.png)

最后是最常见的alpha混合：**SourceColor[小新] * SourceAlpha+DestinationColor[城堡] * (1-SourceAlpha)**

![xin1](./img/xin8.png)

### PS Blend Mode



## Clip