﻿namespace Intersect.Network.Packets.Client
{
    public class ResetPasswordPacket : CerasPacket
    {
        public string NameOrEmail { get; set; }
        public string ResetCode { get; set; }
        public string NewPassword { get; set; }

        public ResetPasswordPacket(string nameOrEmail, string resetCode, string newPassword)
        {
            NameOrEmail = nameOrEmail;
            ResetCode = resetCode;
            NewPassword = newPassword;
        }
    }
}