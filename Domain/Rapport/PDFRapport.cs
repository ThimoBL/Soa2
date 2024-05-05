using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Rapport.RapportStructure;
using Domain.Rapport.Visitor;

namespace Domain.Rapport
{
    public class PDFRapport(Header? header, Body body, Footer? footer) : Rapport(header, body, footer)
    {
        public override void Accept(IRapportVisitor visitor)
        {
            visitor.VisitPdf(this);
        }
    }
}
