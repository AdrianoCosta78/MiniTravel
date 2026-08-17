using System;
using System.Collections.Generic;
using System.Text;
using MiniTravel.Application.Messaging;

namespace MiniTravel.Application.Interfaces.Messaging
{
    public interface IMessageBus
    {
        Task PublicarReservaConfirmadaAsync(
            ReservaConfirmadaMessage message);
    }
}
