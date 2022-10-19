using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ParticleLifeSimulation;

public static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        Stopwatch stopwatch = new();
        ApplicationConfiguration.Initialize();
        Application.Idle += new((s, ev) =>
        {
            while (AppStillIdle)
            {
                long deltaTime = stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();
                foreach (IFormLoop form in Application.OpenForms.OfType<IFormLoop>())
                {
                    // Render a frame during idle time (no messages are waiting)
                    form.UpdateEnvironment(deltaTime);
                    form.RenderEnvironment(deltaTime);
                }
            }
        });
        Application.Run(new FrmSimulation());
    }

    [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    private static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);
    private static bool AppStillIdle => !PeekMessage(out _, IntPtr.Zero, 0, 0, 0);
}

internal interface IFormLoop
{
    public void UpdateEnvironment(long deltaTime);
    public void RenderEnvironment(long deltaTime);
}