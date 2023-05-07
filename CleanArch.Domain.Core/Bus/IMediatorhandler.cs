using CleanArch.Domain.Core.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Core.Bus
{
    public interface IMediatorhandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task SendQuery<T>(T query) where T : Command;

    }
}
