namespace AutoRace.Core.Screen;

public interface IPixelReader
{
    Rgb24 GetColorAt(int x, int y);
}

public readonly record struct Rgb24(byte R, byte G, byte B)
{
    public int ToRgb() => (R << 16) | (G << 8) | B;

    public static Rgb24 FromRgb(int rgb)
    {
        var r = (byte)((rgb >> 16) & 0xFF);
        var g = (byte)((rgb >> 8) & 0xFF);
        var b = (byte)(rgb & 0xFF);
        return new Rgb24(r, g, b);
    }

    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}";
}
