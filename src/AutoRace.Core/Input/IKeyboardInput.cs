namespace AutoRace.Core.Input;

public interface IKeyboardInput
{
    void KeyDown(ScanCode key);
    void KeyUp(ScanCode key);
    void Press(ScanCode key);
}

/// <summary>
/// Minimal set of keyboard scan codes (Set 1) used by AutoRace.
/// Values match legacy Eco_Lib ScanCodeShort for compatibility.
/// </summary>
public enum ScanCode : ushort
{
    Escape = 0x01,

    Digit1 = 0x02,
    Digit2 = 0x03,
    Digit3 = 0x04,
    Digit4 = 0x05,
    Digit5 = 0x06,
    Digit6 = 0x07,
    Digit7 = 0x08,
    Digit8 = 0x09,
    Digit9 = 0x0A,
    Digit0 = 0x0B,

    Q = 0x10,
    W = 0x11,
    E = 0x12,
    R = 0x13,
    T = 0x14,
    Y = 0x15,
    U = 0x16,
    I = 0x17,
    O = 0x18,
    P = 0x19,

    Enter = 0x1C,
    LeftControl = 0x1D,

    A = 0x1E,
    S = 0x1F,
    D = 0x20,
    F = 0x21,
    G = 0x22,
    H = 0x23,
    J = 0x24,
    K = 0x25,
    L = 0x26,

    LeftShift = 0x2A,

    Z = 0x2C,
    X = 0x2D,
    C = 0x2E,
    V = 0x2F,
    B = 0x30,
    N = 0x31,
    M = 0x32,

    LeftAlt = 0x38,
    Space = 0x39,

    Up = 0x48,
    Left = 0x4B,
    Right = 0x4D,
    Down = 0x50
}
