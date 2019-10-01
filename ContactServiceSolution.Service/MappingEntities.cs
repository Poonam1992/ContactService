using AutoMapper;
using ContactServiceSolution.Data.Entity;


namespace ContactServiceSolution.Service
{
   public class MappingEntities:Profile
    {
        public MappingEntities()
        {
            CreateMap<ContactEntity, Domain.ContactModel>().ReverseMap();
        }
    }
}
