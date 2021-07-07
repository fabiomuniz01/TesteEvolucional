using System;
using Evolucional.Application.Common.Interfaces;

namespace Evolucional.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
