using pdp11_emulator.Core.Signaling;

namespace pdp11_emulator.Core.Executing;
using Components;

public class DataPath
{
    private readonly Register[] Registers =
    [
        new (), // REGISTER O
        new (), // REGISTER 1
        new (), // REGISTER 2
        new (), // REGISTER 3
        new (), // REGISTER 4
        new (), // REGISTER 5
        new (), // STACK POINTER
        new () // PROGRAM COUNTER
    ];

    private readonly Register MDR = new ();
    private readonly Register IR = new ();
    private readonly Register MAR = new ();

    public void Init()
    {
    }

    public ushort Drive(Register signals)
    {
        return 0;
    }
}