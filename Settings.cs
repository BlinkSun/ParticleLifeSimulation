using System.Configuration;

namespace ParticleLifeSimulation
{
    public class Settings : ApplicationSettingsBase
    {
        //[ApplicationScopedSetting]
        //public string FormText
        //{
        //    get { return (string)this["FormText"]; }
        //    set => this[nameof(FormText)] = value;
        //}

        //[UserScopedSetting]
        //[DefaultSettingValue("0, 0")]
        //public Point FormLocation
        //{
        //    get { return (Point)this["FormLocation"]; }
        //    set { this["FormLocation"] = value; }
        //}

        //[UserScopedSetting]
        //[DefaultSettingValue("225, 200")]
        //public Size FormSize
        //{
        //    get { return (Size)this["FormSize"]; }
        //    set { this["FormSize"] = value; }
        //}

        [UserScopedSetting]
        [DefaultSettingValue("Control")]
        public Color CanvasBackColor
        {
            get => (Color)this[nameof(CanvasBackColor)];
            set => this[nameof(CanvasBackColor)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("")]
        public string CanvasBackgroundImage
        {
            get => (string)this[nameof(CanvasBackgroundImage)];
            set => this[nameof(CanvasBackgroundImage)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("15")]   // 15 ~ (1000 millisecs / 60 frames)
        public int CanvasInterval
        {
            get => (int)this[nameof(CanvasInterval)];
            set => this[nameof(CanvasInterval)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool CanvasAnimated
        {
            get => (bool)this[nameof(CanvasAnimated)];
            set => this[nameof(CanvasAnimated)] = value;
        }
    }
}
