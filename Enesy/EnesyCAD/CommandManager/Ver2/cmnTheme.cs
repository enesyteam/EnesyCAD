using System.Drawing;

namespace Enesy.EnesyCAD.CommandManager.Ver2
{
    public class cmnTheme
    {
        private static int TWENTYFIVE_PERCENT_GRAY = 576;
        private static int SIXTYFIVE_PERCENT_GRAY = 267;
        private Color crAdjusted3dObject;

        public Color BaseColor
        {
            get
            {
                Color control = SystemColors.Control;
                if ((int)control.R + (int)control.G + (int)control.B > cmnTheme.TWENTYFIVE_PERCENT_GRAY)
                    cmnTheme.AdjustColorToBrightness(ref control, cmnTheme.TWENTYFIVE_PERCENT_GRAY);
                else if ((int)control.R + (int)control.G + (int)control.B < cmnTheme.SIXTYFIVE_PERCENT_GRAY)
                    cmnTheme.AdjustColorToBrightness(ref control, cmnTheme.SIXTYFIVE_PERCENT_GRAY);
                return control;
            }
        }

        public Color ESWBackground
        {
            get
            {
                return cmnTheme.ShadeOf(this.crAdjusted3dObject, -30);
            }
        }

        public Color GroupHeaderBack
        {
            get
            {
                return this.crAdjusted3dObject;
            }
        }

        public Color GroupHeaderText
        {
            get
            {
                return SystemColors.ActiveCaptionText;
            }
        }

        public cmnTheme()
        {
            this.Update();
        }

        private static int MulDiv(int nNumber, int nNumerator, int nDenominator)
        {
            if (nDenominator == 0)
                return -1;
            return (int)((double)(nNumber * nNumerator) / (double)nDenominator);
        }

        private static void AdjustColorToBrightness(ref Color color, int nBrightness)
        {
            if (nBrightness < 0 || nBrightness > 765)
                return;
            int r = (int)color.R;
            int g = (int)color.G;
            int b = (int)color.B;
            int num1 = (r + g + b - nBrightness) / 3;
            if (num1 == 0)
                return;
            int red = r - num1;
            int green = g - num1;
            int blue = b - num1;
            if (num1 < 0 && (red > (int)byte.MaxValue || green > (int)byte.MaxValue || blue > (int)byte.MaxValue))
            {
                while (red > (int)byte.MaxValue || green > (int)byte.MaxValue || blue > (int)byte.MaxValue)
                {
                    bool flag1 = false;
                    bool flag2 = false;
                    bool flag3 = false;
                    int num2 = 0;
                    if (red >= (int)byte.MaxValue)
                    {
                        num2 += red - (int)byte.MaxValue;
                        red = (int)byte.MaxValue;
                        flag1 = true;
                    }
                    if (green >= (int)byte.MaxValue)
                    {
                        num2 += green - (int)byte.MaxValue;
                        green = (int)byte.MaxValue;
                        flag2 = true;
                    }
                    if (blue >= (int)byte.MaxValue)
                    {
                        num2 += blue - (int)byte.MaxValue;
                        blue = (int)byte.MaxValue;
                        flag3 = true;
                    }
                    if (flag1 && flag2)
                        blue += num2;
                    else if (flag1 && flag3)
                        green += num2;
                    else if (flag2 && flag3)
                        red += num2;
                    else if (flag1)
                    {
                        green += num2 / 2;
                        blue += num2 / 2;
                    }
                    else if (flag2)
                    {
                        red += num2 / 2;
                        blue += num2 / 2;
                    }
                    else if (flag3)
                    {
                        red += num2 / 2;
                        green += num2 / 2;
                    }
                }
            }
            else if (num1 > 0 && (red < 0 || green < 0 || blue < 0))
            {
                while (red < 0 || green < 0 || blue < 0)
                {
                    bool flag1 = false;
                    bool flag2 = false;
                    bool flag3 = false;
                    int num2 = 0;
                    if (red <= 0)
                    {
                        num2 += -red;
                        red = 0;
                        flag1 = true;
                    }
                    if (green <= 0)
                    {
                        num2 += -green;
                        green = 0;
                        flag2 = true;
                    }
                    if (blue <= 0)
                    {
                        num2 += -blue;
                        blue = 0;
                        flag3 = true;
                    }
                    if (flag1 && flag2)
                        blue -= num2;
                    else if (flag1 && flag3)
                        green -= num2;
                    else if (flag2 && flag3)
                        red -= num2;
                    else if (flag1)
                    {
                        green -= num2 / 2;
                        blue -= num2 / 2;
                    }
                    else if (flag2)
                    {
                        red -= num2 / 2;
                        blue -= num2 / 2;
                    }
                    else if (flag3)
                    {
                        red -= num2 / 2;
                        green -= num2 / 2;
                    }
                }
            }
            color = Color.FromArgb(red, green, blue);
        }

        private static Color GetAdjusted3dObjectColor()
        {
            Color control = SystemColors.Control;
            if ((int)control.R + (int)control.G + (int)control.B > cmnTheme.TWENTYFIVE_PERCENT_GRAY)
                cmnTheme.AdjustColorToBrightness(ref control, cmnTheme.TWENTYFIVE_PERCENT_GRAY);
            else if ((int)control.R + (int)control.G + (int)control.B < cmnTheme.SIXTYFIVE_PERCENT_GRAY)
                cmnTheme.AdjustColorToBrightness(ref control, cmnTheme.SIXTYFIVE_PERCENT_GRAY);
            return control;
        }

        public static Color BlendColors(Color color1, int nColor1Factor, Color color2, int nColor2Factor)
        {
            return Color.FromArgb(cmnTheme.MulDiv((int)color1.R, nColor1Factor, 100) + cmnTheme.MulDiv((int)color2.R, nColor2Factor, 100), cmnTheme.MulDiv((int)color1.G, nColor1Factor, 100) + cmnTheme.MulDiv((int)color2.G, nColor2Factor, 100), cmnTheme.MulDiv((int)color1.B, nColor1Factor, 100) + cmnTheme.MulDiv((int)color2.B, nColor2Factor, 100));
        }

        private static Color ShadeOf(Color cr, int pct)
        {
            if (pct < 0)
            {
                double num = (double)-pct * 0.01;
                cr = Color.FromArgb((int)((double)byte.MaxValue - (double)((int)byte.MaxValue - (int)cr.R) * num), (int)((double)byte.MaxValue - (double)((int)byte.MaxValue - (int)cr.G) * num), (int)((double)byte.MaxValue - (double)((int)byte.MaxValue - (int)cr.B) * num));
            }
            else
            {
                double num = (double)pct * 0.01;
                cr = Color.FromArgb((int)cr.R - (int)((double)cr.R * num), (int)cr.G - (int)((double)cr.G * num), (int)cr.B - (int)((double)cr.B * num));
            }
            return cr;
        }

        public void Update()
        {
            this.crAdjusted3dObject = this.BaseColor;
        }
    }
}
