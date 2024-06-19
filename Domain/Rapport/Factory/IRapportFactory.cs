using Domain.Rapport.RapportStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rapport.Factory
{
    public interface IRapportFactory
    {
        void CreateRapport(Footer? footer, Header? header, Body body, RapportTypes type);
    }
}
