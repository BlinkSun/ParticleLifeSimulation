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
        public string CanvasBackgroundBitmap
        {
            get => (string)this[nameof(CanvasBackgroundBitmap)];
            set => this[nameof(CanvasBackgroundBitmap)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("1")]
        public int CanvasStepsPerFrame
        {
            get => (int)this[nameof(CanvasStepsPerFrame)];
            set => this[nameof(CanvasStepsPerFrame)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool CanvasAnimated
        {
            get => (bool)this[nameof(CanvasAnimated)];
            set => this[nameof(CanvasAnimated)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool ParticlesContraste
        {
            get => (bool)this[nameof(ParticlesContraste)];
            set => this[nameof(ParticlesContraste)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool ParticlesBorder
        {
            get => (bool)this[nameof(ParticlesBorder)];
            set => this[nameof(ParticlesBorder)] = value;
        }
    }
}
