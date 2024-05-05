using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rapport.RapportStructure
{
    public class Header(string? logo, string? title, string? project, string? version, DateOnly? date)
    {
        public string? Logo = logo;
        public string? Title = title;
        public string? Project = project;
        public string? Version = version;
        public DateOnly? Date = date;
    }
}
