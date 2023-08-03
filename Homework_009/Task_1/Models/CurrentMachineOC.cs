namespace Task_1.Models
{
    public class CurrentMachineOS
    {
        public string OS { get; private set; }
        public string OS_Version { get; private set; }

        public CurrentMachineOS()
        {
            OperatingSystem os = Environment.OSVersion;
            Version version = os.Version;

            string osName = os.Platform switch
            {
                PlatformID.Win32NT => "Windows",
                PlatformID.Unix => "Unix",
                PlatformID.MacOSX => "MacOS",
                _ => "Unknown"
            };

            OS = osName;
            OS_Version = version.ToString();
        }
    }
}
