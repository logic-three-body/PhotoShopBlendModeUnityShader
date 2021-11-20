# 3200 Blend 

老师源项目 ：[here](https://github.com/sunkai174634/PhotoShopBlendModeUnityShader)

项目地址：[here](https://github.com/logic-three-body/PhotoShopBlendModeUnityShader)

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

#### 透明物体的渲染次序

Unity中透明物体是根据距离相机由远及近地渲染

![image-20211120151232330](./img/sceneXX.png)

![image-20211120151603096](./img/yellow.png)

![image-20211120151603096](./img/red.png)

![image-20211120151603096](./img/green.png)

下面记录了更改黄色小新距离相机时，渲染次序的变化，注意看Frame Debug的透明队列Render.TransparentGeometry

![distCamera](./img/distCamera.gif)

当然根据相机距离确定渲染顺序是透明物体的专属，且前提需要每个参与比较的透明材质渲染队列值一致

![image-20211120154449920](./img/que.png)

如果更改渲染队列值，那么最终还是按照渲染队列大小排列的（小->大）

![gr1](./img/gr1.png)

![gr2](./img/gr2.png)

![gr3](./img/gr3.png)

此时就通过控制渲染队列让他们由近及远渲染

![image-20211120155953998](./img/reverse.png)
