using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigcatmin.pro.Domain.Enums;

namespace Sigcatmin.pro.Application.Contracts.Requests
{
    public record struct GraficarDerechoMineroRequest(
        string MapName,
        bool IsDMGraphVisible,
        CoordinateSystem Datum,
        int Radio,
        string Codigo,
        string Zona,
        string StateGraph);

}
