using Harvyball.Helpers;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Drawing.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace Harvyball.CustomControls
{
    /// <summary>
    /// Interaction logic for HarvyControl.xaml
    /// </summary>
    public partial class HarvyControl : System.Windows.Controls.UserControl
    {
        public ICommand SelectColorCommand { get; private set; }

        public List<PPTThemeColor> PrimaryColors { get; set; }
        public List<PPTThemeColor> Row1Colors { get; set; }
        public List<PPTThemeColor> Row2Colors { get; set; }
        public List<PPTThemeColor> Row3Colors { get; set; }
        public List<PPTThemeColor> Row4Colors { get; set; }
        public List<PPTThemeColor> Row5Colors { get; set; }

        //public List<int> RGBCollection { get; set; }

        public HarvyControl()
        {
            InitializeComponent();
            setInitialValue();
            SelectColorCommand = new RelayCommand(SelectColor,CanApplyColor);
            
            
            InitColors();
            this.DataContext = this;
        }

        private void InitColors()
        {
            PrimaryColors = new List<PPTThemeColor>();
            Row1Colors = new List<PPTThemeColor>();
            Row2Colors = new List<PPTThemeColor>();
            Row3Colors = new List<PPTThemeColor>();
            Row4Colors = new List<PPTThemeColor>();
            Row5Colors = new List<PPTThemeColor>();
            Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
            DocumentWindow activeWindow = presentation.Windows[1];
            var currentScheme = activeWindow.View.Slide.ColorScheme.Colors[1].RGB;
            var theme = presentation.Designs[1].SlideMaster.Theme;
            int backgroundfill;
            int foregroundfill;
            System.Windows.Media.Color mediaColor;
            System.Windows.Media.Color mediaColorInverted;
            if (currentScheme!=0)
            {
                backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeLight1).RGB;
                PopulateShades(backgroundfill,1,currentScheme);
                foregroundfill = backgroundfill ^ 0x00ffffff;
                mediaColor = getColorRGB(backgroundfill);
                mediaColorInverted = getColorRGB(foregroundfill);
                PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeDark1).RGB;
                PopulateShades(backgroundfill, 2, currentScheme);
                
                foregroundfill = backgroundfill ^ 0x00ffffff;
                mediaColor = getColorRGB(backgroundfill);
                mediaColorInverted = getColorRGB(foregroundfill);
                PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeLight2).RGB;
                PopulateShades(backgroundfill, 3, currentScheme);
                
                foregroundfill = backgroundfill ^ 0x00ffffff;
                mediaColor = getColorRGB(backgroundfill);
                mediaColorInverted = getColorRGB(foregroundfill);
                PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeDark2).RGB;
                PopulateShades(backgroundfill, 4, currentScheme);
                
                foregroundfill = backgroundfill ^ 0x00ffffff;
                mediaColor = getColorRGB(backgroundfill);
                mediaColorInverted = getColorRGB(foregroundfill);
                PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                
            }
            else
            {
                backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeDark1).RGB;
                PopulateShades(backgroundfill, 1, currentScheme);
                
                foregroundfill = backgroundfill ^ 0x00ffffff;
                mediaColor = getColorRGB(backgroundfill);
                mediaColorInverted = getColorRGB(foregroundfill);
                PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeLight1).RGB;
                PopulateShades(backgroundfill, 2, currentScheme);
                
                foregroundfill = backgroundfill ^ 0x00ffffff;
                mediaColor = getColorRGB(backgroundfill);
                mediaColorInverted = getColorRGB(foregroundfill);
                PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeDark2).RGB;
                PopulateShades(backgroundfill, 3, currentScheme);
                
                foregroundfill = backgroundfill ^ 0x00ffffff;
                mediaColor = getColorRGB(backgroundfill);
                mediaColorInverted = getColorRGB(foregroundfill);
                PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeLight2).RGB;
                PopulateShades(backgroundfill, 4, currentScheme);
                
                foregroundfill = backgroundfill ^ 0x00ffffff;
                mediaColor = getColorRGB(backgroundfill);
                mediaColorInverted = getColorRGB(foregroundfill);
                PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
            }
            backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeAccent1).RGB;
            PopulateShades(backgroundfill, 5, currentScheme);            
            foregroundfill = backgroundfill ^ 0x00ffffff;
            mediaColor = getColorRGB(backgroundfill);
            mediaColorInverted = getColorRGB(foregroundfill);
            PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

            backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeAccent2).RGB;
            PopulateShades(backgroundfill, 6, currentScheme);            
            foregroundfill = backgroundfill ^ 0x00ffffff;
            mediaColor = getColorRGB(backgroundfill);
            mediaColorInverted = getColorRGB(foregroundfill);
            PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

            backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeAccent3).RGB;
            PopulateShades(backgroundfill, 7, currentScheme);
            
            foregroundfill = backgroundfill ^ 0x00ffffff;
            mediaColor = getColorRGB(backgroundfill);
            mediaColorInverted = getColorRGB(foregroundfill);
            PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

            backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeAccent4).RGB;
            PopulateShades(backgroundfill, 8, currentScheme);            
            foregroundfill = backgroundfill ^ 0x00ffffff;
            mediaColor = getColorRGB(backgroundfill);
            mediaColorInverted = getColorRGB(foregroundfill);
            PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

            backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeAccent5).RGB;
            PopulateShades(backgroundfill, 9, currentScheme);            
            foregroundfill = backgroundfill ^ 0x00ffffff;
            mediaColor = getColorRGB(backgroundfill);
            mediaColorInverted = getColorRGB(foregroundfill);
            PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

            backgroundfill = theme.ThemeColorScheme.Colors(Microsoft.Office.Core.MsoThemeColorSchemeIndex.msoThemeAccent6).RGB;
            PopulateShades(backgroundfill, 10, currentScheme);            
            foregroundfill = backgroundfill ^ 0x00ffffff;
            mediaColor = getColorRGB(backgroundfill);
            mediaColorInverted = getColorRGB(foregroundfill);
            PrimaryColors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

        }

        private void PopulateShades(int backgroundfill, int col, int colorScheme)
        {
            int colorShade = 0;
            int shadeColor;
            int shadeColorInverse;
            System.Windows.Media.Color mediaColor, mediaColorInverted;
            #region delete
            /*
            if (col>4 )
            {
                shadeColor = GetShadeTintColor(backgroundfill, 80, false);
                shadeColorInverse = colorShade ^ 0x00ffffff;
                mediaColor = getColorRGB(shadeColor);
                mediaColorInverted = getColorRGB(shadeColorInverse);
                Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                shadeColor = GetShadeTintColor(backgroundfill, 60, false);
                shadeColorInverse = colorShade ^ 0x00ffffff;
                mediaColor = getColorRGB(shadeColor);
                mediaColorInverted = getColorRGB(shadeColorInverse);
                Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                shadeColor = GetShadeTintColor(backgroundfill, 50, false);
                shadeColorInverse = colorShade ^ 0x00ffffff;
                mediaColor = getColorRGB(shadeColor);
                mediaColorInverted = getColorRGB(shadeColorInverse);
                Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                shadeColor = GetShadeTintColor(backgroundfill, 25, true);
                shadeColorInverse = colorShade ^ 0x00ffffff;
                mediaColor = getColorRGB(shadeColor);
                mediaColorInverted = getColorRGB(shadeColorInverse);
                Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                shadeColor = GetShadeTintColor(backgroundfill, 50, true);
                shadeColorInverse = colorShade ^ 0x00ffffff;
                mediaColor = getColorRGB(shadeColor);
                mediaColorInverted = getColorRGB(shadeColorInverse);
                Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
            }
            else
            {
                if (colorScheme == 0)
                {
                    if (col==1)
                    {
                        shadeColor = GetShadeTintColor(backgroundfill, 50, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 35, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 25, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 15, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 5, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                    }
                    if (col == 2)
                    {
                        shadeColor = GetShadeTintColor(backgroundfill, 5, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 15, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 25, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 35, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 50, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                    }
                    if (col == 3)
                    {
                        shadeColor = GetShadeTintColor(backgroundfill,90, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 75, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 50, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 25, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 10, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                    }
                    if (col == 4)
                    {
                        shadeColor = GetShadeTintColor(backgroundfill, 10, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 25, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 50, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 75, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 90, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                    }
                }
                else
                {
                    if (col == 1)
                    {
                        shadeColor = GetShadeTintColor(backgroundfill, 5, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 15, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 25, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 35, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 50, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                    }
                    if (col == 2)
                    {
                        shadeColor = GetShadeTintColor(backgroundfill, 50, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 35, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 25, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 15, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 5, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                    }
                    if (col == 3)
                    {
                        shadeColor = GetShadeTintColor(backgroundfill, 10, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 25, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 50, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 75, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 90, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                    }
                    if (col == 4)
                    {
                        shadeColor = GetShadeTintColor(backgroundfill, 80, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 60, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 40, false);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 25, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });

                        shadeColor = GetShadeTintColor(backgroundfill, 50, true);
                        shadeColorInverse = colorShade ^ 0x00ffffff;
                        mediaColor = getColorRGB(shadeColor);
                        mediaColorInverted = getColorRGB(shadeColorInverse);
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                    }

                }
            
            }
            */
            #endregion
            for (int i = 0; i < 5; i++)
            {
                var hsl = HSLColor.FromRGB(backgroundfill);
                var ts = HSLColor.SelectTintOrShade(hsl, i);
                var newhsl = HSLColor.ApplyTintandShade(hsl, (float)ts);

                
                shadeColor = new HSLColor(newhsl.Hue,newhsl.Saturation, newhsl.Luminosity).ToRGB();
                shadeColorInverse = colorShade ^ 0x00ffffff;
                mediaColor = getColorRGB(shadeColor);
                mediaColorInverted = getColorRGB(shadeColorInverse);
                switch (i)
                {
                    case 0:
                        Row1Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                        break;
                    case 1:
                        Row2Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                        break;
                    case 2:
                        Row3Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                        break;

                    case 3:
                        Row4Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                        break;
                    case 4:
                        Row5Colors.Add(new PPTThemeColor() { FillColor = mediaColor, InvertedFillColor = mediaColorInverted });
                        break;
                    default:
                        break;
                }
                                
            }
        }

        private System.Windows.Media.Color getColorRGB(int color)
        {
           return System.Windows.Media.Color.FromRgb(
                            (byte)(color & 0xFF),
                            (byte)((color >> 8) & 0xFF),
                             (byte)((color >> 16) & 0xFF));
        }

        private int GetShadeTintColor(int color,float factor,bool isDark) // 0 is dark
        {

            //Color cl = ColorTranslator.FromOle(color);
            int newR,newG, newB;
            int newColor;
            var currentR = (color >> 16)& 0xFF;
            var currentG = ((color >> 8) & 0xFF) ;
            var currentB = (color & 0xFF) ;
            
            if (isDark)
            {
                newR = (int)Math.Ceiling((currentR * (1 - factor/100)));
                newG = (int)Math.Ceiling(currentG * (1 - factor/100));
                newB = (int)Math.Ceiling(currentB * (1 - factor / 100));
                
            }
            else
            {
                newR = (int)(currentR + (255 - currentR) * factor/100);
                newG = (int)(currentG + (255 - currentG) * factor/100);    
                newB = (int)(currentB + (255 - currentB) * factor/100);
                
            }

            newColor = (newR << 16) | (newG << 8) | newB;
            return newColor;
        }
        private void setInitialValue()
        {
            Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
            DocumentWindow activeWindow = presentation.Windows[1];
            
            Shape w_shp = activeWindow.Selection.ShapeRange[1];
            
            Shape h_shp = w_shp.GroupItems[1];

            var oleColorValue = h_shp.Fill.ForeColor.RGB;

            System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromRgb(
    
    (byte)(oleColorValue & 0xFF),
    (byte)((oleColorValue >> 8) & 0xFF),
     (byte)((oleColorValue >> 16) & 0xFF)
);
            
            ColorButton.Background = new SolidColorBrush(mediaColor);
            double adjustmentsItem2 = (double)h_shp.Adjustments[2];
            if (adjustmentsItem2 > -180 && adjustmentsItem2 < -90)
            {
                NUDTextBox.Text = ((360d + adjustmentsItem2 + 90d) / 3.6d).ToString();
            }
            else
            {
                NUDTextBox.Text = ((adjustmentsItem2 + 90d) / 3.6d).ToString();
            }
            NUDTextBox.Focus();
        }

        private bool CanApplyColor(object arg)
        {
            return true;
        }
        private void SelectColor(Object obj)
        {            
            Color color = ColorTranslator.FromHtml(obj.ToString());
            mod_HarveyBalls.set_HB_Color(color);
            System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromArgb(
               color.A, color.R, color.G, color.B);
            ColorButton.Background =  new SolidColorBrush(mediaColor);
            popup.IsOpen = false;
        }

        int minvalue = 0,
        maxvalue = 100,
        startvalue = 25;
        
        private void NUDButtonUP_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox.Text = Convert.ToString(number + 1);

        }

        private void NUDButtonDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox.Text = Convert.ToString(number - 1);
        }

        private void NUDTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                typeof(System.Windows.Controls.Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                typeof(System.Windows.Controls.Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { true });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            MyDialog.ShowHelp = true;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Color color = MyDialog.Color;
                mod_HarveyBalls.set_HB_Color(MyDialog.Color);
                System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromArgb(
               color.A, color.R, color.G, color.B);
                ColorButton.Background = new SolidColorBrush(mediaColor);
            }

            
        }
        private int RGB(int red, int green, int blue)
        {

            return (red & 0xFF) << 16 | (green & 0xFF) << 8 | (blue & 0xFF);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitColors();
        }

        private void NUDTextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(System.Windows.Controls.Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { false });

            if (e.Key == Key.Down)
                typeof(System.Windows.Controls.Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { false });
        }

        private void NUDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox.Text != "")
                if (!int.TryParse(NUDTextBox.Text, out number)) NUDTextBox.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox.Text = minvalue.ToString();
            NUDTextBox.SelectionStart = NUDTextBox.Text.Length;
            if (Information.IsNumeric(NUDTextBox.Text))
            {
                int percent = Convert.ToInt32(NUDTextBox.Text);
                if (percent >= 0d & percent <= 100d)
                {
                    mod_HarveyBalls.set_HB_Percent(percent);
                }
            }

        } 
        
       

    }
    public class PPTThemeColor
    {
        public System.Windows.Media.Color FillColor 
        {
            get;set;
        }
        public System.Windows.Media.Color InvertedFillColor
        {
            get;set;
        }
        public SolidColorBrush BackgroundFill 
        {
            get
            {
                return new SolidColorBrush(FillColor);
            }
        }
        public SolidColorBrush ForegroundFill 
        {
            get
            {
                return new SolidColorBrush(InvertedFillColor);
            }
        }


    }
}

