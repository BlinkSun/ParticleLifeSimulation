using System.Drawing.Imaging;

namespace ParticleLifeSimulation
{
    public partial class FrmCanvasSettings : Form
    {
        public FrmCanvasSettings(Settings settings)
        {
            InitializeComponent();
            PicColor.DataBindings.Add("BackColor", settings, "CanvasBackColor", true, DataSourceUpdateMode.OnPropertyChanged);
            TxtFilename.DataBindings.Add("Text", settings, "CanvasBackgroundImage", true, DataSourceUpdateMode.OnPropertyChanged);
            ChkAnimated.DataBindings.Add("Checked", settings, "CanvasAnimated", true, DataSourceUpdateMode.OnPropertyChanged);
            NumInterval.DataBindings.Add("Value", settings, "CanvasInterval", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void BtnOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Select background image :",
                FilterIndex = GetImageFilter().Length,
                Filter = GetImageFilter()
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TxtFilename.Text = openFileDialog.FileName;
            }
        }
        private void PicColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new()
            {
                SolidColorOnly = true,
                Color = PicColor.BackColor
            };
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                PicColor.BackColor = colorDialog.Color;
            }
        }

        /// <summary>
        /// Get the Filter string for all supported image types.
        /// To be used in the FileDialog class Filter Property.
        /// Source: https://stackoverflow.com/a/69318375/5001494
        /// </summary>
        /// <returns></returns>
        public static string GetImageFilter()
        {
            string imageExtensions = string.Empty;
            string separator = "";
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            Dictionary<string, string> imageFilters = new();
            foreach (ImageCodecInfo codec in codecs)
            {
                imageExtensions = $"{imageExtensions}{separator}{codec.FilenameExtension!.ToLower()}";
                separator = ";";
                imageFilters.Add($"{codec.FormatDescription} files ({codec.FilenameExtension.ToLower()})", codec.FilenameExtension.ToLower());
            }
            string result = string.Empty;
            separator = "";
            foreach (KeyValuePair<string, string> filter in imageFilters)
            {
                result += $"{separator}{filter.Key}|{filter.Value}";
                separator = "|";
            }
            if (!string.IsNullOrEmpty(imageExtensions))
            {
                result += $"{separator}Image files|{imageExtensions}";
            }
            return result;
        }

    }
}