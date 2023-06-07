using System;

namespace Focus.Business.CharityResource.Model
{
    public class CharityResourcesLookupModel
    {
        public Guid Id { get; set; }
        public int ChartiyId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public bool Business { get; set; }
        public bool IsActive { get; set; }
        public Guid? ContactPerson { get; set; }

    }
}
