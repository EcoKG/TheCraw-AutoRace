using System.Runtime.InteropServices;
using AutoRace.Core.Input;

namespace AutoRace.Infrastructure.Input;

public sealed class Win32KeyboardInput : IKeyboardInput
{
    public void KeyDown(ScanCode key) => Send(key, isKeyUp: false);

    public void KeyUp(ScanCode key) => Send(key, isKeyUp: true);

    public void Press(ScanCode key)
    {
        KeyDown(key);
        KeyUp(key);
    }

    private static void Send(ScanCode key, bool isKeyUp)
    {
        var flags = KeyEventF.Scancode;

        if (isKeyUp)
        {
            flags |= KeyEventF.KeyUp;
        }

        // Arrow keys are "extended".
        if (key is ScanCode.Up or ScanCode.Down or ScanCode.Left or ScanCode.Right)
        {
            flags |= KeyEventF.ExtendedKey;
        }

        INPUT[] inputs =
        [
            new INPUT
            {
                type = InputType.Keyboard,
                U = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = 0,
                        wScan = (ushort)key,
                        dwFlags = flags,
                        time = 0,
                        dwExtraInfo = UIntPtr.Zero
                    }
                }
            }
        ];

        _ = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());
    }

    private enum InputType : uint
    {
        Keyboard = 1
    }

    [Flags]
    private enum KeyEventF : uint
    {
        ExtendedKey = 0x0001,
        KeyUp = 0x0002,
        Scancode = 0x0008
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public InputType type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)] public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public KeyEventF dwFlags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
}
