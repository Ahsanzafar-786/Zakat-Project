using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.BenificiariesNotes.Model
{
    public class BenificaryNoteLookupModel
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public string Date { get; set; }
        public string BenificaryCode { get; set; }
        public Guid? BenificaryId { get; set; }
        public string BenificaryName { get; set; }
        public string NationalId { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
    }
}
