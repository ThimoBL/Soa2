using Domain.Rapport.RapportStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Rapport.Visitor;

namespace Domain.Rapport
{
    public abstract class Rapport(Header? header, Body body, Footer? footer)
    {
        public Header? Header = header;
        public Body Body = body;
        public Footer? Footer = footer;

        public abstract void Accept(IRapportVisitor visitor);
    }
}
