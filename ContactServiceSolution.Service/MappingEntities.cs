using AutoMapper;
using ContactServiceSolution.Data.Entity;


namespace ContactServiceSolution.Service
{
   public class MappingEntities:Profile
    {
        public MappingEntities()
        {
            CreateMap<Contact, Domain.ContactModel>().ReverseMap();
        }
    }
}
