using System.Configuration;

namespace ParticleLifeSimulation
{
    public class Settings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [DefaultSettingValue("Control")]
        public Color SimulationBackColor
        {
            get => (Color)this[nameof(SimulationBackColor)];
            set => this[nameof(SimulationBackColor)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("1")]
        public int SimulationStepsPerFrame
        {
            get => (int)this[nameof(SimulationStepsPerFrame)];
            set => this[nameof(SimulationStepsPerFrame)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("true")]
        public bool SimulationAnimated
        {
            get => (bool)this[nameof(SimulationAnimated)];
            set => this[nameof(SimulationAnimated)] = value;
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
        public bool ParticlesBorderless
        {
            get => (bool)this[nameof(ParticlesBorderless)];
            set => this[nameof(ParticlesBorderless)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool ParticlesSystemColorsIncluded
        {
            get => (bool)this[nameof(ParticlesSystemColorsIncluded)];
            set => this[nameof(ParticlesSystemColorsIncluded)] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool ParticlesCompoundColorNamesIncluded
        {
            get => (bool)this[nameof(ParticlesCompoundColorNamesIncluded)];
            set => this[nameof(ParticlesCompoundColorNamesIncluded)] = value;
        }
    }
}
