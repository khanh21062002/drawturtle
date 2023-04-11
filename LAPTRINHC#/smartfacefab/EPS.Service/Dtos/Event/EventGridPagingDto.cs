using EPS.Service.Models;
using EPS.Utils.Common;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Event
{
    public class EventGridPagingDto : PagingParams<EventGridDto>
    {
        public Nullable<int> compId { get; set; }
        public string FilterText { get; set; }
        public string person { get; set; }
        public string FilterPhone { get; set; }
        public Nullable<DateTime> filterDateRequestFrom { get; set; }
        public Nullable<DateTime> filterDateRequestTo { get; set; }
        public Nullable<DateTime> filterDateOrderTo { get; set; }
        public Nullable<int> personType { get; set; }
        public string personCode { get; set; }

        public string[] personTypeArray { get; set; }
        //public string HiddenParentField { get; set; }
        public string EventId { get; set; }
        public double ValueThreshold { get; set; }
        public string FileData { get; set; }

        public override List<Expression<Func<EventGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            //if (FileData != null && string.IsNullOrEmpty(EventId))
            //{
            //    predicates.Add(x => x.Gender == 8);
            //}
            if (!string.IsNullOrEmpty(EventId))
            {
                List<Guid> lstEventId = EventId.Split(';').Select(Guid.Parse).ToList();
                predicates.Add(x => lstEventId.Contains(x.EventId));
            }

            //Tìm kiếm theo companyId
            if (compId.HasValue)
            {
                predicates.Add(x => x.CompId == compId);
            }

            //Tìm kiém theo tên nhân viên
            if (!string.IsNullOrEmpty(person))
            {
                predicates.Add((x => x.Person.Contains(person.Trim())));
            }

            //Tìm kiém theo ma nhân viên
            if (!string.IsNullOrEmpty(personCode))
            {
                predicates.Add((x => x.PersonCode.Contains(personCode.Trim())));
            }

            //if (!string.IsNullOrEmpty(HiddenParentField))
            //{
            //    predicates.Add(x => x.HiddenParentCompanyField.StartsWith(HiddenParentField));
            //}

            //Tìm kiếm theo personname
            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Person.Contains(FilterText.Trim()));
            }

            //Tìm kiếm theo số điện thoại
            if (!string.IsNullOrEmpty(FilterPhone))
            {
                predicates.Add(x => x.PhoneNumber.Contains(FilterPhone.Trim()));
            }

            //Tìm kiếm theo thời gian
            if (filterDateRequestFrom.HasValue)
            {
                predicates.Add(x => x.AccessTime >= filterDateRequestFrom);
            }
            if (filterDateRequestTo.HasValue)
            {
                predicates.Add(x => x.AccessTime <= filterDateRequestTo);
            }

            //Tìm kiếm theo thời gian pre order
            if (filterDateOrderTo.HasValue)
            {
                var timeStart = DateTime.Parse(filterDateOrderTo.Value.ToString("yyyy-MM-dd 00:00"));
                var timeEnd = DateTime.Parse(filterDateOrderTo.Value.ToString("yyyy-MM-dd 23:59"));
                predicates.Add(x => x.TimeOrder >= timeStart && x.TimeOrder <= timeEnd && x.AccessTime >= timeStart && x.AccessTime <= timeEnd && x.TimeOrder != null);
            }

            //Tìm kiếm theo đối tượng
            if (personType.HasValue)
            {
                if (personType.Value == -1) { }
                else if (personType.Value == 4)
                {
                    predicates.Add(x => x.PersonType == 4 || x.PersonType == 5 || x.PersonType == 6 || x.PersonType == 7);
                }
                else
                {
                    //predicates.Add(x => (x.PersonType.HasValue && x.PersonType == personType) || (!x.PersonType.HasValue && personType == 1));
                    predicates.Add(x => x.PersonType == personType);
                }
            }

            if (personTypeArray != null && personTypeArray.Length > 0)
            {
                predicates.Add(x => personTypeArray.Select(int.Parse).ToList().Contains((int)x.PersonType));
            } 
                

            return predicates;
        }
    }
}
