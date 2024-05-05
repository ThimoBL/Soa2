using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rapport.Visitor
{
    public interface IRapportVisitor
    {
        void VisitPng(PNGRapport rapport);
        void VisitPdf(PDFRapport rapport);
    }
}
