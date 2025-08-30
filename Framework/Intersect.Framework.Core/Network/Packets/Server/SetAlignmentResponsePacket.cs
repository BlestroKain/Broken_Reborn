using System;
using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class SetAlignmentResponsePacket : IntersectPacket
{
    // Parameterless constructor for MessagePack
    public SetAlignmentResponsePacket()
    {
    }

    public SetAlignmentResponsePacket(bool success, string message, Alignment newAlignment, DateTime? nextAllowedChangeAt)
    {
        Success = success;
        Message = message;
        NewAlignment = newAlignment;
        NextAllowedChangeAt = nextAllowedChangeAt;
    }

    [Key(0)]
    public bool Success { get; set; }

    [Key(1)]
    public string Message { get; set; } = string.Empty;

    [Key(2)]
    public Alignment NewAlignment { get; set; }

    [Key(3)]
    public DateTime? NextAllowedChangeAt { get; set; }
}

