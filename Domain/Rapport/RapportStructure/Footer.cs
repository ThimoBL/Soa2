using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rapport.RapportStructure
{
    public class Footer(string? logo, string? title, string? version, string? project, DateOnly? date)
    {
        public string? Logo = logo;
        public string? Title = title;
        public string? Project = project;
        public string? Version = version;
        public DateOnly? Date = date;
    }
}
