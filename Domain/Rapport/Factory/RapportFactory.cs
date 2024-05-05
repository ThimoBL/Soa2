using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Rapport.RapportStructure;
using Domain.Rapport.Visitor;

namespace Domain.Rapport.Factory
{
    public class RapportFactory : IRapportFactory
    {
        public void CreateRapport(Footer? footer, Header? header, Body body, RapportTypes type)
        {
            Rapport rapport = type switch
            {
                RapportTypes.PNG => new PNGRapport(header, body, footer),
                RapportTypes.PDF => new PDFRapport(header, body, footer),
                _ => throw new NotImplementedException()
            };

            rapport.Accept(new RapportVisitor());
        }
    }
}