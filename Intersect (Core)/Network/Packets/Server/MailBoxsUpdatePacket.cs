using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intersect.Collections;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class MailBoxsUpdatePacket:IPacket
    {
        private bool disposedValue;

        public MailBoxsUpdatePacket() { }

        public MailBoxsUpdatePacket(MailBoxUpdatePacket[] mailboxs)
        {
            Mails = mailboxs;
        }
        [Key(0)]
        public MailBoxUpdatePacket[] Mails { get; set; }

        byte[] IPacket.Data => throw new NotImplementedException();

        bool IPacket.IsValid => throw new NotImplementedException();

        long IPacket.ReceiveTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        long IPacket.ProcessTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Dictionary<string, SanitizedValue<object>> IPacket.Sanitize()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~MailBoxsUpdatePacket()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
