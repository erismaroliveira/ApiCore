﻿using MiniLoja.Business.Notificacoes;
using System.Collections.Generic;

namespace MiniLoja.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
