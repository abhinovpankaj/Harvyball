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

namespace Harvyball.CustomControls
{
    /// <summary>
    /// Interaction logic for HarvyControl.xaml
    /// </summary>
    public partial class HarvyControl : System.Windows.Controls.UserControl
    {
        public ICommand SelectColorCommand { get; private set; }

        public HarvyControl()
        {
            InitializeComponent();
            NUDTextBox.Text = startvalue.ToString();
            SelectColorCommand = new RelayCommand(SelectColor,CanApplyColor);
            this.DataContext = this;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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
                
            frm_HB.ActiveForm.Hide();
        }
        private int RGB(int red, int green, int blue)
        {
            return (red & 0xFF) << 16 | (green & 0xFF) << 8 | (blue & 0xFF);
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
}

