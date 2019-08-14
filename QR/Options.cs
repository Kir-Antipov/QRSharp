using CommandLine;

namespace QR
{
    public class Options
    {
        [Option('i', "in", Required = true, HelpText = "String to be encoded")]
        public string Input { get; set; }

        [Option('o', "out", Required = true, HelpText = "Output file name")]
        public string Output { get; set; }

        [Option('s', "scale", Required = false, HelpText = "Scale factor")]
        public int Scale { get; set; } = 10;

        [Option('b', "border", Required = false, HelpText = "Border size (in pixels)")]
        public int Border { get; set; } = 40;

        [Option("foreground", Required = false, HelpText = "Foreground color (decimal representation of HEX-color)")]
        public int Foreground { get; set; } = 0;

        [Option("background", Required = false, HelpText = "Background color (decimal representation of HEX-color)")]
        public int Background { get; set; } = 0xFFFFFF;
    }
}
