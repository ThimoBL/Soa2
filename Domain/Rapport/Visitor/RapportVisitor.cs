using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rapport.Visitor
{
    public class RapportVisitor : IRapportVisitor
    {
        public void VisitPng(PNGRapport rapport)
        {
            Console.WriteLine("PNG rapport Generated");
        }

        public void VisitPdf(PDFRapport rapport)
        {
            Console.WriteLine("PDF rapport visited");
        }
    }
}
