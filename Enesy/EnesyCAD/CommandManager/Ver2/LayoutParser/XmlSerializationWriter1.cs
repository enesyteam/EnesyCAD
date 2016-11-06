using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    internal class XmlSerializationWriter1 : XmlSerializationWriter
    {
        public XmlSerializationWriter1()
        {
        }

        protected override void InitCallbacks()
        {
        }

        private void Write1_AcCalcControlData(string n, string ns, cmnControlData o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType && o.GetType() != typeof(cmnControlData))
            {
                throw base.CreateUnknownTypeException(o);
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("AcCalcControlData", "");
            }
            base.WriteAttribute("AllowGroupHide", "", XmlConvert.ToString(o.mbAllowHide));
            this.Write2_AcCalcToolbarData("ToolBar", "", o.mToolbarData, false, false);
            CMNBtnGroupData[] acCalcBtnGroupDataArray = o.mBtnGroups;
            if (acCalcBtnGroupDataArray != null)
            {
                for (int i = 0; i < (int)acCalcBtnGroupDataArray.Length; i++)
                {
                    this.Write5_AcCalcBtnGroupData("ButtonGroup", "", acCalcBtnGroupDataArray[i], false, false);
                }
            }
            //this.Write12_AcCalcUnitGroupData("UnitsGroup", "", o.mUnitGroup, false, false);
            //this.Write15_AcCalcVariableGroupData("VariablesGroup", "", o.mVariableGroup, false, false);
            this.Write8_Size("ESWMinSize", "", o.mESWMinSize, false);
            this.Write8_Size("ESWDefaultSize", "", o.mESWDefaultSize, false);
            this.Write20_StringResource("StringResource", "", o.mStringResource, false, false);
            ArrayList groupList = o.GroupList;
            if (groupList != null)
            {
                base.WriteStartElement("GroupList", "");
                for (int j = 0; j < groupList.Count; j++)
                {
                    this.Write4_Object("anyType", "", groupList[j], true, false);
                }
                base.WriteEndElement();
            }
            base.WriteEndElement(o);
        }

        private string Write10_Action(cmnBtnData.Action v)
        {
            string str = null;
            switch (v)
            {
                case cmnBtnData.Action.append:
                    {
                        str = "append";
                        break;
                    }
                case cmnBtnData.Action.clear:
                    {
                        str = "clear";
                        break;
                    }
                case cmnBtnData.Action.clear_history:
                    {
                        str = "clear_history";
                        break;
                    }
                case cmnBtnData.Action.backspace:
                    {
                        str = "backspace";
                        break;
                    }
                case cmnBtnData.Action.evaluate:
                    {
                        str = "evaluate";
                        break;
                    }
                case cmnBtnData.Action.mem_store:
                    {
                        str = "mem_store";
                        break;
                    }
                case cmnBtnData.Action.mem_plus:
                    {
                        str = "mem_plus";
                        break;
                    }
                case cmnBtnData.Action.mem_minus:
                    {
                        str = "mem_minus";
                        break;
                    }
                case cmnBtnData.Action.mem_recall:
                    {
                        str = "mem_recall";
                        break;
                    }
                case cmnBtnData.Action.mem_clear:
                    {
                        str = "mem_clear";
                        break;
                    }
                default:
                    {
                        str = ((long)v).ToString();
                        break;
                    }
            }
            return str;
        }

        private string Write11_ExpressionType(ExpressionType v)
        {
            string str = null;
            switch (v)
            {
                case ExpressionType.UIExpression:
                    {
                        str = "UIExpression";
                        break;
                    }
                case ExpressionType.Operand:
                    {
                        str = "Operand";
                        break;
                    }
                case ExpressionType.Operator_postfix:
                    {
                        str = "Operator_postfix";
                        break;
                    }
                case ExpressionType.Operator_prefix:
                    {
                        str = "Operator_prefix";
                        break;
                    }
                case ExpressionType.Unit:
                    {
                        str = "Unit";
                        break;
                    }
                case ExpressionType.Result:
                    {
                        str = "Result";
                        break;
                    }
                default:
                    {
                        str = ((long)v).ToString();
                        break;
                    }
            }
            return str;
        }

        //private void Write12_AcCalcUnitGroupData(string n, string ns, AcCalcUnitGroupData o, bool isNullable, bool needType)
        //{
        //    if (o == null)
        //    {
        //        if (isNullable)
        //        {
        //            base.WriteNullTagLiteral(n, ns);
        //        }
        //        return;
        //    }
        //    if (!needType && o.GetType() != typeof(AcCalcUnitGroupData))
        //    {
        //        throw base.CreateUnknownTypeException(o);
        //    }
        //    base.WriteStartElement(n, ns, o);
        //    if (needType)
        //    {
        //        base.WriteXsiType("AcCalcUnitGroupData", "");
        //    }
        //    base.WriteAttribute("GroupName", "", o.msGroupName);
        //    base.WriteAttribute("GroupNumber", "", XmlConvert.ToString(o.miGroupNumber));
        //    base.WriteAttribute("IsCollapsed", "", XmlConvert.ToString(o.mbCollapsed));
        //    base.WriteElementStringRaw("Hide", "", XmlConvert.ToString(o.Hide));
        //    AcCalcUnitType[] acCalcUnitTypeArray = o.mUnitTypes;
        //    if (acCalcUnitTypeArray != null)
        //    {
        //        for (int i = 0; i < (int)acCalcUnitTypeArray.Length; i++)
        //        {
        //            this.Write13_AcCalcUnitType("UnitType", "", acCalcUnitTypeArray[i], false, false);
        //        }
        //    }
        //    this.Write13_AcCalcUnitType("DefaultType", "", o.DefaultType, false, false);
        //    base.WriteEndElement(o);
        //}

        //private void Write13_AcCalcUnitType(string n, string ns, AcCalcUnitType o, bool isNullable, bool needType)
        //{
        //    if (o == null)
        //    {
        //        if (isNullable)
        //        {
        //            base.WriteNullTagLiteral(n, ns);
        //        }
        //        return;
        //    }
        //    if (!needType && o.GetType() != typeof(AcCalcUnitType))
        //    {
        //        throw base.CreateUnknownTypeException(o);
        //    }
        //    base.WriteStartElement(n, ns, o);
        //    if (needType)
        //    {
        //        base.WriteXsiType("AcCalcUnitType", "");
        //    }
        //    base.WriteAttribute("Name", "", o.msName);
        //    base.WriteAttribute("Label", "", o.msLabel);
        //    base.WriteAttribute("IsDefault", "", XmlConvert.ToString(o.mIsDefault));
        //    AcCalcUnitItem[] acCalcUnitItemArray = o.mUnitItems;
        //    if (acCalcUnitItemArray != null)
        //    {
        //        for (int i = 0; i < (int)acCalcUnitItemArray.Length; i++)
        //        {
        //            this.Write14_AcCalcUnitItem("UnitSubType", "", acCalcUnitItemArray[i], false, false);
        //        }
        //    }
        //    this.Write14_AcCalcUnitItem("DefaultItem", "", o.DefaultItem, false, false);
        //    base.WriteEndElement(o);
        //}

        //private void Write14_AcCalcUnitItem(string n, string ns, AcCalcUnitItem o, bool isNullable, bool needType)
        //{
        //    if (o == null)
        //    {
        //        if (isNullable)
        //        {
        //            base.WriteNullTagLiteral(n, ns);
        //        }
        //        return;
        //    }
        //    if (!needType && o.GetType() != typeof(AcCalcUnitItem))
        //    {
        //        throw base.CreateUnknownTypeException(o);
        //    }
        //    base.WriteStartElement(n, ns, o);
        //    if (needType)
        //    {
        //        base.WriteXsiType("AcCalcUnitItem", "");
        //    }
        //    base.WriteAttribute("Name", "", o.msName);
        //    base.WriteAttribute("Label", "", o.msLabel);
        //    base.WriteAttribute("IsDefault", "", XmlConvert.ToString(o.mIsDefault));
        //    base.WriteEndElement(o);
        //}

        //private void Write15_AcCalcVariableGroupData(string n, string ns, AcCalcVariableGroupData o, bool isNullable, bool needType)
        //{
        //    if (o == null)
        //    {
        //        if (isNullable)
        //        {
        //            base.WriteNullTagLiteral(n, ns);
        //        }
        //        return;
        //    }
        //    if (!needType && o.GetType() != typeof(AcCalcVariableGroupData))
        //    {
        //        throw base.CreateUnknownTypeException(o);
        //    }
        //    base.WriteStartElement(n, ns, o);
        //    if (needType)
        //    {
        //        base.WriteXsiType("AcCalcVariableGroupData", "");
        //    }
        //    base.WriteAttribute("GroupName", "", o.msGroupName);
        //    base.WriteAttribute("GroupNumber", "", XmlConvert.ToString(o.miGroupNumber));
        //    base.WriteAttribute("IsCollapsed", "", XmlConvert.ToString(o.mbCollapsed));
        //    base.WriteAttribute("MinHeight", "", XmlConvert.ToString(o.mMinHeight));
        //    base.WriteElementStringRaw("Hide", "", XmlConvert.ToString(o.Hide));
        //    this.Write16_AcCalcVariablesData("AcCalcVariablesData", "", o.mVarData, false, false);
        //    base.WriteEndElement(o);
        //}

        //private void Write16_AcCalcVariablesData(string n, string ns, AcCalcVariablesData o, bool isNullable, bool needType)
        //{
        //    if (o == null)
        //    {
        //        if (isNullable)
        //        {
        //            base.WriteNullTagLiteral(n, ns);
        //        }
        //        return;
        //    }
        //    if (!needType && o.GetType() != typeof(AcCalcVariablesData))
        //    {
        //        throw base.CreateUnknownTypeException(o);
        //    }
        //    base.WriteStartElement(n, ns, o);
        //    if (needType)
        //    {
        //        base.WriteXsiType("AcCalcVariablesData", "");
        //    }
        //    ArrayList arrayLists = o.mVarCategories;
        //    if (arrayLists != null)
        //    {
        //        for (int i = 0; i < arrayLists.Count; i++)
        //        {
        //            this.Write17_AcCalcVarCat("VarCategory", "", (AcCalcVarCat)arrayLists[i], false, false);
        //        }
        //    }
        //    base.WriteElementString("Path", "", o.msPath);
        //    base.WriteEndElement(o);
        //}

        //private void Write17_AcCalcVarCat(string n, string ns, AcCalcVarCat o, bool isNullable, bool needType)
        //{
        //    if (o == null)
        //    {
        //        if (isNullable)
        //        {
        //            base.WriteNullTagLiteral(n, ns);
        //        }
        //        return;
        //    }
        //    if (!needType && o.GetType() != typeof(AcCalcVarCat))
        //    {
        //        throw base.CreateUnknownTypeException(o);
        //    }
        //    base.WriteStartElement(n, ns, o);
        //    if (needType)
        //    {
        //        base.WriteXsiType("AcCalcVarCat", "");
        //    }
        //    base.WriteAttribute("Name", "", o.mName);
        //    base.WriteAttribute("Description", "", o.msDescription);
        //    ArrayList arrayLists = o.mVarItems;
        //    if (arrayLists != null)
        //    {
        //        for (int i = 0; i < arrayLists.Count; i++)
        //        {
        //            this.Write18_AcCalcVarItem("Variable", "", (AcCalcVarItem)arrayLists[i], false, false);
        //        }
        //    }
        //    base.WriteElementStringRaw("CLIDefined", "", XmlConvert.ToString(o.CLIDefined));
        //    base.WriteEndElement(o);
        //}

        //private void Write18_AcCalcVarItem(string n, string ns, AcCalcVarItem o, bool isNullable, bool needType)
        //{
        //    if (o == null)
        //    {
        //        if (isNullable)
        //        {
        //            base.WriteNullTagLiteral(n, ns);
        //        }
        //        return;
        //    }
        //    if (!needType && o.GetType() != typeof(AcCalcVarItem))
        //    {
        //        throw base.CreateUnknownTypeException(o);
        //    }
        //    base.WriteStartElement(n, ns, o);
        //    if (needType)
        //    {
        //        base.WriteXsiType("AcCalcVarItem", "");
        //    }
        //    base.WriteAttribute("Name", "", o.mName);
        //    base.WriteAttribute("Value", "", o.msValue);
        //    base.WriteAttribute("Type", "", this.Write19_varType(o.msType));
        //    base.WriteAttribute("Description", "", o.msDescription);
        //    base.WriteEndElement(o);
        //}

        //private string Write19_varType(AcCalcVarItem.varType v)
        //{
        //    string str = null;
        //    switch (v)
        //    {
        //        case AcCalcVarItem.varType.int_constant:
        //            {
        //                str = "int_constant";
        //                break;
        //            }
        //        case AcCalcVarItem.varType.double_constant:
        //            {
        //                str = "double_constant";
        //                break;
        //            }
        //        case AcCalcVarItem.varType.vector_constant:
        //            {
        //                str = "vector_constant";
        //                break;
        //            }
        //        case AcCalcVarItem.varType.function:
        //            {
        //                str = "function";
        //                break;
        //            }
        //        default:
        //            {
        //                str = ((long)v).ToString();
        //                break;
        //            }
        //    }
        //    return str;
        //}

        private void Write2_AcCalcToolbarData(string n, string ns, cmnToolbarData o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType && o.GetType() != typeof(cmnToolbarData))
            {
                throw base.CreateUnknownTypeException(o);
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("AcCalcToolbarData", "");
            }
            cmnToolbarButtonData[] acCalcToolbarButtonDataArray = o.mButtons;
            if (acCalcToolbarButtonDataArray != null)
            {
                for (int i = 0; i < (int)acCalcToolbarButtonDataArray.Length; i++)
                {
                    this.Write3_AcCalcToolbarButtonData("ToolBarButton", "", acCalcToolbarButtonDataArray[i], false, false);
                }
            }
            base.WriteEndElement(o);
        }

        private void Write20_StringResource(string n, string ns, StringResource o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType && o.GetType() != typeof(StringResource))
            {
                throw base.CreateUnknownTypeException(o);
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("StringResource", "");
            }
            ResString[] resStringArray = o.mStrings;
            if (resStringArray != null)
            {
                for (int i = 0; i < (int)resStringArray.Length; i++)
                {
                    this.Write21_ResString("ResString", "", resStringArray[i], false, false);
                }
            }
            base.WriteEndElement(o);
        }

        private void Write21_ResString(string n, string ns, ResString o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType && o.GetType() != typeof(ResString))
            {
                throw base.CreateUnknownTypeException(o);
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("ResString", "");
            }
            base.WriteAttribute("name", "", o.mName);
            base.WriteAttribute("value", "", o.mValue);
            base.WriteEndElement(o);
        }

        public void Write22_AcCalcControlData(object o)
        {
            base.WriteStartDocument();
            if (o == null)
            {
                base.WriteNullTagLiteral("AcCalcControlData", "");
                return;
            }
            base.TopLevelElement();
            this.Write1_AcCalcControlData("AcCalcControlData", "", (cmnControlData)o, true, false);
        }

        private void Write3_AcCalcToolbarButtonData(string n, string ns, cmnToolbarButtonData o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType && o.GetType() != typeof(cmnToolbarButtonData))
            {
                throw base.CreateUnknownTypeException(o);
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("AcCalcToolbarButtonData", "");
            }
            base.WriteAttribute("Image", "", o.msImage);
            base.WriteAttribute("Expression", "", o.msExpression);
            base.WriteAttribute("ToolTip", "", o.msToolTip);
            base.WriteAttribute("Type", "", o.msType);
            base.WriteEndElement(o);
        }

        private void Write4_Object(string n, string ns, object o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType)
            {
                Type type = o.GetType();
                if (type != typeof(object))
                {
                    if (type == typeof(cmnControlData))
                    {
                        this.Write1_AcCalcControlData(n, ns, (cmnControlData)o, isNullable, true);
                        return;
                    }
                    if (type == typeof(StringResource))
                    {
                        this.Write20_StringResource(n, ns, (StringResource)o, isNullable, true);
                        return;
                    }
                    if (type == typeof(ResString))
                    {
                        this.Write21_ResString(n, ns, (ResString)o, isNullable, true);
                        return;
                    }
                    //if (type == typeof(AcCalcVariablesData))
                    //{
                    //    this.Write16_AcCalcVariablesData(n, ns, (AcCalcVariablesData)o, isNullable, true);
                    //    return;
                    //}
                    //if (type == typeof(AcCalcVarCat))
                    //{
                    //    this.Write17_AcCalcVarCat(n, ns, (AcCalcVarCat)o, isNullable, true);
                    //    return;
                    //}
                    //if (type == typeof(AcCalcVarItem))
                    //{
                    //    this.Write18_AcCalcVarItem(n, ns, (AcCalcVarItem)o, isNullable, true);
                    //    return;
                    //}
                    //if (type == typeof(AcCalcUnitType))
                    //{
                    //    this.Write13_AcCalcUnitType(n, ns, (AcCalcUnitType)o, isNullable, true);
                    //    return;
                    //}
                    //if (type == typeof(AcCalcUnitItem))
                    //{
                    //    this.Write14_AcCalcUnitItem(n, ns, (AcCalcUnitItem)o, isNullable, true);
                    //    return;
                    //}
                    //if (type == typeof(AcCalcBtnData))
                    //{
                    //    this.Write9_AcCalcBtnData(n, ns, (AcCalcBtnData)o, isNullable, true);
                    //    return;
                    //}
                    if (type == typeof(Size))
                    {
                        this.Write8_Size(n, ns, (Size)o, true);
                        return;
                    }
                    if (type == typeof(cmnGroupData))
                    {
                        this.Write6_AcCalcGroupData(n, ns, (cmnGroupData)o, isNullable, true);
                        return;
                    }
                    //if (type == typeof(AcCalcVariableGroupData))
                    //{
                    //    this.Write15_AcCalcVariableGroupData(n, ns, (AcCalcVariableGroupData)o, isNullable, true);
                    //    return;
                    //}
                    //if (type == typeof(AcCalcUnitGroupData))
                    //{
                    //    this.Write12_AcCalcUnitGroupData(n, ns, (AcCalcUnitGroupData)o, isNullable, true);
                    //    return;
                    //}
                    if (type == typeof(CMNBtnGroupData))
                    {
                        this.Write5_AcCalcBtnGroupData(n, ns, (CMNBtnGroupData)o, isNullable, true);
                        return;
                    }
                    if (type == typeof(cmnToolbarData))
                    {
                        this.Write2_AcCalcToolbarData(n, ns, (cmnToolbarData)o, isNullable, true);
                        return;
                    }
                    if (type == typeof(cmnToolbarButtonData))
                    {
                        this.Write3_AcCalcToolbarButtonData(n, ns, (cmnToolbarButtonData)o, isNullable, true);
                        return;
                    }
                    if (type == typeof(FlatStyle))
                    {
                        base.Writer.WriteStartElement(n, ns);
                        base.WriteXsiType("FlatStyle", "");
                        base.Writer.WriteString(this.Write7_FlatStyle((FlatStyle)o));
                        base.Writer.WriteEndElement();
                        return;
                    }
                    if (type == typeof(cmnBtnData.Action))
                    {
                        base.Writer.WriteStartElement(n, ns);
                        base.WriteXsiType("Action", "");
                        base.Writer.WriteString(this.Write10_Action((cmnBtnData.Action)o));
                        base.Writer.WriteEndElement();
                        return;
                    }
                    if (type == typeof(ExpressionType))
                    {
                        base.Writer.WriteStartElement(n, ns);
                        base.WriteXsiType("ExpressionType", "");
                        base.Writer.WriteString(this.Write11_ExpressionType((ExpressionType)o));
                        base.Writer.WriteEndElement();
                        return;
                    }
                    //if (type == typeof(AcCalcVarItem.varType))
                    //{
                    //    base.Writer.WriteStartElement(n, ns);
                    //    base.WriteXsiType("varType", "");
                    //    base.Writer.WriteString(this.Write19_varType((AcCalcVarItem.varType)o));
                    //    base.Writer.WriteEndElement();
                    //    return;
                    //}
                    if (type != typeof(ArrayList))
                    {
                        base.WriteTypedPrimitive(n, ns, o, true);
                        return;
                    }
                    base.Writer.WriteStartElement(n, ns);
                    base.WriteXsiType("ArrayOfAnyType", "");
                    ArrayList arrayLists = (ArrayList)o;
                    if (arrayLists != null)
                    {
                        for (int i = 0; i < arrayLists.Count; i++)
                        {
                            this.Write4_Object("anyType", "", arrayLists[i], true, false);
                        }
                    }
                    base.Writer.WriteEndElement();
                    return;
                }
            }
            base.WriteStartElement(n, ns, o);
            base.WriteEndElement(o);
        }

        private void Write5_AcCalcBtnGroupData(string n, string ns, CMNBtnGroupData o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType && o.GetType() != typeof(CMNBtnGroupData))
            {
                throw base.CreateUnknownTypeException(o);
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("AcCalcBtnGroupData", "");
            }
            base.WriteAttribute("GroupName", "", o.msGroupName);
            base.WriteAttribute("GroupNumber", "", XmlConvert.ToString(o.miGroupNumber));
            base.WriteAttribute("IsCollapsed", "", XmlConvert.ToString(o.mbCollapsed));
            base.WriteAttribute("ButtonStyle", "", this.Write7_FlatStyle(o.mBtnStyle));
            base.WriteAttribute("MaxHRatio", "", XmlConvert.ToString((double)((double)o.mBtnMaxHRatio)));
            base.WriteElementStringRaw("Hide", "", XmlConvert.ToString(o.Hide));
            this.Write8_Size("ButtonSize", "", o.mBtnSize, false);
            cmnBtnData[] acCalcBtnDataArray = o.mButtons;
            if (acCalcBtnDataArray != null)
            {
                for (int i = 0; i < (int)acCalcBtnDataArray.Length; i++)
                {
                    this.Write9_AcCalcBtnData("Button", "", acCalcBtnDataArray[i], false, false);
                }
            }
            base.WriteEndElement(o);
        }

        private void Write6_AcCalcGroupData(string n, string ns, cmnGroupData o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType)
            {
                Type type = o.GetType();
                if (type != typeof(cmnGroupData))
                {
                    //if (type == typeof(AcCalcVariableGroupData))
                    //{
                    //    this.Write15_AcCalcVariableGroupData(n, ns, (AcCalcVariableGroupData)o, isNullable, true);
                    //    return;
                    //}
                    //if (type == typeof(AcCalcUnitGroupData))
                    //{
                    //    this.Write12_AcCalcUnitGroupData(n, ns, (AcCalcUnitGroupData)o, isNullable, true);
                    //    return;
                    //}
                    if (type != typeof(CMNBtnGroupData))
                    {
                        throw base.CreateUnknownTypeException(o);
                    }
                    this.Write5_AcCalcBtnGroupData(n, ns, (CMNBtnGroupData)o, isNullable, true);
                    return;
                }
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("AcCalcGroupData", "");
            }
            base.WriteAttribute("GroupName", "", o.msGroupName);
            base.WriteAttribute("GroupNumber", "", XmlConvert.ToString(o.miGroupNumber));
            base.WriteAttribute("IsCollapsed", "", XmlConvert.ToString(o.mbCollapsed));
            base.WriteElementStringRaw("Hide", "", XmlConvert.ToString(o.Hide));
            base.WriteEndElement(o);
        }

        private string Write7_FlatStyle(FlatStyle v)
        {
            string str = null;
            switch (v)
            {
                case FlatStyle.Flat:
                    {
                        str = "Flat";
                        break;
                    }
                case FlatStyle.Popup:
                    {
                        str = "Popup";
                        break;
                    }
                case FlatStyle.Standard:
                    {
                        str = "Standard";
                        break;
                    }
                case FlatStyle.System:
                    {
                        str = "System";
                        break;
                    }
                default:
                    {
                        str = ((long)v).ToString();
                        break;
                    }
            }
            return str;
        }

        private void Write8_Size(string n, string ns, Size o, bool needType)
        {
            if (!needType && o.GetType() != typeof(Size))
            {
                throw base.CreateUnknownTypeException(o);
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("Size", "");
            }
            base.WriteElementStringRaw("Width", "", XmlConvert.ToString(o.Width));
            base.WriteElementStringRaw("Height", "", XmlConvert.ToString(o.Height));
            base.WriteEndElement(o);
        }

        private void Write9_AcCalcBtnData(string n, string ns, cmnBtnData o, bool isNullable, bool needType)
        {
            if (o == null)
            {
                if (isNullable)
                {
                    base.WriteNullTagLiteral(n, ns);
                }
                return;
            }
            if (!needType && o.GetType() != typeof(cmnBtnData))
            {
                throw base.CreateUnknownTypeException(o);
            }
            base.WriteStartElement(n, ns, o);
            if (needType)
            {
                base.WriteXsiType("AcCalcBtnData", "");
            }
            base.WriteAttribute("Label", "", o.msLabel);
            base.WriteAttribute("Action", "", this.Write10_Action(o.msAction));
            base.WriteAttribute("Expression", "", o.msExpression);
            base.WriteAttribute("ExpressionType", "", this.Write11_ExpressionType(o.msExpressionType));
            base.WriteAttribute("HIndex", "", XmlConvert.ToString(o.mHIndex));
            base.WriteAttribute("VIndex", "", XmlConvert.ToString(o.mVIndex));
            base.WriteAttribute("Color", "", o.msColor);
            base.WriteAttribute("ToolTip", "", o.msToolTip);
            base.WriteEndElement(o);
        }
    }
}