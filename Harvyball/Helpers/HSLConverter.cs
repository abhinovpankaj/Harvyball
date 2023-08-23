using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harvyball.Helpers
{
    public class HSLColor
    {
        public float Hue;
        public float Saturation;
        public float Luminosity;

        public HSLColor(float H, float S, float L)
        {
            Hue = H;
            Saturation = S;
            Luminosity = L;
        }

        //public static HSLColor FromRGB(Color Clr)
        //{
        //    return FromRGB(Clr.R, Clr.G, Clr.B);
        //}

        public static HSLColor FromRGB(int color)
        {
            var R = (color >> 16) & 0xFF;
            var G = ((color >> 8) & 0xFF);
            var B = (color & 0xFF);
            float _R = (R / 255f);
            float _G = (G / 255f);
            float _B = (B / 255f);

            float _Min = Math.Min(Math.Min(_R, _G), _B);
            float _Max = Math.Max(Math.Max(_R, _G), _B);
            float _Delta = _Max - _Min;

            float H = 0;
            float S = 0;
            float L = (float)((_Max + _Min) / 2.0f);

            if (_Delta != 0)
            {
                if (L < 0.5f)
                {
                    S = (float)(_Delta / (_Max + _Min));
                }
                else
                {
                    S = (float)(_Delta / (2.0f - _Max - _Min));
                }


                if (_R == _Max)
                {
                    H = (_G - _B) / _Delta;
                }
                else if (_G == _Max)
                {
                    H = 2f + (_B - _R) / _Delta;
                }
                else if (_B == _Max)
                {
                    H = 4f + (_R - _G) / _Delta;
                }
            }

            return new HSLColor(H, S, L);
        }

        private float Hue_2_RGB(float v1, float v2, float vH)
        {
            if (vH < 0) vH += 1;
            if (vH > 1) vH -= 1;
            if ((6 * vH) < 1) return (v1 + (v2 - v1) * 6 * vH);
            if ((2 * vH) < 1) return (v2);
            if ((3 * vH) < 2) return (v1 + (v2 - v1) * ((2 / 3) - vH) * 6);
            return (v1);
        }
        public int ToRGB()
        {
            int r, g, b;
            if (Saturation == 0)
            {
                r = (int)Math.Round(Luminosity * 255d);
                g = (int)Math.Round(Luminosity * 255d);
                b = (int)Math.Round(Luminosity * 255d);
            }
            else
            {
                double t1, t2;
                double th = Hue / 6.0d;

                if (Luminosity < 0.5d)
                {
                    t2 = Luminosity * (1d + Saturation);
                }
                else
                {
                    t2 = (Luminosity + Saturation) - (Luminosity * Saturation);
                }
                t1 = 2d * Luminosity - t2;

                double tr, tg, tb;
                tr = th + (1.0d / 3.0d);
                tg = th;
                tb = th - (1.0d / 3.0d);

                tr = ColorCalc(tr, t1, t2);
                tg = ColorCalc(tg, t1, t2);
                tb = ColorCalc(tb, t1, t2);

                r = (int)Math.Round(tr * 255d);
                g = (int)Math.Round(tg * 255d);
                b = (int)Math.Round(tb * 255d);
            }
            return (r << 16) | (g << 8) | b;
        }
       
        private static double ColorCalc(double c, double t1, double t2)
        {

            if (c < 0) c += 1d;
            if (c > 1) c -= 1d;
            if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
            if (2.0d * c < 1.0d) return t2;
            if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
            return t1;
        }


        public static double SelectTintOrShade(HSLColor hsl, int variationIndex)
        {
            double[,] shades = new double[5, 5] 
            {
                {0.5, 0.35, 0.25, 0.15, 0.05},
                { 0.9, 0.75, 0.5, 0.25, 0.1},
                {0.8, 0.6, 0.4, -0.25, -0.5},
                {-0.1, -0.25, -0.5, -0.75, -0.9},
                {-0.05, -0.15, -0.25, -0.35, -0.5}
            };
            if (hsl.Luminosity<0.001)
            {
                return shades[0,variationIndex];
            }
            else if(hsl.Luminosity<0.2)
                return shades[1,variationIndex];
            else if (hsl.Luminosity < 0.8)
                return shades[2,variationIndex];
            else if (hsl.Luminosity < 0.999)
                return shades[3,variationIndex];
            else
                return shades[4,variationIndex];
        }

        public static HSLColor ApplyTintandShade(HSLColor hsl, float tintandshade)
        {
            if (tintandshade>0)
            {
                hsl.Luminosity = hsl.Luminosity + (1 - hsl.Luminosity) * tintandshade;
            }
            else
            {
                hsl.Luminosity = hsl.Luminosity + hsl.Luminosity * tintandshade;
            }
            return hsl;
        }
        
    }
}
