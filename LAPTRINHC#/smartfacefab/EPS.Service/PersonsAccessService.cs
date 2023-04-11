using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.PersonsAccess;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class PersonsAccessService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public PersonsAccessService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<int> CreatePersonsAccess(PersonsAccessCreateDto personsAccessCreateDto, bool isExploiting = false)
        {
            personsAccessCreateDto.IsDelete = false;
            await _baseService.CreateAsync<PersonsAccess, PersonsAccessCreateDto>(personsAccessCreateDto);
            return personsAccessCreateDto.Id;
        }


        public async Task<int> DeletePersonsAccess(int[] ids)
        {
            return await _baseService.DeleteAsync<PersonsAccess, int>(ids);
        }

        public List<int> GetListAccessMachines(Guid perId, DateTime formDate, DateTime toDate)
        {
            List<int> rs = new List<int>();

            var lst = _repository.Filter<PersonsAccess>(x => x.PersonId == perId && x.FromDate == formDate && x.ToDate == toDate).ToList();

            if(lst.Count > 0)
            {
                rs = lst.Select(x => (int)x.MachineId).ToList();
            }    

            return rs;
        }

        public string GetStrAccessMachines(List<int> listId)
        {
            string rs = "";

            var lstName = _repository.Filter<Machine>(x => listId.Contains(x.Id)).Select(x => x.DeviceName);

            rs = string.Join(", ", lstName);

            return rs;
        }


        public List<int> GetListPersonsAccessId(Guid perId, DateTime formDate, DateTime toDate)
        {
            List<int> rs = new List<int>();

            var lst = _repository.Filter<PersonsAccess>(x => x.PersonId == perId && x.FromDate == formDate && x.ToDate == toDate).ToList();

            if (lst.Count > 0)
            {
                rs = lst.Select(x => x.Id).ToList();
            }

            return rs;
        }
    }
}
