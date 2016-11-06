using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    internal class XmlSerializationReader1 : XmlSerializationReader
    {
        private string id30_AcCalcUnitGroupData;
        private string id44_Button;
        private string id61_Variable;
        private string id15_AcCalcToolbarButtonData;
        private string id43_ButtonSize;
        private string id25_AcCalcUnitItem;
        private string id2_Item;
        private string id56_DefaultItem;
        private string id31_AcCalcBtnGroupData;
        private string id28_AcCalcGroupData;
        private string id64_name;
        private string id17_Expression;
        private string id3_AllowGroupHide;
        private string id62_CLIDefined;
        private string id21_AcCalcVariablesData;
        private string id33_Action;
        private string id46_Height;
        private string id13_AcCalcToolbarData;
        private string id12_anyType;
        private string id55_UnitSubType;
        private string id6_UnitsGroup;
        private string id10_StringResource;
        private string id52_DefaultType;
        private string id4_ToolBar;
        private string id32_FlatStyle;
        private string id49_VIndex;
        private string id48_HIndex;
        private string id36_ArrayOfAnyType;
        private string id65_value;
        private string id38_GroupNumber;
        private string id63_Value;
        private string id35_varType;
        private string id34_ExpressionType;
        private string id11_GroupList;
        private string id39_IsCollapsed;
        private string id58_VarCategory;
        private string id26_cmnBtnData;
        private string id1_AcCalcControlData;
        private string id19_Type;
        private string id40_ButtonStyle;
        private string id57_MinHeight;
        private string id16_Image;
        private string id23_AcCalcVarItem;
        private string id60_Description;
        private string id37_GroupName;
        private string id8_ESWMinSize;
        private string id7_VariablesGroup;
        private string id54_IsDefault;
        private string id59_Path;
        private string id50_Color;
        private string id20_ResString;
        private string id24_AcCalcUnitType;
        private string id9_ESWDefaultSize;
        private string id22_AcCalcVarCat;
        private string id53_Name;
        private string id27_Size;
        private string id14_ToolBarButton;
        private string id51_UnitType;
        private string id41_MaxHRatio;
        private string id47_Label;
        private string id45_Width;
        private string id42_Hide;
        private string id18_ToolTip;
        private string id5_ButtonGroup;
        private string id29_AcCalcVariableGroupData;

        private cmnControlData Read1_AcCalcControlData(bool isNullable, bool checkType)
        {
            if (isNullable && this.ReadNull())
                return (cmnControlData)null;
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (!(xsiType == null) && (xsiType.Name != this.id1_AcCalcControlData || xsiType.Namespace != this.id2_Item))
                    throw this.CreateUnknownTypeException(xsiType);
            }
            cmnControlData acCalcControlData = new cmnControlData();
            CMNBtnGroupData[] calcBtnGroupDataArray = null;
            int num = 0;
            ArrayList groupList1 = acCalcControlData.GroupList;
            bool[] flagArray = new bool[9];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[1] && this.Reader.LocalName == this.id3_AllowGroupHide && this.Reader.NamespaceURI == this.id2_Item)
                {
                    acCalcControlData.mbAllowHide = XmlConvert.ToBoolean(this.Reader.Value);
                    flagArray[1] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(acCalcControlData);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                acCalcControlData.mBtnGroups = (CMNBtnGroupData[])this.ShrinkArray((Array)calcBtnGroupDataArray, num, typeof(CMNBtnGroupData), true);
                return acCalcControlData;
            }
            this.Reader.ReadStartElement();
            int content1 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!flagArray[0] && this.Reader.LocalName == this.id4_ToolBar && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        acCalcControlData.mToolbarData = this.Read2_AcCalcToolbarData(false, true);
                        flagArray[0] = true;
                    }
                        /* NO NEED TO READ BUTTON GROUP DATA

                    else if (this.Reader.LocalName == this.id5_ButtonGroup && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        calcBtnGroupDataArray = (cmnBtnGroupData[])this.EnsureArrayIndex((Array)calcBtnGroupDataArray, num, typeof(cmnBtnGroupData));
                        calcBtnGroupDataArray[num++] = this.Read5_AcCalcBtnGroupData(false, true);
                    }
                    //else if (!flagArray[3] && this.Reader.LocalName == this.id6_UnitsGroup && this.Reader.NamespaceURI == this.id2_Item)
                    //{
                    //    acCalcControlData.mUnitGroup = this.Read12_AcCalcUnitGroupData(false, true);
                    //    flagArray[3] = true;
                    //}
                    //else if (!flagArray[4] && this.Reader.LocalName == this.id7_VariablesGroup && this.Reader.NamespaceURI == this.id2_Item)
                    //{
                    //    acCalcControlData.mVariableGroup = this.Read15_AcCalcVariableGroupData(false, true);
                    //    flagArray[4] = true;
                    //}
                    else if (!flagArray[5] && this.Reader.LocalName == this.id8_ESWMinSize && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        acCalcControlData.mESWMinSize = this.Read8_Size(true);
                        flagArray[5] = true;
                    }
                    else if (!flagArray[6] && this.Reader.LocalName == this.id9_ESWDefaultSize && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        acCalcControlData.mESWDefaultSize = this.Read8_Size(true);
                        flagArray[6] = true;
                    }
                    //else if (!flagArray[7] && this.Reader.LocalName == this.id10_StringResource && this.Reader.NamespaceURI == this.id2_Item)
                    //{
                    //    acCalcControlData.mStringResource = this.Read20_StringResource(false, true);
                    //    flagArray[7] = true;
                    //}
                    else if (this.Reader.LocalName == this.id11_GroupList && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        if (!this.ReadNull())
                        {
                            ArrayList groupList2 = acCalcControlData.GroupList;
                            if (this.Reader.IsEmptyElement)
                            {
                                this.Reader.Skip();
                            }
                            else
                            {
                                this.Reader.ReadStartElement();
                                int content2 = (int)this.Reader.MoveToContent();
                                while (this.Reader.NodeType != XmlNodeType.EndElement)
                                {
                                    if (this.Reader.NodeType == XmlNodeType.Element)
                                    {
                                        if (this.Reader.LocalName == this.id12_anyType && this.Reader.NamespaceURI == this.id2_Item)
                                        {
                                            if (groupList2 == null)
                                                this.Reader.Skip();
                                            else
                                                groupList2.Add(this.Read4_Object(true, true));
                                        }
                                        else
                                            this.UnknownNode(null);
                                    }
                                    else
                                        this.UnknownNode(null);
                                    int content3 = (int)this.Reader.MoveToContent();
                                }
                                this.ReadEndElement();
                            }
                        }
                    }*/
                    else
                        this.UnknownNode(acCalcControlData);
                }
                else
                    this.UnknownNode(acCalcControlData);
                int content4 = (int)this.Reader.MoveToContent();
            }
            acCalcControlData.mBtnGroups = (CMNBtnGroupData[])this.ShrinkArray((Array)calcBtnGroupDataArray, num, typeof(CMNBtnGroupData), true);
            this.ReadEndElement();
            return acCalcControlData;
        }

        private cmnToolbarData Read2_AcCalcToolbarData(bool isNullable, bool checkType)
        {
            if (isNullable && this.ReadNull())
                return (cmnToolbarData)null;
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id13_AcCalcToolbarData || xsiType.Namespace != this.id2_Item))
                    throw this.CreateUnknownTypeException(xsiType);
            }
            cmnToolbarData acCalcToolbarData = new cmnToolbarData();
            cmnToolbarButtonData[] toolbarButtonDataArray = null;
            int num = 0;
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(acCalcToolbarData);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                acCalcToolbarData.mButtons = (cmnToolbarButtonData[])this.ShrinkArray((Array)toolbarButtonDataArray, num, typeof(cmnToolbarButtonData), true);
                return acCalcToolbarData;
            }
            this.Reader.ReadStartElement();
            int content1 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                {
                    if (this.Reader.LocalName == this.id14_ToolBarButton && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        toolbarButtonDataArray = (cmnToolbarButtonData[])this.EnsureArrayIndex((Array)toolbarButtonDataArray, num, typeof(cmnToolbarButtonData));
                        toolbarButtonDataArray[num++] = this.Read3_AcCalcToolbarButtonData(false, true);
                    }
                    else
                        this.UnknownNode(acCalcToolbarData);
                }
                else
                    this.UnknownNode(acCalcToolbarData);
                int content2 = (int)this.Reader.MoveToContent();
            }
            acCalcToolbarData.mButtons = (cmnToolbarButtonData[])this.ShrinkArray((Array)toolbarButtonDataArray, num, typeof(cmnToolbarButtonData), true);
            this.ReadEndElement();
            return acCalcToolbarData;
        }

        private cmnToolbarButtonData Read3_AcCalcToolbarButtonData(bool isNullable, bool checkType)
        {
            if (isNullable && this.ReadNull())
                return null;
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id15_AcCalcToolbarButtonData || xsiType.Namespace != this.id2_Item))
                    throw this.CreateUnknownTypeException(xsiType);
            }
            cmnToolbarButtonData toolbarButtonData = new cmnToolbarButtonData();
            bool[] flagArray = new bool[4];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id16_Image && this.Reader.NamespaceURI == this.id2_Item)
                {
                    toolbarButtonData.msImage = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!flagArray[1] && this.Reader.LocalName == this.id17_Expression && this.Reader.NamespaceURI == this.id2_Item)
                {
                    toolbarButtonData.msExpression = this.Reader.Value;
                    flagArray[1] = true;
                }
                else if (!flagArray[2] && this.Reader.LocalName == this.id18_ToolTip && this.Reader.NamespaceURI == this.id2_Item)
                {
                    toolbarButtonData.msToolTip = this.Reader.Value;
                    flagArray[2] = true;
                }
                else if (!flagArray[3] && this.Reader.LocalName == this.id19_Type && this.Reader.NamespaceURI == this.id2_Item)
                {
                    toolbarButtonData.msType = this.Reader.Value;
                    flagArray[3] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(toolbarButtonData);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return toolbarButtonData;
            }
            this.Reader.ReadStartElement();
            int content1 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                    this.UnknownNode(toolbarButtonData);
                else
                    this.UnknownNode(toolbarButtonData);
                int content2 = (int)this.Reader.MoveToContent();
            }
            this.ReadEndElement();
            return toolbarButtonData;
        }

        private object Read4_Object(bool isNullable, bool checkType)
        {
            if (isNullable && this.ReadNull())
                return null;
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (xsiType == (XmlQualifiedName)null)
                    return this.ReadTypedPrimitive(new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema"));
                if (xsiType.Name == this.id1_AcCalcControlData && xsiType.Namespace == this.id2_Item)
                    return this.Read1_AcCalcControlData(isNullable, false);
                //if (xsiType.Name == this.id10_StringResource && xsiType.Namespace == this.id2_Item)
                //    return this.Read20_StringResource(isNullable, false);
                //if (xsiType.Name == this.id20_ResString && xsiType.Namespace == this.id2_Item)
                //    return this.Read21_ResString(isNullable, false);
                //if (xsiType.Name == this.id21_AcCalcVariablesData && xsiType.Namespace == this.id2_Item)
                //    return this.Read16_AcCalcVariablesData(isNullable, false);
                //if (xsiType.Name == this.id22_AcCalcVarCat && xsiType.Namespace == this.id2_Item)
                //    return this.Read17_AcCalcVarCat(isNullable, false);
                //if (xsiType.Name == this.id23_AcCalcVarItem && xsiType.Namespace == this.id2_Item)
                //    return this.Read18_AcCalcVarItem(isNullable, false);
                //if (xsiType.Name == this.id24_AcCalcUnitType && xsiType.Namespace == this.id2_Item)
                //    return this.Read13_AcCalcUnitType(isNullable, false);
                //if (xsiType.Name == this.id25_AcCalcUnitItem && xsiType.Namespace == this.id2_Item)
                //    return this.Read14_AcCalcUnitItem(isNullable, false);
                if (xsiType.Name == this.id26_cmnBtnData && xsiType.Namespace == this.id2_Item)
                    return this.Read9_cmnBtnData(isNullable, false);
                if (xsiType.Name == this.id27_Size && xsiType.Namespace == this.id2_Item)
                    return this.Read8_Size(false);
                if (xsiType.Name == this.id28_AcCalcGroupData && xsiType.Namespace == this.id2_Item)
                    return this.Read6_AcCalcGroupData(isNullable, false);
                //if (xsiType.Name == this.id29_AcCalcVariableGroupData && xsiType.Namespace == this.id2_Item)
                //    return this.Read15_AcCalcVariableGroupData(isNullable, false);
                //if (xsiType.Name == this.id30_AcCalcUnitGroupData && xsiType.Namespace == this.id2_Item)
                //    return this.Read12_AcCalcUnitGroupData(isNullable, false);
                if (xsiType.Name == this.id31_AcCalcBtnGroupData && xsiType.Namespace == this.id2_Item)
                    return this.Read5_AcCalcBtnGroupData(isNullable, false);
                if (xsiType.Name == this.id13_AcCalcToolbarData && xsiType.Namespace == this.id2_Item)
                    return this.Read2_AcCalcToolbarData(isNullable, false);
                if (xsiType.Name == this.id15_AcCalcToolbarButtonData && xsiType.Namespace == this.id2_Item)
                    return this.Read3_AcCalcToolbarButtonData(isNullable, false);
                if (xsiType.Name == this.id32_FlatStyle && xsiType.Namespace == this.id2_Item)
                {
                    this.Reader.ReadStartElement();
                    object obj = this.Read7_FlatStyle(this.Reader.ReadString());
                    this.ReadEndElement();
                    return obj;
                }
                if (xsiType.Name == this.id33_Action && xsiType.Namespace == this.id2_Item)
                {
                    this.Reader.ReadStartElement();
                    object obj = this.Read10_Action(this.Reader.ReadString());
                    this.ReadEndElement();
                    return obj;
                }
                if (xsiType.Name == this.id34_ExpressionType && xsiType.Namespace == this.id2_Item)
                {
                    this.Reader.ReadStartElement();
                    object obj = this.Read11_ExpressionType(this.Reader.ReadString());
                    this.ReadEndElement();
                    return obj;
                }
                if (xsiType.Name == this.id35_varType && xsiType.Namespace == this.id2_Item)
                {
                    //this.Reader.ReadStartElement();
                    //object obj = this.Read19_varType(this.Reader.ReadString());
                    //this.ReadEndElement();
                    //return obj;
                }
                if (xsiType.Name != this.id36_ArrayOfAnyType || xsiType.Namespace != this.id2_Item)
                    return this.ReadTypedPrimitive(xsiType);
                ArrayList arrayList1 = (ArrayList)null;
                if (!this.ReadNull())
                {
                    if (arrayList1 == null)
                        arrayList1 = new ArrayList();
                    ArrayList arrayList2 = arrayList1;
                    if (this.Reader.IsEmptyElement)
                    {
                        this.Reader.Skip();
                    }
                    else
                    {
                        this.Reader.ReadStartElement();
                        int content1 = (int)this.Reader.MoveToContent();
                        while (this.Reader.NodeType != XmlNodeType.EndElement)
                        {
                            if (this.Reader.NodeType == XmlNodeType.Element)
                            {
                                if (this.Reader.LocalName == this.id12_anyType && this.Reader.NamespaceURI == this.id2_Item)
                                {
                                    if (arrayList2 == null)
                                        this.Reader.Skip();
                                    else
                                        arrayList2.Add(this.Read4_Object(true, true));
                                }
                                else
                                    this.UnknownNode(null);
                            }
                            else
                                this.UnknownNode(null);
                            int content2 = (int)this.Reader.MoveToContent();
                        }
                        this.ReadEndElement();
                    }
                }
                return arrayList1;
            }
            object o = new object();
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(o);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return o;
            }
            this.Reader.ReadStartElement();
            int content3 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                    this.UnknownNode(o);
                else
                    this.UnknownNode(o);
                int content1 = (int)this.Reader.MoveToContent();
            }
            this.ReadEndElement();
            return o;
        }

        private CMNBtnGroupData Read5_AcCalcBtnGroupData(bool isNullable, bool checkType)
        {
            if (isNullable && this.ReadNull())
                return null;
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (!(xsiType == null) && (xsiType.Name != this.id31_AcCalcBtnGroupData || xsiType.Namespace != this.id2_Item))
                    throw this.CreateUnknownTypeException(xsiType);
            }
            CMNBtnGroupData calcBtnGroupData = new CMNBtnGroupData();
            cmnBtnData[] cmnBtnDataArray = null;
            int num = 0;
            bool[] flagArray = new bool[8];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id37_GroupName && this.Reader.NamespaceURI == this.id2_Item)
                {
                    calcBtnGroupData.msGroupName = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!flagArray[1] && this.Reader.LocalName == this.id38_GroupNumber && this.Reader.NamespaceURI == this.id2_Item)
                {
                    calcBtnGroupData.miGroupNumber = XmlConvert.ToInt32(this.Reader.Value);
                    flagArray[1] = true;
                }
                else if (!flagArray[2] && this.Reader.LocalName == this.id39_IsCollapsed && this.Reader.NamespaceURI == this.id2_Item)
                {
                    calcBtnGroupData.mbCollapsed = XmlConvert.ToBoolean(this.Reader.Value);
                    flagArray[2] = true;
                }
                else if (!flagArray[4] && this.Reader.LocalName == this.id40_ButtonStyle && this.Reader.NamespaceURI == this.id2_Item)
                {
                    calcBtnGroupData.mBtnStyle = this.Read7_FlatStyle(this.Reader.Value);
                    flagArray[4] = true;
                }
                else if (!flagArray[6] && this.Reader.LocalName == this.id41_MaxHRatio && this.Reader.NamespaceURI == this.id2_Item)
                {
                    calcBtnGroupData.mBtnMaxHRatio = XmlConvert.ToDouble(this.Reader.Value);
                    flagArray[6] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(calcBtnGroupData);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                calcBtnGroupData.mButtons = (cmnBtnData[])this.ShrinkArray((Array)cmnBtnDataArray, num, typeof(cmnBtnData), true);
                return calcBtnGroupData;
            }
            this.Reader.ReadStartElement();
            int content1 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!flagArray[3] && this.Reader.LocalName == this.id42_Hide && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        calcBtnGroupData.Hide = XmlConvert.ToBoolean(this.Reader.ReadElementString());
                        flagArray[3] = true;
                    }
                    else if (!flagArray[5] && this.Reader.LocalName == this.id43_ButtonSize && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        calcBtnGroupData.mBtnSize = this.Read8_Size(true);
                        flagArray[5] = true;
                    }
                    else if (this.Reader.LocalName == this.id44_Button && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        cmnBtnDataArray = (cmnBtnData[])this.EnsureArrayIndex((Array)cmnBtnDataArray, num, typeof(cmnBtnData));
                        cmnBtnDataArray[num++] = this.Read9_cmnBtnData(false, true);
                    }
                    else
                        this.UnknownNode(calcBtnGroupData);
                }
                else
                    this.UnknownNode(calcBtnGroupData);
                int content2 = (int)this.Reader.MoveToContent();
            }
            calcBtnGroupData.mButtons = (cmnBtnData[])this.ShrinkArray((Array)cmnBtnDataArray, num, typeof(cmnBtnData), true);
            this.ReadEndElement();
            return calcBtnGroupData;
        }

        private cmnGroupData Read6_AcCalcGroupData(bool isNullable, bool checkType)
        {
            if (isNullable && this.ReadNull())
                return null;
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id28_AcCalcGroupData || xsiType.Namespace != this.id2_Item))
                {
                    //if (xsiType.Name == this.id29_AcCalcVariableGroupData && xsiType.Namespace == this.id2_Item)
                    //    return (cmnGroupData)this.Read15_AcCalcVariableGroupData(isNullable, false);
                    //if (xsiType.Name == this.id30_AcCalcUnitGroupData && xsiType.Namespace == this.id2_Item)
                    //    return (cmnGroupData)this.Read12_AcCalcUnitGroupData(isNullable, false);
                    if (xsiType.Name == this.id31_AcCalcBtnGroupData && xsiType.Namespace == this.id2_Item)
                        return (cmnGroupData)this.Read5_AcCalcBtnGroupData(isNullable, false);
                    throw this.CreateUnknownTypeException(xsiType);
                }
            }
            cmnGroupData acCalcGroupData = new cmnGroupData();
            bool[] flagArray = new bool[4];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id37_GroupName && this.Reader.NamespaceURI == this.id2_Item)
                {
                    acCalcGroupData.msGroupName = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!flagArray[1] && this.Reader.LocalName == this.id38_GroupNumber && this.Reader.NamespaceURI == this.id2_Item)
                {
                    acCalcGroupData.miGroupNumber = XmlConvert.ToInt32(this.Reader.Value);
                    flagArray[1] = true;
                }
                else if (!flagArray[2] && this.Reader.LocalName == this.id39_IsCollapsed && this.Reader.NamespaceURI == this.id2_Item)
                {
                    acCalcGroupData.mbCollapsed = XmlConvert.ToBoolean(this.Reader.Value);
                    flagArray[2] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(acCalcGroupData);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return acCalcGroupData;
            }
            this.Reader.ReadStartElement();
            int content1 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!flagArray[3] && this.Reader.LocalName == this.id42_Hide && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        acCalcGroupData.Hide = XmlConvert.ToBoolean(this.Reader.ReadElementString());
                        flagArray[3] = true;
                    }
                    else
                        this.UnknownNode(acCalcGroupData);
                }
                else
                    this.UnknownNode(acCalcGroupData);
                int content2 = (int)this.Reader.MoveToContent();
            }
            this.ReadEndElement();
            return acCalcGroupData;
        }

        private FlatStyle Read7_FlatStyle(string s)
        {
            switch (s)
            {
                case "Flat":
                    return FlatStyle.Flat;
                case "Popup":
                    return FlatStyle.Popup;
                case "Standard":
                    return FlatStyle.Standard;
                case "System":
                    return FlatStyle.System;
                default:
                    throw this.CreateUnknownConstantException(s, typeof(FlatStyle));
            }
        }

        private Size Read8_Size(bool checkType)
        {
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id27_Size || xsiType.Namespace != this.id2_Item))
                    throw this.CreateUnknownTypeException(xsiType);
            }
            Size instance = (Size)Activator.CreateInstance(typeof(Size));
            bool[] flagArray = new bool[2];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(instance);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return instance;
            }
            this.Reader.ReadStartElement();
            int content1 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                {
                    if (!flagArray[0] && this.Reader.LocalName == this.id45_Width && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        instance.Width = XmlConvert.ToInt32(this.Reader.ReadElementString());
                        flagArray[0] = true;
                    }
                    else if (!flagArray[1] && this.Reader.LocalName == this.id46_Height && this.Reader.NamespaceURI == this.id2_Item)
                    {
                        instance.Height = XmlConvert.ToInt32(this.Reader.ReadElementString());
                        flagArray[1] = true;
                    }
                    else
                        this.UnknownNode(instance);
                }
                else
                    this.UnknownNode(instance);
                int content2 = (int)this.Reader.MoveToContent();
            }
            this.ReadEndElement();
            return instance;
        }

        private cmnBtnData Read9_cmnBtnData(bool isNullable, bool checkType)
        {
            if (isNullable && this.ReadNull())
                return null;
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (!(xsiType == null) && (xsiType.Name != this.id26_cmnBtnData || xsiType.Namespace != this.id2_Item))
                    throw this.CreateUnknownTypeException(xsiType);
            }
            cmnBtnData cmnBtnData = new cmnBtnData();
            bool[] flagArray = new bool[8];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id47_Label && this.Reader.NamespaceURI == this.id2_Item)
                {
                    cmnBtnData.msLabel = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!flagArray[1] && this.Reader.LocalName == this.id33_Action && this.Reader.NamespaceURI == this.id2_Item)
                {
                    cmnBtnData.msAction = this.Read10_Action(this.Reader.Value);
                    flagArray[1] = true;
                }
                else if (!flagArray[2] && this.Reader.LocalName == this.id17_Expression && this.Reader.NamespaceURI == this.id2_Item)
                {
                    cmnBtnData.msExpression = this.Reader.Value;
                    flagArray[2] = true;
                }
                else if (!flagArray[3] && this.Reader.LocalName == this.id34_ExpressionType && this.Reader.NamespaceURI == this.id2_Item)
                {
                    cmnBtnData.msExpressionType = this.Read11_ExpressionType(this.Reader.Value);
                    flagArray[3] = true;
                }
                else if (!flagArray[4] && this.Reader.LocalName == this.id48_HIndex && this.Reader.NamespaceURI == this.id2_Item)
                {
                    cmnBtnData.mHIndex = XmlConvert.ToInt32(this.Reader.Value);
                    flagArray[4] = true;
                }
                else if (!flagArray[5] && this.Reader.LocalName == this.id49_VIndex && this.Reader.NamespaceURI == this.id2_Item)
                {
                    cmnBtnData.mVIndex = XmlConvert.ToInt32(this.Reader.Value);
                    flagArray[5] = true;
                }
                else if (!flagArray[6] && this.Reader.LocalName == this.id50_Color && this.Reader.NamespaceURI == this.id2_Item)
                {
                    cmnBtnData.msColor = this.Reader.Value;
                    flagArray[6] = true;
                }
                else if (!flagArray[7] && this.Reader.LocalName == this.id18_ToolTip && this.Reader.NamespaceURI == this.id2_Item)
                {
                    cmnBtnData.msToolTip = this.Reader.Value;
                    flagArray[7] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(cmnBtnData);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return cmnBtnData;
            }
            this.Reader.ReadStartElement();
            int content1 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                    this.UnknownNode(cmnBtnData);
                else
                    this.UnknownNode(cmnBtnData);
                int content2 = (int)this.Reader.MoveToContent();
            }
            this.ReadEndElement();
            return cmnBtnData;
        }

        private cmnBtnData.Action Read10_Action(string s)
        {
            switch (s)
            {
                case "append":
                    return cmnBtnData.Action.append;
                case "clear":
                    return cmnBtnData.Action.clear;
                case "clear_history":
                    return cmnBtnData.Action.clear_history;
                case "backspace":
                    return cmnBtnData.Action.backspace;
                case "evaluate":
                    return cmnBtnData.Action.evaluate;
                case "mem_store":
                    return cmnBtnData.Action.mem_store;
                case "mem_plus":
                    return cmnBtnData.Action.mem_plus;
                case "mem_minus":
                    return cmnBtnData.Action.mem_minus;
                case "mem_recall":
                    return cmnBtnData.Action.mem_recall;
                case "mem_clear":
                    return cmnBtnData.Action.mem_clear;
                default:
                    throw this.CreateUnknownConstantException(s, typeof(cmnBtnData.Action));
            }
        }

        private ExpressionType Read11_ExpressionType(string s)
        {
            switch (s)
            {
                case "UIExpression":
                    return ExpressionType.UIExpression;
                case "Operand":
                    return ExpressionType.Operand;
                case "Operator_postfix":
                    return ExpressionType.Operator_postfix;
                case "Operator_prefix":
                    return ExpressionType.Operator_prefix;
                case "Unit":
                    return ExpressionType.Unit;
                case "Result":
                    return ExpressionType.Result;
                default:
                    throw this.CreateUnknownConstantException(s, typeof(ExpressionType));
            }
        }

        //private AcCalcUnitGroupData Read12_AcCalcUnitGroupData(bool isNullable, bool checkType)
        //{
        //    if (isNullable && this.ReadNull())
        //        return (AcCalcUnitGroupData)null;
        //    if (checkType)
        //    {
        //        XmlQualifiedName xsiType = this.GetXsiType();
        //        if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id30_AcCalcUnitGroupData || xsiType.Namespace != this.id2_Item))
        //            throw this.CreateUnknownTypeException(xsiType);
        //    }
        //    AcCalcUnitGroupData calcUnitGroupData = new AcCalcUnitGroupData();
        //    AcCalcUnitType[] acCalcUnitTypeArray = (AcCalcUnitType[])null;
        //    int num = 0;
        //    bool[] flagArray = new bool[6];
        //    while (this.Reader.MoveToNextAttribute())
        //    {
        //        if (!flagArray[0] && this.Reader.LocalName == this.id37_GroupName && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            calcUnitGroupData.msGroupName = this.Reader.Value;
        //            flagArray[0] = true;
        //        }
        //        else if (!flagArray[1] && this.Reader.LocalName == this.id38_GroupNumber && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            calcUnitGroupData.miGroupNumber = XmlConvert.ToInt32(this.Reader.Value);
        //            flagArray[1] = true;
        //        }
        //        else if (!flagArray[2] && this.Reader.LocalName == this.id39_IsCollapsed && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            calcUnitGroupData.mbCollapsed = XmlConvert.ToBoolean(this.Reader.Value);
        //            flagArray[2] = true;
        //        }
        //        else if (!this.IsXmlnsAttribute(this.Reader.Name))
        //            this.UnknownNode(calcUnitGroupData);
        //    }
        //    this.Reader.MoveToElement();
        //    if (this.Reader.IsEmptyElement)
        //    {
        //        this.Reader.Skip();
        //        calcUnitGroupData.mUnitTypes = (AcCalcUnitType[])this.ShrinkArray((Array)acCalcUnitTypeArray, num, typeof(AcCalcUnitType), true);
        //        return calcUnitGroupData;
        //    }
        //    this.Reader.ReadStartElement();
        //    int content1 = (int)this.Reader.MoveToContent();
        //    while (this.Reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        if (this.Reader.NodeType == XmlNodeType.Element)
        //        {
        //            if (!flagArray[3] && this.Reader.LocalName == this.id42_Hide && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                calcUnitGroupData.Hide = XmlConvert.ToBoolean(this.Reader.ReadElementString());
        //                flagArray[3] = true;
        //            }
        //            else if (this.Reader.LocalName == this.id51_UnitType && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                acCalcUnitTypeArray = (AcCalcUnitType[])this.EnsureArrayIndex((Array)acCalcUnitTypeArray, num, typeof(AcCalcUnitType));
        //                acCalcUnitTypeArray[num++] = this.Read13_AcCalcUnitType(false, true);
        //            }
        //            else if (!flagArray[5] && this.Reader.LocalName == this.id52_DefaultType && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                calcUnitGroupData.DefaultType = this.Read13_AcCalcUnitType(false, true);
        //                flagArray[5] = true;
        //            }
        //            else
        //                this.UnknownNode(calcUnitGroupData);
        //        }
        //        else
        //            this.UnknownNode(calcUnitGroupData);
        //        int content2 = (int)this.Reader.MoveToContent();
        //    }
        //    calcUnitGroupData.mUnitTypes = (AcCalcUnitType[])this.ShrinkArray((Array)acCalcUnitTypeArray, num, typeof(AcCalcUnitType), true);
        //    this.ReadEndElement();
        //    return calcUnitGroupData;
        //}

        //private AcCalcUnitType Read13_AcCalcUnitType(bool isNullable, bool checkType)
        //{
        //    if (isNullable && this.ReadNull())
        //        return (AcCalcUnitType)null;
        //    if (checkType)
        //    {
        //        XmlQualifiedName xsiType = this.GetXsiType();
        //        if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id24_AcCalcUnitType || xsiType.Namespace != this.id2_Item))
        //            throw this.CreateUnknownTypeException(xsiType);
        //    }
        //    AcCalcUnitType acCalcUnitType = new AcCalcUnitType();
        //    AcCalcUnitItem[] acCalcUnitItemArray = (AcCalcUnitItem[])null;
        //    int num = 0;
        //    bool[] flagArray = new bool[5];
        //    while (this.Reader.MoveToNextAttribute())
        //    {
        //        if (!flagArray[0] && this.Reader.LocalName == this.id53_Name && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcUnitType.msName = this.Reader.Value;
        //            flagArray[0] = true;
        //        }
        //        else if (!flagArray[1] && this.Reader.LocalName == this.id47_Label && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcUnitType.msLabel = this.Reader.Value;
        //            flagArray[1] = true;
        //        }
        //        else if (!flagArray[2] && this.Reader.LocalName == this.id54_IsDefault && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcUnitType.mIsDefault = XmlConvert.ToBoolean(this.Reader.Value);
        //            flagArray[2] = true;
        //        }
        //        else if (!this.IsXmlnsAttribute(this.Reader.Name))
        //            this.UnknownNode(acCalcUnitType);
        //    }
        //    this.Reader.MoveToElement();
        //    if (this.Reader.IsEmptyElement)
        //    {
        //        this.Reader.Skip();
        //        acCalcUnitType.mUnitItems = (AcCalcUnitItem[])this.ShrinkArray((Array)acCalcUnitItemArray, num, typeof(AcCalcUnitItem), true);
        //        return acCalcUnitType;
        //    }
        //    this.Reader.ReadStartElement();
        //    int content1 = (int)this.Reader.MoveToContent();
        //    while (this.Reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        if (this.Reader.NodeType == XmlNodeType.Element)
        //        {
        //            if (this.Reader.LocalName == this.id55_UnitSubType && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                acCalcUnitItemArray = (AcCalcUnitItem[])this.EnsureArrayIndex((Array)acCalcUnitItemArray, num, typeof(AcCalcUnitItem));
        //                acCalcUnitItemArray[num++] = this.Read14_AcCalcUnitItem(false, true);
        //            }
        //            else if (!flagArray[4] && this.Reader.LocalName == this.id56_DefaultItem && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                acCalcUnitType.DefaultItem = this.Read14_AcCalcUnitItem(false, true);
        //                flagArray[4] = true;
        //            }
        //            else
        //                this.UnknownNode(acCalcUnitType);
        //        }
        //        else
        //            this.UnknownNode(acCalcUnitType);
        //        int content2 = (int)this.Reader.MoveToContent();
        //    }
        //    acCalcUnitType.mUnitItems = (AcCalcUnitItem[])this.ShrinkArray((Array)acCalcUnitItemArray, num, typeof(AcCalcUnitItem), true);
        //    this.ReadEndElement();
        //    return acCalcUnitType;
        //}

        //private AcCalcUnitItem Read14_AcCalcUnitItem(bool isNullable, bool checkType)
        //{
        //    if (isNullable && this.ReadNull())
        //        return (AcCalcUnitItem)null;
        //    if (checkType)
        //    {
        //        XmlQualifiedName xsiType = this.GetXsiType();
        //        if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id25_AcCalcUnitItem || xsiType.Namespace != this.id2_Item))
        //            throw this.CreateUnknownTypeException(xsiType);
        //    }
        //    AcCalcUnitItem acCalcUnitItem = new AcCalcUnitItem();
        //    bool[] flagArray = new bool[3];
        //    while (this.Reader.MoveToNextAttribute())
        //    {
        //        if (!flagArray[0] && this.Reader.LocalName == this.id53_Name && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcUnitItem.msName = this.Reader.Value;
        //            flagArray[0] = true;
        //        }
        //        else if (!flagArray[1] && this.Reader.LocalName == this.id47_Label && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcUnitItem.msLabel = this.Reader.Value;
        //            flagArray[1] = true;
        //        }
        //        else if (!flagArray[2] && this.Reader.LocalName == this.id54_IsDefault && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcUnitItem.mIsDefault = XmlConvert.ToBoolean(this.Reader.Value);
        //            flagArray[2] = true;
        //        }
        //        else if (!this.IsXmlnsAttribute(this.Reader.Name))
        //            this.UnknownNode(acCalcUnitItem);
        //    }
        //    this.Reader.MoveToElement();
        //    if (this.Reader.IsEmptyElement)
        //    {
        //        this.Reader.Skip();
        //        return acCalcUnitItem;
        //    }
        //    this.Reader.ReadStartElement();
        //    int content1 = (int)this.Reader.MoveToContent();
        //    while (this.Reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        if (this.Reader.NodeType == XmlNodeType.Element)
        //            this.UnknownNode(acCalcUnitItem);
        //        else
        //            this.UnknownNode(acCalcUnitItem);
        //        int content2 = (int)this.Reader.MoveToContent();
        //    }
        //    this.ReadEndElement();
        //    return acCalcUnitItem;
        //}

        //private AcCalcVariableGroupData Read15_AcCalcVariableGroupData(bool isNullable, bool checkType)
        //{
        //    if (isNullable && this.ReadNull())
        //        return (AcCalcVariableGroupData)null;
        //    if (checkType)
        //    {
        //        XmlQualifiedName xsiType = this.GetXsiType();
        //        if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id29_AcCalcVariableGroupData || xsiType.Namespace != this.id2_Item))
        //            throw this.CreateUnknownTypeException(xsiType);
        //    }
        //    AcCalcVariableGroupData variableGroupData = new AcCalcVariableGroupData();
        //    bool[] flagArray = new bool[6];
        //    while (this.Reader.MoveToNextAttribute())
        //    {
        //        if (!flagArray[0] && this.Reader.LocalName == this.id37_GroupName && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            variableGroupData.msGroupName = this.Reader.Value;
        //            flagArray[0] = true;
        //        }
        //        else if (!flagArray[1] && this.Reader.LocalName == this.id38_GroupNumber && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            variableGroupData.miGroupNumber = XmlConvert.ToInt32(this.Reader.Value);
        //            flagArray[1] = true;
        //        }
        //        else if (!flagArray[2] && this.Reader.LocalName == this.id39_IsCollapsed && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            variableGroupData.mbCollapsed = XmlConvert.ToBoolean(this.Reader.Value);
        //            flagArray[2] = true;
        //        }
        //        else if (!flagArray[4] && this.Reader.LocalName == this.id57_MinHeight && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            variableGroupData.mMinHeight = XmlConvert.ToInt32(this.Reader.Value);
        //            flagArray[4] = true;
        //        }
        //        else if (!this.IsXmlnsAttribute(this.Reader.Name))
        //            this.UnknownNode(variableGroupData);
        //    }
        //    this.Reader.MoveToElement();
        //    if (this.Reader.IsEmptyElement)
        //    {
        //        this.Reader.Skip();
        //        return variableGroupData;
        //    }
        //    this.Reader.ReadStartElement();
        //    int content1 = (int)this.Reader.MoveToContent();
        //    while (this.Reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        if (this.Reader.NodeType == XmlNodeType.Element)
        //        {
        //            if (!flagArray[3] && this.Reader.LocalName == this.id42_Hide && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                variableGroupData.Hide = XmlConvert.ToBoolean(this.Reader.ReadElementString());
        //                flagArray[3] = true;
        //            }
        //            else if (!flagArray[5] && this.Reader.LocalName == this.id21_AcCalcVariablesData && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                variableGroupData.mVarData = this.Read16_AcCalcVariablesData(false, true);
        //                flagArray[5] = true;
        //            }
        //            else
        //                this.UnknownNode(variableGroupData);
        //        }
        //        else
        //            this.UnknownNode(variableGroupData);
        //        int content2 = (int)this.Reader.MoveToContent();
        //    }
        //    this.ReadEndElement();
        //    return variableGroupData;
        //}

        //private AcCalcVariablesData Read16_AcCalcVariablesData(bool isNullable, bool checkType)
        //{
        //    if (isNullable && this.ReadNull())
        //        return (AcCalcVariablesData)null;
        //    if (checkType)
        //    {
        //        XmlQualifiedName xsiType = this.GetXsiType();
        //        if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id21_AcCalcVariablesData || xsiType.Namespace != this.id2_Item))
        //            throw this.CreateUnknownTypeException(xsiType);
        //    }
        //    AcCalcVariablesData calcVariablesData = new AcCalcVariablesData();
        //    if (calcVariablesData.mVarCategories == null)
        //        calcVariablesData.mVarCategories = new ArrayList();
        //    ArrayList mVarCategories = calcVariablesData.mVarCategories;
        //    bool[] flagArray = new bool[2];
        //    while (this.Reader.MoveToNextAttribute())
        //    {
        //        if (!this.IsXmlnsAttribute(this.Reader.Name))
        //            this.UnknownNode(calcVariablesData);
        //    }
        //    this.Reader.MoveToElement();
        //    if (this.Reader.IsEmptyElement)
        //    {
        //        this.Reader.Skip();
        //        return calcVariablesData;
        //    }
        //    this.Reader.ReadStartElement();
        //    int content1 = (int)this.Reader.MoveToContent();
        //    while (this.Reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        if (this.Reader.NodeType == XmlNodeType.Element)
        //        {
        //            if (this.Reader.LocalName == this.id58_VarCategory && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                if (mVarCategories == null)
        //                    this.Reader.Skip();
        //                else
        //                    mVarCategories.Add(this.Read17_AcCalcVarCat(false, true));
        //            }
        //            else if (!flagArray[1] && this.Reader.LocalName == this.id59_Path && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                calcVariablesData.msPath = this.Reader.ReadElementString();
        //                flagArray[1] = true;
        //            }
        //            else
        //                this.UnknownNode(calcVariablesData);
        //        }
        //        else
        //            this.UnknownNode(calcVariablesData);
        //        int content2 = (int)this.Reader.MoveToContent();
        //    }
        //    this.ReadEndElement();
        //    return calcVariablesData;
        //}

        //private AcCalcVarCat Read17_AcCalcVarCat(bool isNullable, bool checkType)
        //{
        //    if (isNullable && this.ReadNull())
        //        return (AcCalcVarCat)null;
        //    if (checkType)
        //    {
        //        XmlQualifiedName xsiType = this.GetXsiType();
        //        if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id22_AcCalcVarCat || xsiType.Namespace != this.id2_Item))
        //            throw this.CreateUnknownTypeException(xsiType);
        //    }
        //    AcCalcVarCat acCalcVarCat = new AcCalcVarCat();
        //    if (acCalcVarCat.mVarItems == null)
        //        acCalcVarCat.mVarItems = new ArrayList();
        //    ArrayList mVarItems = acCalcVarCat.mVarItems;
        //    bool[] flagArray = new bool[4];
        //    while (this.Reader.MoveToNextAttribute())
        //    {
        //        if (!flagArray[0] && this.Reader.LocalName == this.id53_Name && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcVarCat.mName = this.Reader.Value;
        //            flagArray[0] = true;
        //        }
        //        else if (!flagArray[1] && this.Reader.LocalName == this.id60_Description && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcVarCat.msDescription = this.Reader.Value;
        //            flagArray[1] = true;
        //        }
        //        else if (!this.IsXmlnsAttribute(this.Reader.Name))
        //            this.UnknownNode(acCalcVarCat);
        //    }
        //    this.Reader.MoveToElement();
        //    if (this.Reader.IsEmptyElement)
        //    {
        //        this.Reader.Skip();
        //        return acCalcVarCat;
        //    }
        //    this.Reader.ReadStartElement();
        //    int content1 = (int)this.Reader.MoveToContent();
        //    while (this.Reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        if (this.Reader.NodeType == XmlNodeType.Element)
        //        {
        //            if (this.Reader.LocalName == this.id61_Variable && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                if (mVarItems == null)
        //                    this.Reader.Skip();
        //                else
        //                    mVarItems.Add(this.Read18_AcCalcVarItem(false, true));
        //            }
        //            else if (!flagArray[3] && this.Reader.LocalName == this.id62_CLIDefined && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                acCalcVarCat.CLIDefined = XmlConvert.ToBoolean(this.Reader.ReadElementString());
        //                flagArray[3] = true;
        //            }
        //            else
        //                this.UnknownNode(acCalcVarCat);
        //        }
        //        else
        //            this.UnknownNode(acCalcVarCat);
        //        int content2 = (int)this.Reader.MoveToContent();
        //    }
        //    this.ReadEndElement();
        //    return acCalcVarCat;
        //}

        //private AcCalcVarItem Read18_AcCalcVarItem(bool isNullable, bool checkType)
        //{
        //    if (isNullable && this.ReadNull())
        //        return (AcCalcVarItem)null;
        //    if (checkType)
        //    {
        //        XmlQualifiedName xsiType = this.GetXsiType();
        //        if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id23_AcCalcVarItem || xsiType.Namespace != this.id2_Item))
        //            throw this.CreateUnknownTypeException(xsiType);
        //    }
        //    AcCalcVarItem acCalcVarItem = new AcCalcVarItem();
        //    bool[] flagArray = new bool[4];
        //    while (this.Reader.MoveToNextAttribute())
        //    {
        //        if (!flagArray[0] && this.Reader.LocalName == this.id53_Name && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcVarItem.mName = this.Reader.Value;
        //            flagArray[0] = true;
        //        }
        //        else if (!flagArray[1] && this.Reader.LocalName == this.id63_Value && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcVarItem.msValue = this.Reader.Value;
        //            flagArray[1] = true;
        //        }
        //        else if (!flagArray[2] && this.Reader.LocalName == this.id19_Type && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcVarItem.msType = this.Read19_varType(this.Reader.Value);
        //            flagArray[2] = true;
        //        }
        //        else if (!flagArray[3] && this.Reader.LocalName == this.id60_Description && this.Reader.NamespaceURI == this.id2_Item)
        //        {
        //            acCalcVarItem.msDescription = this.Reader.Value;
        //            flagArray[3] = true;
        //        }
        //        else if (!this.IsXmlnsAttribute(this.Reader.Name))
        //            this.UnknownNode(acCalcVarItem);
        //    }
        //    this.Reader.MoveToElement();
        //    if (this.Reader.IsEmptyElement)
        //    {
        //        this.Reader.Skip();
        //        return acCalcVarItem;
        //    }
        //    this.Reader.ReadStartElement();
        //    int content1 = (int)this.Reader.MoveToContent();
        //    while (this.Reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        if (this.Reader.NodeType == XmlNodeType.Element)
        //            this.UnknownNode(acCalcVarItem);
        //        else
        //            this.UnknownNode(acCalcVarItem);
        //        int content2 = (int)this.Reader.MoveToContent();
        //    }
        //    this.ReadEndElement();
        //    return acCalcVarItem;
        //}

        //private AcCalcVarItem.varType Read19_varType(string s)
        //{
        //    switch (s)
        //    {
        //        case "int_constant":
        //            return AcCalcVarItem.varType.int_constant;
        //        case "double_constant":
        //            return AcCalcVarItem.varType.double_constant;
        //        case "vector_constant":
        //            return AcCalcVarItem.varType.vector_constant;
        //        case "function":
        //            return AcCalcVarItem.varType.function;
        //        default:
        //            throw this.CreateUnknownConstantException(s, typeof(AcCalcVarItem.varType));
        //    }
        //}

        //private StringResource Read20_StringResource(bool isNullable, bool checkType)
        //{
        //    if (isNullable && this.ReadNull())
        //        return (StringResource)null;
        //    if (checkType)
        //    {
        //        XmlQualifiedName xsiType = this.GetXsiType();
        //        if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id10_StringResource || xsiType.Namespace != this.id2_Item))
        //            throw this.CreateUnknownTypeException(xsiType);
        //    }
        //    StringResource stringResource = new StringResource();
        //    ResString[] resStringArray = (ResString[])null;
        //    int num = 0;
        //    while (this.Reader.MoveToNextAttribute())
        //    {
        //        if (!this.IsXmlnsAttribute(this.Reader.Name))
        //            this.UnknownNode(stringResource);
        //    }
        //    this.Reader.MoveToElement();
        //    if (this.Reader.IsEmptyElement)
        //    {
        //        this.Reader.Skip();
        //        stringResource.mStrings = (ResString[])this.ShrinkArray((Array)resStringArray, num, typeof(ResString), true);
        //        return stringResource;
        //    }
        //    this.Reader.ReadStartElement();
        //    int content1 = (int)this.Reader.MoveToContent();
        //    while (this.Reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        if (this.Reader.NodeType == XmlNodeType.Element)
        //        {
        //            if (this.Reader.LocalName == this.id20_ResString && this.Reader.NamespaceURI == this.id2_Item)
        //            {
        //                resStringArray = (ResString[])this.EnsureArrayIndex((Array)resStringArray, num, typeof(ResString));
        //                resStringArray[num++] = this.Read21_ResString(false, true);
        //            }
        //            else
        //                this.UnknownNode(stringResource);
        //        }
        //        else
        //            this.UnknownNode(stringResource);
        //        int content2 = (int)this.Reader.MoveToContent();
        //    }
        //    stringResource.mStrings = (ResString[])this.ShrinkArray((Array)resStringArray, num, typeof(ResString), true);
        //    this.ReadEndElement();
        //    return stringResource;
        //}

        private ResString Read21_ResString(bool isNullable, bool checkType)
        {
            if (isNullable && this.ReadNull())
                return (ResString)null;
            if (checkType)
            {
                XmlQualifiedName xsiType = this.GetXsiType();
                if (!(xsiType == (XmlQualifiedName)null) && (xsiType.Name != this.id20_ResString || xsiType.Namespace != this.id2_Item))
                    throw this.CreateUnknownTypeException(xsiType);
            }
            ResString resString = new ResString();
            bool[] flagArray = new bool[2];
            while (this.Reader.MoveToNextAttribute())
            {
                if (!flagArray[0] && this.Reader.LocalName == this.id64_name && this.Reader.NamespaceURI == this.id2_Item)
                {
                    resString.mName = this.Reader.Value;
                    flagArray[0] = true;
                }
                else if (!flagArray[1] && this.Reader.LocalName == this.id65_value && this.Reader.NamespaceURI == this.id2_Item)
                {
                    resString.mValue = this.Reader.Value;
                    flagArray[1] = true;
                }
                else if (!this.IsXmlnsAttribute(this.Reader.Name))
                    this.UnknownNode(resString);
            }
            this.Reader.MoveToElement();
            if (this.Reader.IsEmptyElement)
            {
                this.Reader.Skip();
                return resString;
            }
            this.Reader.ReadStartElement();
            int content1 = (int)this.Reader.MoveToContent();
            while (this.Reader.NodeType != XmlNodeType.EndElement)
            {
                if (this.Reader.NodeType == XmlNodeType.Element)
                    this.UnknownNode(resString);
                else
                    this.UnknownNode(resString);
                int content2 = (int)this.Reader.MoveToContent();
            }
            this.ReadEndElement();
            return resString;
        }

        protected override void InitCallbacks()
        {
        }

        public object Read23_AcCalcControlData()
        {
            object obj = null;
            int content = (int)this.Reader.MoveToContent();
            if (this.Reader.NodeType == XmlNodeType.Element)
            {
                if (this.Reader.LocalName != this.id1_AcCalcControlData || this.Reader.NamespaceURI != this.id2_Item)
                    throw this.CreateUnknownNodeException();
                obj = this.Read1_AcCalcControlData(true, true);
            }
            else
                this.UnknownNode(null);
            return obj;
        }

        protected override void InitIDs()
        {
            this.id30_AcCalcUnitGroupData = this.Reader.NameTable.Add("AcCalcUnitGroupData");
            this.id44_Button = this.Reader.NameTable.Add("Button");
            this.id61_Variable = this.Reader.NameTable.Add("Variable");
            this.id15_AcCalcToolbarButtonData = this.Reader.NameTable.Add("AcCalcToolbarButtonData");
            this.id43_ButtonSize = this.Reader.NameTable.Add("ButtonSize");
            this.id25_AcCalcUnitItem = this.Reader.NameTable.Add("AcCalcUnitItem");
            this.id2_Item = this.Reader.NameTable.Add("");
            this.id56_DefaultItem = this.Reader.NameTable.Add("DefaultItem");
            this.id31_AcCalcBtnGroupData = this.Reader.NameTable.Add("AcCalcBtnGroupData");
            this.id28_AcCalcGroupData = this.Reader.NameTable.Add("AcCalcGroupData");
            this.id64_name = this.Reader.NameTable.Add("name");
            this.id17_Expression = this.Reader.NameTable.Add("Expression");
            this.id3_AllowGroupHide = this.Reader.NameTable.Add("AllowGroupHide");
            this.id62_CLIDefined = this.Reader.NameTable.Add("CLIDefined");
            this.id21_AcCalcVariablesData = this.Reader.NameTable.Add("AcCalcVariablesData");
            this.id33_Action = this.Reader.NameTable.Add("Action");
            this.id46_Height = this.Reader.NameTable.Add("Height");
            this.id13_AcCalcToolbarData = this.Reader.NameTable.Add("AcCalcToolbarData");
            this.id12_anyType = this.Reader.NameTable.Add("anyType");
            this.id55_UnitSubType = this.Reader.NameTable.Add("UnitSubType");
            this.id6_UnitsGroup = this.Reader.NameTable.Add("UnitsGroup");
            this.id10_StringResource = this.Reader.NameTable.Add("StringResource");
            this.id52_DefaultType = this.Reader.NameTable.Add("DefaultType");
            this.id4_ToolBar = this.Reader.NameTable.Add("ToolBar");
            this.id32_FlatStyle = this.Reader.NameTable.Add("FlatStyle");
            this.id49_VIndex = this.Reader.NameTable.Add("VIndex");
            this.id48_HIndex = this.Reader.NameTable.Add("HIndex");
            this.id36_ArrayOfAnyType = this.Reader.NameTable.Add("ArrayOfAnyType");
            this.id65_value = this.Reader.NameTable.Add("value");
            this.id38_GroupNumber = this.Reader.NameTable.Add("GroupNumber");
            this.id63_Value = this.Reader.NameTable.Add("Value");
            this.id35_varType = this.Reader.NameTable.Add("varType");
            this.id34_ExpressionType = this.Reader.NameTable.Add("ExpressionType");
            this.id11_GroupList = this.Reader.NameTable.Add("GroupList");
            this.id39_IsCollapsed = this.Reader.NameTable.Add("IsCollapsed");
            this.id58_VarCategory = this.Reader.NameTable.Add("VarCategory");
            this.id26_cmnBtnData = this.Reader.NameTable.Add("cmnBtnData");
            this.id1_AcCalcControlData = this.Reader.NameTable.Add("AcCalcControlData");
            this.id19_Type = this.Reader.NameTable.Add("Type");
            this.id40_ButtonStyle = this.Reader.NameTable.Add("ButtonStyle");
            this.id57_MinHeight = this.Reader.NameTable.Add("MinHeight");
            this.id16_Image = this.Reader.NameTable.Add("Image");
            this.id23_AcCalcVarItem = this.Reader.NameTable.Add("AcCalcVarItem");
            this.id60_Description = this.Reader.NameTable.Add("Description");
            this.id37_GroupName = this.Reader.NameTable.Add("GroupName");
            this.id8_ESWMinSize = this.Reader.NameTable.Add("ESWMinSize");
            this.id7_VariablesGroup = this.Reader.NameTable.Add("VariablesGroup");
            this.id54_IsDefault = this.Reader.NameTable.Add("IsDefault");
            this.id59_Path = this.Reader.NameTable.Add("Path");
            this.id50_Color = this.Reader.NameTable.Add("Color");
            this.id20_ResString = this.Reader.NameTable.Add("ResString");
            this.id24_AcCalcUnitType = this.Reader.NameTable.Add("AcCalcUnitType");
            this.id9_ESWDefaultSize = this.Reader.NameTable.Add("ESWDefaultSize");
            this.id22_AcCalcVarCat = this.Reader.NameTable.Add("AcCalcVarCat");
            this.id53_Name = this.Reader.NameTable.Add("Name");
            this.id27_Size = this.Reader.NameTable.Add("Size");
            this.id14_ToolBarButton = this.Reader.NameTable.Add("ToolBarButton");
            this.id51_UnitType = this.Reader.NameTable.Add("UnitType");
            this.id41_MaxHRatio = this.Reader.NameTable.Add("MaxHRatio");
            this.id47_Label = this.Reader.NameTable.Add("Label");
            this.id45_Width = this.Reader.NameTable.Add("Width");
            this.id42_Hide = this.Reader.NameTable.Add("Hide");
            this.id18_ToolTip = this.Reader.NameTable.Add("ToolTip");
            this.id5_ButtonGroup = this.Reader.NameTable.Add("ButtonGroup");
            this.id29_AcCalcVariableGroupData = this.Reader.NameTable.Add("AcCalcVariableGroupData");
        }
    }
}
