using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class BlendModeGUI : ShaderGUI
{
    private Material mat;

    enum BlendModeCategory
    {
        Normal = 0,
        Darken = 1,
        Lighten = 2,
        Contrast = 3,
        Inversion = 4,
        Component = 5,
    }

    enum NormalBlendMode
    {
        正常_Normal=0,
        透明混合_Alphablend=1,
    }
    enum DarkenBlendMode
    {
        变暗_Darken = 2,
        正片叠底_Multiply = 3,
        颜色加深_ColorBurn = 4,
        线性加深_LinearBurn = 5,
        深色_DarkerColor = 6,
    }

    enum LightenBlendMode
    {
        变亮_Lighten = 7,
        滤色_Screen = 8,
        颜色减淡_ColorDodge = 9,
        线性减淡_LinearDodge = 10,
        浅色_LighterColor = 11,
    }

    enum ContrastBlendMode
    {
        叠加_Overlay = 12,
        柔光_SoftLight = 13,
        强光_HardLight = 14,
        亮光_VividLight = 15,
        线性光_LinearLight = 16,
        点光_PinLight = 17,
        实色混合_HardMix = 18,
    }

    enum InversionBlendMode
    {
        差值_Difference = 19,
        排除_Exclusion = 20,
        减去_Subtract = 21,
        划分_Divide = 22,
    }

    enum ComponentBlendMode
    {
        色相_Hue = 23,
        饱和度_Saturation = 24,
        颜色_Color = 25,
        明度_Luminosity = 26,
    }

    private MaterialProperty ModeID;
   // private MaterialProperty BlendCategory;
    private MaterialProperty ModeChooseProps;
    private MaterialProperty CategoryChooseProps;
    string[] NormalModeChoosenames = System.Enum.GetNames(typeof(NormalBlendMode));
    string[] DarkenModeChoosenames = System.Enum.GetNames(typeof(DarkenBlendMode));
    string[] LightenModeChoosenames = System.Enum.GetNames(typeof(LightenBlendMode));
    string[] ContrastModeChoosenames = System.Enum.GetNames(typeof(ContrastBlendMode));
    string[] InversionModeChoosenames = System.Enum.GetNames(typeof(InversionBlendMode));
    string[] ComponentModeChoosenames = System.Enum.GetNames(typeof(ComponentBlendMode));
    string[] BlendCategoryChoosenames = System.Enum.GetNames(typeof(BlendModeCategory));

    private MaterialProperty DstColorProps;
    private MaterialProperty DstTextureProps;
    private MaterialProperty SrcColorProps;
    private MaterialProperty SrcTextureProps;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        // base.OnGUI(materialEditor, properties);
        EditorGUILayout.BeginVertical(new GUIStyle("U2D.createRect"));
        EditorGUILayout.Space(10);
        ModeChooseProps = FindProperty("_IDChoose", properties);
        ModeID = FindProperty("_ModeID", properties);
        //  BlendCategory = FindProperty("_BlendMode", properties);
        CategoryChooseProps = FindProperty("_BlendCategoryChoose", properties);

        //选择混合范畴
        // BlendCategory.floatValue = EditorGUILayout.Popup(
        //"BlendCategory", (int)CategoryChooseProps.floatValue,
        // BlendCategoryChoosenames);

        //选择混合范畴
        CategoryChooseProps.floatValue = EditorGUILayout.Popup(
        "BlendCategory", (int)CategoryChooseProps.floatValue,
        BlendCategoryChoosenames);

        float choice = 0f;
        ///EditorGUILayout.Popup
        ///返回的是第几个选项而不是enum的值
        ///所以前面虽然enum显式赋值但是Popup不看enum里的值
        ///人家只看是哪个选项然后返回第几个选项
        ///所以choice这个变量就是偏移量
        ///在每个category之后要加相应的偏移量才能到下一个领域



        //选择范畴后选择具体混合模式
        ///if normal:
        switch (CategoryChooseProps.floatValue)
        {
            case (float)BlendModeCategory.Normal:
                ModeChooseProps.floatValue = EditorGUILayout.Popup(
                "NormalBlendMode", (int)ModeChooseProps.floatValue,
            NormalModeChoosenames);
                choice = ModeChooseProps.floatValue;
                break;
            case (float)BlendModeCategory.Darken:
                ModeChooseProps.floatValue = EditorGUILayout.Popup(
                "DarkenBlendMode", (int)ModeChooseProps.floatValue,
            DarkenModeChoosenames);
                choice = ModeChooseProps.floatValue;
                choice += 2;
                break;
            case (float)BlendModeCategory.Lighten:
                ModeChooseProps.floatValue = EditorGUILayout.Popup(
                "LightenBlendMode", (int)ModeChooseProps.floatValue,
            LightenModeChoosenames);
                choice = ModeChooseProps.floatValue;
                choice += 7;
                break;
            case (float)BlendModeCategory.Contrast:
                ModeChooseProps.floatValue = EditorGUILayout.Popup(
                "ContrastBlendMode", (int)ModeChooseProps.floatValue,
            ContrastModeChoosenames);
                choice = ModeChooseProps.floatValue;
                choice += 12;
                break;
            case (float)BlendModeCategory.Inversion:
                ModeChooseProps.floatValue = EditorGUILayout.Popup(
                "InversionBlendMode", (int)ModeChooseProps.floatValue,
            InversionModeChoosenames);
                choice = ModeChooseProps.floatValue;
                choice += 19;
                break;
            case (float)BlendModeCategory.Component:
                ModeChooseProps.floatValue = EditorGUILayout.Popup(
                "ComponentBlendMode", (int)ModeChooseProps.floatValue,
            ComponentModeChoosenames);
                choice = ModeChooseProps.floatValue;
                choice += 23;
                break;
        }

        //ModeChooseProps.floatValue = EditorGUILayout.Popup(
        //    "NormalBlendMode", (int) ModeChooseProps.floatValue,
        //    MateritalChoosenames);
        

        switch (choice)
        {
            case 0:
                ModeID.floatValue = 0;
                break;
            case 1:
                ModeID.floatValue = 1;
                break;
            case 2:
                ModeID.floatValue = 2;
                break;
            case 3:
                ModeID.floatValue = 3;
                break;
            case 4:
                ModeID.floatValue = 4;
                break;
            case 5:
                ModeID.floatValue = 5;
                break;
            case 6:
                ModeID.floatValue = 6;
                break;
            case 7:
                ModeID.floatValue = 7;
                break;
            case 8:
                ModeID.floatValue = 8;
                break;
            case 9:
                ModeID.floatValue = 9;
                break;
            case 10:
                ModeID.floatValue = 10;
                break;
            case 11:
                ModeID.floatValue = 11;
                break;
            case 12:
                ModeID.floatValue = 12;
                break;
            case 13:
                ModeID.floatValue = 13;
                break;
            case 14:
                ModeID.floatValue = 14;
                break;
            case 15:
                ModeID.floatValue = 15;
                break;
            case 16:
                ModeID.floatValue = 16;
                break;
            case 17:
                ModeID.floatValue = 17;
                break;
            case 18:
                ModeID.floatValue = 18;
                break;
            case 19:
                ModeID.floatValue = 19;
                break;

            case 20:
                ModeID.floatValue = 20;
                break;
            case 21:
                ModeID.floatValue = 21;
                break;
            case 22:
                ModeID.floatValue = 22;
                break;
            case 23:
                ModeID.floatValue = 23;
                break;
            case 24:
                ModeID.floatValue = 24;
                break;
            case 25:
                ModeID.floatValue = 25;
                break;
            case 26:
                ModeID.floatValue = 26;
                break;
        }

        //for debug visualization:
        ///debug start
        //string m = ModeID.floatValue.ToString();
        //int pp = (int)ComponentBlendMode.饱和度_Saturation;
        //string p = pp.ToString();
        //string p1 = BlendModeCategory.Component.ToString();
        //EditorGUILayout.LabelField(m);
        //EditorGUILayout.LabelField(p);
        //EditorGUILayout.LabelField(p1);
        ///debug end

        EditorGUILayout.Space(10);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(30);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.Space(10);

        SrcColorProps = FindProperty("_Color2", properties);
        materialEditor.ColorProperty(SrcColorProps, "BlendColor");
        SrcTextureProps = FindProperty("_MainTex2", properties);
        materialEditor.TextureProperty(SrcTextureProps, "BlendTexture");



        EditorGUILayout.Space(20);

        DstColorProps = FindProperty("_Color1", properties);
        materialEditor.ColorProperty(DstColorProps, "BaseColor");
        DstTextureProps = FindProperty("_MainTex1", properties);
        materialEditor.TextureProperty(DstTextureProps, "BaseTexture");

        EditorGUILayout.Space(10);
        EditorGUILayout.EndVertical();
    }
}