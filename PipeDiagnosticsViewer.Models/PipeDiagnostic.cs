namespace PipeDiagnosticsViewer.Models
{
    public class PipeDiagnostic
    {
        public string Name { get; set; }

        public double Distance { get; set; }

        public double Angle { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public bool IsDefect { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, [{Distance}, {Angle}], [{Width}, {Height}], Defect: {IsDefect}";
        }
    }
}